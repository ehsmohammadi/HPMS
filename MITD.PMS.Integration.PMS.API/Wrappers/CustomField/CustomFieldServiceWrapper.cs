using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Integration.Core;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public class CustomFieldServiceWrapper : ICustomFieldServiceWrapper
    {
        public CustomFieldServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        private readonly string apiCustomFieldAddress = PMSClientConfig.BaseApiAddress + "CustomFields";
        private readonly string apiEntityTypeAddress = PMSClientConfig.BaseApiAddress + "EntityTypes";
        private IUserProvider userProvider;


        public void GetCustomField(Action<CustomFieldDTO, Exception> action, long id)
        {
            var url = string.Format(apiCustomFieldAddress + "?Id=" + id, UriKind.Absolute);
            IntegrationWebClient.Get(new Uri(url), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void AddCustomField(Action<CustomFieldDTO, Exception> action, CustomFieldDTO customField)
        {
            var url = string.Format(apiCustomFieldAddress, UriKind.Absolute);
            IntegrationWebClient.Post(new Uri(url), action, customField, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void UpdateCustomField(Action<CustomFieldDTO, Exception> action, CustomFieldDTO customField)
        {
            var url = string.Format(apiCustomFieldAddress, UriKind.Absolute);
            IntegrationWebClient.Put(new Uri(url), action, customField, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void DeleteCustomField(Action<string, Exception> action, long id)
        {
            var url = string.Format(apiCustomFieldAddress + "?Id=" + id, UriKind.Absolute);
            IntegrationWebClient.Delete(new Uri(url), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        //public void GetAllCustomFieldes(Action<PageResultDTO<CustomFieldDTOWithActions>, Exception> action, int pageSize,
        //                                int pageIndex, Dictionary<string, string> sortBy, CustomFieldCriteria criteria)
        //{
        //    var url = string.Format(apiCustomFieldAddress + "?PageSize={0}&PageIndex={1}&EntityId={2}",
        //                            pageSize, pageIndex, criteria.EntityId,
        //                            UriKind.Absolute);
        //    if (criteria.EntityId != null && criteria.EntityId != 0)
        //        url += "&Filter=" + GetFilterQueryString(criteria);

        //    if (sortBy.Count > 0)
        //        url += "&SortBy=" + QueryConditionHelper.GetSortByQueryString(sortBy);

        //    IntegrationWebClient.Get(new Uri(url), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        public void GetCustomFieldEntityList(Action<List<CustomFieldEntity>, Exception> action)
        {
            var url = string.Format(apiEntityTypeAddress);
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat
               , PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllCustomFields(Action<List<CustomFieldDTO>, Exception> action, string entityType)
        {
            var url = string.Format(apiCustomFieldAddress + "?EntityType=" + entityType);
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllCustomFieldsDescription(Action<List<AbstractCustomFieldDescriptionDTO>, Exception> action, string entityType)
        {
            var url = string.Format(apiCustomFieldAddress + "?EntityType=" + entityType + "&ReturnAbstractCustomFieldDesc=1");
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        //public static string GetFilterQueryString(CustomFieldCriteria criteria)
        //{
        //    return "EntityId=" + criteria.EntityId;
        //}
    }
}
