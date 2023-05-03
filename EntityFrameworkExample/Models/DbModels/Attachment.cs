namespace EntityFrameworkExample.Models.DbModels
{
    public partial class Attachment
    {
        public long Id { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public long TestId { get; set; }

        public virtual Test Test { get; set; }
    }
}
