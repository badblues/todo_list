namespace TodoList.Persistence.Configuration
{
    public class DbSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectionString
        {
            get
            {
                return $"User ID = {Username}; Password = {Password}; Host = {Host}; Port = {Port}; Database = {DatabaseName};";
            }

        }
    }
}
