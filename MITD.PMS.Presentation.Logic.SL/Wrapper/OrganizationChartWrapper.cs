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
    public partial class PeriodServiceWrapper : IPeriodServiceWrapper
    {
        private List<OrganizationElementDto> organizationElementList = new List<OrganizationElementDto>()
            {
                new OrganizationElementDto(){Id=0, Title = "چارت سازمانی" ,ParentId = null},
                  new OrganizationElementDto(){Id=1, Title = "مدیریت" ,ParentId = 0},
                    new OrganizationElementDto(){Id=2, Title = "مدیریت مالی" ,ParentId = 1},
                      new OrganizationElementDto(){Id=3, Title = "مدیریت اداری" ,ParentId = 1}
            };
        private List<UnitDto> UnitList = new List<UnitDto>();
        private OrganizationElementDto organizationTree = new OrganizationElementDto();

        public void GetAllUnit(Action<ObservableCollection<UnitDto>, Exception> action)
        {
            UnitList = new List<UnitDto>
                {
                    new UnitDto(){Id=1,Title = "اداری"},
                    new UnitDto(){Id=2,Title = "مالی"},
                    new UnitDto(){Id=3,Title = "رایانه"},
                };
            action(new ObservableCollection<UnitDto>(UnitList), null);
        }

        public void AddUnitOrgElement(Action<UnitOrgElementDto, Exception> action, UnitOrgElementDto unitOrgElement)
        {
            long id = 1;
            if (organizationElementList.Count > 0)
                id = organizationElementList.Max(p => p.Id) + 1;

            unitOrgElement.Id = id;
            unitOrgElement.Title = UnitList.FirstOrDefault(j => j.Id == unitOrgElement.UnitId).Title;
            //unitOrgElement.ActionCodes = new List<int> { 21, 22, 23 };
            organizationElementList.Add(unitOrgElement);
            action(unitOrgElement, null);
        }

        public void UpdateUnitOrgElement(Action<UnitOrgElementDto, Exception> action, UnitOrgElementDto unitOrgElement)
        {
            var element = organizationElementList.FirstOrDefault(p => p.Id == unitOrgElement.Id);
            organizationElementList.Remove(element);
            organizationElementList.Add(unitOrgElement);
            action(unitOrgElement, null);
        }

        public void GetOrganizationChartTree(Action<OrganizationElementDto, Exception> action)
        {
            organizationTree = organizationElementList.Single(l => l.ParentId == null);
            var parent = organizationTree;
            SetChildNodes(parent);
            action(organizationTree, null);
        }

        private void SetChildNodes(OrganizationElementDto parent)
        {
            parent.ActionCodes = new List<int> {21, 22, 23, 24, 25, 26};
            parent.ChildNodes = new ObservableCollection<OrganizationElementDto>();
            foreach (var element in organizationElementList.Where(e => e.ParentId == parent.Id))
            {
                parent.ChildNodes.Add(element);
                SetChildNodes(element);
            }
        }
    }
}
