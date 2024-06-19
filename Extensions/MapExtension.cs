namespace Speedy.Extensions;

public static class MapExtension
{
    public static Delivery MapToDelivery(this DeliveryFormViewModel model)
    {
        var delivery = new Delivery
        {
            //Id = model.Id,
            //LicenseNumber = model.LicenseNumber,
            //MedicalSpecialtyId = model.MedicalSpecialtyId,
            //AppUserId = model.UserId,
            //CreatedById = model.UserId
        };

        return delivery;
    }

}
