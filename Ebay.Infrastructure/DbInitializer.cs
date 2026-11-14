using Bogus;
using Ebay.Domain.Products;
using Typesense;
using TypesenseSchema = Typesense.Schema;

namespace Ebay.Infrastructure;

public class DbInitializer(ApplicationDbContext dbContext, ITypesenseClient typesenseClient)
{

    public async Task Init()
    {
        await InputData();
        await CreateCollection();
    }
    private async Task InputData()
    {
        List<Category> categories = AddCategories();

        await dbContext.Set<Category>().AddRangeAsync(categories);

        await dbContext.SaveChangesAsync();
    }

    private async Task CreateCollection()
    {
        // Create product schema
        var productSchema = new TypesenseSchema(
            "products",
            new List<Field>
            {
                new Field("id", FieldType.String, false),
                new Field("name", FieldType.String, false),
                new Field("description", FieldType.String, false),
                new Field("category", FieldType.String, true),
                new Field("price", FieldType.Int32, false)
            }, "price");

        await typesenseClient.CreateCollection(productSchema);
    }

    private static List<Category> AddCategories()
    {
        string[] items = ["Camera", "Phone", "Tablet", "Kitchen Item"];

        List<Category> categories = [];

        for(int i =0; i < items.Length; i++)
        {
            var categoryFaker = new Faker<Category>()
                .RuleFor(cat => cat.Id, f => Guid.NewGuid())
                .RuleFor(cat => cat.Name, f => items[i]);

            categories.Add(categoryFaker.Generate());
        }

        return categories;
    }
}
