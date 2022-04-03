﻿using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BA.Core.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation($"Command '{typeof(TRequest).Name}': {JsonConvert.SerializeObject(request)}");

        var response = await next();

        return response;
    }
}