﻿using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vigo.Service.DTO
{
    public class ResponseData
    {
        public string Message { get; set; } = string.Empty;
        public int Status { get; set; }
        public MetaData MetaData { get; set; } = null!;
        public Option? Options { get; set; }
    }
}
