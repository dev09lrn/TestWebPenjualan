using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TestWebPenjualan.Infrastructure.Persistance.Dapper;

public class TestWebPenjualanDbContext
{
    private ILogger<TestWebPenjualanDbContext> _logger;
    private readonly IConfiguration _configuration;

    public TestWebPenjualanDbContext(ILogger<TestWebPenjualanDbContext> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public SqlConnection OpenConnection()
    {
        try
        {
            var connectionString = _configuration.GetConnectionString("TestWebPenjualanConnection");
            var connection = new SqlConnection(connectionString);
            connection.Open();

            return connection;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed connect to database: {ex.Message}");
            throw new ArgumentException("Failed connect to database, please contact the administrator to check the error log.");
        }
    }

}
