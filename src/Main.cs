using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        var list = new List<SimpleCoin>();
        Task.Run(async () => 
                {
                list = await client.GetCoinsList();        
                var onlyIds = list.Select(c => c.id);
                var txtStream = File.AppendText("./market.txt");
                Func<string[], Task> api = async (string[] _list) => {
                    var mktList = await client.GetMarketData(vs, _list);
                    mktList.ForEach((c) => 
                        {
                            if (c.market_cap_rank is not null && c.market_cap_rank < 300)
                            {
                                Console.WriteLine("success");
                                Console.WriteLine(c.market_cap_rank + " " + c.market_cap_rank.GetType().ToString());
                            }
                            txtStream.WriteLine(
                                new UTF8Encoding(true)
                                    .GetBytes(c.name + " " + c.market_cap_rank.ToString() + "\n")
                                );
                            names.Add(c.name);
                        });
                };
                var l = new List<string>();
                for(int i = 0; i < onlyIds.Count(); i++)
                {
                    var id = onlyIds.ElementAt(i);
                    l.Add(id);
                    if(i%100== 0)
                    {
                        var arr = l.ToArray();
                        await api(l.ToArray());
                        l.Clear();    
                    }
                }
                }).Wait();
        foreach(var n in names)
        {
            Console.WriteLine(n);
        }
    }
}

