using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Common;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Exceptions;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Domain.Model.JobIndexPoints;
using Omu.ValueInjecter;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMSReport.Domain.Model;
using MITD.PMS.Domain.Model.JobIndexPoints;
using System;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Model;


namespace MITD.PMS.Interface
{
  //  [Interceptor(typeof(Interception))]
    public class JobIndexPointFacadeService : IJobIndexPointFacadeService
    {
        private IJobIndexPointRepository repository;
        private ICalculationRepository calculationRep;
       
        private IMapper<JobIndexPointWithEmployee, JobIndexPointSummaryDTOWithAction> jobIndexPointMapper;
        private readonly IEmployeeRepository employeeRep;

        public JobIndexPointFacadeService(IJobIndexPointRepository repository,
            ICalculationRepository calculationRep, IMapper<JobIndexPointWithEmployee, 
            JobIndexPointSummaryDTOWithAction>  jobIndexPointMapper,
            IEmployeeRepository  employeeRep
            )
        {
            this.repository = repository;
            this.calculationRep = calculationRep;
            this.jobIndexPointMapper = jobIndexPointMapper;
            this.employeeRep = employeeRep;
        }

        [RequiredPermission(ActionType.ShowCalculationResult)]
        public PageResultDTO<JobIndexPointSummaryDTOWithAction> GetAllJobIndexPoints(long periodId, long calculationId, int pageSize, int pageIndex)
        {
            var fs = new ListFetchStrategy<JobIndexPointWithEmployee>(Enums.FetchInUnitOfWorkOption.NoTracking);
            var cal = calculationRep.GetById(new CalculationId(calculationId));
            if (cal.PeriodId.Id != periodId)
                throw new Exception("چنین محاسبه ای در دوره ذکر شده نداریم.");
            fs.WithPaging(pageSize, pageIndex).OrderByDescending(c=>c.JobIndexPoint.Value);

            repository.Find<SummaryEmployeePoint>(c => c.CalculationId == cal.Id && c.IsFinal, fs);

            var res = new PageResultDTO<JobIndexPointSummaryDTOWithAction>();
            res.InjectFrom(fs.PageCriteria.PageResult);
            var i = (res.CurrentPage - 1) * res.PageSize;
            var lst = fs.PageCriteria.PageResult.Result.Select(p =>
                {
                    i++;
                    var m = jobIndexPointMapper.MapToModel(p);
                    m.EmployeeRankInPeriod =  i;
                    return m;
                }).ToList();
            res.Result = lst;
            return res;
        }

        [RequiredPermission(ActionType.ShowCalculationResult)]
        public JobIndexPointSummaryDTOWithAction GetEmployeeSummaryCalculationResult(long periodId, long calculationId, string employeeNo)
        {
            var fs = new ListFetchStrategy<JobIndexPointWithEmployee>(Enums.FetchInUnitOfWorkOption.NoTracking);
            
            var cal = calculationRep.GetById(new CalculationId(calculationId));
            if (cal.PeriodId.Id != periodId)
                throw new Exception("چنین محاسبه ای در دوره ذکر شده نداریم.");
            var empId = new EmployeeId(employeeNo, new PeriodId(periodId));
            fs.WithPaging(1, 0).OrderByDescending(c => c.JobIndexPoint.Value);

             repository.Find<SummaryEmployeePoint>(c => c.CalculationId == cal.Id && c.IsFinal && c.EmployeeId.EmployeeNo == empId.EmployeeNo
                 ,fs);
            if (!fs.PageCriteria.PageResult.Result.Any())
                throw new JobIndexPointException((int)ApiExceptionCode.DoesNotExistEvaluationForEmployee, ApiExceptionCode.DoesNotExistEvaluationForEmployee.DisplayName);
             var sumaryPoints = fs.PageCriteria.PageResult.Result.Single();
            return jobIndexPointMapper.MapToModel(sumaryPoints);
        }

        [RequiredPermission(ActionType.ShowCalculationResult)]
        public List<JobPositionValueDTO> GetEmployeeJobPositionsCalculationResult(long periodId, long calculationId, string employeeNo)
        {
            var res = new List<JobPositionValueDTO>();
            var empPoints = employeeRep.GetPoints(employeeRep.GetBy(new EmployeeId(employeeNo, new PeriodId(periodId))),
                                         new CalculationId(calculationId));

            //empPoints.SummaryEmployeePoints
            foreach (var itm in empPoints.SummaryJobPositionPoints)
            {
                res.Add(new JobPositionValueDTO()
                    {
                        Name = itm.Key.Name,
                        JobPoints = itm.Value.SummaryJobPositionPoints.Select(p => new PointValueDTO { Name = p.Name, Value = p.Value }).ToList(),
                        JobIdexPoints = itm.Value.JobIndexPoints.Select(p => new JobIndexResultValueDTO()
                        {
                            JobIndexName = p.Key.Name,
                            IndexPoints = p.Value.Select(k => new PointValueDTO() { Name = k.Name, Value = k.Value }).ToList()
                        }).ToList()
                    });
            }
            return res;
        }
    }
}
