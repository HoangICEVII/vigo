using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vigo.Service.DTO.Admin.Account
{
    public class BusinessPartnerDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string BusinessKey { get; set; }
        public string Email { get; set; }
        public Guid AccountId { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
