using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vigo.Service.DTO.Admin.Account
{
    public class CreateEmployeeAccount
    {
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public decimal Salary { get; set; }
        public string BankNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
