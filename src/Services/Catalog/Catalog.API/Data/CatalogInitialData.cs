using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();
        if (await session.Query<Product>().AnyAsync())
            return;
        session.Store<Product>(GetProductsList);
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetProductsList => new List<Product>()
    {
        new Product
        {
            Id = new Guid("97DAAE1E-C74A-4212-AF58-B727290346B8"),
            Name = "Nothing Phone 1",
            Categories = new List<string>()
            {
                "Mobile Phone",
            },
            Description = "The Nothing Phone 1 - NP1\n| 4G/5G | 12GB RAM | 256GB Storage | Snapdragon 778g | 6.7\" OLED Screen |",
            Price = 1550,
            ImageFile = "/Images/Nothing/P1.jpeg"
        },
        new Product
        {
            Id = new Guid("E1B4B20B-D29A-45BF-998D-59A59FB83E28"),
            Name = "Nothing Ear 1",
            Categories = new List<string>()
            {
                "Earphone",
            },
            Description = "The Nothing Ear 1 - NE1",
            Price = 370,
            ImageFile = "/Images/Nothing/E1.jpeg"
        },
    };
}
