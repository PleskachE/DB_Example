﻿using System.Collections.Generic;

namespace EntityFrameworkExample.Models.DbModels
{
    public partial class Status
    {
        public Status()
        {
            Test = new HashSet<Test>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Test> Test { get; set; }
    }
}
