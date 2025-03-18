using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using PRN222.Lab2.Repositories.Models;
using PRN222.Lab2.Services;
using PRN222.Lab2.Services.Interfaces;

namespace PRN222.Lab2.MVC.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IHubContext<SignalRServer> _hubContext;

        public CreateModel(IProductService productService, ICategoryService categoryService, IHubContext<SignalRServer> hubContext)
        {
            _productService = productService;
            _categoryService = categoryService;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> OnGet()
        {
        ViewData["CategoryId"] = new SelectList( await _categoryService.GetAllCategory(), "CategoryId", "CategoryName");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        { 
            await _productService.AddProduct(Product);
            await _hubContext.Clients.All.SendAsync("LoadAllItems");
            return RedirectToPage("./Index");
        }
    }
}
