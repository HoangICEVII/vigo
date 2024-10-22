using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vigo.Domain.Entity
{
    public class ShowRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProvinceId {  get; set; }
        public int DistrictId { get; set; }
        public int BusinessPartnerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}
