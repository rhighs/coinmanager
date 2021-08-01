using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CoinManager.GUI;
using CoinManager.API;
using CoinManager.Shared;

public class Startup
{
    public static void Main(string[] args)
    {
        const string TITLE = "CoinManager Desktop";

        new Eto.Forms.Application().Run(new CoinsForm(TITLE));
        string vs = "usd";
        var client = new CoingeckoClient();
        List<SimpleCoin> list = null;
        Task.Run(async () => 
                {
                list = await client.GetCoinsList();                
                var mktList = await client.GetMarketData(vs, "bitcoin", "ethereum");
                mktList.ForEach((c) => 
                        {
                            Console.WriteLine(c.name);
                        });
                }).Wait();
    }
}

