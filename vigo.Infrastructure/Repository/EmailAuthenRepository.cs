﻿using System;
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
    public class EmailAuthenRepository : VigoGeneric<EmailAuthen>, IEmailAuthenRepository
    {
        public EmailAuthenRepository(VigoDatabaseContext context) : base(context)
        {
        }
    }
}
