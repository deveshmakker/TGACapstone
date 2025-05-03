using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payment.Application.DTO;
using Payment.Application.Interfaces;

namespace Payment.API.Controllers
{
    [ApiController]
    [Route("api/payment")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin, ADMIN, user, USER")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentservice;
        private readonly ResponseDTO _responseDTO;
        public PaymentController(IPaymentService paymentservice)
        {
            _paymentservice = paymentservice;
            _responseDTO = new ResponseDTO();
        }
        
        [HttpGet]
        [Route("{paymentid:int}")]
        public IActionResult Get(int paymentid)
        {
            try
            {
                _responseDTO.Result = _paymentservice.GetPaymentDetails(paymentid);
                _responseDTO.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpPut("ProcessPayment")]
        public IActionResult ProcessPayment([FromBody] PaymentDTO payment)
        {
            try
            {
                _responseDTO.Result = _paymentservice.ProcessPayment(payment);
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = ("Payment has been Processed");
            }
            catch (Exception ex)
            {
                _responseDTO.Result = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpPut("RefundPayment/{paymentid:int}")]
        public IActionResult RefundPayment(int paymentid)
        {
            try
            {
                _responseDTO.Result = _paymentservice.RefundPayment(paymentid);
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = ("Payment has been Refunded");
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
