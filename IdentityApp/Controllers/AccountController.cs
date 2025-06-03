using System.Threading.Tasks;
using IdentityApp.Models;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        Console.WriteLine("=== Login İşlemi Başladı ===");
        Console.WriteLine($"Gelen Email: {model.Email}");

        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email!);
            Console.WriteLine($"Kullanıcı bulundu mu: {user != null}");

            if (user != null)
            {
                await _signInManager.SignOutAsync();
                var emailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
                Console.WriteLine($"Email onaylı mı: {emailConfirmed}");

                if (!emailConfirmed)
                {
                    Console.WriteLine("Email onaylanmamış, hata mesajı gösteriliyor");
                    ModelState.AddModelError("", "Lütfen Mail adresini onaylayın");
                    return View(model);
                }

                Console.WriteLine($"Giriş denemesi - Email: {model.Email}");
                var result = await _signInManager.PasswordSignInAsync(user, model.Password!, model.RememberMe, true);
                Console.WriteLine($"Giriş sonucu: {result.Succeeded}");

                if (result.Succeeded)
                {
                    Console.WriteLine("Giriş başarılı, ana sayfaya yönlendiriliyor");
                    await _userManager.ResetAccessFailedCountAsync(user);
                    await _userManager.SetLockoutEndDateAsync(user, null);
                    return RedirectToAction("Index", "Home");
                }
                else if (result.IsLockedOut)
                {
                    var lockoutDate = await _userManager.GetLockoutEndDateAsync(user);
                    var timeLeft = lockoutDate.Value - DateTime.UtcNow;
                    Console.WriteLine($"Hesap kilitli, kalan süre: {timeLeft.Minutes} dakika");
                    ModelState.AddModelError("", $"Hesabınız kitlendi, {timeLeft.Minutes} dakika sonra tekrar deneyin");
                }
                else
                {
                    var errors = await _userManager.GetAccessFailedCountAsync(user);
                    Console.WriteLine($"Başarısız giriş denemesi sayısı: {errors}");
                    ModelState.AddModelError("", $"Parolanız hatalı. Başarısız giriş denemesi: {errors}");

                    // Şifre doğrulama hatası
                    if (result.IsNotAllowed)
                    {
                        ModelState.AddModelError("", "Giriş yapmanıza izin verilmiyor. Lütfen e-posta adresinizi doğrulayın.");
                    }
                    else if (result.RequiresTwoFactor)
                    {
                        ModelState.AddModelError("", "İki faktörlü kimlik doğrulama gerekiyor.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Kullanıcı bulunamadı");
                ModelState.AddModelError("", "Bu e-posta adresi ile kayıtlı bir kullanıcı bulunamadı.");
            }
        }
        else
        {
            Console.WriteLine("ModelState geçersiz");
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"ModelState hatası: {error.ErrorMessage}");
            }
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateViewModel model)
    {
        // BREAKPOINT 1: Yeni kullanıcı oluşturma başlangıcı
        Console.WriteLine("=== Yeni Kullanıcı Oluşturma Başladı ===");
        Console.WriteLine($"Gelen Email: {model.Email}");
        Console.WriteLine($"Gelen Kullanıcı Adı: {model.UserName}");

        if (ModelState.IsValid)
        {
            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };

            // BREAKPOINT 2: Kullanıcı oluşturma öncesi
            Console.WriteLine("Kullanıcı nesnesi oluşturuldu, CreateAsync çağrılıyor");
            IdentityResult result = await _userManager.CreateAsync(user, model.Password!);
            Console.WriteLine($"CreateAsync sonucu: {result.Succeeded}");

            if (result.Succeeded)
            {
                // BREAKPOINT 3: Token oluşturma öncesi
                Console.WriteLine("Kullanıcı başarıyla oluşturuldu, token oluşturuluyor");
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                Console.WriteLine($"Token oluşturuldu: {token}");

                // BREAKPOINT 4: URL oluşturma öncesi
                var url = Url.Action(
                    action: "ConfirmEmail",
                    controller: "Account",
                    values: new { Id = user.Id, token = token },
                    protocol: Request.Scheme,
                    host: Request.Host.Value
                );

                Console.WriteLine($"Kullanıcı ID: {user.Id}");
                Console.WriteLine($"Oluşturulan URL: {url}");

                if (string.IsNullOrEmpty(url))
                {
                    Console.WriteLine("HATA: URL oluşturulamadı!");
                }

                TempData["message"] = "Mailinize Onay Gönderildi";
                return RedirectToAction("Login", "Account");
            }

            Console.WriteLine("Kullanıcı oluşturma hataları:");
            foreach (IdentityError err in result.Errors)
            {
                Console.WriteLine($"Hata: {err.Description}");
                ModelState.AddModelError("", err.Description);
            }
        }
        else
        {
            Console.WriteLine("ModelState geçersiz");
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"ModelState hatası: {error.ErrorMessage}");
            }
        }
        return View(model);
    }

    [HttpGet]
    [Route("Account/ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string Id, string token)
    {
        Console.WriteLine($"ConfirmEmail çağrıldı - ID: {Id}, Token: {token}"); // Debug için

        if (Id == null || token == null)
        {
            TempData["message"] = "Geçersiz Token Bilgisi";
            return View();
        }

        var user = await _userManager.FindByIdAsync(Id);
        if (user == null)
        {
            TempData["message"] = "Kullanıcı bulunamadı";
            return View();
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (result.Succeeded)
        {
            TempData["message"] = "Hesabınız Onaylandı";
            return View();
        }

        TempData["message"] = "Hesap onaylama işlemi başarısız oldu";
        return View();
    }
}

