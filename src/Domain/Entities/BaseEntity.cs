using FluentValidation.Results;
using System.Linq;

namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public ValidationResult ValidationResult { get; protected set; }
        public string[] ErrorMessages => ValidationResult?.Errors?.Select(e => e.ErrorMessage)?.ToArray();

        public abstract bool IsValid();
    }
}
