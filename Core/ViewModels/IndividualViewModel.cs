namespace Speedy.Core.ViewModels
{
    public class IndividualViewModel : BaseViewModel
    {
        #region User AppUser Properties                
        public string Email { get; set; } = null!;
        public string? ProfilePictureIUrl { get; set; } = string.Empty;
        public string NID { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        #endregion

        #region Properties
        public int Id {  get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Byte? Rate { get; set; }
        public string ServiceArea { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? ProfilePictureUrl { get; set; }
        public string MobileNumber { get; set; } = null!;
        #endregion

        #region Overloaded Data      
        public string GovernorateName { get; set; } = null!;
        public string CityName { get; set; } = null!;
        public bool IsDeleted { get; set; }
        #endregion
    }
}
