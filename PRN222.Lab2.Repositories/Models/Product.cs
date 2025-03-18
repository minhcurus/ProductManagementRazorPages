﻿using System;
using System.Collections.Generic;

namespace PRN222.Lab2.Repositories.Models;



public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int CategoryId { get; set; }

    public short? UnitsInStock { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual Category Category { get; set; } = null!;
}
