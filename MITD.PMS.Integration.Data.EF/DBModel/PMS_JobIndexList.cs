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
    
    public partial class PMS_JobIndexList
    {
        public long JobID { get; set; }
        public string JobTitle { get; set; }
        public string IndexTitle { get; set; }
        public string IndexTypeTitle { get; set; }
        public long IndexTypeID { get; set; }
        public bool ItemState { get; set; }
        public short coefficient { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.Guid> TransferId { get; set; }
        public long IndexId { get; set; }
    }
}
