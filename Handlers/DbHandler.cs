using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace OrderInquiry.Handlers
{
    public class DbHandler : IDisposable
    {
        private SqlConnection _connection;

        public DbHandler()
        {
            var objBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

            var configuration = objBuilder.Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(connectionString);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public async Task<SqlDataReader> ExecuteReaderAsync(string query, CancellationToken cancellationToken, List<SqlParameter> parameters)
        {
            await _connection.OpenAsync(cancellationToken);
            var command = new SqlCommand(query, _connection);
            if ((parameters?.Count ?? 0) > 0)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }            
            return await command.ExecuteReaderAsync(cancellationToken);
        }
    }
}
