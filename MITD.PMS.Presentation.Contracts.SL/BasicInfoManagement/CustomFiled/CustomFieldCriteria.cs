using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public class CustomFieldCriteria : ViewModelBase
    {
        private int? entityId;
        public int? EntityId
        {
            get { return entityId; }
            set
            {
                this.SetField(p => p.EntityId, ref entityId, value);
            }
        }

    }
}
