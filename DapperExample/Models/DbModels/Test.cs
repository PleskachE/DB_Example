using System;

namespace DapperExample.Models.DbModel
{
    public class Test
    {
        public long id { get; set; }
        public string name { get; set; }
        public int? status_id { get; set; }
        public string method_name { get; set; }
        public long project_id { get; set; }
        public long session_id { get; set; }
        public DateTime? start_time { get; set; }
        public DateTime? end_time { get; set; }
        public string env { get; set; }
        public string browser { get; set; }
        public long? author_id { get; set; }
    }
}
