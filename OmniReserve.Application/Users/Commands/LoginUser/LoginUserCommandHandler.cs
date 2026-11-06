using MediatR;
using OmniReserve.Application.Common.Interfaces;
using OmniReserve.Domain.ValueObjects;

namespace OmniReserve.Application.Users.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public LoginUserCommandHandler(
        IUserRepository userRepository, 
        IPasswordHasher passwordHasher, 
        IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var emailAddress = new EmailAddress(request.Email);

        var user = await _userRepository.GetByEmailAsync(emailAddress);

        if (user is null)
        {
            throw new Exception("Credenciales inválidas.");
        }

        bool isValidPassword = _passwordHasher.Verify(request.Password, user.PasswordHash);

        if (!isValidPassword)
        {
            throw new Exception("Credenciales inválidas.");
        }

        string token = _jwtProvider.Generate(user);

        return token;
    }
}