namespace EntityFrameworkExample.Models.DbModels
{
    public partial class Log
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public bool IsException { get; set; }
        public long TestId { get; set; }

        public virtual Test Test { get; set; }
    }
}
