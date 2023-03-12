using Microsoft.Data.SqlClient;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args) {
            Console.WriteLine("STARTING PROGRAM...");
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=DESKTOP-30CLT21;Initial Catalog=TODODB;User ID=; Password=; TrustServerCertificate=True";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            Console.WriteLine("CONNECTED");
            cnn.Close();
        }
    }
}