using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class EmailDTO
    {
        private string email;
        public string Email
        {
            get { return email; }
            set { this.SetField(p => p.Email, ref email, value); }
        }

        private int status;
        public int Status
        {
            get { return status; }
            set { this.SetField(p => p.Status, ref status, value); }
        }

        
    }
}
