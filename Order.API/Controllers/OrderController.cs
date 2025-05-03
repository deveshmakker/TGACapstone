using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Application.DTO;
using Order.Application.Interfaces;
using RabbitMqHandler.Interfaces;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("api/order")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin, ADMIN, user, USER")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMqUtilityService _mqUtilityService;
        private readonly ResponseDTO _responseDTO;
        private readonly IConfiguration _configuration;

        public OrderController(IOrderService orderService, IMqUtilityService mqUtilityService, IConfiguration configuration) 
        {
            _orderService = orderService;              
            _mqUtilityService = mqUtilityService;
            _configuration = configuration;
            _responseDTO = new ResponseDTO();
        }

        [HttpGet]
        [Route("{orderid:int}")]
        public IActionResult Get(int orderid)
        {
            try
            {
                _responseDTO.Result = _orderService.GetOrder(orderid);
                _responseDTO.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpPost("CreateOrder/{customerid:int}")]
        public async Task<IActionResult> CreateOrder(int customerid)
        {
            try
            {
                _responseDTO.Result = await _orderService.CreateOrder(customerid);
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = ("Order has been created");
                await _mqUtilityService.PublishMessageToQueue(_configuration.GetValue<string>("ApiSettings:OrderQueueName"), "An Order has been created");
            }
            catch (Exception ex)
            {
                _responseDTO.Result = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpPut("CancelOrder/{orderid:int}")]
        public IActionResult CancelOrder(int orderid)
        {
            try
            {
                _responseDTO.Result = _orderService.CancelOrder(orderid);
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = ("Order has been cancelled");
            }
            catch (Exception ex)
            {
                _responseDTO.Result = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpPut("UpdatePaymentDetails/{orderid:int}/{paymentid:int}")]
        public IActionResult UpdatePaymentDetails(int orderid, int paymentid)
        {
            try
            {
                _responseDTO.Result = _orderService.UpdatePaymentDetails(orderid, paymentid);
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = ("Payment has been Updated");
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
