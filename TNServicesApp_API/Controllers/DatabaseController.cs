using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace TNServicesApp_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseController:Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DatabaseController> _logger;

        public DatabaseController(IConfiguration configuration, ILogger<DatabaseController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet("test-connection")]
        public async Task<IActionResult> GetConnectionStatus()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return StatusCode(500, new
                {
                    status = "Failed",
                    message = "Connection string is not configured."
                });
            }

            try
            {
                await using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                return Ok(new
                {
                    status = "Success",
                    message = "Database connection is successful.",
                    server = connection.DataSource,
                    database = connection.Database
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while testing the database connection.");
                return StatusCode(500, new
                {
                    status = "Failed",
                    message = "SQL Server connection failed.",
                    error = ex.Message
                });
            }
        }
    }
}
