using Microsoft.AspNetCore.Mvc.Rendering;
using Speedy.Core.Models.RelatedData;
using System.Diagnostics.Contracts;

namespace Speedy.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            // Delivery Agent
            CreateMap<Delivery, DeliveryViewModel>()
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City!.Name))
            .ForMember(dest => dest.GovernorateName, opt => opt.MapFrom(src => src.City!.Governorate!.Name))                       
            .ForMember(dest => dest.ShippingMethodName, opt => opt.MapFrom(src => src.ShippingMethods!.Name))           
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.AppUser!.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.AppUser!.LastName))
            .ForMember(dest => dest.NID, opt => opt.MapFrom(src => src.AppUser!.NID))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser!.Email))
            .ForMember(dest => dest.MobileNumber, opt => opt.MapFrom(src => src.AppUser!.PhoneNumber))                     
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.AppUser!.Gender))                     
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.AppUser!.Gender))                     
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.AppUser!.IsActive))                     
            .ForMember(dest => dest.ProfilePictureIUrl, opt => opt.MapFrom(src => src.AppUser!.ProfilePictureIUrl))                     
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.AppUser!.CreatedOn));
            CreateMap<Delivery, DeliveryFormViewModel>().ReverseMap();
            CreateMap<Delivery, DeliveryProfileFormViewModel> ().ReverseMap();            
            

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

            // Order
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

            //Users Profiles
            CreateMap<StartUp, StartUpProfileViewModel>();
            CreateMap<AppUser, EditStartUpFormViewModel>();
            CreateMap<Individual, IndividualProfileViewModel>();

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

            //ShippingMethods
            //CreateMap<PaymentMethod, PaymentViewModel>();
            CreateMap<PaymentMethod, PaymentFormViewModel>().ReverseMap();
            CreateMap<PaymentMethod, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Title));

            //ServiceMethods                    
            CreateMap<ServiceArea, ServiceAreaFormViewModel>().ReverseMap();
            CreateMap<ServiceArea, ServiceAreaViewModel>();
            CreateMap<ServiceArea, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));
            #endregion

        }
    }
}
