using System;

using CoinManager.GUI;
using CoinManager.API;
using CoinManager.EF;

public class Startup
{
    public static void Main(string[] args)
    {
        const string TITLE = "CoinManager Desktop";
        new Eto.Forms.Application().Run(new CoinsForm(TITLE));
        var client = new CoingeckoClient();
        var db = new CMDbContext("localhost", "coinmanager", "rob", "rob"); 
        var bitcoin = db.Crypto.Find("bitcoin");
        Console.WriteLine(bitcoin);
        Console.WriteLine(bitcoin.Id + " " + bitcoin.CurrentPrice);
    }
}

