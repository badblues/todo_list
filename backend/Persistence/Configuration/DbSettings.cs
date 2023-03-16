namespace TodoList.Persistence.Configuration
{
    public class DbSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConnectionString
        {
            get
            {
                return @$"Data Source=;Initial Catalog=TODODB;User ID={Username}; Password={Password}; TrustServerCertificate=True";
            }
        }
    }
}
