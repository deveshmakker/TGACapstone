

using Auth.Common.Model;

namespace Auth.Common.Repository
{
    public interface IAuthRepository
    {
        ApplicationUser GetUserByEmail(string email);
        ApplicationUser GetUserByUserName(string userName);
        bool IsValidUser(ApplicationUser users);
        RefreshToken AddRefreshTokens(RefreshToken token);
        bool UpdateUserRefreshTokens(RefreshToken token);
        RefreshToken GetRefreshTokens(string token);
        bool DeleteRefreshTokens(string userId);
        ApplicationUser GetUserById(string userId);
    }
}
