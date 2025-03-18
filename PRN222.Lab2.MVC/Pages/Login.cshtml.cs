using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN222.Lab2.Repositories.Models;
using PRN222.Lab2.Services.Interfaces;

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
            var loginId = HttpContext.Session.GetInt32("Account").ToString();
            if (!string.IsNullOrEmpty(loginId))
            {
                return RedirectToPage("/Products/Index");
            }
            return Page();
        }

        [BindProperty]
        public AccountMember AccountMember { get; set; } = default!;
        public string ErrorMessage { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var loginId = HttpContext.Session.GetInt32("Account").ToString();
            if (!string.IsNullOrEmpty(loginId))
            {
                return RedirectToPage("/Products/Index");
            }

            var memberAccount =await _accountService.GetAccountMemberById(AccountMember.MemberId);

            if (memberAccount == null)
            {
                ErrorMessage = "You do not have permission to do this function!";
                ModelState.AddModelError(string.Empty, ErrorMessage);

                return Page();
            }
            else if (memberAccount.MemberRole == 1 || memberAccount.MemberRole == 2)
            {
                HttpContext.Session.SetInt32("Account", memberAccount?.MemberRole ?? 0);
                return RedirectToPage("/Products/Index");
            }
            else
            {
                ErrorMessage = "You do not have permission to do this function!";
                ModelState.AddModelError(string.Empty, ErrorMessage);
                return Page();
            }
        }

    }
}

