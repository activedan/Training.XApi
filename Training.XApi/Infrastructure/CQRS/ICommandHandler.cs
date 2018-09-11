using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Training.XApi.Infrastructure.CQRS
{
    public interface ICommandHandlerAsync<TCommand>
    {
        IEnumerable<ValidationResult> Validate(TCommand command);

        Task HandleAsync(TCommand command);
    }
}
