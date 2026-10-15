using Bogus;
using Ebay.Domain.Products;

namespace Ebay.Infrastructure;

public class RandomDataDbSeed(ApplicationDbContext dbContext)
{
    public async Task InputData()
    {
        List<Category> categories = AddCategories();

        await dbContext.Set<Category>().AddRangeAsync(categories);

        await dbContext.SaveChangesAsync();
    }

    private static List<Category> AddCategories()
    {
        string[] categories = ["Camera", "Phone", "Tablet", "Kitchen Item"];

        var categoryFaker = new Faker<Category>()
            .RuleFor(cat => cat.Id, f => Guid.NewGuid())
            .RuleFor(cat => cat.Name, f => f.PickRandomParam<string>(categories));

        return categoryFaker.Generate(4);
    }
}
