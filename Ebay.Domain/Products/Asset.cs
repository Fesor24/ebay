using Ebay.Domain.Abstractions;
using Ebay.Domain.Shared;

namespace Ebay.Domain.Products;

public sealed class Asset : AuditableEntity
{
    public Guid Id { get; private set; }
    public string FileUri { get; private set; }
    public FileType FileType { get; private set; }

    public static Asset Create(string fileUri, FileType fileType, DateTime createdAtUtc, DateTime? updatedAtUtc)
    {
        Asset asset = new()
        {
            FileType = fileType,
            FileUri = fileUri,
            Id = Guid.NewGuid()
        };

        asset.SetAudits(createdAtUtc, updatedAtUtc);

        return asset;
    }
}
