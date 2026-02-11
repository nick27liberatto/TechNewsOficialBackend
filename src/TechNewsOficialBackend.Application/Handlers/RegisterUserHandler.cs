namespace TechNewsOficialBackend.Application.Handlers
{
    using MediatR;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;
    using System.Threading.Tasks;
    using TechNewsOficialBackend.Application.Commands;
    using TechNewsOficialBackend.Application.Dtos;

    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserDto>
    {
        public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var response = new UserDto();


            return response;
        }
    }
}
