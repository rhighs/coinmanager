using System;
using Npgsql;
using Npgsql.PostgresTypes;
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

        public async Task InsertCryptos(List<CoinMarket> list)
        {
            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            foreach(var crypto in list) 
            {
                if(crypto.id.Length > 20) continue;
                var cmd = new NpgsqlCommand("INSERT INTO crypto (id, name, symbol, currentprice, imageurl, marketcap, marketcaprank, circulatingsupply, totalvolume) VALUES (:id, :name, :symbol, :currentprice, :imageurl, :marketcap, :marketcaprank, :circulatingsupply, :totalvolume);", conn);
 
                NpgsqlParameter id = new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Text);
                id.Value = crypto.id;

                NpgsqlParameter name = new NpgsqlParameter("name", NpgsqlTypes.NpgsqlDbType.Text);
                name.Value = crypto.name;

                NpgsqlParameter sym = new NpgsqlParameter("symbol", NpgsqlTypes.NpgsqlDbType.Text);
                sym.Value = crypto.symbol;

                NpgsqlParameter curr = new NpgsqlParameter("currentprice", NpgsqlTypes.NpgsqlDbType.Real);
                curr.Value = crypto.current_price;

                NpgsqlParameter image = new NpgsqlParameter("imageurl", NpgsqlTypes.NpgsqlDbType.Text);
                image.Value = crypto.image;

                NpgsqlParameter mkt = new NpgsqlParameter("marketcap", NpgsqlTypes.NpgsqlDbType.Real);
                mkt.Value = crypto.market_cap;

                NpgsqlParameter mktrank = new NpgsqlParameter("marketcaprank", NpgsqlTypes.NpgsqlDbType.Real);
                mktrank.Value = crypto.market_cap_rank;

                NpgsqlParameter circ = new NpgsqlParameter("circulatingsupply", NpgsqlTypes.NpgsqlDbType.Real);
                circ.Value = crypto.circulating_supply;

                NpgsqlParameter tot = new NpgsqlParameter("totalvolume", NpgsqlTypes.NpgsqlDbType.Real);
                tot.Value = crypto.total_volume;

                cmd.Parameters.Add(id);
                cmd.Parameters.Add(name);
                cmd.Parameters.Add(sym);
                cmd.Parameters.Add(curr);
                cmd.Parameters.Add(image);
                cmd.Parameters.Add(mkt);
                cmd.Parameters.Add(mktrank);
                cmd.Parameters.Add(circ);
                cmd.Parameters.Add(tot);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
