using System;
using Npgsql;
using System.Threading.Tasks;

namespace CoinManager.DB
{
    public class DbHelper
    {
        private string connectionString;

        public DbHelper(string connString)
        {
            connectionString = connString;
        }

        public async Task Connect()
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();
            await using (var cmd = new NpgsqlCommand("SELECT * FROM countries", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while(await reader.ReadAsync())
                {
                    Console.WriteLine(reader.GetString(0));
                }
            }
        }
    }
}
