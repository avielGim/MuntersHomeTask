using OpenQA.Selenium;
using MuntersHomeTask.Utility;
using MuntersHomeTask.Entities;

namespace MuntersHomeTask.PageObject
{
    public class LoginPage
    {
        // TODO: log

        private By _emailField = By.Id("signInName");
        private By _passwordField = By.Id("password");

        public Button SignInButton = new(By.Id("next"));

        public void Login(string email, string password)
        {
            ElementAction.SetText(_emailField, email);
            ElementAction.SetText(_passwordField, password);
            // log
        }
    }
}
