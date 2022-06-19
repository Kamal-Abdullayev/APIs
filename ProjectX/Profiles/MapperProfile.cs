using AutoMapper;
using ProjectX.Dto;
using ProjectX.Entities.Models;

namespace ProjectX.Profiles
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ReturnProduct>()
                .ForMember(dest =>
                dest.FullImagePath,
                opt => opt.MapFrom(src => "https://localhost:44365/assets/product/" + src.Image));  
                //.ForMember(dest => 
                //dest.CategoryName)
        }
    }
}
