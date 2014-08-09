using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class UserDTO 
    {
        
        public string FullName
        {
          get { return firstName + " " + LastName; }  
        }

    }


}
