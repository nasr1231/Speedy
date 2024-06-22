using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Speedy.Core.Enums;
using Speedy.Core.Models;
using Speedy.Core.ViewModels;
using Speedy.Services.User;
using static Azure.Core.HttpHeader;
using static Speedy.Core.Enums.Variables;

namespace Speedy.Controllers
{
    public class OrdersController(ApplicationDbContext context, IMapper mapper, UserManager<AppUser> userManager, IAttachmentService attachImage) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IAttachmentService _attachImage = attachImage;
        public IActionResult Index(string id)
        {
            var delivery = _context.Deliveries.SingleOrDefault(x => x.AppUserId == id);
            var orders = _context.Orders.Where(i => i.DeliveryId == delivery!.Id).ToList();
            var viewModels = new List<OrderDetailsViewModel>();

            foreach (var order in orders)
            {
                var viewModel = new OrderDetailsViewModel
                {
                    TrackingNumber = order.TrackingNumber,
                    SenderName = order.SenderName,
                    SenderAddress = order.SenderAddress,
                    OrderDate = order.ShippingDate,
                    RecieverAddress = order.RecieverAddress,
                    RecieverName = order.RecieverName
                };

                viewModels.Add(viewModel);
            }

            if (User.IsInRole(AppRoles.StartUp))
                return View("OrderStartup");

            if (User.IsInRole(AppRoles.Individual))
                return View("OrderIndividual");

            return View();
        }

        [HttpPost]
        public IActionResult Filter(CitiesHomeViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var deliveryCitiesFliter = _context.Deliveries.Include(au => au.AppUser)
            //    .Where(n => n.CityId == model.CityId && !n.IsDeleted).ToList();

            //if (deliveryCitiesFliter is null)
            //    return NotFound();

            var orderViewModel = new OrderFormViewModel { CityId = model.CityId };

            return View("DeliveryPreview", InitialOrderForm(orderViewModel));
        }

        //[HttpGet]
        //public IActionResult InitiateCreate(int id)
        //{
        //    //ModelState.AddModelError(string.Empty, "Sorry, There are no appointments available right now");
        //    var appointmentsViewModel = new OrderFormViewModel { DeliveryId = id };

        //    return PartialView("_ReservationForm", appointmentsViewModel);
        //}

        [HttpPost]
        public async Task<IActionResult> Create(OrderFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            using var transaction = _context.Database.BeginTransaction();

            var imageAttachment = await _attachImage.UploadImageAsync(
                attachedFile: model.OrderImage,
                entityName: "Delivery Agents",
               userName: model.UserId);

            if (!imageAttachment.isUploaded)
                return BadRequest(imageAttachment.errorMessage);

            var priceList = TotalPriceCalculator(model.RecieverCityId, model.CityId, model.ShippingMethodId);

            var order = new Order
            {
                Notes = model.Notes,
                AppUserId = model.UserId,
                CreatedById = model.UserId,
                Description = model.Description,
                IsSensitive = model.IsSensitive,
                RecieveDate = model.RecieveDate,
                RecieverName = model.RecieverName,
                RecieverAddress = model.RecieverAddress,
                RecieverPhoneNumber = model.RecieverPhoneNumber,
                ShippingDate = model.ShippingDate,
                PaymentMethod = new PaymentMethod
                {
                    Title = "Cash",
                    HolderName = model.RecieverName,
                },
                SenderName = model.SenderName,
                SenderAddress = model.SenderAddress,
                SenderPhoneNumber = model.SenderPhoneNumber,
                OrderAttachment = imageAttachment.AttachmentUrl!,
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
            transaction.Commit();

            var receiptViewModel = new ReceiptFormViewModel
            {
                OrderId = order.OrderId,
                Fees = priceList.Fees,
                OrderTotal = priceList.NetPrice
            };

            return View("_Receipt", receiptViewModel);
        }

        public IActionResult AcceptOrder(ReceiptFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = _context.Orders.Find(model.OrderId);

            if (order == null)
                return NotFound();

            order.OrderTotal = model.OrderTotal;
            order.Fees = model.Fees;

            _context.Update(order);
            _context.SaveChanges();

            var cityView = new CitiesHomeViewModel();  
            
            if (User.IsInRole(AppRoles.StartUp))
                return View("OrderStartup");

            if (User.IsInRole(AppRoles.Individual))
                return View("OrderIndividual");

            return RedirectToAction("Filter", "Orders", cityView);
        }

        public IActionResult RejectOrder(ReceiptFormViewModel model) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = _context.Orders.Find(model.OrderId);

            if (order == null)
                return NotFound(ModelState);

            _context.Remove(order);
            _context.SaveChanges();

            var cityView = new CitiesHomeViewModel();

            if (User.IsInRole(AppRoles.StartUp))
                return View("OrderStartup");

            if (User.IsInRole(AppRoles.Individual))
                return View("OrderIndividual");

            return RedirectToAction("Filter", "Orders", cityView);
        }

        private OrderFormViewModel InitialOrderForm(OrderFormViewModel? model = null)
        {
            OrderFormViewModel startupFormView = model ?? new OrderFormViewModel();

            var governoratesTask = _context.Governorates.Where(c => !c.IsDeleted).OrderBy(c => c.Name).ToList();
            var shippingMethodsTask = _context.ShippingMethods.Where(c => !c.IsDeleted).OrderBy(c => c.Name).ToList();

            startupFormView.Governorates = _mapper.Map<IEnumerable<SelectListItem>>(governoratesTask);
            startupFormView.ShippingMethods = _mapper.Map<IEnumerable<SelectListItem>>(shippingMethodsTask);

            return startupFormView;
        }
        private PriceCalculationResult TotalPriceCalculator(int receiverCityId, int senderCityId, int shippingMethodId)
        {
            int netPrice = 0;
            int fees = 0;
            switch (shippingMethodId)
            {
                case 1011:
                    netPrice = (int)(120 * 0.5);
                    fees = (int)(netPrice * 0.20);

                    if ((receiverCityId == 11 && senderCityId == 12) || (receiverCityId == 12 && senderCityId == 11))
                    {
                        netPrice = (int)(120 * 0.666666667);
                        fees = (int)(netPrice * 0.20);
                    }

                    else if ((receiverCityId == 7 && senderCityId == 10) || (receiverCityId == 10 && senderCityId == 7))
                    {
                        netPrice = (int)(120 * 0.333333333);
                        fees = (int)(netPrice * 0.20);
                    }
                    else if ((receiverCityId == 9 && senderCityId == 10) || (receiverCityId == 10 && senderCityId == 9))
                    {
                        netPrice = (int)(120 * 0.516666667);
                        fees = (int)(netPrice * 0.20);
                    }
                    break;
                case 1012:
                    netPrice = (int)(50 * 0.5);
                    fees = (int)(netPrice * 0.20);
                    if ((receiverCityId == 7 && senderCityId == 10) || (receiverCityId == 10 && senderCityId == 7))
                    {
                        netPrice = (int)(50 * 0.333333333);
                        fees = (int)(netPrice * 0.20);
                    }
                    else if ((receiverCityId == 11 && senderCityId == 7) || (receiverCityId == 7 && senderCityId == 11))
                    {
                        netPrice = (int)(50 * 0.4);
                        fees = (int)(netPrice * 0.20);                        
                    }
                    break;
                default:
                    netPrice = 30;
                    fees = 30;
                    break;
            }

            return new PriceCalculationResult { NetPrice = netPrice, Fees = fees };
        }
    }
}
