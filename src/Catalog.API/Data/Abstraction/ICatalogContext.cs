using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data.Abstraction
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
