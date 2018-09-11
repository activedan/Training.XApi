using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Training.XApi.Infrastructure.CQRS
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _container;

        public CommandDispatcher(IServiceProvider container)
        {
            _container = container;
        }

        public async Task<IEnumerable<ValidationResult>> DispatchAsync<TCommand>(TCommand command)
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
    }
}