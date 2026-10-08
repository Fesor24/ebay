using Ebay.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Ebay.Infrastructure;

internal sealed class ApplicationDbContext(DbContextOptions options) : DbContext(options), IUnitOfWork
{

}
