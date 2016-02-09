using System;
using MITD.PMS.Integration.Core;
using MITD.PMS.Integration.PMS.Contract;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public class UnitInPeriodServiceWrapperWrapper : IUnitInPeriodServiceWrapper
    {
        #region Fields

        private readonly Uri apiUri = new Uri(PMSClientConfig.BaseApiAddress);

        private readonly IUserProvider userProvider; 
        #endregion

        #region Constructors
        public UnitInPeriodServiceWrapperWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        } 
        #endregion

        #region Private methods
        private string makeEndPoint(long periodId)
        {
            return "Periods/" + periodId + "/Units";
        } 
        #endregion

        #region Public Methods
        public UnitInPeriodDTO AddUnitInPeriod(long periodId, UnitInPeriodDTO unitInPeriodDto)
        {
            return IntegrationHttpClient.Post<UnitInPeriodDTO, UnitInPeriodDTO>(apiUri, makeEndPoint(periodId), unitInPeriodDto);
            //var url = string.Format(baseAddress + makeApiAdress(periodId));
            //IntegrationWebClient.Post(new Uri(url, PMSClientConfig.UriKind), action, unitInPeriodDto, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public UnitInPeriodDTO GetUnitInPeriod(long periodId, long unitId)
        {
            return GetUnitInPeriod(periodId, unitId, "");
        }



        public UnitInPeriodDTO GetUnitInPeriod(long periodId, long unitId, string columnNames)
        {
            var endpoint = makeEndPoint(periodId) + "?unitId=" + unitId;
            endpoint += !string.IsNullOrWhiteSpace(columnNames) ? "&SelectedColumns=" + columnNames : "";
            return IntegrationHttpClient.Get<UnitInPeriodDTO>(apiUri, endpoint);
            //var url = string.Format(baseAddress + makeApiAdress(periodId) + "?unitId=" + unitId);
            //  url += !string.IsNullOrWhiteSpace(columnNames) ? "&SelectedColumns=" + columnNames : "";
            //IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        } 
        #endregion

        //public void AddUnitInPeriod(Action<UnitInPeriodAssignmentDTO, Exception> action, UnitInPeriodAssignmentDTO unitInPeriod)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(unitInPeriod.PeriodId));
        //    IntegrationWebClient.Post(new Uri(url, PMSClientConfig.UriKind), action, unitInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void DeleteUnitInPeriod(Action<string, Exception> action, long periodId, long unitId)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?UnitId=" + unitId);
        //    IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetAllUnits(Action<List<UnitInPeriodDTO>, Exception> action, long periodId)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?Type=UnitInPeriodDTO");
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetUnitsWithActions(Action<List<UnitInPeriodDTOWithActions>, Exception> action, long periodId)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?Type=UnitInPeriodDTOWithActions");
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetAllUnitInPeriod(Action<IList<UnitInPeriodDTO>, Exception> action, long periodId, string columnNames)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?Type=unitInPeriodDTO");
        //    // url += !string.IsNullOrWhiteSpace(columnNames) ? "&SelectedColumns=" + columnNames : "";
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //public void GetAllUnitInPeriod(Action<IList<UnitInPeriodDTO>, Exception> action, long periodId)
        //{
        //    GetAllUnitInPeriod(action, periodId, "");
        //}

        //public void UpdateUnitInPeriod(Action<UnitInPeriodDTO, Exception> action, long periodId, UnitInPeriodDTO unitInPeriodDto)
        //{

        //    var url = string.Format(baseAddress + makeApiAdress(periodId));
        //    IntegrationWebClient.Put(new Uri(url, PMSClientConfig.UriKind), action, unitInPeriodDto, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}


        //public void AddInquirer(Action<string, Exception> action, long periodId, long unitId, string personalNo, long unitIndexInUnitId)
        //{
        //    throw new NotImplementedException();
        //}

        //public void UpdateInquirySubjectInquirers(Action<InquirySubjectWithInquirersDTO, Exception> action, long periodId, long unitId,
        //    InquirySubjectWithInquirersDTO inquirySubjectWithInquirersDTO)
        //{
        //    throw new NotImplementedException();
        //}

        //public void GetInquirySubjectWithInquirers(Action<List<InquirySubjectWithInquirersDTO>, Exception> action, long periodId, long unitId)
        //{
        //    var url = string.Format(baseAddress + makeUnitPositionInquirySubjectsApiAdress(periodId, unitId) + "?Include=Inquirers");
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //private string makeUnitPositionInquirySubjectsApiAdress(long periodId, long unitId)
        //{
        //    return "Periods/" + periodId + "/Units/" + unitId + "/InquirySubjects";
        //}

        //public void AddInquirer(Action<string, Exception> action, long periodId, long unitId, string personalNo)
        //{
        //    var url = string.Format(baseAddress + makeUnitPositionInquirySubjectsApiAdress(periodId, unitId) + "?employeeNo=" + personalNo);
        //    IntegrationWebClient.Put(new Uri(url, PMSClientConfig.UriKind), action, personalNo, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}


        //public void DeleteInquirer(Action<string, Exception> action, long periodId, long unitId, string personalNo)
        //{
        //    var url = string.Format(baseAddress + makeUnitPositionInquirySubjectsApiAdress(periodId, unitId) + "?employeeNo=" + personalNo);
        //    IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

    }
}
