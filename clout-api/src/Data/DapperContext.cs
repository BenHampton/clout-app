using System.Data;
using Npgsql;

namespace clout_api.Data;

public class DapperContext
{
    private readonly string _connectionString;

    /// <summary>
    /// Unused for now. Leaving in place in case we need a quick fix for a tough query
    /// </summary>
    /// <param name="configuration"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public DapperContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("postgres");
        if (connectionString == null)
        {
            throw new InvalidOperationException("Connection string 'postgres' not found.");
        }
        _connectionString = connectionString;
    }
    public IDbConnection CreateConnection()
        => new NpgsqlConnection(_connectionString);
}
