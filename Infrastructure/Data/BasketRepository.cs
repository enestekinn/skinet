using System;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {

        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            // we get connection here from our  database
_database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            // json comes from our client  and we going to seriliaze that into the string which is stored in our redis database as a string 
            // later we deseriliaze it into our basket 
                var data = await _database.StringGetAsync(basketId);
                // if we have data we Deserialize into the basket  if not we return empty string 
                return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            // if we are updating our baasket , what we are going to do is simply replace the existing basket in our redis db
            // TimeSpan.FromDays(30) urunu sepette 30 gun tutuyoruz.
var created = await _database.StringSetAsync(basket.Id,JsonSerializer.Serialize(basket),
TimeSpan.FromDays(30));
if(!created) return null;

return await GetBasketAsync(basket.Id);
        }
    }
}