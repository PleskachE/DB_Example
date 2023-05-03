using Newtonsoft.Json;

namespace DapperExample.Models
{
    public class SqlQueryModel
    {
        [JsonProperty("selectAllAuthors")]
        public string SelectAllAuthors { get; private set; }

        [JsonProperty("selectTestsByAuthor")]
        public string SelectTestsByAuthor { get; private set; }

        [JsonProperty("selectAuthorByEmail")]
        public string SelectAuthorByEmail { get; private set; }

        [JsonProperty("selectAuthorById")]
        public string SelectAuthorById { get; private set; }

        [JsonProperty("insertNewAuthor")]
        public string InsertNewAuthor { get; private set; }

        [JsonProperty("updateAuthorById")]
        public string UpdateAuthorById { get; private set; }

        [JsonProperty("deleteAuthorById")]
        public string DeleteAuthorById { get; private set; }
    }
}
