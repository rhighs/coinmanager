using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using CoinManager.ApiData;
using CoinManager.Shared;

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
            try {
                return JsonSerializer.Deserialize<List<CoinMarket>>(dataString);
            }
            catch
            {
                Console.WriteLine(dataString);
                return JsonSerializer.Deserialize<List<CoinMarket>>(dataString);
            }
        }
    }
}
