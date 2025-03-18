using System;
using System.Collections.Generic;

namespace PRN222.Lab2.Repositories.Models;

public partial class AccountMember
{
    public int MemberId { get; set; }

    public string MemberPassword { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public int MemberRole { get; set; }
}
