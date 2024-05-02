


using Microsoft.AspNetCore.Mvc.Rendering;

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

        }
    }
}
