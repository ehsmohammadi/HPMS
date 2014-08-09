using System;
using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using MITD.Presentation;


namespace MITD.PMS.Presentation.Logic.Wrapper
{
    public class UnitInPeriodServiceWrapper : IUnitInPeriodServiceWrapper
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
            WebClientHelper.Post(new Uri(url, PMSClientConfig.UriKind), action, unitInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void DeleteUnitInPeriod(Action<string, Exception> action, long periodId, long unitId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?UnitId=" + unitId);
            WebClientHelper.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllUnits(Action<List<UnitInPeriodDTO>, Exception> action, long periodId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?Type=UnitInPeriodDTO");
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetUnitsWithActions(Action<List<UnitInPeriodDTOWithActions>, Exception> action, long periodId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?Type=UnitInPeriodDTOWithActions");
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

    }
}
