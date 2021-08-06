using System;
using Npgsql;
using NpgsqlTypes;
using System.Threading.Tasks;
using System.Collections.Generic;

using CoinManager.Shared;

namespace CoinManager.DB
{
    public class DbHelper
    {
        private string connectionString;

        public DbHelper(string connString)
        {
            connectionString = connString;
        }

        public async Task InsertCryptos(List<CoinMarket> list)
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();
            string head = "INSERT INTO \"Crypto\" VALUES";
            string values = "(:id, :name, :sym, :price, :imgurl, :mkt, :mktrank, :circ, :vol);";

            Action<string, object, NpgsqlTypes.NpgsqlDbType, NpgsqlCommand> makeCommand =
            (token, value, type, cmd) => 
            {
                cmd.Parameters.Add(new NpgsqlParameter(token, type){ Value = value });
            };
            foreach(var crypto in list) 
            {
                var cmd = new NpgsqlCommand($"{head} {values}", conn);
                makeCommand("id",       crypto.id,                  NpgsqlDbType.Text,      cmd);
                makeCommand("name",     crypto.name,                NpgsqlDbType.Text,      cmd);
                makeCommand("sym",      crypto.symbol,              NpgsqlDbType.Text,      cmd);
                makeCommand("price",    crypto.current_price,       NpgsqlDbType.Real,      cmd);
                makeCommand("imgurl",   crypto.image,               NpgsqlDbType.Text,      cmd);
                makeCommand("mkt",      crypto.market_cap,          NpgsqlDbType.Real,      cmd);
                makeCommand("mktrank",  crypto.market_cap_rank,     NpgsqlDbType.Integer,   cmd);
                makeCommand("circ",     crypto.circulating_supply,  NpgsqlDbType.Bigint,    cmd);
                makeCommand("vol",      crypto.total_volume,        NpgsqlDbType.Real,      cmd);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
