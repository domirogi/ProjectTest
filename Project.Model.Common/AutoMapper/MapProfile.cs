using AutoMapper;
using Project.Model.Common.DTOs;
using Project.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Common.AutoMapper
{
  public  class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<VehicleMake, MakeDTO>().ReverseMap();
            CreateMap<VehicleMake, CreateMakeDTO>().ReverseMap();
            CreateMap<VehicleModel, ModelDTO>().ReverseMap();
            CreateMap<VehicleModel, CreateModelDTO>().ReverseMap();
            CreateMap<VehicleModel, ModelDTO>()
                .ForMember(m => m.Make, opt => opt.MapFrom
                (src => src.Make.Name));



        }
    }
}
