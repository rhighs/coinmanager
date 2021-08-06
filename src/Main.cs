using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using CoinManager.GUI;
using CoinManager.API;
using CoinManager.DB;
using CoinManager.Shared;

public class Startup
{
    public static void Main(string[] args)
    {
        const string TITLE = "CoinManager Desktop";
        new Eto.Forms.Application().Run(new CoinsForm(TITLE));
        var client = new CoingeckoClient();
        var connString = "Host=localhost;Username=rob;Password=rob;Database=coinmanager";
        var db = new DbHelper(connString);

        List<CoinMarket> items;
        using (StreamReader r = new StreamReader("./market.json"))
        {
            string json = r.ReadToEnd();
            items = JsonSerializer.Deserialize<List<CoinMarket>>(json);
        }
        Task.Run(async () => 
                {
                    await db.InsertCryptos(items);
                }).Wait();
    }
}

