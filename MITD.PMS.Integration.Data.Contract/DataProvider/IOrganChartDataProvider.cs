using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.Data.Contract.DTO;

namespace MITD.PMS.Integration.Data.Contract.DataProvider
{
    public interface IOrganChartDataProvider
    {

        IList<long> GetOrganChartIds();

        OrganChartNodeDto GetOrganChartDetails(long id);

        IList<long> GetUnitIds();

        UnitDto GetUnitDetail(long id);


        IList<long> GetJobTitleIds();

        JobTitleDto GetJobTitleDetail(long id);


        IList<long> GetNodeTypeIds();

        NodeTypeDto GetNodeTypeDetail(long id);

    }
}
