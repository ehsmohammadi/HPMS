
using System;
using System.Threading;
using MITD.PMS.Integration.Domain.Contract;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public class ConverterManager
    {


        #region Fields

        private Period period;
        private bool isInitialized = false;
        private readonly IUnitIndexConverter unitIndexConverter;
        
        #endregion

        #region Properties & BackFields


 
        #endregion

        #region Constructors
        public ConverterManager(IUnitIndexConverter unitIndexConverter)
        {
            this.unitIndexConverter = unitIndexConverter;
        }

        #endregion

        #region Public methods
        public void Run()
        {
            if (isInitialized)
            {
                unitIndexConverter.ConvertUnitIndex(period);

            }
            else
            {
                throw new ArgumentNullException("period","ConverterManager was not initialized");
            }
        }

        public void Init(Period preiodParam)
        {
            if (preiodParam == null)
               throw new ArgumentNullException("period", "Period can not be null");
            this.period = preiodParam;
            isInitialized=true;
        }

        #endregion

        #region Temp

        public void CreateCustomFields()
        {
            // create customfield for Jobindex
            //var PmsCustomField = new CustomFieldDTO 
            //{
            //    Name = "اهمیت",
            //    TypeId="string",
            //    DictionaryName = "JobIndexImportance",
            //    MinValue=0,
            //    MaxValue=10,
            //    EntityId=(int)EntityTypeEnum.JobIndex,
            //};
            ////CustomFieldService.GetAllCustomFields((res, exp) => 
            ////{
            ////    string s = ";";
            ////}, "Job");
            //CustomFieldService.AddCustomField((res, exp) =>
            //{
            //}, PmsCustomField);

            // //create customfield for Unitindex 
            //var PmsCustomFieldu = new CustomFieldDTO
            //{
            //    Name = "اهمیت",
            //    TypeId = "string",
            //    DictionaryName = "UnitIndexImportance",
            //    MinValue = 0,
            //    MaxValue = 10,
            //    EntityId = (int)EntityTypeEnum.UnitIndex,
            //};

            //CustomFieldService.AddCustomField((resu, expu) =>
            //{
            //}, PmsCustomFieldu);
        }



        public void CreateUnitIndexCategory()
        {
            // Create Unit Index Category
            //var PmsUnitIndexCategory = new UnitIndexCategoryDTO
            //{
            //    Name = "گروه شاخص های سازمانی",
            //    DictionaryName = "UnitIndexCategoryDicName"
            //};

            //UnitIndexService.AddUnitIndexCategory((res, exp) =>
            //{
            //    if (exp != null)
            //    {
            //        throw new Exception("Error in Add Unit Index Category!");
            //    }
            //}, PmsUnitIndexCategory);

        }

        public void ConvertUnit(long PeriodID)
        {
            // unitConverter.ConvertUnits(PeriodID);
        }

        #endregion
    }
}
