using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PRN222.Lab2.Repositories.Models;
using PRN222.Lab2.Services.Interfaces;

namespace PRN222.Lab2.MVC.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public IList<Product> Product { get;set; } = default!;

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        //Search
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public string CurrentSortOrder { get; set; }


        public async Task<IActionResult> OnGetAsync(string sortOrder, int currentPage = 1)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                CurrentSortOrder = sortOrder;

                var result = await _productService.GetPagedAsync(
                pageNumber: currentPage,
                pageSize: PageSize,
                sortOrder: sortOrder,
                ascending: true,
                searchString: SearchString);

                Product = result.Items;
                TotalCount = result.TotalCount;
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
                CurrentPage = currentPage;

                return Page();
            }
            return RedirectToPage("/Login");
        }

    }
}
