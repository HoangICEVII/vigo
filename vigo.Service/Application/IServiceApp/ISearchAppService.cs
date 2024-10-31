﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Service.DTO.Application.Search;

namespace vigo.Service.Application.IServiceApp
{
    public interface ISearchAppService
    {
        Task<SearchResultDTO> ReturnSearchTyping(string searchInput);
    }
}