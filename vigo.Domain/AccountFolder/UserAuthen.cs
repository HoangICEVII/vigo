﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vigo.Domain.AccountFolder
{
    public class UserAuthen
    {
        public string BusinessKey { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public string Permission {  get; set; } = string.Empty;
    }
}
