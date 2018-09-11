using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Training.XApi.Infrastructure.CQRS
{
    public interface ICommandDispatcher
    {
        Task<IEnumerable<ValidationResult>> DispatchAsync<TCommand>(TCommand command);
    }
}
