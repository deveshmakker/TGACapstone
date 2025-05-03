
using Cart.Application.DTO;
using Cart.Application.Interfaces;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace Cart.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ProductApi");
                var response = await client.GetAsync("api/product");
                var apiResult = await response.Content.ReadAsStringAsync();
                var Result = JsonConvert.DeserializeObject<ResponseDTO>(apiResult);
                if(Result.IsSuccess)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(Convert.ToString(Result.Result));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new List<ProductDTO>();
        }
    }
}
