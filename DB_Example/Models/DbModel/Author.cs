namespace DB_Example.Models.DbModel
{
    public class Author
    {
        public Author() { }

        public Author(string name, string login, string email)
        {
            this.name = name;
            this.login = login;
            this.email = email;
        }

        public Author(int id, string name, string login, string email)
        {
            this.id = id;
            this.name = name;
            this.login = login;
            this.email = email;
        }

        public int id { get; set; }
        public string name { get; set; }
        public string login { get; set; }
        public string email { get; set; }
    }
}
