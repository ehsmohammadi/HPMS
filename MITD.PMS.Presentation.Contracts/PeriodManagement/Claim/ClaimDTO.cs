using System;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class ClaimDTO :ClaimDescriptionDTO
    {
       
        private string request = "";
        private string response = "";
      
        [Required(AllowEmptyStrings = false, ErrorMessage = "متن درخواست اعتراض الزامی می باشد")]
        [StringLength(512,ErrorMessage="طول متن درخواست حداکثر 512 کاراکتر میتواند باشد")]
        public string Request
        {
            get { return request; }
            set { this.SetField(p => p.Request, ref request, value); }
        }

        [StringLength(512, ErrorMessage = "طول متن پاسخ حداکثر 512 کاراکتر میتواند باشد")]
        public string Response
        {
            get { return response; }
            set { this.SetField(p => p.Response, ref response, value); }
        }
    }
}
