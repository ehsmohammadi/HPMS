using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace MITD.PMS.Presentation.Logic.Wrapper
{
    public partial class PeriodServiceWrapper : IOrganizationChartServiceWrapper
    {
        //private List<TreeElement> organizationElementList = new List<TreeElement>()
        //    {
        //        new TreeElement{Id=0, Title = "واحد های چارت سازمانی" ,ParentId = null,ActionCodes = new List<int> {21, 22, 23}},
        //        new UnitOrgElement {Id=1, Title = "واحد مدیریت" ,ParentId = 0,UnitId = 4,ActionCodes = new List<int> {21, 22, 23}},
        //         new UnitOrgElement{Id=2, Title = "مدیریت مالی" ,ParentId = 1,UnitId = 2,ActionCodes = new List<int> {21, 22, 23}},
        //         new UnitOrgElement{Id=3, Title = "مدیریت اداری" ,ParentId = 1,UnitId = 1,ActionCodes = new List<int> {21, 22, 23}}
        //    };
        private List<UnitDTO> UnitList = new List<UnitDTO>();
        //private TreeElement organizationTree = new TreeElement();

        public void GetAllUnit(Action<ObservableCollection<UnitDTO>, Exception> action)
        {
            UnitList = new List<UnitDTO>
                {
                    new UnitDTO{Id=1,Name = "اداری"},
                    new UnitDTO{Id=2,Name = "مالی"},
                    new UnitDTO{Id=3,Name = "رایانه"},
                    new UnitDTO{Id=4,Name = "واحد مدیریت"}
                };
            action(new ObservableCollection<UnitDTO>(UnitList), null);
        }


        public void DeleteUnitOrgElement(Action<bool, Exception> action, long id)
        {
            //var element = organizationElementList.FirstOrDefault(p => p.Id == id);
            //organizationElementList.Remove(element);
            action(true, null);
        }

       
        //public void AddUnitOrgElement(Action<UnitOrgElement, Exception> action, UnitOrgElement unitOrgElement)
        //{
        //    //long id = 1;
        //    //if (organizationElementList.Count > 0)
        //    //    id = organizationElementList.Max(p => p.Id) + 1;

        //    //unitOrgElement.Id = id;
        //    //unitOrgElement.Title = UnitList.FirstOrDefault(j => j.Id == unitOrgElement.UnitId).Title;
        //    //unitOrgElement.ActionCodes = new List<int> { 21, 22, 23 };
        //    //organizationElementList.Add(unitOrgElement);
        //    action(unitOrgElement, null);
        //}

        //public void UpdateUnitOrgElement(Action<UnitOrgElement, Exception> action, UnitOrgElement unitOrgElement)
        //{
        //    //var element = organizationElementList.FirstOrDefault(p => p.Id == unitOrgElement.Id);
        //    //organizationElementList.Remove(element);
        //    //unitOrgElement.Title = UnitList.FirstOrDefault(j => j.Id == unitOrgElement.UnitId).Title;
        //    //organizationElementList.Add(unitOrgElement);
        //    action(unitOrgElement, null);
        //}

        //public void GetOrganizationChartTree(Action<List<TreeElement>, Exception> action)
        //{
        //    var url = string.Format(baseAddress +"/OrganizationChart");
        //    WebClientHelper.Get<List<TreeElement>>(new Uri(url, UriKind.Absolute),
        //        (res, exp) => action(res, exp),
        //        WebClientHelper.MessageFormat.Json);
        //    //action(organizationElementList, null);
        //}

        //private void SetChildNodes(TreeElement parent)
        //{
        //    //parent.ActionCodes = new List<int> { 21, 22, 23 };
        //    //parent.ChildNodes = new ObservableCollection<TreeElement>();
        //    //foreach (var element in organizationElementList.Where(e => e.ParentId == parent.Id))
        //    //{
        //    //    parent.ChildNodes.Add(element);
        //    //    SetChildNodes(element);
        //    //}
        //}

        #region JobPositionChartTree

        //private List<TreeElement> jobPositionElementList = new List<TreeElement>()
        //    {
        //        //new TreeElement{Id=0, Title = "پست های چارت سازمانی" ,ParentId = null,ActionCodes = new List<int> {24, 25, 26}},
        //        //new JobPositionOrgElement {Id=1, Title = "مدیر عامل" ,ParentId = 0,UnitId = 4,ActionCodes = new List<int> { 24, 25, 26}},
        //        // new TreeElement(){Id=2, Title = "مدیریت مالی" ,ParentId = 1,ActionCodes = new List<int> {21, 22, 23, 24, 25, 26}},
        //        // new TreeElement(){Id=3, Title = "مدیریت اداری" ,ParentId = 1,ActionCodes = new List<int> {21, 22, 23, 24, 25, 26}}
        //    };

        //private List<JobPosition> JobPositions = new List<JobPosition>
        //        {
        //            new JobPosition{Id=1,Title = "مدیر عامل"},
        //            new JobPosition{Id=2,Title = "مدیر مالی"},
        //            new JobPosition{Id=3,Title = "کارشناس رایانه"},
        //            new JobPosition{Id=4,Title = "منشی مدیریت"}
        //        };

        //public void GetJobPositionChartTree(Action<List<TreeElement>, Exception> action)
        // {
        //   //action(jobPositionElementList, null);
        // }

        public void GetAllJobPosition(Action<ObservableCollection<JobPositionDTO>, Exception> action)
        {
            
            //action(new ObservableCollection<JobPositionDTO>(JobPositions), null);
        }

        //public void AddJobPositionOrgChart(Action<JobPositionOrgElement, Exception> action, JobPositionOrgElement jobPositionOrgElement)
        //{
        //    long id = 1;
        //    if (jobPositionElementList.Count > 0)
        //        id = jobPositionElementList.Max(p => p.Id) + 1;

        //    jobPositionOrgElement.Id = id;
        //    jobPositionOrgElement.Title = JobPositions.FirstOrDefault(j => j.Id == jobPositionOrgElement.JobPositionId).Title;
        //    jobPositionOrgElement.ActionCodes = new List<int> { 24, 25, 26 };
        //    jobPositionElementList.Add(jobPositionOrgElement);
        //    action(jobPositionOrgElement, null);
        //}

        //public void UpdateJobPositionOrgChart(Action<JobPositionOrgElement, Exception> action, JobPositionOrgElement jobPositionOrgElement)
        //{
        //    var element = jobPositionElementList.FirstOrDefault(p => p.Id == jobPositionOrgElement.Id);
        //    jobPositionElementList.Remove(element);
        //    jobPositionOrgElement.Title = JobPositions.FirstOrDefault(j => j.Id == jobPositionOrgElement.JobPositionId).Title;
        //    jobPositionElementList.Add(jobPositionOrgElement);
        //    action(jobPositionOrgElement, null);
        //}

        #endregion
    }
}
