
using Auth.Application.DTO;

namespace Auth.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Task<(TokenRequestDTO, string JwtId)> GenerateJwtToken(UserDTO user, IEnumerable<string> roles);
    }
}
