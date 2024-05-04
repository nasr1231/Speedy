namespace Speedy.Core.ViewModels
{
    public class IndividualViewModel : BaseViewModel
    {
        #region Properties
        public int FirstName { get; set; }
        public int LastName { get; set; }
        public Byte? Rate { get; set; }
        public string ServiceArea { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? ProfilePictureUrl { get; set; }
        public string MobileNumber { get; set; } = null!;                
        #endregion

        #region Relations        
        #endregion
    }
}
