using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TEKApp.Models;

namespace TEKApp.Services
{
    public class OrderLoaderService : ILoaderService<Order>
    {
        private readonly IOrderService _orderService;

        public OrderLoaderService(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public void Load(string input)
        {
            using (StreamReader streamReader = new StreamReader(input))
            {
                string inputJson = streamReader.ReadToEnd();
                var items = JsonConvert.DeserializeObject<Dictionary<string, Airport>>(inputJson);
                foreach (var kvp in items)
                {
                    _orderService.SaveOrder(kvp.Key, kvp.Value.Destination);
                }
            }
        }
    }
}
