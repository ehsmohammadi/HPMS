using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class ChangePasswordDTO
    {

        private string oldPassword;
        [Required(AllowEmptyStrings = false,ErrorMessage = "فیلد کلمه عبور قدیمی الزامی است")]
        public string OldPassword
        {
            get { return oldPassword; }
            set { this.SetField(p => p.OldPassword, ref oldPassword, value); }
        }

        
        private string newPassword;
        [Required(AllowEmptyStrings = false, ErrorMessage = "فیلد کلمه عبور جدید الزامی است")]
        public string NewPassword
        {
            get { return newPassword; }
            set { this.SetField(p => p.NewPassword, ref newPassword, value); }
        }
        
    }
}
