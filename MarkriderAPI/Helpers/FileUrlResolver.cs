using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs.Delivery;
using Core.Entities;
using MarkriderAPI.Controllers.DTOS;
using Microsoft.Extensions.Configuration;

namespace MarkriderAPI.Helpers
{
    public class FileUrlResolver : IValueResolver<DeliveryItem, DeliveryItemReturnDTO, string>
    {
        private readonly IConfiguration _config;
        public FileUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(DeliveryItem source, DeliveryItemReturnDTO destination, string destMember, ResolutionContext context)
        {
           if(!string.IsNullOrEmpty(source.ImageUrl))
           {
               return source.ImageUrl;
           }
           return null;
        }
    }
}