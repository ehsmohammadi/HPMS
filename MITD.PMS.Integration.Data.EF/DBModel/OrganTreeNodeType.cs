//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MITD.PMS.Integration.Data.EF.DBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrganTreeNodeType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> LogUserID { get; set; }
        public string LogActionType { get; set; }
        public Nullable<System.DateTime> LogActionDate { get; set; }
        public string LogComputerName { get; set; }
    }
}
