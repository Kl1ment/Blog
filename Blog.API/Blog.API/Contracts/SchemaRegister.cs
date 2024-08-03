namespace Blog.API.Contracts
{
    public record SchemaRegister(
        string Email,
        string Password,
        string Nickname,
        string PhoneNumber
        );
}
