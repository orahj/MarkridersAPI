using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs.Delivery;
using Core.Entities;
using MarkriderAPI.Controllers.DTOS;

namespace MarkriderAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // CreateMap<Delivery,DeliveryDTO>();

            // CreateMap<DeliveryItem,DeliveryItemDto>()
            //     .ForMember(x =>x.FileURL, o =>o.MapFrom<FileUrlResolver>());
            CreateMap<Delivery,DeliveryReturnDTO>();
            CreateMap<DeliveryItem,DeliveryItemReturnDTO>()
                .ForMember(x=>x.ImageUrl, o=> o.MapFrom(s => s.FileData.URL));
            CreateMap<DeliveryLocation,DeliveryLocationReturnDTO>();
        }
    }
}