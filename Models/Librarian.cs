using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class Librarian : User
    {
        public string? EmployeeId { get; set; }
        public Librarian(string id, string name, string? employeeId) : base(id, name)
        {
            EmployeeId = employeeId;
        }
    }
}
