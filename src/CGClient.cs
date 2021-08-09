using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using CoinManager.ApiData;
using CoinManager.Models.CG;

namespace CoinManager.API
{
    public class CoingeckoClient
    {
        public List<SimpleCoin> CoinsList { get; private set; }
        private HttpClient http;

        public CoingeckoClient()
        {
            http = new HttpClient();
        }

        public async Task<List<SimpleCoin>> GetCoinsList() 
        {
            var data = (await http.GetAsync(CGPaths.CoinsList)).Content;
            var dataString = await data.ReadAsStringAsync();
            return JsonSerializer
                .Deserialize<List<SimpleCoin>>(
                    await data.ReadAsStringAsync()
                    );
        }

        public async Task<CoinInfo> GetCoinHistory(int coinId)
        {
            string completeUrl = String.Format(CGPaths.CoinHistoryId, coinId);
            var data = (await http.GetAsync(completeUrl)).Content;
            string dataString = await data.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CoinInfo>(dataString);
        }

        public async Task<List<CoinMarket>> GetMarketData(string vsSymbol, params string[] ids)
        {
            string completeUrl = CGPaths.CoinsMarket + "?vs_currency=" + vsSymbol;
            if(ids.Length != 0)
            {
                string paramChain = string.Join(",", ids);
                completeUrl += $"&ids={paramChain}";
            }
            var data = (await http.GetAsync(completeUrl)).Content;
            var dataString = await data.ReadAsStringAsync();
            Console.WriteLine($"reveiced {string.Join(",", ids)}");
            Thread.Sleep(1000);
            return JsonSerializer.Deserialize<List<CoinMarket>>(dataString);
        }

        public async Task<List<CoinMarket>> MarketRanked(int rankCap, string vs="usd")
        {
            var coinsList = await GetCoinsList();        
            var txtStream = File.AppendText("./market.txt");
            var idsList = new List<string>();
            var finalList = new List<CoinMarket>();
            var maxParams = 30;
            Func<string[], Task> fetchMarket = async (string[] list) => {
                var mktList = await GetMarketData(vs, list);
                if(list.Length == 0) return;
                mktList.ForEach((c) => 
                        {
                            if (c.market_cap_rank is not null && c.market_cap_rank <= rankCap)
                                finalList.Add(c);
                        });
            };
            for(int i = 0; i < coinsList.Count(); i++)
            {
                if(finalList.Count >= rankCap){
                    Console.WriteLine("All coins retreived, stopping procedure...");
                    break;
                }
                idsList.Add(coinsList.ElementAt(i).id);
                if(i % maxParams == 0)
                {
                    var arr = idsList.ToArray();
                    await fetchMarket(idsList.ToArray());
                    idsList.Clear();    
                }
            }
            await fetchMarket(idsList.ToArray());
            return finalList;
        }

        public async Task DumpMarketJson(int rankCap, string vs="usd")
        {
            var list = await MarketRanked(rankCap, vs);
            string json = JsonSerializer.Serialize<List<CoinMarket>>(list);
            File.WriteAllText("./market.json", json);
        }
    }
}
