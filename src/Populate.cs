using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using CoinManager.EF;
using CoinManager.API;
using CoinManager.ApiData;
using CoinManager.Models.RD;

namespace CoinManager.Util
{
    public class Populator
    {
        private HttpClient http;
        private CMDbContext db;
        private CoingeckoClient coinGecko;

        public Populator()
        {
            http = new HttpClient();
            db = CMDbContext.Instance;
            coinGecko = new CoingeckoClient();
        }

        private async Task<List<User>> RandomUsers(int noUsers)
        {
            var httpResponse = await http.PostAsync(RDPaths.RandomUser + "?size=" + noUsers.ToString(), null);
            var stringContent = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<User>>(stringContent);
        }

        public async Task GenerateUsers(int noUsers)
        {
            var list = await RandomUsers(noUsers);
            db.Add(list.Select(u => {
                return new UserStandard {
                    Id = u.id,
                    Username = u.username,
                    Password = u.password
                    };
                }));
            db.SaveChanges();
        }

        public async Task GenerateCryptos(int maxRank)
        {
            var marketData = await coinGecko.MarketRanked(maxRank);
            db.Add(marketData.Select(c => {
                    return new Crypto {
                        Id = c.id,
                        Name = c.name,
                        Symbol = c.symbol,
                        CurrentPrice = c.current_price.Value,
                        ImageUrl = c.image,
                        MarketCap = c.market_cap.Value,
                        MarketCapRank = c.market_cap_rank.Value,
                        CirculatingSupply = Convert.ToInt32(c.circulating_supply),
                        TotalVolume = c .total_volume.Value
                    };
                })
            );
            db.SaveChanges();
        }
    }
}
