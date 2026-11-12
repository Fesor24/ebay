using Ebay.Application.Abstractions.Messaging;
using Ebay.Application.Abstractions.Security;
using Ebay.Domain.Abstractions;
using Ebay.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Ebay.Application.Auth.Login;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponse>
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public LoginCommandHandler(ITokenService tokenService, IUserRepository userRepository, 
        IPasswordHasher<User> passwordHasher)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username, cancellationToken) ??
            throw new UnauthorizedAccessException("Incorrect login credentials");

        PasswordVerificationResult res = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

        if(res == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException("Incorrect login credentials");

        var (token, expiresAt) = _tokenService.GenerateToken(user);

        return new LoginResponse(token, expiresAt);
    }
}
