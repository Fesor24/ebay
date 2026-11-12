using Ebay.Application.Abstractions.Messaging;
using Ebay.Application.Abstractions.Security;
using Ebay.Application.Auth.Login;
using Ebay.Application.Exceptions;
using Ebay.Domain.Abstractions;
using Ebay.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Ebay.Application.Users.CreateUser;

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public CreateUserCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository,
        IPasswordHasher<User> passwordHasher, IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }
    public async Task<Result<LoginResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var usernameExist = (await _userRepository.GetByUsernameAsync(request.Username, cancellationToken)) != null;

        if (usernameExist)
            return Result.Failure<LoginResponse>(new Error("USERNAME.EXIST", "User with username exist"));

        var emailExist = (await _userRepository.GetByEmailAsync(request.Email, cancellationToken)) != null;

        if (emailExist)
            return Result.Failure<LoginResponse>(new Error("EMAIL.EXIST", "User with email exist"));

        Role? role;

        if (request.RoleId is not null)
            role = await _roleRepository.GetByIdAsync(request.RoleId.Value, cancellationToken);
        else
            role = await _roleRepository.GetByCodeAsync(DbRole.USER, cancellationToken);

        if (role is null)
            throw new NotFoundException("Role not found");

        Guid userId = Guid.NewGuid();

        User user = User.Create(
            userId,
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNo,
            request.Username,
            role.Id
            );

        string hashedPassword = _passwordHasher.HashPassword(user, request.Password);

        user.SetPassword(hashedPassword);

        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var (token, expiresAt) = _tokenService.GenerateToken(user);

        return new LoginResponse(token, expiresAt);
    }
}
