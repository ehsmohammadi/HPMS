using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Integration.Core;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public class UnitInPeriodServiceWrapper //: IUnitAssignmentService
    {
        private readonly IUserProvider userProvider;
        private readonly string baseAddress = PMSClientConfig.BaseApiAddress;

        public UnitInPeriodServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        private string makeApiAdress(long periodId)
        {
            return "Periods/" + periodId + "/Units";
        }

        public void AddUnitInPeriod(Action<UnitInPeriodAssignmentDTO, Exception> action, UnitInPeriodAssignmentDTO unitInPeriod)
        {
            var url = string.Format(baseAddress + makeApiAdress(unitInPeriod.PeriodId));
            IntegrationWebClient.Post(new Uri(url, PMSClientConfig.UriKind), action, unitInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void DeleteUnitInPeriod(Action<string, Exception> action, long periodId, long unitId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?UnitId=" + unitId);
            IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllUnits(Action<List<UnitInPeriodDTO>, Exception> action, long periodId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?Type=UnitInPeriodDTO");
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetUnitsWithActions(Action<List<UnitInPeriodDTOWithActions>, Exception> action, long periodId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?Type=UnitInPeriodDTOWithActions");
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }


        public void GetUnitInPeriod(Action<UnitInPeriodDTO, Exception> action, long periodId, long unitId)
        {
            GetUnitInPeriod(action, periodId, unitId, "");
        }



        public void GetUnitInPeriod(Action<UnitInPeriodDTO, Exception> action, long periodId, long unitId, string columnNames)
        {

            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?unitId=" + unitId);
            //  url += !string.IsNullOrWhiteSpace(columnNames) ? "&SelectedColumns=" + columnNames : "";
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void GetAllUnitInPeriod(Action<IList<UnitInPeriodDTO>, Exception> action, long periodId, string columnNames)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?Type=unitInPeriodDTO");
            // url += !string.IsNullOrWhiteSpace(columnNames) ? "&SelectedColumns=" + columnNames : "";
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void GetAllUnitInPeriod(Action<IList<UnitInPeriodDTO>, Exception> action, long periodId)
        {
            GetAllUnitInPeriod(action, periodId, "");
        }


        public void AddUnitInPeriod(Action<UnitInPeriodDTO, Exception> action, long periodId, UnitInPeriodDTO unitInPeriodDto)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId));
            IntegrationWebClient.Post(new Uri(url, PMSClientConfig.UriKind), action, unitInPeriodDto, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void UpdateUnitInPeriod(Action<UnitInPeriodDTO, Exception> action, long periodId, UnitInPeriodDTO unitInPeriodDto)
        {

            var url = string.Format(baseAddress + makeApiAdress(periodId));
            IntegrationWebClient.Put(new Uri(url, PMSClientConfig.UriKind), action, unitInPeriodDto, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }


        public void UpdateInquirySubjectInquirers(Action<InquirySubjectWithInquirersDTO, Exception> action, long periodId, long unitId,
            InquirySubjectWithInquirersDTO inquirySubjectWithInquirersDTO)
        {
            throw new NotImplementedException();
        }

        public void GetInquirySubjectWithInquirers(Action<List<InquirySubjectWithInquirersDTO>, Exception> action, long periodId, long unitId)
        {
            var url = string.Format(baseAddress + makeUnitPositionInquirySubjectsApiAdress(periodId, unitId) + "?Include=Inquirers");
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        private string makeUnitPositionInquirySubjectsApiAdress(long periodId, long unitId)
        {
            return "Periods/" + periodId + "/Units/" + unitId + "/InquirySubjects";
        }

        public void AddInquirer(Action<string, Exception> action, long periodId, long unitId, string personalNo)
        {
            var url = string.Format(baseAddress + makeUnitPositionInquirySubjectsApiAdress(periodId, unitId) + "?employeeNo=" + personalNo);
            IntegrationWebClient.Put(new Uri(url, PMSClientConfig.UriKind), action, personalNo, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }


        public void DeleteInquirer(Action<string, Exception> action, long periodId, long unitId, string personalNo)
        {
            var url = string.Format(baseAddress + makeUnitPositionInquirySubjectsApiAdress(periodId, unitId) + "?employeeNo=" + personalNo);
            IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }
    }
}
