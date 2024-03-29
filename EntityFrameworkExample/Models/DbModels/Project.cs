﻿using System.Collections.Generic;

namespace EntityFrameworkExample.Models.DbModels
{
    public partial class Project
    {
        public Project()
        {
            Test = new HashSet<Test>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Test> Test { get; set; }
    }
}
