namespace DevFreela.Application.Models
{
    public class LoginInputModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public LoginInputModel(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }

    public class LoginViewModel
    {
        public string Token { get; set; }

        public LoginViewModel(string token)
        {
            Token = token;
        }
    }
}
