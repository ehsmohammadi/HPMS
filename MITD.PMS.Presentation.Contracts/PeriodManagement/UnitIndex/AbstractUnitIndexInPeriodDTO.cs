using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MITD.Presentation;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MITD.PMS.Presentation.Contracts
{
    [KnownType(typeof(UnitIndexInPeriodDTO))]
    [KnownType(typeof(UnitIndexGroupInPeriodDTO))]
    [KnownType(typeof(AbstractUnitIndexInPeriodDTOWithActions))]
    [JsonConverter(typeof(AbstarctUnitIndexDtoConverter))]

    public partial class AbstractUnitIndexInPeriodDTO
    {
        public string DTOTypeName
        {
            get { return GetType().Name; }
        }

        private long id;
        public long Id
        {
            get { return id; }
            set { this.SetField(p => p.Id, ref id, value); }
        }

        private string name;
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام الزامی است")]
        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }

        private string dictionaryName;
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام در لغت نامه الزامی است")]
        public string DictionaryName
        {
            get { return dictionaryName; }
            set { this.SetField(p => p.DictionaryName, ref dictionaryName, value); }
        }

        private long? parentId;
        public long? ParentId
        {
            get { return parentId; }
            set { this.SetField(p => p.ParentId, ref parentId, value); }
        }

        private long periodId;
        [Range(1, Int64.MaxValue, ErrorMessage = "انتخاب دوره الزامی است")]
        public long PeriodId
        {
            get { return periodId; }
            set { this.SetField(p => p.PeriodId, ref periodId, value); }
        }
    }


    public class AbstarctUnitIndexDtoConverter : JsonCreationConverter<AbstractUnitIndexInPeriodDTO>
    {

        protected override AbstractUnitIndexInPeriodDTO Create(Type objectType,
          JObject jObject)
        {
            var dtoType = jObject.Value<string>("DTOTypeName");
            if (dtoType.Equals("UnitIndexInPeriodDTO"))
                return new AbstractUnitIndexInPeriodDTO();
            else
                return new AbstractUnitIndexInPeriodDTO();

            //if (jObject.Value<string>("$type") != null)
            //{
            //    if (jObject.Value<string>("$type").Contains("MITD.PMS.Presentation.Contracts.JobIndexDTO"))
            //        return new JobIndexDTO();

            //    if (jObject.Value<string>("$type").Contains("MITD.PMS.Presentation.Contracts.JobIndexCategoryDTO"))
            //        return new JobIndexCategoryDTO();
            //    else
            //        return new JobIndexDTO();
            //}
            //else 
            //{
            //    return (AbstractIndex)Activator.CreateInstance(objectType);
            //}
        }
    }
}
