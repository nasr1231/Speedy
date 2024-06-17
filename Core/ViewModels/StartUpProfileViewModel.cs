namespace Speedy.Core.ViewModels
{
    public class StartUpProfileViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public DateTime EstablishDate { get; set; }
        public string City { get; set; }
        public List<string> Urls { get; set; }
        public string CompanyName { get; set; }

    }
}
