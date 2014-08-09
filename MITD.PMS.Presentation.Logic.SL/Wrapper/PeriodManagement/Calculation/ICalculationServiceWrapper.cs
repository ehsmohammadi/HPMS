using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public interface ICalculationServiceWrapper:IServiceWrapper
    {
        void GetCalculation(Action<CalculationDTO, Exception> action, long periodId, long id);
        void AddCalculation(Action<CalculationDTO, Exception> action, CalculationDTO calculation);
        void GetAllCalculation(Action<PageResultDTO<CalculationBriefDTOWithAction>, Exception> action, long periodId, int pageSize, int pageIndex);
        void DeleteCalculation(Action<string, Exception> action, long periodId, long id);

        
        void GetAllCalculationResults(Action<PageResultDTO<JobIndexPointSummaryDTOWithAction>, Exception> action, 
            long periodId, long calculationId, int pageSize, int pageIndex);

        void GetEmployeeSummaryCalculationResult(Action<JobIndexPointSummaryDTOWithAction, Exception> action, long periodId, long calculationId, string employeeNo);

       
        //void GetCalculationResultDetails(Action<EmployeeCalculationResultDetailsDTO, Exception> action, long calculationId, string employeeNo, int pageSize, int pageIndex);
        void GetCalculationState(Action<CalculationStateWithRunSummaryDTO, Exception> action, long periodId, long id);
        void ChangeCalculationState(Action<Exception> action, long periodId, long id, CalculationStateDTO calculationState);
        void GetEmployeeJobPositionsCalculationResult(Action<List<JobPositionValueDTO>, Exception> action,long periodId, long calculationId,string employeeNo);
        void GetDeterministicCalculation(Action<CalculationDTO, Exception> action, long periodId);
        void ChangeDeterministicCalculation(Action<Exception> action, CalculationDTO calacDto);
        void GetAllCalculationException(Action<PageResultDTO<CalculationExceptionBriefDTOWithAction>, Exception> action, long periodId, long calculationId, int pageSize, int pageIndex);
        void GetCalculationException(Action<CalculationExceptionDTO, Exception> action, long periodId, long calculationId,long exceptionId);
        
    }
}
