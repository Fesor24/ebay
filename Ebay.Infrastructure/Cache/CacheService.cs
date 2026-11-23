using Ebay.Application.Abstractions.Cache;

namespace Ebay.Infrastructure.Cache;

internal sealed class CacheService : ICacheService
{
    public Task Add(string key, string value, TimeSpan? expiration = null)
    {
        throw new NotImplementedException();
    }

    public Task Add<T>(string key, T value, TimeSpan? expiration = null)
    {
        throw new NotImplementedException();
    }

    public Task<T?> GetItem<T>(string key)
    {
        throw new NotImplementedException();
    }

    public Task RemoveItem(string key)
    {
        throw new NotImplementedException();
    }
}
