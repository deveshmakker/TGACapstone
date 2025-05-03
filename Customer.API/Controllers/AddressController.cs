using Customer.Application.DTO;
using Customer.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Customer.API.Controllers
{
    [ApiController]
    [Route("api/address")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin, ADMIN, user, USER")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly ResponseDTO _responseDTO;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
            _responseDTO = new ResponseDTO();
        }

        [HttpGet]
        [Route("{customerid:int}/{addressid:int}")]
        public IActionResult Get(int customerid, [FromBody] int addressid = 0)
        {
            try
            {
                var address = _addressService.FindAddressByCustomerId(customerid);
                _responseDTO.Result = address;
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
                var address = _addressService.FindAddressByCustomerId(customerid);
                _responseDTO.Result = address;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpPost("addaddress")]
        public IActionResult AddAddress([FromBody] AddressDTO address)
        {
            try
            {
                var addressResponse = _addressService.AddAddress(address);
                _responseDTO.Result = addressResponse;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpPut("editaddress/{id:int}")]
        public IActionResult EditAddress(int addressid, [FromBody] AddressDTO address)
        {
            try
            {
                var addressResponse = _addressService.EditAddress(addressid, address);
                _responseDTO.Result = addressResponse;
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }


        [HttpDelete("deleteaddress/{addressid:int}")]
        public IActionResult DeleteAddress(int addressid)
        {
            try
            {
                _responseDTO.Result = _addressService.DeleteAddress(addressid);
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = "Address Deleted Successfully";
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
