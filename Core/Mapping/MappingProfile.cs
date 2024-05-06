using Microsoft.AspNetCore.Mvc.Rendering;
using Speedy.Core.Models.RelatedData;
using Speedy.Core.ViewModels.RelatedData;

namespace Speedy.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            // Delivery Agent
            CreateMap<Delivery, DeliveryViewModel>().ReverseMap();
            CreateMap<DeliveryFormViewModel, DeliveryViewModel>().ReverseMap();
            CreateMap<DeliveryFormViewModel, Delivery>().ReverseMap();

            //CreateMap<Delivery, SelectListItem>()
            //.ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.CategoryId))
            //.ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.CategoryName));

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
