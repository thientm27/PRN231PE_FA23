using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class MemberAccount
    {
        public int MemberId { get; set; }
        public string MemberPassword { get; set; } = null!;
        public string MemberFullName { get; set; } = null!;
        public string? MemberEmail { get; set; }
        public int? MemberRole { get; set; }
    }
}
