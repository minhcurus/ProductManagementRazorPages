using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN222.Lab2.Repositories.Models;
using PRN222.Lab2.Services.Interfaces;
using System.Security.Claims;

namespace PRN222.Lab2.MVC.Pages
{
    public class LoginModel : PageModel
    {

        private readonly IAccountService _accountService; // using Dependency Injection
        public LoginModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.Identity.IsAuthenticated) // Kiểm tra user đã đăng nhập hay chưa
            {
                return RedirectToPage("/Products/Index");
            }
            return Page();
        }

        [BindProperty]
        public AccountMember AccountMember { get; set; } = default!;
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _accountService.GetAccountMemberByEmail(AccountMember.EmailAddress);

            if (user != null && user.MemberPassword == AccountMember.MemberPassword)
            {
                var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.MemberId.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.MemberRole.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // Giữ đăng nhập ngay cả khi đóng trình duyệt
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Hết hạn sau 30 phút
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToPage("/Products/Index");
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return Page();
        }

    }
}

