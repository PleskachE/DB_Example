using System.Collections.Generic;

namespace DapperExample.Models.DbModel
{
    public class Author
    {
        public Author()
        {
            Test = new HashSet<Test>();
        }

        public long id { get; set; }
        public string name { get; set; }
        public string login { get; set; }
        public string email { get; set; }

        public virtual ICollection<Test> Test { get; set; }
    }
}
