namespace Supportly.Presentation.Models.DTO.Register
{
    public class RegisterRequest
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
        public string? PasswordConfirmation { get; set; }

    }
}