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

            Action<string, object, NpgsqlTypes.NpgsqlDbType, NpgsqlCommand> buildCommand =
            (token, value, type, cmd) => 
            {
                cmd.Parameters.Add(new NpgsqlParameter(token, type){ Value = value });
            };
            foreach(var crypto in list) 
            {
                var cmd = new NpgsqlCommand($"{head} {values}", conn);
                buildCommand("id",       crypto.id,                  NpgsqlDbType.Text,      cmd);
                buildCommand("name",     crypto.name,                NpgsqlDbType.Text,      cmd);
                buildCommand("sym",      crypto.symbol,              NpgsqlDbType.Text,      cmd);
                buildCommand("price",    crypto.current_price,       NpgsqlDbType.Real,      cmd);
                buildCommand("imgurl",   crypto.image,               NpgsqlDbType.Text,      cmd);
                buildCommand("mkt",      crypto.market_cap,          NpgsqlDbType.Real,      cmd);
                buildCommand("mktrank",  crypto.market_cap_rank,     NpgsqlDbType.Integer,   cmd);
                buildCommand("circ",     crypto.circulating_supply,  NpgsqlDbType.Bigint,    cmd);
                buildCommand("vol",      crypto.total_volume,        NpgsqlDbType.Real,      cmd);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
