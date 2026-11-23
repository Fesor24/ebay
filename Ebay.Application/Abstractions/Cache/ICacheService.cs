namespace Ebay.Application.Abstractions.Cache;

public interface ICacheService
{
    Task Add(string key, string value, TimeSpan? expiration = null);
    Task Add<T>(string key, T value, TimeSpan? expiration = null);
    Task<T?> GetItem<T>(string key);
    Task RemoveItem(string key);
}
