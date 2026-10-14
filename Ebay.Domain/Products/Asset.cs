using Ebay.Domain.Abstractions;
using Ebay.Domain.Shared;

namespace Ebay.Domain.Products;

public sealed class Asset : AuditableEntity
{
    private Asset() { }

    public Asset(Guid id): base(id)
    {
        
    }
    public string FileUri { get; private set; }
    public FileType FileType { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }    

    public static Asset Create(Guid assetId, string fileUri, 
        FileType fileType, Guid productId, DateTime createdAtUtc, DateTime? updatedAtUtc)
    {
        Asset asset = new(assetId)
        {
            FileType = fileType,
            FileUri = fileUri,
            ProductId = productId
        };

        asset.SetAudits(createdAtUtc, updatedAtUtc);

        return asset;
    }
}
