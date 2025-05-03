using Newtonsoft.Json;
using Order.Application.DTO;
using Order.Application.Interfaces;
using System.Net.Http;


namespace Order.Application.Services
{
    public class CartService : ICartService
    {

        private readonly IHttpClientFactory _httpClientFactory;
        public CartService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CartObjectDTO> GetCartForCustomer(int customerId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("CartApi");
                var response = await client.GetAsync(string.Concat("api/cart/",customerId.ToString()));
                var apiResult = await response.Content.ReadAsStringAsync();
                var Result = JsonConvert.DeserializeObject<ResponseDTO>(apiResult);
                if (Result.IsSuccess)
                {
                    return JsonConvert.DeserializeObject<CartObjectDTO>(Convert.ToString(Result.Result));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new CartObjectDTO();            
        }

        public async Task<bool> ClearCart(int customerid)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("CartApi");
                var response = await client.DeleteAsync(string.Concat("api/cart/ClearCart/", customerid.ToString()));
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
