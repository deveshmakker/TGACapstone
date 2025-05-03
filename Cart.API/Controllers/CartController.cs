using Cart.Application.DTO;
using Cart.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cart.API.Controllers
{
    [ApiController]
    [Route("api/cart")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin, ADMIN, user, USER")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly ResponseDTO _responseDTO;
        public CartController(ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
            _responseDTO = new ResponseDTO();
        }       

        [HttpGet]
        [Route("{customerid:int}")]
        public async Task<IActionResult> Get(int customerid)
        {
            try
            {
                _responseDTO.Result = await _cartService.GetCartByCustomerId(customerid);
                _responseDTO.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpPost("UpsertCartDetails")]
        public IActionResult UpsertCartDetails([FromBody]CartObjectDTO cartObject)
        {
            try
            {
                _responseDTO.Result = _cartService.UpsertCartDetails(cartObject);
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = ("Cart has been updated");
            }
            catch(Exception ex)
            {
                _responseDTO.Result=false;
                _responseDTO.Message=ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpPost("RemoveCartContentDetails/{cartcontentdetailsid:int}")]
        public IActionResult RemoveCartContentDetails(int cartcontentdetailsid)
        {
            try
            {
                _responseDTO.Result = _cartService.RemoveCartContentDetails(cartcontentdetailsid);
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = ("Cart has been updated");
            }
            catch (Exception ex)
            {
                _responseDTO.Result = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpDelete("ClearCart/{customerid:int}")]
        public async Task<IActionResult> ClearCart(int customerid)
        {
            try
            {
                _responseDTO.Result = await _cartService.ClearCart(customerid);
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = ("Cart has been updated");
            }
            catch (Exception ex)
            {
                _responseDTO.Result = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

    }
}
