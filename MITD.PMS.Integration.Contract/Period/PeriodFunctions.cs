using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.Domain;

namespace MITD.PMS.Integration.Contract.Period
{
    public class PeriodFunctions : IPeriodFunctions
    {

        private PeriodProperties _Period = new PeriodProperties();
        private Boolean _IsInsertable = new Boolean();
        private EmployeeConverter _EmployeeService;
        private OrganChartConverter _OrganchartService;

        public PeriodFunctions(PeriodProperties Period, IEmployeeDataProvider EmployeeService, IOrganChartDataProvider OrganchartService)
        {
            _Period.PeriodID = Period.PeriodID;
            _Period.PeriodName = Period.PeriodName;
            _Period.PeriodStateID = Period.PeriodID;
            _Period.PeriodStateName = Period.PeriodStateName;

            _IsInsertable = Period.PeriodStateID == 2;


            //IEmployeeDataProvider EmployeeService;
            _EmployeeService = new EmployeeConverter(EmployeeService);

            //IOrganChartDataProvider OrganchartService;
            _OrganchartService = new OrganChartConverter(OrganchartService);
        }


        public void InsertEmployees()
        {
            if (_IsInsertable)
            {
                _EmployeeService.InsertEmployees();
            }
            else
                return;
        }

        public void InsertUnits()
        {
            if (_IsInsertable)
            {
                _OrganchartService.InsertUnits();
            }
            else
                return;
        }

        public void InsertNodeType()
        {
            if (_IsInsertable)
            {
                _OrganchartService.InsertNodeType();
            }
            else
                return;
        }


        public void InsertJobTitles()
        {
            if (_IsInsertable)
            {
                _OrganchartService.InsertJobTitles();
            }
            return;

        }

        public void InsertOrganChart()
        {
            if (_IsInsertable)
            {
                _OrganchartService.InsertOrganChart();
            }
            else
                return;
        }

    }
}
