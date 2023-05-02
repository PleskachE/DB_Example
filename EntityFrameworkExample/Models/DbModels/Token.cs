using System;

namespace EntityFrameworkExample.Models.DbModels
{
    public partial class Token
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int VariantId { get; set; }
        public DateTime CreationTime { get; set; }

        public virtual Variant Variant { get; set; }
    }
}
