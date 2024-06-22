using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Speedy.Services.delivery;
using Speedy.Services.User;

namespace Speedy.Controllers
{
    public class DeliveryPanelController(IDeliveryService deliveryService, IMapper mapper) : Controller
    {
        private readonly IDeliveryService _deliveryService = deliveryService;
        private readonly IMapper _mapper = mapper;
        public async Task<IActionResult> Profile(string id)
        {
            var delivery = await _deliveryService.GetDeliveryAsync(deliveryId: id);


            if (delivery is null)
                return NotFound();

            var deliveriesView = _mapper.Map<DeliveryViewModel>(delivery);

            return View("Profile", deliveriesView);
        }

    }
}
