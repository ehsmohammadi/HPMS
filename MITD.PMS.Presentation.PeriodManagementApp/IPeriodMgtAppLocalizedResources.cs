using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public interface IPeriodMgtAppLocalizedResources : ILocalizedResources
    {
        string PeriodViewTitle { get; }
        string PeriodViewLabelPeriodNameTitle { get; }
        string PeriodViewLabelStartDateTitle { get; }
        string PeriodViewLabelEndDateTitle { get; }

        string PeriodListViewTitle { get; }
        string UnitInPeriodViewTitle { get; }
        string UnitInPeriodTreeViewTitle { get; }
        string JobPositionInPeriodViewTitle { get;}
        string JobPositionInPeriodTreeViewTitle { get; }
        string JobIndexInPeriodTreeViewTitle { get;}
        string JobIndexInPeriodViewTitle { get; }
        string JobIndexGroupInPeriodViewTitle { get; }
        string JobPositionInPeriodInquiryView { get; }
        string CalculationListViewTitle { get;  }
        string ClaimListViewTitle { get; }
        string ClaimViewTitle { get; }
        string ShowClaimViewTitle { get; }
        string PermittedUserListToMyTasksViewTitle { get;  }
        string PermittedUserToMyTasksViewTitle { get; }
        string PeriodBasicDataCopyViewTitle { get;  }
        string CalculationExceptionListViewTitle { get; }
        string CalculationExceptionViewTitle { get; }
        string UnitIndexInPeriodViewTitle { get; }
        string UnitIndexInPeriodTreeViewTitle { get; }
        string UnitIndexGroupInPeriodViewTitle { get; }

    }
}
