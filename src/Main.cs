using System;
using System.Linq;
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
        var names = new List<string>();
        var client = new CoingeckoClient();
        List<SimpleCoin> list = null;
        Task.Run(async () => 
                {
                list = await client.GetCoinsList();        
                var onlyIds = list.Select(c => c.id);
                Func<string[], Task> api = async (string[] list) => {
                    var mktList = await client.GetMarketData(vs, list);
                    mktList.ForEach((c) => 
                        {
                            names.Add(c.name);
                        });
                };
                var l = new List<string>();
                for(int i = 0; i < onlyIds.Count(); i++)
                {
                    l.Add(onlyIds.ElementAt(i));
                    if(i%10 == 0)
                    {
                        l.Clear();    
                        await api(l.ToArray());
                    }
                }
                }).Wait();

        foreach(var n in names)
        {
            Console.WriteLine(n);
        }

    }
}

