﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Interface.IRepository;
using vigo.Domain.Interface.IUnitOfWork;
using vigo.Infrastructure.DBContext;
using vigo.Infrastructure.Repository;

namespace vigo.Infrastructure.UnitOfWork
{
    public class UnitOfWorkVigo : IUnitOfWorkVigo
    {
        private readonly VigoDatabaseContext _context;

        #region a
        private Lazy<IAccountRepository> _accounts;
        private Lazy<IBookingRepository> _bookings;
        private Lazy<IBusinessPartnerRepository> _businessPartners;
        private Lazy<IDiscountRepository> _discounts;
        private Lazy<IDistrictRepository> _districts;
        private Lazy<IImageRepository> _images;
        private Lazy<IImageTypeRepository> _imageTypes;
        private Lazy<IInvoiceRepository> _invoices;
        private Lazy<IProvinceRepository> _provinces;
        private Lazy<IRatingRepository> _ratings;
        private Lazy<IRoleRepository> _roles;
        private Lazy<IRolePermissionRepository> _rolePermissions;
        private Lazy<IRoomServiceRepository> _roomServices;
        private Lazy<IRoomTypeRepository> _roomTypes;
        private Lazy<IServiceRepository> _services;
        private Lazy<IShowRoomRepository> _showRooms;
        private Lazy<ISystemEmployeeRepository> _systemEmployees;
        private Lazy<ITouristRepository> _tourists;
        private Lazy<IRoomRepository> _rooms;
        #endregion

        #region b
        public IAccountRepository Accounts => _accounts.Value;
        public IBookingRepository Bookings => _bookings.Value;
        public IBusinessPartnerRepository BusinessPartners => _businessPartners.Value;
        public IDiscountRepository Discounts => _discounts.Value;
        public IDistrictRepository Districts => _districts.Value;
        public IImageRepository Images => _images.Value;
        public IImageTypeRepository ImageTypes => _imageTypes.Value;
        public IInvoiceRepository Invoices => _invoices.Value;
        public IProvinceRepository Provinces => _provinces.Value;
        public IRatingRepository Ratings => _ratings.Value;
        public IRoleRepository Roles => _roles.Value;
        public IRolePermissionRepository RolePermissions => _rolePermissions.Value;
        public IRoomServiceRepository RoomServices => _roomServices.Value;
        public IRoomTypeRepository RoomTypes => _roomTypes.Value;
        public IServiceRepository Services => _services.Value;
        public IShowRoomRepository ShowRooms => _showRooms.Value;
        public ISystemEmployeeRepository SystemEmployees => _systemEmployees.Value;
        public ITouristRepository Tourists => _tourists.Value;
        public IRoomRepository Rooms => _rooms.Value;
        #endregion

        public UnitOfWorkVigo(VigoDatabaseContext context)
        {
            _context = context;

            _accounts = new Lazy<IAccountRepository>(() => new AccountRepository(_context));
            _bookings = new Lazy<IBookingRepository>(() => new BookingRepository(_context));
            _businessPartners = new Lazy<IBusinessPartnerRepository>(() => new BusinessPartnerRepository(_context));

            _discounts = new Lazy<IDiscountRepository>(() => new DiscountCouponRepository(_context));
            _districts = new Lazy<IDistrictRepository>(() => new DistrictRepository(_context));
            _images = new Lazy<IImageRepository>(() => new ImageRepository(_context));

            _imageTypes = new Lazy<IImageTypeRepository>(() => new ImageTypeRepository(_context));
            _invoices = new Lazy<IInvoiceRepository>(() => new InvoiceRepository(_context));
            _provinces = new Lazy<IProvinceRepository>(() => new ProvinceRepository(_context));

            _ratings = new Lazy<IRatingRepository>(() => new RatingRepository(_context));
            _roles = new Lazy<IRoleRepository>(() => new RoleRepository(_context));
            _rolePermissions = new Lazy<IRolePermissionRepository>(() => new RolePermissionRepository(_context));
            _roomServices = new Lazy<IRoomServiceRepository>(() => new RoomServiceRepository(_context));

            _roomTypes = new Lazy<IRoomTypeRepository>(() => new RoomTypeRepository(_context));
            _services = new Lazy<IServiceRepository>(() => new ServiceRepository(_context));
            _showRooms = new Lazy<IShowRoomRepository>(() => new ShowRoomRepository(_context));

            _systemEmployees = new Lazy<ISystemEmployeeRepository>(() => new SystemEmployeeRepository(_context));
            _tourists = new Lazy<ITouristRepository>(() => new TouristRepository(_context));
            _rooms = new Lazy<IRoomRepository>(() => new RoomRepository(_context));
        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
