


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
            CreateMap<AppUser, UserViewModel>().ReverseMap();
            CreateMap<UserViewModel, UserFormViewModel>().ReverseMap();

        }
    }
}
