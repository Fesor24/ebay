using Ebay.Domain.Abstractions;

namespace Ebay.Application.Abstractions.Data;

public interface ISearchService<TDocument>
    where TDocument : class
{
    /// <summary>
    /// Creates a new document and indexes it in Typesense
    /// </summary>
    /// <returns></returns>
    Task IndexAsync(TDocument document, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new document and indexes it in Typesense
    /// </summary>
    /// <returns></returns>
    Task IndexAsync(TDocument document, string collection, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<TDocument> Items, int Count, int PageNumber)> Search(string searchTerm, decimal minPrice, decimal maxPrice,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(TDocument document, string documentId, 
        CancellationToken cancellationToken = default);

    Task UpdateAsync(string collection, string documentId,
        TDocument document, CancellationToken cancellationToken = default);

    Task RemoveAsync(string documentId);
    Task RemoveAsync(string collection, string documentId);
}
