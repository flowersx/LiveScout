// MappingProfiles/CarMappingProfile.cs
using AutoMapper;
using livescout_backend.Models;
using livescout_backend.DTOs;

namespace livescout_backend.MappingProfiles
{
    public class CarMappingProfile : Profile
    {
        public CarMappingProfile()
        {
            // Mapping de la Car la DTO-uri (pentru GET)
            CreateMap<Car, CarListDto>();
            CreateMap<Car, CarDetailsDto>();

            // Mapping de la DTO-uri la Car (pentru POST/PUT)
            CreateMap<CreateCarDto, Car>();
            CreateMap<UpdateCarDto, Car>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());  // Ignoră Id la update, ca să nu se modifice


            CreateMap<Car, CarListDto>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

            CreateMap<Car, CarDetailsDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

            // Mapping custom dacă ai nevoie (ex: pentru proprietăți computate, dar DTO-urile tale le au deja ca get-only)
        }
    }
}