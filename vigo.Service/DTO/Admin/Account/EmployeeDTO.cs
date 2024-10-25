﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vigo.Service.DTO.Admin.Account
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Salary { get; set; }
        public Guid AccountId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
