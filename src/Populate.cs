using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using CoinManager.EF;
using CoinManager.ApiData;
using CoinManager.Models.RD;

namespace CoinManager.Util
{
    public class Populator
    {
        private HttpClient http;
        private CMDbContext db;

        public Populator()
        {
            http = new HttpClient();
            db = CMDbContext.Instance;
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
    }
}
