using Microsoft.AspNetCore.Mvc.Rendering;
using Speedy.Core.Models.RelatedData;

namespace Speedy.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            // Delivery Agent
            CreateMap<Delivery, DeliveryViewModel>()
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City!.Name))
            .ForMember(dest => dest.GovernorateName, opt => opt.MapFrom(src => src.City!.Governorate!.Name))                       
            .ForMember(dest => dest.ShippingMethodName, opt => opt.MapFrom(src => src.ShippingMethods!.Name));            
            CreateMap<Delivery, DeliveryFormViewModel> ().ReverseMap();

            // Start Up            
            CreateMap<StartUp, StartUpViewModel>()
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City!.Name))
            .ForMember(dest => dest.GovernorateName, opt => opt.MapFrom(src => src.City!.Governorate!.Name));
            CreateMap<StartUp, StartUpFormViewModel>().ReverseMap();

            // Individual            
            CreateMap<Individual, IndividualViewModel>()
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City!.Name))
            .ForMember(dest => dest.GovernorateName, opt => opt.MapFrom(src => src.City!.Governorate!.Name));
            CreateMap<Individual, IndividualFormViewModel>().ReverseMap();

            //User ViewModel
            CreateMap<AppUser, UserViewModel>();
            CreateMap<UserViewModel, UserFormViewModel>().ReverseMap();
            CreateMap<UserFormViewModel, AppUser>()
                .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
                .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.UserName.ToUpper()))
                .ReverseMap();

            #region Related Data

            //Governorates
            CreateMap<Governorate, GovernorateFormViewModel>().ReverseMap();
            CreateMap<Governorate, GovernorateViewModel>();
            CreateMap<Governorate, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));

            //City            
            CreateMap<CityFormViewModel, City>().ReverseMap();
            CreateMap<City, CityViewModel>()
                .ForMember(dest => dest.GovernorateName, opt => opt.MapFrom(src => src.Governorate!.Name));
            CreateMap<City, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));

            //ShippingMethods
            CreateMap<ShippingMethod, ShippingMethodViewModel>();
            CreateMap<ShippingMethod, ShippingMethodFormViewModel>().ReverseMap();
            CreateMap<ShippingMethod, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));

            #endregion

        }
    }
}
