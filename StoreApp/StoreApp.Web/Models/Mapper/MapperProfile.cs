using System;
using AutoMapper;

namespace StoreApp.Web.Models.Mapper;

public class MapperProfile : Profile
{
   public MapperProfile()
   {
        CreateMap<Product, ProductViewModel>();
   }
}
