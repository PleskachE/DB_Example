using System;
using System.Collections.Generic;

namespace EntityFrameworkExample.Models.DbModels
{
    public partial class Test
    {
        public Test()
        {
            Attachment = new HashSet<Attachment>();
            DevInfo = new HashSet<DevInfo>();
            Log = new HashSet<Log>();
            RelFailReasonTest = new HashSet<RelFailReasonTest>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int? StatusId { get; set; }
        public string MethodName { get; set; }
        public long ProjectId { get; set; }
        public long SessionId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Env { get; set; }
        public string Browser { get; set; }
        public long? AuthorId { get; set; }

        public virtual Author Author { get; set; }
        public virtual Project Project { get; set; }
        public virtual Session Session { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<Attachment> Attachment { get; set; }
        public virtual ICollection<DevInfo> DevInfo { get; set; }
        public virtual ICollection<Log> Log { get; set; }
        public virtual ICollection<RelFailReasonTest> RelFailReasonTest { get; set; }
    }
}
