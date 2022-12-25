using Npgsql;

namespace Discount.GRPC.Extensions
{
    public static class MigrationExtensions
    {
        public static IHost SeedDatabase<TContext>(this IHost host)
        {
            Console.WriteLine("SeedDatabase is started");
            using (IServiceScope scope = host.Services.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                try
                {
                    Console.WriteLine($"Connection Starting: {configuration.GetSection("DatabaseSettings:ConnectionString").Value}");

                    NpgsqlConnection connection = new NpgsqlConnection(configuration.GetSection("DatabaseSettings:ConnectionString").Value);
                    connection.Open();

                    Console.WriteLine($"Connection started");

                    var command = connection.CreateCommand();
                    command.CommandText = string.Format("CREATE EXTENSION IF NOT EXISTS postgres_fdw;");
                    command.ExecuteNonQuery();

                    Console.WriteLine($"EXTENSION created");


                    command.CommandText = string.Format("CREATE SERVER IF NOT EXISTS DiscountServer FOREIGN DATA WRAPPER postgres_fdw OPTIONS (host 'discountDb', dbname 'coupon', port '5432');");
                    command.ExecuteNonQuery();
                    Console.WriteLine($"SERVER created");

                    command.CommandText = string.Format("DROP TABLE IF EXISTS Coupon");

                    command.ExecuteNonQuery();

                    command.CommandText = string.Format(@"CREATE TABLE Coupon (
                                                            id SERIAL PRIMARY KEY,
                                                            ProductName VARCHAR(255) NOT NULL,
                                                            Description VARCHAR(255),
                                                            Amount decimal);");

                    command.ExecuteNonQuery();

                    Console.WriteLine($"TABLE created");

                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
                    command.ExecuteNonQuery();

                    Console.WriteLine($"2 items added");

                }
                catch (NpgsqlException ex)
                {
                    throw;
                }
            }
            Console.WriteLine("SeedDatabase is finished");
            return host;
        }
    }
}
