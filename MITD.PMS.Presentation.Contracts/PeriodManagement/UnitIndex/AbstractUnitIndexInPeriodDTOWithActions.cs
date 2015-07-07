using System.Runtime.Serialization;
using MITD.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MITD.PMS.Presentation.Contracts
{
    [KnownType(typeof(UnitIndexInPeriodDTOWithActions))]
    [KnownType(typeof(UnitIndexGroupInPeriodDTOWithActions))]
    [JsonConverter(typeof(AbstractUnitIndexInPeriodDTOWithActionsConverter))]
                          
    public partial class AbstractUnitIndexInPeriodDTOWithActions : AbstractUnitIndexInPeriodDTO, IActionDTO
    {

        //private long id;
        //public long Id
        //{
        //    get { return id; }
        //    set { this.SetField(p => p.Id, ref id, value); }
        //}

        //private string name;
        //public string Name
        //{
        //    get { return name; }
        //    set { this.SetField(p => p.Name, ref name, value); }
        //}

        //private long? parentId;
        //public long? ParentId
        //{
        //    get { return parentId; }
        //    set { this.SetField(p => p.ParentId, ref parentId, value); }
        //}

        public List<int> ActionCodes { get; set; }
    }


    public class AbstractUnitIndexInPeriodDTOWithActionsConverter : JsonCreationConverter<AbstractUnitIndexInPeriodDTOWithActions>
    {

        protected override AbstractUnitIndexInPeriodDTOWithActions Create(Type objectType,
          JObject jObject)
        {
            var dtoType = jObject.Value<string>("DTOTypeName");
            if (dtoType.Equals("UnitIndexInPeriodDTOWithActions"))
                return new UnitIndexInPeriodDTOWithActions();
            else
                return new UnitIndexGroupInPeriodDTOWithActions();

        }
    }
}
