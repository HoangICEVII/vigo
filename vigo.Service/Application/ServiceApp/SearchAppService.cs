using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Entity;
using vigo.Domain.Interface.IUnitOfWork;
using vigo.Domain.User;
using vigo.Service.Application.IServiceApp;
using vigo.Service.DTO.Application.Account;
using vigo.Service.DTO.Application.Room;
using vigo.Service.DTO.Application.Search;
using vigo.Service.EmailAuthenModule;

namespace vigo.Service.Application.ServiceApp
{
    public class SearchAppService : ISearchAppService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkVigo _unitOfWorkVigo;

        public SearchAppService(IUnitOfWorkVigo unitOfWorkVigo, IMapper mapper)
        {
            _unitOfWorkVigo = unitOfWorkVigo;
            _mapper = mapper;
        }

        public async Task<SearchResultReturnDTO> ReturnSearchResult(string searchInput)
        {
            List<Expression<Func<Province, bool>>> conditions = new List<Expression<Func<Province, bool>>>()
            {
                e => e.Name.ToLower().Contains(searchInput.ToLower())
            };
            var province = await _unitOfWorkVigo.Provinces.GetAll(conditions);
            if (province.Count() != 0) {
                SearchResultReturnDTO result = new SearchResultReturnDTO();
                foreach (var item in province)
                {
                    List<Expression<Func<BusinessPartner, bool>>> con2 = new List<Expression<Func<BusinessPartner, bool>>>()
                    {
                        e => e.ProvinceId.Equals(item.Id)
                    };
                    var business = await _unitOfWorkVigo.BusinessPartners.GetAll(con2);
                    List<BusinessAppDTO> businessAppDTOs = new List<BusinessAppDTO>();
                    foreach (var businessPartner in business)
                    {
                        List<Expression<Func<Room, bool>>> conRoom = new List<Expression<Func<Room, bool>>>()
                        {
                            e => e.BusinessPartnerId == businessPartner.Id
                        };
                        BusinessAppDTO businessAppDTO = new BusinessAppDTO();
                        businessAppDTO.Address = businessPartner.Address;
                        businessAppDTO.PhoneNumber = businessPartner.PhoneNumber;
                        businessAppDTO.CompanyName = businessPartner.CompanyName;

                        var rooms = await _unitOfWorkVigo.Rooms.GetAll(conRoom);
                        foreach (var room in rooms)
                        {
                            businessAppDTO.RoomAppDTOs.Add(new RoomAppDTO()
                            {
                                Id = room.Id,
                                Name = room.Name,
                                Address = room.Address,
                                Avaiable = room.Avaiable,
                                DefaultDiscount = room.DefaultDiscount,
                                Description = room.Description,
                                Price = room.Price,
                                Thumbnail = room.Thumbnail
                            });
                        }
                        result.BusinessPartnerDTOs.Add(businessAppDTO);
                    }
                }
                return result;
            }
            else
            {
                SearchResultReturnDTO result = new SearchResultReturnDTO();
                List<Expression<Func<BusinessPartner, bool>>> con2 = new List<Expression<Func<BusinessPartner, bool>>>()
                {
                    e => e.Name.ToLower().Contains(searchInput.ToLower())
                };
                var business = await _unitOfWorkVigo.BusinessPartners.GetAll(con2);
                List<BusinessAppDTO> businessAppDTOs = new List<BusinessAppDTO>();
                foreach (var businessPartner in business) {
                    List<Expression<Func<Room, bool>>> conRoom = new List<Expression<Func<Room, bool>>>()
                    {
                        e => e.BusinessPartnerId == businessPartner.Id
                    };
                    BusinessAppDTO businessAppDTO = new BusinessAppDTO();
                    businessAppDTO.Address = businessPartner.Address;
                    businessAppDTO.PhoneNumber = businessPartner.PhoneNumber;
                    businessAppDTO.CompanyName = businessPartner.CompanyName;
                    
                    var rooms = await _unitOfWorkVigo.Rooms.GetAll(conRoom);
                    foreach (var room in rooms) {
                        businessAppDTO.RoomAppDTOs.Add(new RoomAppDTO()
                        {
                            Id = room.Id,
                            Name = room.Name,
                            Address = room.Address,
                            Avaiable = room.Avaiable,
                            DefaultDiscount = room.DefaultDiscount,
                            Description = room.Description,
                            Price = room.Price,
                            Thumbnail = room.Thumbnail
                        });
                    }
                    result.BusinessPartnerDTOs.Add(businessAppDTO);
                }
                return result;
            }
        }

        public async Task<SearchResultDTO> ReturnSearchTyping(string? searchInput)
        {
            if (!searchInput.IsNullOrEmpty())
            {
                List<Expression<Func<Province, bool>>> conditions = new List<Expression<Func<Province, bool>>>()
                {
                    e => e.Name.ToLower().Contains(searchInput!.ToLower())
                };
                List<Expression<Func<BusinessPartner, bool>>> conditions2 = new List<Expression<Func<BusinessPartner, bool>>>()
                {
                    e => e.Name.ToLower().Contains(searchInput!.ToLower())
                };
                var province = await _unitOfWorkVigo.Provinces.GetAll(conditions);
                var business = await _unitOfWorkVigo.BusinessPartners.GetAll(conditions2);
                var result = new SearchResultDTO();
                foreach (var item in province)
                {
                    List<Expression<Func<Room, bool>>> con = new List<Expression<Func<Room, bool>>>()
                    {
                        e => e.ProvinceId.Equals(item.Id)
                    };
                    result.ProvinceShortDTOs.Add(new ProvinceShortDTO()
                    {
                        Name = item.Name,
                        Image = item.Image,
                        RoomNumber = (await _unitOfWorkVigo.Rooms.GetAll(con)).Count()
                    });
                }
                foreach (var item in business)
                {
                    List<Expression<Func<Room, bool>>> con = new List<Expression<Func<Room, bool>>>()
                    {
                        e => e.BusinessPartnerId == item.Id
                    };
                    result.BPShortDTOs.Add(new BPShortDTO()
                    {
                        Name = item.Name,
                        Logo = item.Logo,
                        RoomNumber = (await _unitOfWorkVigo.Rooms.GetAll(con)).Count()
                    });
                }
                return result;
            }
            else
            {
                var result = new SearchResultDTO();
                var province = await _unitOfWorkVigo.Provinces.GetAll(null);
                foreach (var item in province)
                {
                    List<Expression<Func<Room, bool>>> con = new List<Expression<Func<Room, bool>>>()
                    {
                        e => e.ProvinceId.Equals(item.Id)
                    };
                    result.ProvinceShortDTOs.Add(new ProvinceShortDTO()
                    {
                        Name = item.Name,
                        Image = item.Image,
                        RoomNumber = (await _unitOfWorkVigo.Rooms.GetAll(con)).Count()
                    });
                }
                return result;
            }
        }
    }
}
