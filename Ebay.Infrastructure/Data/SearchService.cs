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
