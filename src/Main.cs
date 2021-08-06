using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using CoinManager.GUI;
using CoinManager.API;
using CoinManager.DB;
using CoinManager.Shared;
using CoinManager.EF;

public class Startup
{
    public static void Main(string[] args)
    {
        const string TITLE = "CoinManager Desktop";
        new Eto.Forms.Application().Run(new CoinsForm(TITLE));
        var client = new CoingeckoClient();
        var connString = "Host=localhost;Username=rob;Password=rob;Database=coinmanager";

        List<CoinMarket> items;
        using (StreamReader r = new StreamReader("./market.json"))
        {
            string json = r.ReadToEnd();
            items = JsonSerializer.Deserialize<List<CoinMarket>>(json);
            var db = new CMDbContext("localhost", "coinmanager", "rob", "rob"); 
            var bitcoin = db.crypto.Find("bitcoin");
            var sku = db.userstandard.Find(0);
            Console.WriteLine(bitcoin.id + " " + bitcoin.currentprice);
            Console.WriteLine(sku);
       }
        /*
        Task.Run(async () =>
                {
                    await db.InsertCryptos(items);
                }).Wait();
                */
    }
}

