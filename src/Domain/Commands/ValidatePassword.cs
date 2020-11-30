using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Commands
{
    public class ValidatePassword: IRequest<bool>
    {
        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
