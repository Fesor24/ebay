using Ebay.Application.Abstractions.Messaging;
using Ebay.Application.Abstractions.Security;
using Ebay.Domain.Abstractions;
using Ebay.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Ebay.Application.Users.CreateUser;

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IUnitOfWork unitOfWork,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }
    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var usernameExist = await _userRepository.GetByUsernameAsync(request.Username) != null;

        if (usernameExist)
            return Result.Failure<string>(new Error("USERNAME.EXIST", "User with username exist"));

        var emailExist = await _userRepository.GetByEmailAsync(request.Email) != null;

        if (emailExist)
            return Result.Failure<string>(new Error("EMAIL.EXIST", "User with email exist"));

        Guid userId = Guid.NewGuid();

        User user = User.Create(
            userId,
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNo,
            request.Username
            );

        string hashedPassword = _passwordHasher.HashPassword(user, request.Password);

        user.SetPassword(hashedPassword);

        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var (token, expiresAt) = _tokenService.GenerateToken(user);

        return token;
    }
}
