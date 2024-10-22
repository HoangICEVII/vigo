using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Interface.IRepository;

namespace vigo.Domain.Interface.IUnitOfWork
{
    public interface IUnitOfWorkVigo
    {
        IAccountRepository Accounts { get; }
        IBookingRepository Bookings { get; }

        IDiscountRepository Discounts { get; }
        IDistrictRepository Districts { get; }

        IImageRepository Images { get; }
        IImageTypeRepository ImageTypes { get; }

        IInvoiceRepository Invoices { get; }
        IProvinceRepository Provinces { get; }

        IRatingRepository Ratings { get; }
        IRoleRepository Roles { get; }
        IRolePermissionRepository RolePermissions { get; }

        IRoomRepository Rooms { get; }
        IRoomServiceRepository RoomServices { get; }

        IRoomTypeRepository RoomTypes { get; }
        IServiceRepository Services { get; }

        IShowRoomRepository ShowRooms { get; }
        IBusinessPartnerRepository BusinessPartners { get; }

        ITouristRepository Tourists { get; }
        ISystemEmployeeRepository SystemEmployees { get; }

        Task<int> Complete();
    }
}
