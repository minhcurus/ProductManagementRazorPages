using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN222.Lab2.Repositories.Models;



public partial class Product
{
    public int ProductId { get; set; }


    [Required]
    [MaxLength(100, ErrorMessage = "ProductName không được vượt quá 100 ký tự.")]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "ProductName không được chứa ký tự đặc biệt.")]
    public string ProductName { get; set; } = null!;

    public int CategoryId { get; set; }

    [Range(0, short.MaxValue, ErrorMessage = "UnitsInStock không được nhỏ hơn 0.")]
    public short? UnitsInStock { get; set; }


    [Range(0, double.MaxValue, ErrorMessage = "UnitPrice không được nhỏ hơn 0.")]
    public decimal UnitPrice { get; set; }

    public virtual Category Category { get; set; } = null!;
}
