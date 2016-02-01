
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MITD.PMS.Integration.PMS.API;
using MITD.PMS.Integration.PMS.Contract;
using MITD.PMS.Presentation.Contracts;

using MITD.PMS.Integration.Data.Contract;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Integration.Data.EF;

using MITD.PMS.Integration.Data.EF;
using MITD.PMS.Integration.Domain;
using MITD.PMS.Integration.PMS.API;
using MITD.PMS.Presentation.Contracts;


namespace MITD.PMS.Integration.Domain
{
    public class ConverterManager
    {
        #region Fields

        private bool IsInitialized = false;
        private ICustomFieldServiceWrapper CustomFieldService;
        private IUnitIndexServiceWrapper UnitIndexService;
        private IUnitIndexInPeriodServiceWrapper UnitIndexInPeriodService;


        private UnitConverter unitConverter = new UnitConverter();
        private UnitIndexConverter unitIndexConverter;//= new UnitIndexConverter();
        

        #endregion

        #region Properties & BackFields

        private Period period;
        public Period Period
        {
            get
            {
                return period;
            }
            set
            {
                period = value;
            }
        }
 
        #endregion

        public ConverterManager(ICustomFieldServiceWrapper CustomFieldService, IUnitIndexDataProvider UnitIndexDataProvider, IUnitIndexServiceWrapper UnitIndexService, IUnitIndexInPeriodServiceWrapper UnitIndexInPeriodService)
        {
            this.CustomFieldService = CustomFieldService;
            this.unitIndexConverter = new UnitIndexConverter(UnitIndexDataProvider, UnitIndexService, UnitIndexInPeriodService);

        }

        public void Run(Period preiodParam)
        {
            Init(preiodParam);
            if (IsInitialized)
            {
                //CreateCustomFields();
                //Update Convert State to CustomFields Created!

                //CreateUnitIndexCategory();
                unitIndexConverter.ConvertUnitIndex(preiodParam);
                //Update Convert State to UnitIndexCategory Created!

                ConvertUnit(preiodParam.ID);
                //Update Convert State to Units Converted!

            }
            else
            {
                throw new ArgumentNullException("Converter manager class is not initialized!");
            }
        }

        #region Initialize

        public void Init(Period preiodParam)
        {
            if (preiodParam == null)
               throw new ArgumentNullException("period", "Period can not be null");
            this.period = preiodParam;
            IsInitialized=true;
        }

        #endregion


        #region Create Custom Fields

        public void CreateCustomFields()
        {
            // create customfield for Jobindex
            var PmsCustomField = new CustomFieldDTO 
            {
                Name = "اهمیت",
                TypeId="string",
                DictionaryName = "JobIndexImportance",
                MinValue=0,
                MaxValue=10,
                EntityId=(int)EntityTypeEnum.JobIndex,
            };
            //CustomFieldService.GetAllCustomFields((res, exp) => 
            //{
            //    string s = ";";
            //}, "Job");
            CustomFieldService.AddCustomField((res, exp) =>
            {
            }, PmsCustomField);

             //create customfield for Unitindex 
            var PmsCustomFieldu = new CustomFieldDTO
            {
                Name = "اهمیت",
                TypeId = "string",
                DictionaryName = "UnitIndexImportance",
                MinValue = 0,
                MaxValue = 10,
                EntityId = (int)EntityTypeEnum.UnitIndex,
            };

            CustomFieldService.AddCustomField((resu, expu) =>
            {
            }, PmsCustomFieldu);

            Thread.Sleep(10000);
        }

        #endregion


        #region Create Unit Index Category

        public void CreateUnitIndexCategory()
        {
            // Create Unit Index Category
            var PmsUnitIndexCategory = new UnitIndexCategoryDTO
            {
                Name = "گروه شاخص های سازمانی",
                DictionaryName = "UnitIndexCategoryDicName"
            };

            UnitIndexService.AddUnitIndexCategory((res, exp) =>
            {
                if (exp != null)
                {
                    throw new Exception("Error in Add Unit Index Category!");
                }
            }, PmsUnitIndexCategory);

        }

        #endregion



        #region Convert Units

        public void ConvertUnit(long PeriodID)
        {
            unitConverter.ConvertUnits(PeriodID);
        }

        #endregion
    }
}
