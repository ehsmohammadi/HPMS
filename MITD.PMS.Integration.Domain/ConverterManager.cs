
using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.PMS.Integration.PMS.API;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public class ConverterManager
    {
        #region Fields
        private List<UnitIndexInPeriodDTO> unitIndexInperiodList = new List<UnitIndexInPeriodDTO>();
        private List<JobIndexInPeriodDTO> jobIndexInperiodList = new List<JobIndexInPeriodDTO>();
        private List<JobIndexDTO> jobIndexList = new List<JobIndexDTO>();
        private List<JobDTO> jobList = new List<JobDTO>();
        private List<UnitDTO> unitList = new List<UnitDTO>();
        private Period period;
        private bool isInitialized;
        private List<JobPositionDTO> jobPositionList=new List<JobPositionDTO>(); 
        private DelegateHandler<UnitIndexConverted> unitIndexConvertedHandler;
        private DelegateHandler<UnitConverted> unitConvertedHandler;
        private DelegateHandler<JobIndexConverted> jobIndexConvertedHandler;
        private DelegateHandler<JobConverted> jobConvertedHandler;
        private DelegateHandler<JobPositionConverted> jobPositionConvertedHandler;
        private readonly IUnitIndexConverter unitIndexConverter;
        private readonly IUnitConverter unitConverter;
        private readonly IJobIndexConverter jobIndexConverter;
        private readonly IJobConverter jobConverter;
        private readonly IJobPositionConverter jobPositionConverter;
        private readonly IEmployeeConverter employeeConverter;
        private readonly IPeriodServiceWrapper periodService;
        private readonly IEventPublisher publisher;


        #endregion

        #region Properties & BackFields



        #endregion

        #region Constructors

        public ConverterManager(IUnitIndexConverter unitIndexConverter, IUnitConverter unitConverter,
            IJobIndexConverter jobIndexConverter, IJobConverter jobConverter, IEventPublisher publisher,
            IJobPositionConverter jobPositionConverter,IEmployeeConverter employeeConverter,IPeriodServiceWrapper periodService)
        {
            this.unitIndexConverter = unitIndexConverter;
            this.unitConverter = unitConverter;
            this.jobIndexConverter = jobIndexConverter;
            this.jobConverter = jobConverter;
            this.publisher = publisher;
            this.jobPositionConverter = jobPositionConverter;
            this.employeeConverter = employeeConverter;
            this.periodService = periodService;
        }

        #endregion

        #region Public methods

        public void Init()
        {
            var activePeriod = periodService.GetActivePeriod();
            if(activePeriod==null)
                throw new Exception("There is no active period in 'EPMS' activate atleast one period for continue the operation");
            period = new Period(activePeriod.Id,activePeriod.Name,new PeriodState());
            isInitialized = true;
        }

        public void Run()
        {
            if (isInitialized)
            {
                registerHandler();
                
                unitIndexConverter.ConvertUnitIndex(period);
                
            }
            else
            {
                throw new ArgumentNullException("period", "ConverterManager was not initialized");
            }
        }

        private void registerHandler()
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
                unitList = e.UnitList;
                Console.WriteLine("{0} Units converted , Unit progress finished", e.UnitList.Count);
                jobIndexConverter.ConvertJobIndex(period);
            });
            publisher.RegisterHandler(unitConvertedHandler);

            #endregion

            #region JobIndex Converter handler

            jobIndexConvertedHandler = new DelegateHandler<JobIndexConverted>(e =>
                {
                    jobIndexInperiodList = e.JobIndexInperiodList;
                    jobIndexList = e.JobIndexList;
                    Console.WriteLine("{0} Job index converted , Job index progress finished", jobIndexInperiodList.Count);
                    jobConverter.ConvertJobs(period, jobIndexInperiodList, jobIndexList);
                });
            publisher.RegisterHandler(jobIndexConvertedHandler);

            #endregion

            #region Job Conveter Handler

            jobConvertedHandler = new DelegateHandler<JobConverted>(e =>
            {
                jobList = e.JobList;
                Console.WriteLine("{0} Jobs Converted , Job progress finished", e.JobList.Count);
                jobPositionConverter.ConvertJobPositions(period, unitList, jobList);

            });
            publisher.RegisterHandler(jobConvertedHandler);

            #endregion

            #region JobPosition Conveter Handler

            jobPositionConvertedHandler = new DelegateHandler<JobPositionConverted>(e =>
            {
                jobPositionList = e.JobPositionList;
                Console.WriteLine("{0} JobPositions Converted , JobPosition progress finished", e.JobPositionList.Count);
                employeeConverter.ConvertEmployees(period, jobPositionList);

            });
            publisher.RegisterHandler(jobPositionConvertedHandler);

            #endregion

        }

        #endregion

    }
}
