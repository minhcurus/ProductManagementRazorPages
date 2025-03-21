using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PRN222.Lab2.Repositories.Models;
using PRN222.Lab2.Services;
using PRN222.Lab2.Services.Interfaces;

namespace PRN222.Lab2.MVC.Pages.Products
{
    [Authorize(Roles = "1")]
    public class EditModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IHubContext<SignalRServer> _hubContext;

        public EditModel(IProductService productService, ICategoryService categoryService, IHubContext<SignalRServer> hubContext)
        {
            _productService = productService;
            _categoryService = categoryService;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =  await _productService.GetProductById((int)id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
           ViewData["CategoryId"] = new SelectList(await _categoryService.GetAllCategory(), "CategoryId", "CategoryName");
            return Page();
        }

        
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _productService.UpdateProduct(Product);
                await _hubContext.Clients.All.SendAsync("ProductUpdated");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return (_productService.GetProductById(id) == null) ? true : false;
        }
    }
}
