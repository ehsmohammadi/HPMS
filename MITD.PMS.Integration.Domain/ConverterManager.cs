
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public class ConverterManager
    {
        #region Fields
        private List<UnitIndexInPeriodDTO> unitIndexInperiodList = new List<UnitIndexInPeriodDTO>();
        private List<JobIndexInPeriodDTO> jobIndexInperiodList = new List<JobIndexInPeriodDTO>();
        private List<JobInPeriodDTO> jobInperiodList = new List<JobInPeriodDTO>();
        private List<UnitInPeriodDTO> unitInperiodList = new List<UnitInPeriodDTO>();
        private Period period;
        private bool isInitialized = false;
        private DelegateHandler<UnitIndexConverted> unitIndexConvertedHandler;
        private DelegateHandler<UnitConverted> unitConvertedHandler;
        private DelegateHandler<JobIndexConverted> jobIndexConvertedHandler;
        private DelegateHandler<JobConverted> jobConvertedHandler;
        private readonly IUnitIndexConverter unitIndexConverter;
        private readonly IUnitConverter unitConverter;
        private readonly IJobIndexConverter jobIndexConverter;
        private readonly IJobConverter jobConverter;
        private readonly IEventPublisher publisher;

        #endregion

        #region Properties & BackFields



        #endregion

        #region Constructors
        public ConverterManager(IUnitIndexConverter unitIndexConverter, IUnitConverter unitConverter, IJobIndexConverter jobIndexConverter, IJobConverter jobConverter, IEventPublisher publisher)
        {
            this.unitIndexConverter = unitIndexConverter;
            this.unitConverter = unitConverter;
            this.jobIndexConverter = jobIndexConverter;
            this.jobConverter = jobConverter;
            this.publisher = publisher;
        }

        #endregion

        #region Public methods

        public void Init(Period preiodParam)
        {
            if (preiodParam == null)
                throw new ArgumentNullException("period", "Period can not be null");
            this.period = preiodParam;
            isInitialized = true;
        }

        public void Run()
        {
            if (isInitialized)
            {
                RegisterHandler();
                unitIndexConverter.ConvertUnitIndex(period);
            }
            else
            {
                throw new ArgumentNullException("period", "ConverterManager was not initialized");
            }
        }

        private void RegisterHandler()
        {
            #region UnitIndex Converter handler

            unitIndexConvertedHandler = new DelegateHandler<UnitIndexConverted>(e =>
                {
                    unitIndexInperiodList = e.UnitIndexInperiodList;
                    Console.WriteLine("{0} Converted , Unit index progress finished", unitIndexInperiodList.Count);
                    unitConverter.ConvertUnits(period, unitIndexInperiodList);
                });
            publisher.RegisterHandler(unitIndexConvertedHandler);

            #endregion

            #region Unit Converter handler

            unitConvertedHandler = new DelegateHandler<UnitConverted>(e =>
            {
                unitInperiodList = e.UnitInperiodList;
                Console.WriteLine("{0} Units converted , Unit progress finished", e.UnitInperiodList.Count);
                jobIndexConverter.ConvertJobIndex(period);
            });
            publisher.RegisterHandler(unitConvertedHandler);

            #endregion

            #region JobIndex Converter handler

            jobIndexConvertedHandler = new DelegateHandler<JobIndexConverted>(e =>
                {
                    jobIndexInperiodList = e.JobIndexInperiodList;
                    Console.WriteLine("{0} Job index converted , Job index progress finished", jobIndexInperiodList.Count);
                    jobConverter.ConvertJobs(period, jobIndexInperiodList);
                });
            publisher.RegisterHandler(jobIndexConvertedHandler);

            #endregion

            #region Job Conveter Handler

            jobConvertedHandler = new DelegateHandler<JobConverted>(e =>
            {
                jobInperiodList = e.JobInperiodList;
                Console.WriteLine("{0} Jobs Converted , Job progress finished", e.JobInperiodList.Count);

            });
            publisher.RegisterHandler(jobConvertedHandler);

            #endregion

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
