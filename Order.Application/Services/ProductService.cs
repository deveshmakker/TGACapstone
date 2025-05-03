using Newtonsoft.Json;
using Order.Application.DTO;
using Order.Application.Interfaces;
using System.Net.Http;

namespace Order.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> UpdateInventory(int productId, int quantity)
        {
            {
                try
                {
                    var client = _httpClientFactory.CreateClient("ProductApi");
                    var response = await client.PutAsync($"api/product/updateinventory/{productId.ToString()}/{(-1*quantity).ToString()}",null);
                    var apiResult = await response.Content.ReadAsStringAsync();
                    var Result = JsonConvert.DeserializeObject<ResponseDTO>(apiResult);
                    if (Result.IsSuccess)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return false;
            }
        }

    }
}
