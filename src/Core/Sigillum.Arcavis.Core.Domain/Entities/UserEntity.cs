using Sigillum.Arcavis.Core.Domain.Entities.Base;
using Sigillum.Arcavis.Core.Domain.Errors;
using Sigillum.Arcavis.Core.Domain.ExceptionHandling;

namespace Sigillum.Arcavis.Core.Domain.Entities;

public sealed class UserEntity : BaseEntity
{
    public bool IsActive { get; private set; }

    protected UserEntity() { }

    public UserEntity(Guid id) : base(id)
    {
        if (id == Guid.Empty)
            throw new DomainException(UserEntityErrors.InvalidUserId);

        IsActive = false;
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
    }

    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;
    }
}