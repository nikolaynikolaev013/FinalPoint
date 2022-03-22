using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FinalPoint.Data
{
    public class DbService : IDbService
    {
        private readonly IConfiguration configuration;

        public DbService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public SqlConnection Conn { get; set; }

        public SqlDataReader RunProcedure(SqlCommand command)
        {
            this.OpenConnection();
            command.Connection = this.Conn;
            SqlDataReader reader = command.ExecuteReader();
            return reader;

        }

        private void OpenConnection()
        {
            var connetionString = this.configuration.GetConnectionString("DefaultConnection");
            this.Conn = new SqlConnection(connetionString);
            this.Conn.Open();
        }
    }
}
