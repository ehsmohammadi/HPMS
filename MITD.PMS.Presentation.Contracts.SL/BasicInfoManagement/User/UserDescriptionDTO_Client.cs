
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class UserDescriptionDTO : ViewModelBase
    {
        public string FullName
        {
            get { return FirstName + " "+LastName; }
          
        }
    }
}
