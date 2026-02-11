namespace TechNewsOficialBackend.Application.Commands
{
    using MediatR;
    using TechNewsOficialBackend.Application.Dtos;

    public class RegisterUserCommand : IRequest<UserDto>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
