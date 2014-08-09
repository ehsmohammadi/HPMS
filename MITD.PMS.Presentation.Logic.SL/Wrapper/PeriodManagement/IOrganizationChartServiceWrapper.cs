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

    public interface IOrganizationChartServiceWrapper : IServiceWrapper
    {
        //void AddUnitOrgElement(Action<UnitOrgElement, Exception> action, UnitOrgElement unitOrgElement);
        //void UpdateUnitOrgElement(Action<UnitOrgElement, Exception> action, UnitOrgElement unitOrgElement);
        //void GetOrganizationChartTree(Action<List<TreeElement>, Exception> action);
        void GetAllUnit(Action<ObservableCollection<UnitDTO>, Exception> action);
        void DeleteUnitOrgElement(Action<bool, Exception> action, long id);
        //void GetJobPositionChartTree(Action<List<TreeElement>, Exception> action);
        void GetAllJobPosition(Action<ObservableCollection<JobPositionDTO>, Exception> action);
        //void AddJobPositionOrgChart(Action<JobPositionOrgElement, Exception> action, JobPositionOrgElement jobPositionOrgElement);
        //void UpdateJobPositionOrgChart(Action<JobPositionOrgElement, Exception> action, JobPositionOrgElement jobPositionOrgElement);
    }
}
