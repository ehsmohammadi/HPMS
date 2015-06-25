using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MITD.Presentation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MITD.PMS.Presentation.Contracts
{
    [KnownType(typeof(UnitIndexDTOWithActions))]
    [KnownType(typeof(UnitIndexCategoryDTOWithActions))]
    [JsonConverter(typeof(AbstarctUnitIndexWithActionsDtoConverter))]
    public partial class AbstractUnitIndexDTOWithActions : AbstractIndex, IActionDTO
    {
       public List<int> ActionCodes { get; set; }
    }

    public class AbstarctUnitIndexWithActionsDtoConverter : JsonCreationConverter<AbstractUnitIndexDTOWithActions>
    {

        protected override AbstractUnitIndexDTOWithActions Create(Type objectType,
          JObject jObject)
        {
            if (jObject.Value<string>("$type").Contains("MITD.PMS.Presentation.Contracts.UnitIndexDTOWithActions"))
                return new UnitIndexDTOWithActions();

            if (jObject.Value<string>("$type").Contains("MITD.PMS.Presentation.Contracts.UnitIndexCategoryDTOWithActions"))
                return new UnitIndexCategoryDTOWithActions();
            else
                return new AbstractUnitIndexDTOWithActions();
            
        }
    }
    
}
