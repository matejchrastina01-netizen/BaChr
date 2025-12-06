using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UTB.BaChr.Mapy.Application.ViewModels;
using UTB.BaChr.Mapy.Domain.Entities;

namespace UTB.BaChr.Mapy.Areas.Security.Controllers
{
    [Area("Security")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // --- LOGIN ---

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // Pokus o přihlášení
                // false na konci znamená "lockoutOnFailure" - nezamknout účet při chybě
                var result = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, vm.RememberMe, false);

                if (result.Succeeded)
                {
                    // Přihlášení úspěšné -> přesměrovat na Home
                    return RedirectToAction("Index", "Home", new { Area = "" });
                }

                ModelState.AddModelError(string.Empty, "Neplatné přihlašovací údaje.");
            }
            return View(vm);
        }

        // --- REGISTER ---

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // Vytvoření entity User z ViewModelu
                var user = new User
                {
                    UserName = vm.Username,
                    Email = vm.Email,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName
                };

                // Vytvoření uživatele v DB (Identity se postará o hashování hesla)
                var result = await _userManager.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {
                    // DŮLEŽITÉ: Přiřazení role "Customer" novému uživateli
                    // Ujisti se, že role "Customer" existuje v tabulce Roles (pomocí seedingu)
                    await _userManager.AddToRoleAsync(user, "Customer");

                    // Okamžité přihlášení po registraci
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home", new { Area = "" });
                }

                // Pokud nastaly chyby (např. heslo je moc slabé), vypíšeme je
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(vm);
        }

        // --- LOGOUT ---

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        // --- ACCESS DENIED ---
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}