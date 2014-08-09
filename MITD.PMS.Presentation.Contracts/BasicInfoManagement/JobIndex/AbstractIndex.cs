using System.Runtime.Serialization;
using MITD.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MITD.PMS.Presentation.Contracts
{
    [KnownType(typeof(JobIndexDTO))]
    [KnownType(typeof(JobIndexCategoryDTO))]
    [KnownType(typeof(AbstractJobIndexDTOWithActions))]
    [JsonConverter(typeof(AbstarctIndexDtoConverter))]
    public partial class AbstractIndex
    {
        public string DTOTypeName
        {
            get { return GetType().Name; }
        }

        private long id;
        public long Id
        {
            get { return id; }
            set { this.SetField (p => p.Id, ref id, value); }
        }

        private string name;
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام را وارد نمایید")]
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

        private string dictionaryName;
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام در لغت نامه را وارد نمایید")]
        public string DictionaryName
        {
            get { return dictionaryName; }
            set { this.SetField(p => p.DictionaryName, ref dictionaryName, value); }

            
        }
    }

    public class AbstarctIndexDtoConverter : JsonCreationConverter<AbstractIndex>
    {

        protected override AbstractIndex Create(Type objectType,
          JObject jObject)
        {
            var dtoType = jObject.Value<string>("DTOTypeName");
            if (dtoType.Equals("JobIndexDTO"))
                return new JobIndexDTO();
            else
                return new JobIndexCategoryDTO();
        }
    }

}
