using MITD.Presentation;
using System.ComponentModel.DataAnnotations;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class RuleVersionDTO
    {
        private long id;
        private long periodId;
        private int versionId;
        private string content;

        public long Id
        {
            get { return id; }
            set { this.SetField (p => p.Id, ref id, value); }
        }

        public long PeriodId
        {
            get { return periodId; }
            set { this.SetField(p => p.PeriodId, ref periodId, value); }
        }

        public int VersionId
        {
            get { return versionId; }
            set { this.SetField(p => p.VersionId, ref versionId, value); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Rule Body is required")]
        public string Content
        {
            get { return content; }
            set { this.SetField(p => p.Content, ref content, value); }
        }

    }

}
