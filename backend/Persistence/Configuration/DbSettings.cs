namespace TodoList.Persistence.Configuration;

public class DbSettings
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Host { get; set; }
    public required string Port { get; set; }
    public required string DatabaseName { get; set; }
    public string ConnectionString
    {
        get
        {
            return $"User ID = {Username}; Password = {Password}; Host = {Host}; Port = {Port}; Database = {DatabaseName};";
        }

    }
}
