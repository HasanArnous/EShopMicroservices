using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
    (ILogger<LoggingBehavior<TRequest,TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[Start] Handle Request: {request}, Response {response}",
            typeof(TRequest).Name, typeof(TResponse).Name);
        
        var timer = new Stopwatch();
        
        timer.Start();
        var response = await next();
        timer.Stop();

        // Check if the time taken to handle the request was more or equals to 3 seconds,
        // then we should flag this request with a Performance Flag..
        if(timer.Elapsed.Seconds >= 3)
        logger.LogInformation("[Performance] The Request {request} took {time} Seconds to be Handled.."
            ,typeof(TRequest).Name, timer.Elapsed.Seconds);

        logger.LogInformation("[End] Handled {request} with {response}",
            typeof(TRequest).Name, typeof(TResponse));
        return response;
    }
}
