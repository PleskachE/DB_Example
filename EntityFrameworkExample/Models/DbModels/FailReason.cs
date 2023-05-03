using System.Collections.Generic;

namespace EntityFrameworkExample.Models.DbModels
{
    public partial class FailReason
    {
        public FailReason()
        {
            RelFailReasonTest = new HashSet<RelFailReasonTest>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsGlobal { get; set; }
        public bool IsUnremovable { get; set; }
        public bool IsUnchangeable { get; set; }
        public bool IsStatsIgnored { get; set; }
        public bool IsSession { get; set; }
        public bool IsTest { get; set; }

        public virtual ICollection<RelFailReasonTest> RelFailReasonTest { get; set; }
    }
}
