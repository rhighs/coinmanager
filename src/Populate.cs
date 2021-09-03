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
            var httpResponse = await http.GetAsync(RDPaths.RandomUser + "?size=" + noUsers.ToString());
            var stringContent = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<User>>(stringContent);
        }

        public async Task GenerateUsers(int noUsers)
        {
            var list = await RandomUsers(noUsers);
            int maxMiningPower = 10;
            var mapped = list.Select(u =>
            {
                return new UserStandard {
                    Id = u.id,
                    Username = u.username,
                    Password = u.password
               };
            }).ToList();

            var random = new Random();
            mapped.ForEach(u =>
            {
                db.UserStandard.Add(u);
                if(random.Next(1, 101) > 20)
                {
                    db.UserMiner.Add(new UserMiner
                    {
                        Id = u.Id,
                        Miningpower = random.Next(1, maxMiningPower)
                    });
                }
            });

            db.SaveChanges();
        }

        public async Task GenerateCryptos(int maxRank)
        {
            var marketData = await coinGecko.MarketRanked(maxRank);
            var mapped = marketData.Select(c => {
                    return new Crypto {
                        Id = c.id,
                        Name = c.name,
                        Symbol = c.symbol,
                        CurrentPrice = c.current_price.Value,
                        ImageUrl = c.image,
                        MarketCap = c.market_cap.Value,
                        MarketCapRank = c.market_cap_rank.Value,
                        CirculatingSupply = Convert.ToInt64(c.circulating_supply.Value),
                        TotalVolume = c .total_volume.Value
                    };
                }).ToList();
            mapped.ForEach(c => db.Crypto.Add(c));
            db.SaveChanges();
        }

        public void GenerateWallets(int maxPerUser=2)
        {
            var usersList = db.UserStandard.ToList();
            var defaultUsdtQuantity = 200;
            usersList.ForEach(u => 
                    {
                        var rand = new Random();
                        var nWallets = rand.Next(0, maxPerUser);
                        var cryptosList = db.Crypto.ToList();
                        var nCryptos = cryptosList.Count;
                        string prevCryptoId = "";
                        string randomCryptoId = "";
                        var usdt = cryptosList.Find(c => c.Symbol == "usdt");

                        for(int i = 0; i < nWallets; i++) 
                        {
                            do
                            {
                                randomCryptoId = cryptosList.ElementAt(rand.Next(0, nCryptos)).Id;
                            } while(randomCryptoId == prevCryptoId && randomCryptoId == "tether");
                            var wallet = new Wallet
                            {
                                UserId = u.Id,
                                CryptoId = randomCryptoId,
                                Quantity = rand.NextDouble() / rand.NextDouble()
                            };
                            db.Wallet.Add(wallet);
                            prevCryptoId = randomCryptoId;
                        }
                        db.Wallet.Add(new Wallet
                        {
                            UserId = u.Id,
                            CryptoId = usdt.Id,
                            Quantity = defaultUsdtQuantity
                        });
                    });
            db.SaveChanges();
        }
    }
}
