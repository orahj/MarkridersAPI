using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using MarkriderAPI.Controllers.DTOS;

namespace MarkriderAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Delivery,DeliveryDto>()
                .ForMember(x=>x.FirstName, o => o.MapFrom(s => s.AppUser.FirstName))
                .ForMember(x=>x.LastName, o => o.MapFrom(s => s.AppUser.LastName))
                .ForMember(x=>x.Avatar, o => o.MapFrom(s => s.AppUser.Avatar))
                .ForMember(x=>x.Address, o => o.MapFrom(s => s.AppUser.Address))
                .ForMember(x=>x.UserName, o => o.MapFrom(s => s.AppUser.UserName));

            CreateMap<DeliveryItem,DeliveryItemDto>()
                .ForMember(x =>x.FileURL, o =>o.MapFrom<FileUrlResolver>())
                .ForMember(x => x.Address, o => o.MapFrom(s =>s.DeliveryLocation.Address))
                .ForMember(x =>x.Latitude, o =>o.MapFrom(s => s.DeliveryLocation.Latitude))
                .ForMember(x => x.Logitude, o => o.MapFrom(s => s.DeliveryLocation.Logitude))
                .ForMember(x =>x.DeliveryDistance, o => o.MapFrom(s => s.DeliveryLocation.DeliveryDistance));
        }
    }
}