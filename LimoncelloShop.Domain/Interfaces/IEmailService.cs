namespace LimoncelloShop.Domain.Interfaces
{
    public interface IEmailService
    {
        Task SendValidationEmailAsync(string email, string validationLink);
    }
}
