using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Domain.Commands
{
    public class ValidatePassword: IRequest<bool>
    {
        [Required]
        public string Password { get; set; }
    }
}
