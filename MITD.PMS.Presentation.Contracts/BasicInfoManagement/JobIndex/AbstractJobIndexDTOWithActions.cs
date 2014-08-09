using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MITD.Presentation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MITD.PMS.Presentation.Contracts
{
    [KnownType(typeof(JobIndexDTOWithActions))]
    [KnownType(typeof(JobIndexCategoryDTOWithActions))]
    [JsonConverter(typeof(AbstarctIndexWithActionsDtoConverter))]
    public partial class AbstractJobIndexDTOWithActions : AbstractIndex, IActionDTO
    {
       public List<int> ActionCodes { get; set; }
    }

    public class AbstarctIndexWithActionsDtoConverter : JsonCreationConverter<AbstractJobIndexDTOWithActions>
    {

        protected override AbstractJobIndexDTOWithActions Create(Type objectType,
          JObject jObject)
        {
            if (jObject.Value<string>("$type").Contains("MITD.PMS.Presentation.Contracts.JobIndexDTOWithActions"))
                return new JobIndexDTOWithActions();

            if (jObject.Value<string>("$type").Contains("MITD.PMS.Presentation.Contracts.JobIndexCategoryDTOWithActions"))
                return new JobIndexCategoryDTOWithActions();
            else
                return new AbstractJobIndexDTOWithActions();
            
        }
    }
    
}
