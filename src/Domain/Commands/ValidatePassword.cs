using MediatR;

namespace Domain.Commands
{
    public class ValidatePassword: IRequest<bool>
    {
        public string Password { get; set; }
    }
}
