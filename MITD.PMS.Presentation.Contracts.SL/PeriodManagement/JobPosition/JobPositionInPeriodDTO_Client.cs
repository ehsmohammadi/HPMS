using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobPositionInPeriodDTO : ViewModelBase,IChildElement
    {
         public long Id
        {
            get { return JobPositionId; }

        }
    }
}
