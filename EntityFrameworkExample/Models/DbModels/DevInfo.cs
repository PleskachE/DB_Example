namespace EntityFrameworkExample.Models.DbModels
{
    public partial class DevInfo
    {
        public long Id { get; set; }
        public double? DevTime { get; set; }
        public long TestId { get; set; }

        public virtual Test Test { get; set; }
    }
}
