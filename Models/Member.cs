using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class Member : User
    {
        public MembershipType MembershipType { get; set; }
        public Member(string id, string name, MembershipType membershipType) : base(id, name)
        {
            MembershipType = membershipType;
        }
    }
}
