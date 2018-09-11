using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Yokozuna.Logging.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Training.XApi.Infrastructure.CQRS
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _container;

        public CommandDispatcher(IServiceProvider container, ILogger<CommandDispatcher> logger)
        {
            _container = container;
            _logger = logger;
        }

        public async Task<IEnumerable<ValidationResult>> DispatchAsync<TCommand>(TCommand command)
        {
            try
            {
                var handler = _container.GetService<ICommandHandlerAsync<TCommand>>();

                if (handler == null)
                {
                    throw new Exception("Unable to find matching async command handler");
                }
                else
                {
                    var validationResults = handler.Validate(command);

                    if (!validationResults.Any())
                    {
                        await handler.HandleAsync(command);
                    }

                    return validationResults;
                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception, new LogTags().Add(command), "Unable to handle async command");
                throw;
            }
        }
    }
}