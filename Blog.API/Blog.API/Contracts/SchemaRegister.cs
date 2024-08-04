using System.ComponentModel.DataAnnotations;

namespace Blog.API.Contracts
{
    public record SchemaRegister(
        [Required] string Email,
        [Required] string Password,
        [Required] string Nickname,
        [Required] string PhoneNumber);
}
