using log4net;
using OpenQA.Selenium;
using MuntersHomeTask.Utility;
using MuntersHomeTask.Entities;
using MuntersHomeTask.JsonObject;

namespace MuntersHomeTask.PageObject
{
    public class LoginPage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LoginPage));

        private By _emailField = By.Id("signInName");
        private By _passwordField = By.Id("password");

        public Button SignInButton = new(By.Id("next"));

        public void Login(string email, string password)
        {
            ElementAction.SetText(_emailField, email);
            ElementAction.SetText(_passwordField, password);
            log.Info($"Set fields email: {email}, password: DO NOT SHOW");
        }

        public void Login(User user)
        {
            Login(user.Email, user.Password);
        }
    }
}
