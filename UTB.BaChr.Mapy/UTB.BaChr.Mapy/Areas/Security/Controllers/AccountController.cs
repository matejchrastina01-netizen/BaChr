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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            // 1. Pokud data z formuláře nejsou validní (např. prázdné heslo), vrať chybu hned
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            // 2. Pokus o přihlášení
            var result = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, vm.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            // 3. DIAGNOSTIKA CHYB - Abychom věděli, proč se stránka "jen restartovala"
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Účet je uzamčen.");
            }
            else if (result.IsNotAllowed)
            {
                ModelState.AddModelError(string.Empty, "Přihlášení není povoleno (možná chybí potvrzení emailu).");
            }
            else
            {
                // Zkontrolujeme, jestli uživatel vůbec existuje
                var user = await _userManager.FindByNameAsync(vm.Username);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, $"Uživatel '{vm.Username}' v databázi neexistuje!");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Zadali jste špatné heslo.");
                }
            }

            return View(vm);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = new User
            {
                UserName = vm.Username,
                Email = vm.Email,
                FirstName = vm.FirstName,
                LastName = vm.LastName
            };

            var result = await _userManager.CreateAsync(user, vm.Password);

            if (result.Succeeded)
            {
                // Každý registrovaný je Customer
                await _userManager.AddToRoleAsync(user, "Customer");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            // Výpis chyb registrace (např. heslo je moc krátké)
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(vm);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}