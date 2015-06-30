using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public interface IBasicInfoAppLocalizedResources : ILocalizedResources
    {
        string JobListViewDrgJobListColumnName { get; }
        string JobListViewDrgJobListColumnNameInVocab { get; }
        string JobListViewTitle { get; }

        string JobIndexCategoryListViewTitle { get; }
        string JobIndexViewTitle { get; }
        string JobIndexListViewTitle { get;  }
        string JobIndexListViewFilterCommandTitle { get; }
        string CustomFieldListViewFilterCommandTitle { get; }

        string CustomFieldListViewTitle { get;  }

        string CustomFieldViewTitle { get; }
        string CustomFieldListViewFilterLabelTitle { get; }
        string CustomFieldListViewDrgCustomFieldListColumnName { get; }
        string CustomFieldListViewDrgCustomFieldListColumnEntityNameTitle { get; }
        string CustomFieldListViewDrgCustomFieldListColumnCustomFieldTypeTitle { get; }
        string CustomFieldListViewDrgCustomFieldListColumnMinVlueTitle { get; }
        string CustomFieldListViewDrgCustomFieldListColumnMaxVlueTitle { get; }
        string CustomFieldListViewDrgCustomFieldListColumnDictionaryNameTitle { get; }

        string JobPositionListViewTitle { get;  }
        string JobViewAddFieldsCommandTitle { get; }
        string JobViewModifyFieldsCommandTitle { get;}
        string JobCustomFieldManageViewTitle { get;}
        string JobIndexCustomFieldManageViewTitle { get; }
        string UnitListViewTitle { get;}
        string PolicyListViewTitle { get; }
        string RuleListViewTitle { get;  }
        string PolicyViewTitle { get;}
        string GridRuleListViewAddCommandTitle { get;}
        string GridRuleListViewTitle { get;}
        string RuleViewTitle { get; }
        string RuleVersionViewTitle { get;}
        string RuleVersionViewAddRuleVersionTitle { get;}
        string RuleVersionViewShowRuleVersionsTitle { get; }
        string RuleVersionViewModifyRuleVersionTitle { get;}
        string FunctionListViewTitle { get;  }
        string FunctionViewTitle { get; }
        string UserListViewTitle { get; }
        string UserViewTitle { get; }
        string UserGroupListViewTitle { get; }
        string UserGroupViewTitle { get; }
        string UserListViewCommandTitle { get; }
        string ManagePartyCustomActions { get;  }
        string LogListViewTitle { get; }
        string RuleTrailListViewTitle { get; }

        string UnitCustomFieldManageViewTitle { get; }

        string UnitIndexCustomFieldManageViewTitle { get; }
        string UnitIndexViewTitle { get; }

    }
}
