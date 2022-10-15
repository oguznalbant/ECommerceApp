using Npgsql;

namespace Discount.API.Extensions
{
    public static class MigrationExtensions
    {
        public static IHost SeedDatabase<TContext>(this IHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                try
                {
                    NpgsqlConnection connection = new NpgsqlConnection(configuration.GetSection("DatabaseSettings:ConnectionString").Value);
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = string.Format("DROP TABLE IF EXISTS Coupon");

                    command.ExecuteNonQuery();

                    command.CommandText = string.Format(@"CREATE TABLE Coupon (
                                                            id SERIAL PRIMARY KEY,
                                                            ProductName VARCHAR(255) NOT NULL,
                                                            Description VARCHAR(255),
                                                            Amount decimal);");

                    command.ExecuteNonQuery();
                }
                catch (NpgsqlException ex)
                {
                    throw ex;
                }
            }

            return host;
        }
    }
}
