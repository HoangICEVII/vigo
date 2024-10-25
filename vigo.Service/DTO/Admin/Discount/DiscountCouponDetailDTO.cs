using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Helper;

namespace vigo.Service.DTO.Admin.Discount
{
    public class DiscountCouponDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DiscountType DiscountType { get; set; }
        public string DiscountCode { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DiscountMax { get; set; }
        public int DiscountCount { get; set; }
        public string UserUsed { get; set; } = string.Empty;
        public string RoomApply { get; set; } = string.Empty;
        public DiscountApply DiscountApply { get; set; }
        public int ShowRoomId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
