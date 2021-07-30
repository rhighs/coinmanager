using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using CoinManager.Shared;

namespace CoinManager.API
{
    public class CoingeckoClient
    {
        private HttpClient http;

        public List<SimpleCoin> CoinsList { get; private set; }

        public CoingeckoClient()
        {
            http = new HttpClient();
        }

        public async Task<List<SimpleCoin>> GetCoinsList() 
        {
            var data = (await http.GetAsync(CGPaths.Base + CGPaths.CoinsList)).Content;
            var dataString = await data.ReadAsStringAsync();
            return JsonSerializer
                .Deserialize<List<SimpleCoin>>(
                    await data.ReadAsStringAsync()
                    );
        }
    }
}
