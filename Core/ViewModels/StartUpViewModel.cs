namespace Speedy.Core.ViewModels
{
    public class StartUpViewModel : BaseViewModel
    {
        
        #region User Properties
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? ProfilePictureIUrl { get; set; } = string.Empty;
        public string NID { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string MobileNumber { get; set; } = null!;
        #endregion

        #region StartUp Properties          
        public bool IsOnline { get; set; }
        public bool IsDeleted { get; set; }
        public string StartUpName { get; set; } = null!;
        public DateTime FoundingDate { get; set; }
        public string Address { get; set; } = null!;        
        public string GovernorateName { get; set; } = null!;
        public string CityName { get; set; } = null!;
        public bool HasWhatsApp { get; set; }
        #endregion
    }
}
