
using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Core;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.Data.Contract.DTO;
using MITD.PMS.Integration.PMS.Contract;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public class EmployeeConverter : IEmployeeConverter
    {
        #region Fields
        private readonly IEmployeeDataProvider employeeDataProvider;
        private readonly IEmployeeServiceWrapper employeeService;
        private List<JobPositionDTO> jobPositionList = new List<JobPositionDTO>();
        private int totalEmployeesCount;
        private List<EmployeeDTO> employeeList=new List<EmployeeDTO>();
        private readonly IEventPublisher publisher;
        #endregion

        #region Constructors
        public EmployeeConverter(IEmployeeDataProvider employeeDataProvider, IEmployeeServiceWrapper employeeService, IEventPublisher publisher)
        {
            this.employeeDataProvider = employeeDataProvider;
            this.employeeService = employeeService;
            this.publisher = publisher;
        }

        #endregion

        #region public methods

        public void ConvertEmployees(Period period, List<JobPositionDTO> jobPositionInPeriodList)
        {
            Console.WriteLine("Starting employees Convert progress...");
            jobPositionList = jobPositionInPeriodList;
            var employeeIdList = employeeDataProvider.GetIds();
            totalEmployeesCount = employeeIdList.Count;

            foreach (var id in employeeIdList)
            {
                var sourceEmployeeDTO = employeeDataProvider.GetEmployeeDetails(id);
                var desEmployeeDTO = createDestinationEmployee(sourceEmployeeDTO, period);
                var employee = employeeService.AddEmployee(desEmployeeDTO);
                var jobPosition = jobPositionList.Single(c=>c.TransferId==sourceEmployeeDTO.JobPositionTransferId);
                var employeeJobPositionDTO = createEmployeeJobPositionDTO(period, employee, jobPosition);//Selected jobPosition 
                employeeService.AssignJobPositionsToEmployee(period.Id, employee.PersonnelNo,
                    employeeJobPositionDTO);
                employeeList.Add(employee);
                Console.WriteLine("Employee convert progress state: " + employeeList.Count + " From " +
                                  totalEmployeesCount);
            }

            publisher.Publish(new EmployeeConverted(employeeList));
        }

        private EmployeeJobPositionsDTO createEmployeeJobPositionDTO(Period period, EmployeeDTO employee, JobPositionDTO jobPosition)
        {
            var res = new EmployeeJobPositionsDTO
                      {
                          PeriodId = period.Id,
                          EmployeeNo = employee.PersonnelNo,
                          EmployeeJobPositionAssignmentList = new List<EmployeeJobPositionAssignmentDTO>
                                                              {
                                                                  new EmployeeJobPositionAssignmentDTO
                                                                  {
                                                                      JobPositionId = jobPosition.Id,
                                                                      JobPositionName = jobPosition.Name,
                                                                      FromDate = DateTime.Now,
                                                                      ToDate = DateTime.Now.AddDays(3),
                                                                      WorkTimePercent = 100,
                                                                      JobPositionWeight = 1


                                                                  }
                                                              }
                      };
                    return res;
                    
        }

        #endregion

        #region Private methods


        private void handleException(Exception exception)
        {
            throw new Exception("Error In Add Employee", exception);
        }


        private EmployeeDTO createDestinationEmployee(EmployeeIntegrationDTO sourceEmployee,Period period)
        {
            var res = new EmployeeDTO
            {
                FirstName = sourceEmployee.Name,
                LastName = sourceEmployee.Family,
                PeriodId = period.Id,
                PersonnelNo = sourceEmployee.PersonnelCode,
                CustomFields = new List<CustomFieldValueDTO>()

            };
            return res;
        }

        #endregion

    }


}


