using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN222.Lab2.Repositories.Models;

public partial class AccountMember
{
    public int MemberId { get; set; }


    [Required]
    [MinLength(6, ErrorMessage = "Password phải có ít nhất 6 ký tự.")]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Password không được chứa ký tự đặc biệt.")]
    public string MemberPassword { get; set; } = null!;

    [Required]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "FullName chỉ được chứa chữ cái và khoảng trắng.")]
    public string FullName { get; set; } = null!;

    [Required]
    [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
    public string EmailAddress { get; set; } = null!;

    [Range(0, 2, ErrorMessage = "MemberRole chỉ được nhận giá trị 0, 1 hoặc 2.")]
    public int MemberRole { get; set; }
}
