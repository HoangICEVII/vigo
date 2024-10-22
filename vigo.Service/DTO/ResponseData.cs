using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Helper;

namespace vigo.Service.DTO
{
    public class ResponseData
    {
        public string Message { get; set; }
        public int Status { get; set; }
        public MetaData MetaData { get; set; }
        public Option Options { get; set; }
    }
}
