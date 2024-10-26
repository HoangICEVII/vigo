using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using vigo.Domain.AccountFolder;
using vigo.Domain.Entity;
using vigo.Service.DTO.Admin.Role;
using vigo.Service.DTO.Admin.Account;
using vigo.Domain.User;
using vigo.Service.DTO.Admin.Booking;
using vigo.Service.DTO.Admin.Room;
using vigo.Service.DTO.Admin.Discount;
using vigo.Service.DTO.Admin.Rating;
using vigo.Service.DTO.Admin.ShowRoom;
using vigo.Service.DTO.Admin.Service;

namespace vigo.Service.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Role, RoleDTO>()
            .ForMember(dest => dest.Permission, opt => opt.MapFrom(src => SplitPermissions(src.Permission)));
            CreateMap<Role, RoleDetailDTO>()
            .ForMember(dest => dest.Permission, opt => opt.MapFrom(src => SplitPermissions(src.Permission)));
            CreateMap<RolePermission, RolePermissionDTO>();
            CreateMap<BusinessPartner, BusinessPartnerDTO>();
            CreateMap<SystemEmployee, EmployeeDTO>();
            CreateMap<Booking, BookingDetailDTO>();
            CreateMap<Tourist, TouristDetailDTO>();
            CreateMap<Room, RoomDetailDTO>();
            CreateMap<Booking, BookingDTO>();
            CreateMap<DiscountCoupon, DiscountCouponDetailDTO>();
            CreateMap<DiscountCoupon, DiscountCouponDTO>();
            CreateMap<Rating, RatingDTO>();
            CreateMap<RoomType, RoomTypeDTO>();
            CreateMap<Room, RoomDetailDTO > ();
            CreateMap<Room, RoomDTO> ();
            CreateMap<ShowRoom, ShowRoomDetailDTO>();
            CreateMap<ShowRoom, ShowRoomDTO>();
            CreateMap<RoomType, RoomTypeDTO>();
            CreateMap<ServiceR, ServiceDTO>();
        }
        public static List<string> SplitPermissions(string permissions)
        {
            return permissions?.Split(',').ToList() ?? new List<string>();
        }
    }
}
