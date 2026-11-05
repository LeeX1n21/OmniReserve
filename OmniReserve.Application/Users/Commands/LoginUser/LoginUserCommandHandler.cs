using MediatR;
using OmniReserve.Application.Common.Interfaces.Authentication;
using OmniReserve.Application.Common.Interfaces.Persistence;

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
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            throw new Exception("Credenciales inválidas");
        }

        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            throw new Exception("Credenciales inválidas");
        }

        var token = _jwtProvider.Generate(user);

        return token;
    }
}