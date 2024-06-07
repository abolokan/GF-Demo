using MediatR;
using Shared;

namespace Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{

}
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}

public interface IBaseCommand
{

}
