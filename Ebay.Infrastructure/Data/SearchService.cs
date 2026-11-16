using System.Text;
using System.Text.Json;
using Ebay.Application.Abstractions.Data;
using Typesense;

namespace Ebay.Infrastructure.Data;

internal sealed class SearchService<TDocument> : ISearchService<TDocument>
    where TDocument: class
{
    private const string _collection = "products";
    private readonly ITypesenseClient _client;
    public SearchService(ITypesenseClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Creates a new document and indexes it in Typesense
    /// </summary>
    /// <returns></returns>
    public async Task IndexAsync(TDocument document, CancellationToken cancellationToken = default)
    {
        await _client.CreateDocument<TDocument>(_collection, document, cancellationToken);
    }

    /// <summary>
    /// Creates a new document and indexes it in Typesense
    /// </summary>
    /// <returns></returns>
    public async Task IndexAsync(TDocument document, string collection, CancellationToken cancellationToken = default)
    {
        await _client.CreateDocument<TDocument>(collection, document, cancellationToken);
    }

    public async Task<(IReadOnlyList<TDocument> Items, int Count, int PageNumber)> Search(string searchTerm, decimal minPrice, decimal maxPrice, 
        CancellationToken cancellationToken = default)
    {
        string? filterBy = default;

        if (minPrice > 0 && maxPrice > 0)
            filterBy = $"price:>{minPrice}&&price:<{maxPrice}";

        else if (minPrice > 0)
            filterBy = $"price:>{minPrice}";

        else if (maxPrice > 0)
            filterBy = $"price:<{maxPrice}";

        else filterBy = null;

        var query = new SearchParameters(searchTerm, "name")
        {
            SortBy = "price:asc",
            Page = 1,
            PerPage = 15,
            FilterBy = filterBy
        };

        var searchResult = await _client.Search<TDocument>(_collection, query, cancellationToken);

        return (
            searchResult.Hits.Select(h => h.Document).ToList(),
            searchResult.Found,
            searchResult.Page
            );
    }


    public async Task UpdateAsync(TDocument document, string documentId, CancellationToken cancellationToken = default)
    {
        string serializedDoc = JsonSerializer.Serialize(document);

        await _client.UpdateDocument<TDocument>(_collection, documentId, serializedDoc, cancellationToken);
    }

    public async Task UpdateAsync(string collection, string documentId, 
        TDocument document, CancellationToken cancellationToken = default)
    {
        string serializedDoc = JsonSerializer.Serialize(document);

        await _client.UpdateDocument<TDocument>(collection, documentId, serializedDoc, cancellationToken);
    }

    public async Task RemoveAsync(string documentId)
    {
        await _client.DeleteDocument<TDocument>(_collection, documentId);
    }

    public async Task RemoveAsync(string collection, string documentId)
    {
        await _client.DeleteDocument<TDocument>(collection, documentId);
    }
}
