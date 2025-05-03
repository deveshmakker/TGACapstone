using Auth.Application.DTO;
using Auth.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ResponseDTO _responseDTO;
        private readonly TokenValidationParameters _tokenValidationParams;

        public AuthController(IAuthService authService, TokenValidationParameters tokenValidationParameters)
        {
            _authService = authService;
            _tokenValidationParams = tokenValidationParameters;
            _responseDTO = new ResponseDTO();
        }

        [HttpPost("registeruser")]
        public async Task<IActionResult> RegisterUser([FromBody]RegistrationRequestDTO registrationRequestDTO)
        {
            try
            {
                _responseDTO.Result = await _authService.Register(registrationRequestDTO);
                _responseDTO.IsSuccess = true;
                _responseDTO.Message = "User Created Successfully";
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpPost("userlogin")]
        public async Task<IActionResult> UserLogin([FromBody] LoginRequestDTO loginRequestDTO)
        {
            try
            {
                _responseDTO.Result =await _authService.Login(loginRequestDTO);
                if (_responseDTO.Result != null)
                {
                    _responseDTO.IsSuccess = true;
                    _responseDTO.Message = "Login Successful";
                }
                else
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "Login Failed";                    
                }
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return Ok(_responseDTO);
        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromBody]TokenRequestDTO tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Validation 1 - Validation JWT token format
                var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParams, out var validatedToken);

                // Validation 2 - Validate encryption alg
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (result == false)
                    {
                        _responseDTO.IsSuccess = false;
                    }
                }

                // Validation 3 - validate expiry date
                var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

                if (expiryDate > DateTime.UtcNow)
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "Token has not yet expired";
                }

                // validation 4 - validate existence of the token
                var storedToken = await _authService.GetToken(tokenRequest);
                if (storedToken == null)
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "Token does not exist";                }

                // Validation 5 - validate if used
                if (storedToken.IsUsed)
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "Token has been used";
                }

                // Validation 6 - validate if revoked
                if (storedToken.IsRevoked)
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "Token has been revoked";
                }

                // Validation 7 - validate the id
                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                if (storedToken.JwtId != jti)
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "Token doesn't match";
                }

                // update current token 

                storedToken.IsUsed = true;
                _authService.UpdateUserRefreshTokens(storedToken);                

                // Generate a new token
                var dbUser = await _authService.GetUserByName(storedToken.UserName);
                var token = await _authService.GenerateJwtToken(dbUser);

                _responseDTO.IsSuccess = true;
                _responseDTO.Result = new AuthResult { Token = token.Token, RefreshToken = token.RefreshToken, Success = true };

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Lifetime validation failed. The token is expired."))
                {

                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "Token has expired please re-login";

                }
                else
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Message = "Token has expired please re-login";
                }
            }

            return Ok(_responseDTO);
        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();

            return dateTimeVal;
        }

    }
}
