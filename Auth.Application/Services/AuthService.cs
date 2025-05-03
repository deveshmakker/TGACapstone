using Auth.Application.DTO;
using Auth.Application.Interfaces;
using Auth.Common.Model;
using Auth.Common.Repository;
using Microsoft.AspNetCore.Identity;

namespace Auth.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(IAuthRepository authRepository, UserManager<ApplicationUser> userManager , IJwtTokenGenerator jwtTokenGenerator, RoleManager<IdentityRole> roleManager)
        {
            _authRepository = authRepository;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _roleManager = roleManager;
        }
        public async Task<bool> AssignRole(string name, string roleName)
        {
            var user = _authRepository.GetUserByUserName(name);

            if (user != null)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                } 
                
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<TokenRequestDTO> GenerateJwtToken(UserDTO user)
        {
            var (token, jwtId) = await _jwtTokenGenerator.GenerateJwtToken(user, null);
            UpdateRefreshToken(user, token, jwtId);
            return token;
        }

        public async Task<TokenDTO> GetToken(TokenRequestDTO token)
        {
            var tokenfromRepository = _authRepository.GetRefreshTokens(token.RefreshToken);
            if (tokenfromRepository != null)
            {
                var user = _authRepository.GetUserById(tokenfromRepository.UserId);
                if (user != null)
                {
                    return new TokenDTO
                    {
                        RefreshToken = tokenfromRepository.Token,
                        Token = tokenfromRepository.Token,
                        UserName = user.UserName,
                        IsUsed = tokenfromRepository.IsUsed,
                        IsActive = !tokenfromRepository.IsRevoked,
                        IsRevoked = tokenfromRepository.IsRevoked,
                        JwtId = tokenfromRepository.JwtId
                    };
                }
            }
            return null;
        }

        public async Task<UserDTO> GetUserByName(string userName)
        {            
            var user = _authRepository.GetUserByUserName(userName);
            if (user != null)
            {
                return new UserDTO { ID = user.Id, Name = user.UserName };
            }
            return null;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO userDTO)
        {
            try
            {
                var user = _authRepository.GetUserByUserName(userDTO.UserName);
                if (user != null && await _userManager.CheckPasswordAsync(user, userDTO.Password))
                {
                    var role = await _userManager.GetRolesAsync(user);
                    var userDto = new UserDTO { ID = user.Id, Name = user.Name, Email = user.UserName };
                    var (token, jwtid) = await _jwtTokenGenerator.GenerateJwtToken(userDto, role);
                    await UpdateRefreshToken(userDto, token, jwtid);

                    return new LoginResponseDTO { Token = token, User = userDto };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new LoginResponseDTO
            {
                User = null, Token = null
            };
        }

        public async Task<UserDTO> Register(RegistrationRequestDTO requestDTO)
        {
            try
            {
                var user = new ApplicationUser { Name = requestDTO.Name, UserName = requestDTO.Email, PhoneNumber = requestDTO.PhoneNumber, Email = requestDTO.Email };
                var resp = await _userManager.CreateAsync(user, requestDTO.Password);
                if (resp.Succeeded)
                {
                    if (!string.IsNullOrEmpty(requestDTO.Role))
                    {
                        await AssignRole(requestDTO.Email, requestDTO.Role);
                    }
                    var userFromRepository = _authRepository.GetUserByEmail(user.Email);

                    return new UserDTO { ID = userFromRepository.Id, Name = userFromRepository.Name, Email = userFromRepository.Email };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public async Task<bool> UpdateRefreshToken(UserDTO user, TokenRequestDTO token, string jwtId)
        {
            if (_authRepository.DeleteRefreshTokens(user.ID))
            {
                _authRepository.AddRefreshTokens(new RefreshToken
                {
                    AddedDate = DateTime.UtcNow,
                    UserId = user.ID,
                    ExpiryDate = DateTime.UtcNow.AddDays(2),
                    IsUsed = false,
                    IsRevoked = false,
                    Token = token.RefreshToken,
                    JwtId = jwtId
                });
            }
            return true;
        }

        public async Task<bool> UpdateUserRefreshTokens(TokenDTO token)
        {
            var tokenFromRepository = _authRepository.GetRefreshTokens(token.RefreshToken);
            if (tokenFromRepository == null) return false;

            tokenFromRepository.JwtId = token.JwtId;
            tokenFromRepository.Token = token.Token;
            tokenFromRepository.IsRevoked = token.IsRevoked;
            tokenFromRepository.IsUsed = token.IsUsed;
            tokenFromRepository.AddedDate = DateTime.UtcNow;
            
            return _authRepository.UpdateUserRefreshTokens(tokenFromRepository); 
        }
    }
}
