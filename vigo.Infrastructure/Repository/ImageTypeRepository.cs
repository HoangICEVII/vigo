using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Entity;
using vigo.Domain.Interface.IRepository;
using vigo.Infrastructure.DBContext;
using vigo.Infrastructure.Generic;

namespace vigo.Infrastructure.Repository
{
    public class ImageTypeRepository : VigoGeneric<ImageType>, IImageTypeRepository
    {
        public ImageTypeRepository(VigoDatabaseContext context) : base(context)
        {
        }
    }
}
