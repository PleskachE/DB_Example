﻿using System;
using System.Collections.Generic;

namespace EntityFrameworkExample.Models.DbModels
{
    public partial class Session
    {
        public Session()
        {
            Test = new HashSet<Test>();
        }

        public long Id { get; set; }
        public string SessionKey { get; set; }
        public DateTime CreatedTime { get; set; }
        public long BuildNumber { get; set; }

        public virtual ICollection<Test> Test { get; set; }
    }
}
