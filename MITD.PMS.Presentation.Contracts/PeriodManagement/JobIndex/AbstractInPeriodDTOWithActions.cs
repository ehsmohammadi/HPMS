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
    [KnownType(typeof(JobIndexInPeriodDTOWithActions))]
    [KnownType(typeof(JobIndexGroupInPeriodDTOWithActions))]
    [JsonConverter(typeof(AbstarctJobIndexWithActionsDtoConverter))]
    public partial class AbstractIndexInPeriodDTOWithActions : AbstractIndexInPeriodDTO, IActionDTO
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


    public class AbstarctJobIndexWithActionsDtoConverter : JsonCreationConverter<AbstractIndexInPeriodDTOWithActions>
    {

        protected override AbstractIndexInPeriodDTOWithActions Create(Type objectType,
          JObject jObject)
        {
            var dtoType = jObject.Value<string>("DTOTypeName");
            if (dtoType.Equals("JobIndexInPeriodDTOWithActions"))
                return new JobIndexInPeriodDTOWithActions();
            else
                return new JobIndexGroupInPeriodDTOWithActions();

        }
    }
}
