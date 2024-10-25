using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Helper;

namespace vigo.Domain.Entity
{
    public class DiscountCoupon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image {  get; set; }
        public string Description { get; set; }
        public DiscountType DiscountType { get; set; } //kieu theo % hay giam gia co dinh
        public string DiscountCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DiscountMax { get; set; }
        public int DiscountCount { get; set; }
        public string UserUsed { get; set; }
        public bool Status { get; set; }
        public string RoomApply {  get; set; }
        public DiscountApply DiscountApply { get; set; } //cho nhieu phong hay 1 so phong
        public int ShowRoomId { get; set; }
        //public Guid CreatedBy { get; set; }
        //public Guid UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime DeletedDate { get; set; }

    }
}
