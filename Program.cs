using Avaliacao3BimLp3.Database;
using Microsoft.Data.Sqlite;
using Avaliacao3BimLp3.Repositories;
using Avaliacao3BimLp3.Models;

var databaseConfig = new DatabaseConfig();
new DatabaseSetup(databaseConfig);

var productRepository = new ProductRepository(databaseConfig);


var modelName = args[0];
var modelAction = args[1];

if (modelName == "Product")
{

    if (modelAction == "New")
    {
        int id = Convert.ToInt32(args[2]);
        string name = args[3];
        double price = Convert.ToDouble(args[4]);
        bool active = Convert.ToBoolean(args[5]);

        if (productRepository.ExistsById(id))
        {
            Console.WriteLine($"Produto com Id {id} já existe");
        }
        else
        {
            var product = new Product(id, name, price, active);
            productRepository.Save(product);
            Console.WriteLine($"Produto {product.Name} cadastrado com sucesso");
        }
    }

    if (modelAction == "Delete")
    {
        int id = Convert.ToInt32(args[2]);

        if (productRepository.ExistsById(id))
        {
            productRepository.Delete(id);
            Console.WriteLine($"Produto {id} removido com sucesso");
        }
        else
            Console.WriteLine($"Produto {id} não encontrado");
    }

    if (modelAction == "Enable")
    {
        int id = Convert.ToInt32(args[2]);

        if (productRepository.ExistsById(id))
        {
            productRepository.Enable(id);
            Console.WriteLine($"Produto {id} habilitado com sucesso");
        }
        else
            Console.WriteLine($"Produto {id} não encontrado");
    }

    if (modelAction == "Disable")
    {
        int id = Convert.ToInt32(args[2]);

        if (productRepository.ExistsById(id))
        {
            productRepository.Disable(id);
            Console.WriteLine($"Produto {id} desabilitado com sucesso");
        }
        else
            Console.WriteLine($"Produto {id} não encontrado");
    }

    if (modelAction == "List")
    {
        if (productRepository.GetAll().Any())
        {
        foreach (var product in productRepository.GetAll())
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", product.Id, product.Name, product.Price, product.Active);
        }
        }
        else 
        {
            Console.WriteLine("Nenhum produto cadastrado");
        }
    }
}