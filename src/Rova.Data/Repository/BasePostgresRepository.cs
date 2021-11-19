using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Npgsql;
using Rova.Model;

namespace Rova.Data.Repository
{
    public abstract class BasePostgresRepository
    {
        private readonly ConnectionStringOptions _connectionStrings;

        public BasePostgresRepository(
            IOptions<ConnectionStringOptions> options)
        {
            _connectionStrings = options.Value;
        }

        private IDbConnection GetConnection()
        {
            var connectionString = _connectionStrings.Default
                ?? throw new ArgumentNullException(nameof(_connectionStrings.Default));

            return new NpgsqlConnection(connectionString);
        }

        protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
        {
            var connection = GetConnection();

            // Asynchronously execute getData, which has been passed in as a Func<IDBConnection, Task<T>>
            return await getData(connection).ConfigureAwait(false);
        }
    }
}

