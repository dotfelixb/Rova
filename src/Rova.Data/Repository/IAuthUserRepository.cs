using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Rova.Model;
using Rova.Model.AuthProvider;

namespace Rova.Data.Repository
{
    public interface IAuthUserRepository : IUserStore<AuthUser>, IUserPasswordStore<AuthUser>
    {
    }

    public class AuthUserRepository : BasePostgresRepository, IAuthUserRepository
    {
        public AuthUserRepository(
            IOptions<ConnectionStringOptions> options)
            : base(options)
        {
        }

        public Task<IdentityResult> CreateAsync(AuthUser user, CancellationToken cancellationToken)
        {
            return WithConnection(async conn =>
            {
                var query = @"";

                var cmd = await conn.ExecuteAsync(query, new { });

                return cmd > 0 
                    ? IdentityResult.Success 
                    : IdentityResult.Failed(new IdentityError { Description = "User creation failed!"});
            });
        }

        public Task<IdentityResult> DeleteAsync(AuthUser user, CancellationToken cancellationToken)
        {
            return WithConnection(async conn =>
            {
                var query = @"";

                var cmd = await conn.ExecuteAsync(query, new { });

                return cmd > 0
                    ? IdentityResult.Success
                    : IdentityResult.Failed(new IdentityError { Description = "User update failed!" });
            });
        }

        public void Dispose()
        {
        }

        public Task<AuthUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return WithConnection(async conn =>
            {
                var query = @"";

                return await conn.QueryFirstOrDefaultAsync<AuthUser>(query, new { });
            });
        }

        public Task<AuthUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return WithConnection(async conn =>
            {
                var query = @"";

                return await conn.QueryFirstOrDefaultAsync<AuthUser>(query, new { });
            });
        }

        public Task<string> GetNormalizedUserNameAsync(AuthUser user, CancellationToken cancellationToken)
        {
            return WithConnection(async conn =>
            {
                var query = @"";

                return await conn.QueryFirstOrDefaultAsync<string>(query, new { });
            });
        }

        public Task<string> GetPasswordHashAsync(AuthUser user, CancellationToken cancellationToken)
        {
            return WithConnection(async conn =>
            {
                var query = @"";

                return await conn.QueryFirstOrDefaultAsync<string>(query, new { });
            });
        }

        public Task<string> GetUserIdAsync(AuthUser user, CancellationToken cancellationToken)
        {
            return WithConnection(async conn =>
            {
                var query = @"";

                return await conn.QueryFirstOrDefaultAsync<string>(query, new { });
            });
        }

        public Task<string> GetUserNameAsync(AuthUser user, CancellationToken cancellationToken)
        {
            return WithConnection(async conn =>
            {
                var query = @"";

                return await conn.QueryFirstOrDefaultAsync<string>(query, new { });
            });
        }

        public Task<bool> HasPasswordAsync(AuthUser user, CancellationToken cancellationToken)
        {
            return WithConnection(async conn =>
            {
                var query = @"";

                return await conn.QueryFirstOrDefaultAsync<bool>(query, new { });
            });
        }

        public Task SetNormalizedUserNameAsync(AuthUser user, string normalizedName, CancellationToken cancellationToken)
        {
            return WithConnection(async conn =>
            {
                var query = @"";

                return await conn.ExecuteAsync(query, new { });
            });
        }

        public Task SetPasswordHashAsync(AuthUser user, string passwordHash, CancellationToken cancellationToken)
        {
            return WithConnection(async conn =>
            {
                var query = @"";

                return await conn.ExecuteAsync(query, new { });
            });
        }

        public Task SetUserNameAsync(AuthUser user, string userName, CancellationToken cancellationToken)
        {
            return WithConnection(async conn =>
            {
                var query = @"";

                return await conn.ExecuteAsync(query, new { });
            });
        }

        public Task<IdentityResult> UpdateAsync(AuthUser user, CancellationToken cancellationToken)
        {
            return WithConnection(async conn =>
            {
                var query = @"";

                var cmd = await conn.ExecuteAsync(query, new { });

                return cmd > 0
                    ? IdentityResult.Success
                    : IdentityResult.Failed(new IdentityError { Description = "User update failed!" });
            });
        }
    }
}