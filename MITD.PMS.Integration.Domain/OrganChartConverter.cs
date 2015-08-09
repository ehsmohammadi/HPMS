using System.Collections.Generic;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.Data.Contract.DTO;

namespace MITD.PMS.Integration.Domain
{
    public class OrganChartConverter
    {

        IOrganChartDataProvider _OrganChartService; // = new OrganChartDataPrivider();


        public OrganChartConverter(IOrganChartDataProvider OrganChartService)
        {
            _OrganChartService = OrganChartService;
        }

        public void InsertUnits()
        {
            IList<long> IDs = _OrganChartService.GetUnitIds();

            foreach (var id in IDs)
            {
                UnitDto PersonDetail = _OrganChartService.GetUnitDetail(id);

                // We can call PMS APIs for insert Unit data to PMS Database
            }
        }

        public void InsertNodeType()
        {
            IList<long> IDs = _OrganChartService.GetNodeTypeIds();

            foreach (var id in IDs)
            {
                NodeTypeDto PersonDetail = _OrganChartService.GetNodeTypeDetail(id);

                // We can call PMS APIs for insert NodeType data to PMS Database
            }
        }


        public void InsertJobTitles()
        {

            IList<long> IDs = _OrganChartService.GetJobTitleIds();

            foreach (var id in IDs)
            {
                JobTitleDto PersonDetail = _OrganChartService.GetJobTitleDetail(id);

                // We can call PMS APIs for insert JobTitle data to PMS Database
            }
        }

        public void InsertOrganChart()
        {
            IList<long> IDs = _OrganChartService.GetOrganChartIds();

            foreach (var id in IDs)
            {
                OrganChartNodeDto PersonDetail = _OrganChartService.GetOrganChartDetails(id);

                // We can call PMS APIs for insert OrganChartNode data to PMS Database
            }
        }

    }
}
