using System.ComponentModel.DataAnnotations;

namespace Blog.API.Contracts
{
    public record SchemaLogin(
        [Required] string Email,
        [Required] string Password);
}
