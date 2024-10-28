using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Service.DTO.Admin.Account;
using vigo.Service.DTO.Admin.Service;

namespace vigo.Service.DTO.Admin.Room
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public int Days { get; set; }
        public decimal Price { get; set; }
        public int Avaiable { get; set; }
        public int BusinessPartnerId { get; set; }
        public int Province { get; set; }
        public int District { get; set; }
        public int Street { get; set; }
        public BusinessPartnerShortDTO BusinessPartner { get; set; } = new BusinessPartnerShortDTO();
        public List<ServiceDTO> Services { get; set; } = new List<ServiceDTO>();
    }
}
