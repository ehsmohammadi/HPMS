using System;
using System.Collections.Generic;
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

        private List<CustomFieldDTO> customFields = new List<CustomFieldDTO>();
        public List<CustomFieldDTO> CustomFields
        {
            get { return customFields; }
            set { this.SetField(p => p.CustomFields, ref customFields, value); }
        }


        private List<InquiryUnitDTO> inquirers = new List<InquiryUnitDTO>();
        public List<InquiryUnitDTO> Inquirers
        {
            get { return inquirers; }
            set { this.SetField(p => p.Inquirers, ref inquirers, value); }
        }

        private List<UnitVerifierDTO> verifiers = new List<UnitVerifierDTO>();
        public List<UnitVerifierDTO> Verifiers
        {
            get { return verifiers; }
            set { this.SetField(p => p.Verifiers, ref verifiers, value); }
        }

        private List<UnitInPeriodUnitIndexDTO> _unitIndices = new List<UnitInPeriodUnitIndexDTO>();
        public List<UnitInPeriodUnitIndexDTO> UnitIndices
        {
            get { return _unitIndices; }
            set { this.SetField(p => p.UnitIndices, ref _unitIndices, value); }
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
