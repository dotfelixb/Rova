using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Rova.Model;
using Rova.Model.Domain;

namespace Rova.Data.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserExtended>> ListUser(int offset = 0, int limit = 1000);
        Task<int> CreateUser(User user, DbAuditLog auditLog);

        Task<IEnumerable<RoleExtended>> ListRole(int offset = 0, int limit = 1000);
        Task<int> CreateRole(Role role, DbAuditLog auditLog);
    }

    public class UserRepository: BasePostgresRepository, IUserRepository
    {
        
        public UserRepository(
            IOptions<ConnectionStringOptions> options)
            : base(options)
        {
        }

        public Task<IEnumerable<UserExtended>> ListUser(int offset = 0, int limit = 1000)
        {
            return WithConnection(conn =>
            {
                const string query = @"SELECT id, username, displayname, email, enabled
	                                    , deleted, createdby, createdat, updatedby, updatedat 
                                        FROM public.user
                                        OFFSET @Offset LIMIT @Limit;";

                return conn.QueryAsync<UserExtended>(query, new
                {
                    Offset = offset,
                    Limit = limit
                });
            });
        }

        public Task<int> CreateUser(User user, DbAuditLog auditLog)
        {
            return WithConnection(async conn =>
            {
                var userCmd = @"INSERT INTO public.user (
                                  id, username, displayname, email
                                  , passwordhash, enabled, createdby, updatedby) 
                          VALUES(@Id, @Username, @DisplayName, @Email
                                , @PasswordHash, @Enabled, @CreatedBy, @UpdatedBy);
                          ";

                var createUser = await conn.ExecuteAsync(userCmd, new
                {
                    user.Id,
                    user.Username,
                    user.DisplayName,
                    user.Email,
                    user.PasswordHash,
                    user.Enabled,
                    user.CreatedBy,
                    user.UpdatedBy
                });

                var logCmd = @"INSERT INTO public.userauditlog (
                                id, targetid, actionname, objectname
                                , objectdata, createdby
                            )
                            VALUES(@Id, @TargetId, @ActionName, @ObjectName
                                , @ObjectData::jsonb, @CreatedBy
                            );";

                var createLog = await conn.ExecuteAsync(logCmd, new
                {
                    auditLog.Id,
                    auditLog.TargetId,
                    auditLog.ActionName,
                    auditLog.ObjectName,
                    auditLog.ObjectData,
                    auditLog.CreatedBy
                });

                return (createUser + createLog);
            });
        }
        
        public Task<IEnumerable<RoleExtended>> ListRole(int offset = 0, int limit = 1000)
        {
            return WithConnection(conn =>
            {
                const string query = @"SELECT id, rolename, enabled, deleted
                                        , createdby, createdat, updatedby, updatedat 
                                        FROM public.roles
                                        OFFSET @Offset LIMIT @Limit;";

                return conn.QueryAsync<RoleExtended>(query, new
                {
                    Offset = offset,
                    Limit = limit
                });
            });
        }
        
        public Task<int> CreateRole(Role role, DbAuditLog auditLog)
        {
            return WithConnection(async conn =>
            {
                var userCmd = @"INSERT INTO public.roles (
                                  id, rolename, enabled, createdby, updatedby) 
                              VALUES(@Id, @Rolename, @Enabled, @CreatedBy, @UpdatedBy);
                              ";

                var createRole = await conn.ExecuteAsync(userCmd, new
                {
                    role.Id,
                    role.Rolename,
                    role.Enabled,
                    role.CreatedBy,
                    role.UpdatedBy
                });

                var logCmd = @"INSERT INTO public.roleauditlog (
                                id, targetid, actionname, objectname
                                , objectdata, createdby
                            )
                            VALUES(@Id, @TargetId, @ActionName, @ObjectName
                                , @ObjectData::jsonb, @CreatedBy
                            );";

                var createLog = await conn.ExecuteAsync(logCmd, new
                {
                    auditLog.Id,
                    auditLog.TargetId,
                    auditLog.ActionName,
                    auditLog.ObjectName,
                    auditLog.ObjectData,
                    auditLog.CreatedBy
                });

                return (createRole + createLog);
            });
        }
    }
}