
using Auth.Application.DTO;

namespace Auth.Application.Interfaces
{
    public interface IAuthService
    {
        Task<UserDTO> Register(RegistrationRequestDTO userDTO);
        Task<LoginResponseDTO> Login(LoginRequestDTO userDTO);
        Task<bool> AssignRole(string name, string roleName);
        Task<TokenDTO> GetToken(TokenRequestDTO token);
        Task<bool> UpdateUserRefreshTokens(TokenDTO updatedToken);
        Task<TokenRequestDTO> GenerateJwtToken(UserDTO user);
        Task<UserDTO> GetUserByName(string userName);
        Task<bool> UpdateRefreshToken(UserDTO user, TokenRequestDTO token, string jwtId);
    }
}
