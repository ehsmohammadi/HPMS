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
    
    public partial class PMS_GeneralIndex
    {
        public long ID { get; set; }
        public long ID_IndexType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public short coefficient { get; set; }
        public Nullable<System.Guid> TransferId { get; set; }
        public Nullable<int> LogUserID { get; set; }
        public string LogActionType { get; set; }
        public Nullable<System.DateTime> LogActionDate { get; set; }
        public string LogComputerName { get; set; }
    
        public virtual PMS_IndexType PMS_IndexType { get; set; }
    }
}
