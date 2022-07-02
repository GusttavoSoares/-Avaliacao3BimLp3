using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace Avaliacao3BimLp3.Repositories;

class ProductRepository
{
    private DatabaseConfig databaseConfig;

    public ProductRepository(DatabaseConfig databaseConfig) => this.databaseConfig = databaseConfig;

    public Product Save(Product product)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Products VALUES(@Id, @Name, @Price, @Active)", product);

        return product;
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Products WHERE id = @Id", new { Id = id });
    }

    public void Enable(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Products SET  active = @Active  WHERE id = @Id ", new { Id = id, Active = true });
    }

    public void Disable(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Products SET  active = @Active  WHERE id = @Id ", new { Id = id, Active = false });
    }

    public List<Product> GetAll()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var Products = connection.Query<Product>("SELECT * FROM Products");

        return Products.ToList();
    }

    public List<Product> GetAllWithPriceBetween(double initialPrice, double endPrice)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var products = connection.Query<Product>("SELECT  * FROM Products WHERE price BETWEEN @InitialPrice AND @EndPrice", new { InitialPrice = initialPrice, EndPrice = endPrice });

        return products.ToList();
    }

    public List<Product> GetAllWithPriceHigherThan(double price)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var products = connection.Query<Product>("SELECT * FROM Products WHERE price  > @Price", new { Price = price });

        return products.ToList();
    }

    public List<Product> GetAllWithPriceLowerThan(double price)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var products = connection.Query<Product>("SELECT * FROM Products WHERE price  < @Price", new { Price = price });

        return products.ToList();
    }

    public double GetAveragePrice()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var avg = connection.ExecuteScalar<Double>("SELECT  ROUND (AVG (price), 2)  FROM Products;");

        return avg;

    }


    public bool ExistsById(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var count = connection.ExecuteScalar<Boolean>("SELECT COUNT(Id) FROM Products  WHERE id=@Id", new { Id = id });

        return count;

    }
}
