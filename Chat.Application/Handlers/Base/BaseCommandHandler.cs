using AutoMapper;
using Chat.Domain.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Serilog;
using System.Data.SqlClient;

namespace Chat.Application.Handlers.Base
{
    public abstract class BaseCommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
       where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        protected readonly ILogger _logger;
        protected readonly IMapper _mapper;
        protected bool LogRequest = false;
        protected bool LogResponse = false;
        protected BaseCommandHandler(IEnumerable<IValidator<TRequest>> validators, ILogger logger, IMapper mapper)
        {
            _validators = validators;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if (LogRequest)
                _logger.Information("Start process with request: {request}", request);

            if (_validators.Any())
            {
                List<ValidationFailure> failures = await GetFailures(request, cancellationToken);

                if (failures.Any())
                {
                    _logger.Information($"Command handler {request.GetType().Name} validation found: {failures.Count}");
                    throw new ValidationException(failures);
                }
            }

            NormalizeData(request);

            try
            {
                _logger.Information($"Command handler error. {request.GetType().Name} ");

                var response = await Process(request, cancellationToken);

                if (LogResponse)
                    _logger.Information("Finished process with response: {response}", response);

                return response;
            }
            catch (Exception ex)
            {

                if (ex is SqlException)
                {
                    _logger.Error($"Database error. {request.GetType().Name} ", ex);
                }
                else
                {
                    _logger.Error($"Command handler error. {request.GetType().Name} ", ex);
                }

                throw new HandlerException(ex);
            }
        }

        private async Task<List<ValidationFailure>> GetFailures(TRequest request, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
            return failures;
        }

        protected abstract Task<TResponse> Process(TRequest request, CancellationToken cancellationToken);

        protected virtual void NormalizeData(TRequest request) { }
    }
}
