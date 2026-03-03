using Sigillum.Arcavis.Core.Application.Abstraction.EfCore;
using Sigillum.Arcavis.Core.Application.CQRS;
using Sigillum.Arcavis.Core.Domain.Entities;

namespace Sigillum.Arcavis.Core.Application.Features.User.RegisterUser;

public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    #region Dependencies
    private readonly IWriteRepository<UserEntity> _userWriteRepository;
    private readonly IWriteRepository<UserEmailEntity> _userEmailWriteRepository;
    private readonly IWriteRepository<UserPasswordEntity> _userPasswordWriteRepository;
    private readonly IUnitOfWork _context;

    public RegisterUserCommandHandler(
        IWriteRepository<UserEntity> userWriteRepository,
        IWriteRepository<UserEmailEntity> userEmailWriteRepository,
        IWriteRepository<UserPasswordEntity> userPasswordWriteRepository,
        IUnitOfWork context)
    {
        _userWriteRepository = userWriteRepository;
        _userEmailWriteRepository = userEmailWriteRepository;
        _userPasswordWriteRepository = userPasswordWriteRepository;
        _context = context;
    }

    #endregion

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userId = Guid.NewGuid();

        var user = new UserEntity(userId);

        var email = new UserEmailEntity(Guid.NewGuid(), userId, request.Email);

        var password = new UserPasswordEntity(Guid.NewGuid(), userId, request.Password);

        await _userWriteRepository.AddAsync(user);
        await _userEmailWriteRepository.AddAsync(email);
        await _userPasswordWriteRepository.AddAsync(password);

        await _context.SaveChangesAsync(cancellationToken);

        return userId;

    }
}
