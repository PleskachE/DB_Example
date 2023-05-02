using Common.Utils;
using DB_Example.Constants;
using DB_Example.Models;

namespace DB_Example.SqlQueries
{
    public class SqlQuery
    {
        private static SqlQueryModel _sqlQueryModel =>
            JsonParser<SqlQueryModel>.Parse(Utils.FileReader.ReadAllText(PathToFiles.SqlQueriesFile));

        public static string SelectAllAuthors => _sqlQueryModel.SelectAllAuthors;

        public static string SelectTestsByAuthorId(long authorId) => string.Format(_sqlQueryModel.SelectTestsByAuthor, authorId);

        public static string SelectAuthorByEmail(string authorEmail) => string.Format(_sqlQueryModel.SelectAuthorByEmail, authorEmail);

        public static string SelectAuthorById(long authorId) => string.Format(_sqlQueryModel.SelectAuthorById, authorId);

        public static string InsertNewAuthor(string name, string login, string email) =>
            string.Format(_sqlQueryModel.InsertNewAuthor, name, login, email);

        public static string UpdateAuthorById(string property, string value, long id) =>
            string.Format(_sqlQueryModel.UpdateAuthorById, property, value, id);

        public static string DeleteAuthorById(long id) =>
            string.Format(_sqlQueryModel.DeleteAuthorById, id);
    }
}
