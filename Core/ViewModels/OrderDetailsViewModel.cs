namespace Speedy.Core.ViewModels
{
    public class OrderDetailsViewModel
    {
        public int TrackingNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string RecieverName { get; set; }
        public string RecieverAddress { get; set; }
        public string PriceTotal { get; set; }
    }
}
