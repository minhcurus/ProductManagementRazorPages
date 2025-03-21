using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN222.Lab2.Repositories.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    [Required]
    [MaxLength(50, ErrorMessage = "CategoryName không được vượt quá 50 ký tự.")]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "CategoryName không được chứa ký tự đặc biệt.")]
    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
