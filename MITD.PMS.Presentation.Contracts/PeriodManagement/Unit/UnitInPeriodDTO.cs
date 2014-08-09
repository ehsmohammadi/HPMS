using System;
using System.Runtime.Serialization;
using MITD.Presentation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MITD.PMS.Presentation.Contracts
{
    [KnownType(typeof(UnitInPeriodDTOWithActions))]
    //[JsonConverter(typeof(UnitInPeriodDTOConverter))]
    public partial class UnitInPeriodDTO 
    {


        private long unitId; 
        public long UnitId
        {
            get { return unitId; }
            set { this.SetField(p => p.UnitId, ref unitId, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }

        private long? parentId;
        public long? ParentId
        {
            get { return parentId; }
            set { this.SetField(p => p.ParentId, ref parentId, value); }
        }
        
    }

    public class UnitInPeriodDTOConverter:JsonCreationConverter<UnitInPeriodDTO>
    {
        protected override UnitInPeriodDTO Create(Type objectType, JObject jObject)
        {
            return jObject.Value<string>("$Type").Contains("MITD.PMS.Presentation.Contracts.UnitInPeriodDTOWithActions") 
                ? new UnitInPeriodDTOWithActions() 
                : new UnitInPeriodDTO();
        }
    }
}
