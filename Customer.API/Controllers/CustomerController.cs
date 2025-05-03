using Customer.Application.DTO;
using Customer.Application.Interfaces;
using Customer.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Customer.API.Controllers
{
    [ApiController]
    [Route("api/customer")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin, ADMIN, user, USER")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ResponseDTO _responseDTO;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
            _responseDTO = new ResponseDTO();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var customers = _customerService.GetCustomers();
                _responseDTO.Result = customers;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpGet]
        [Route("{customerid:int}")]
        public IActionResult Get(int customerid)
        {
            try
            {
                var customer = _customerService.FindCustomerById(customerid);
                _responseDTO.Result = customer;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpGet]
        [Route("{keyword}")]
        public IActionResult Get(string keyword)
        {
            try
            {
                var customer = _customerService.FindCustomerByKeyword(keyword);
                _responseDTO.Result = customer;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpPost("addcustomer")]
        public IActionResult AddCustomer([FromBody] CustomerDTO customer)
        {
            try
            {
                var customerResponse = _customerService.AddCustomer(customer);
                _responseDTO.Result = customerResponse;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpPut("editcustomer/{id:int}")]
        public IActionResult EditCustomer(int customerid, [FromBody] CustomerDTO customer)
        {
            try
            {
                var customerResponse = _customerService.EditCustomer(customerid, customer);
                _responseDTO.Result = customerResponse;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpPut("disablecustomer/{id:int}")]
        public IActionResult DisableCustomer(int customerid, [FromBody] CustomerDTO customer)
        {
            try
            {
                var customerResponse = _customerService.DisableCustomer(customerid, customer);
                _responseDTO.Result = customerResponse;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpDelete("deletecustomer/{customerid:int}")]
        public IActionResult DeleteCustomer(int customerid)
        {
            try
            {
                _responseDTO.Result = _customerService.DeleteCustomer(customerid);
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = "Customer Deleted Successfully";
            }

            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }
    }
}
