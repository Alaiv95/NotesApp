using MediatR;
using Notes.Application.Interfaces;
using Serilog;

namespace Notes.Application.Common.Behaviors;

public class LoggingBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private ICurrentUserService _userService;

    public LoggingBehaviour(ICurrentUserService userService) => _userService = userService;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var reqName = typeof(TRequest).Name;
        var userId = _userService.UserId;

        Log.Information("Notes Request: {Name} {@UserId} {@Request}", reqName, userId, request);

        var response = await next();

        return response;
    }
}
