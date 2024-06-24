namespace Speedy.Core.ViewModels
{
    public class AdminDashboardViewModel
    {
        public List<Delivery> Deliveries { get; set; }
        public List<StartUp> StartUps { get; set; }
        public List<Individual> Individuals { get; set; }
        public List<Order> Orders { get; set; }

        // Summary properties
        public int TotalDeliveries { get; set; }
        public int TotalStartUps { get; set; }
        public int TotalIndividuals { get; set; }
        public int TotalOrders { get; set; }


    }
}
