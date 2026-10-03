using Ebay.Domain.Abstractions;
using MediatR;

namespace Ebay.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
