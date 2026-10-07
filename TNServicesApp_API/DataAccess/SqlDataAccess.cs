using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json;
using TNServicesApp_API.Models;

namespace TNServicesApp_API.DataAccess
{
    public class SqlDataAccess : IDataAccess
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SqlDataAccess> _logger;

        public SqlDataAccess(
            IConfiguration configuration,
            ILogger<SqlDataAccess> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<DataAccessResponse> ExecuteStoredProcedureAsync(
            string storedProcedureName,
            QueryInputRequest parameters,
            CancellationToken cancellationToken = default)
        {
            var connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Database connection string is not configured.");
            }

            try
            {
                await using var connection =
                    new SqlConnection(connectionString);

                await using var command =
                    new SqlCommand(storedProcedureName, connection);

                command.CommandType = CommandType.StoredProcedure;

                // Recommended command timeout
                command.CommandTimeout = 30;

                // QueryJSON
                var queryJson = parameters.QueryJSON == null
                    ? null
                    : JsonSerializer.Serialize(parameters.QueryJSON);
                _logger.LogInformation("QueryJSON: {QueryJSON}", queryJson);

                command.Parameters.Add(
                    new SqlParameter("@QueryJSON", SqlDbType.NVarChar, -1)
                    {
                        Value = queryJson ?? (object)DBNull.Value
                    });

                // QueryType
                command.Parameters.Add(
                    new SqlParameter("@QueryType", SqlDbType.NVarChar, 100)
                    {
                        Value = parameters.QueryType ?? (object)DBNull.Value
                    });

                // OutJSON
                var outJsonParameter = new SqlParameter(
                    "@OutJSON",
                    SqlDbType.NVarChar,
                    -1)
                {
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(outJsonParameter);

                // OutStatus
                var outStatusParameter = new SqlParameter(
                    "@OutStatus",
                    SqlDbType.NVarChar,
                    20)
                {
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(outStatusParameter);

                await connection.OpenAsync(cancellationToken);

                //_logger.LogInformation("SQL Connection - Server: {Server}, Database: {Database}",connection.DataSource,connection.Database);

                await command.ExecuteNonQueryAsync(cancellationToken);

                var outJson = outJsonParameter.Value == DBNull.Value
                        ? null
                        : outJsonParameter.Value?.ToString();

                return new DataAccessResponse
                {
                    OutJSON = string.IsNullOrWhiteSpace(outJson)
                        ? null
                        : JsonSerializer.Deserialize<JsonElement>(outJson),

                    OutStatus = outStatusParameter.Value == DBNull.Value
                        ? null
                        : outStatusParameter.Value?.ToString()
                };
            }
            catch (SqlException ex)
            {
                _logger.LogError(
                    ex,
                    "SQL error while executing stored procedure {StoredProcedure}",
                    storedProcedureName);
                throw;
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning(
                    "Database operation was cancelled for stored procedure {StoredProcedure}",
                    storedProcedureName);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Unexpected error while executing stored procedure {StoredProcedure}",
                    storedProcedureName);
                throw;
            }
        }
    }
}
