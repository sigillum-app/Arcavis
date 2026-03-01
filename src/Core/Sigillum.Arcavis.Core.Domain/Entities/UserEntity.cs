using Sigillum.Arcavis.Core.Domain.Entities.Base;
using Sigillum.Arcavis.Core.Domain.Errors;
using Sigillum.Arcavis.Core.Domain.Exceptions;

namespace Sigillum.Arcavis.Core.Domain.Entities;

public sealed class UserEntity : BaseEntity
{
    public bool IsActive { get; private set; }

    protected UserEntity() { }

    private UserEntity(Guid id) : base(id)
    {
        IsActive = true;
    }

    public static UserEntity Create(Guid id)
    {
        if (id == Guid.Empty)
            throw new DomainException(DomainError.InvalidUserId);

        return new UserEntity(id);
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }
}