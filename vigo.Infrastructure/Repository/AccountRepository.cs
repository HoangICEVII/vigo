﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.AccountFolder;
using vigo.Domain.Interface.IRepository;
using vigo.Infrastructure.DBContext;
using vigo.Infrastructure.Generic;

namespace vigo.Infrastructure.Repository
{
    public class AccountRepository : VigoGeneric<Account>, IAccountRepository
    {
        public AccountRepository(VigoDatabaseContext context) : base(context)
        {
        }

        public async Task<UserAuthen> LoginViaForm(string email, string password, bool admin)
        {
            var data = await _context.Accounts.Where(e => e.Email.Equals(email)).FirstOrDefaultAsync();
            if (data == null)
            {
                throw new Exception("user not found");
            }
            else
            {
                if (admin)
                {
                    if(data.UserType == "tourist")
                    {
                        throw new Exception("user not found");
                    }
                    if(PasswordHasher.HashPassword(password, data.Salt).Equals(data.Password))
                    {
                        var userAuthen = new UserAuthen();
                        if (data.UserType.Equals("BusinessPartnerEmployee"))
                        {
                            var info = await _context.BusinessPartners.Where(e => e.AccountId.Equals(data.Id)).FirstOrDefaultAsync();
                            userAuthen.BusinessKey = info!.BusinessKey;
                            userAuthen.RoleId = data.RoleId;
                            userAuthen.Permission = (await _context.Roles.Where(e => e.Id == data.RoleId).FirstOrDefaultAsync())!.Permission;
                        }
                        else
                        {
                            userAuthen.RoleId = data.RoleId;
                            userAuthen.Permission = (await _context.Roles.Where(e => e.Id == data.RoleId).FirstOrDefaultAsync())!.Permission;
                        }
                        return userAuthen;
                    }
                    throw new Exception("wrong password");
                }
                else {
                    if (data.UserType != "tourist")
                    {
                        throw new Exception("user not found");
                    }
                    if (PasswordHasher.HashPassword(password, data.Salt).Equals(data.Password))
                    {
                        var userAuthen = new UserAuthen();
                        userAuthen.RoleId = data.RoleId;
                        return userAuthen;
                    }
                    throw new Exception("wrong password");
                }
            }
        }
    }
}
