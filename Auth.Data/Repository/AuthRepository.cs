
using Auth.Common.Model;
using Auth.Common.Repository;
using Auth.Data.Context;

namespace Auth.Data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthDbContext _authDbContext;
        public AuthRepository(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        public RefreshToken AddRefreshTokens(RefreshToken token)
        {
            _authDbContext.refreshTokens.Add(token);
            _authDbContext.SaveChanges();
            return token;
        }

        public bool DeleteRefreshTokens(string userId)
        {
            var tokensFromDb = _authDbContext.refreshTokens.Where(t => t.UserId == userId).ToList();
            if (tokensFromDb.Any())
            {
                _authDbContext.refreshTokens.RemoveRange(tokensFromDb);                
            }
            return true;
        }

        public RefreshToken GetRefreshTokens(string token)
        {
            return _authDbContext.refreshTokens.FirstOrDefault(t => t.Token == token);
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            return _authDbContext.applicationUsers.FirstOrDefault(a => a.Email == email);
        }

        public ApplicationUser GetUserById(string userId)
        {
            return _authDbContext.applicationUsers.FirstOrDefault(a => a.Id == userId);
        }

        public ApplicationUser GetUserByUserName(string userName)
        {
            return _authDbContext.applicationUsers.FirstOrDefault(a => a.UserName  == userName);
        }

        public bool IsValidUser(ApplicationUser users)
        {
            return _authDbContext.applicationUsers.Any(a => a.UserName == users.UserName && a.Id == users.Id);
        }

        public bool UpdateUserRefreshTokens(RefreshToken token)
        {
            var tokenFromDb = _authDbContext.refreshTokens.Find(token.TokenId);

            if (tokenFromDb != null)
            {
                _authDbContext.Entry(tokenFromDb).CurrentValues.SetValues(token);
            }

            return (_authDbContext.SaveChanges() != 0);
        }
    }
}
