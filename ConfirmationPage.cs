using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace FinalProject
{
    public class ConfirmationPage : BasePage
    {
        public void ConfirmBooking()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            
            driver.FindElement(By.Id("first_name")).SendKeys("Test");
            driver.FindElement(By.Id("last_name")).SendKeys("User");
            driver.FindElement(By.Id("address")).SendKeys("Test Address");

            driver.FindElement(By.Id("cc_num")).SendKeys("4111111111111111"); 
            driver.FindElement(By.Id("cc_type")).SendKeys("VISA");
            driver.FindElement(By.Id("cc_exp_month")).SendKeys("December");
            driver.FindElement(By.Id("cc_exp_year")).SendKeys("2025");
            driver.FindElement(By.Id("cc_cvv")).SendKeys("123");
            driver.FindElement(By.Id("book_now")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("order_no")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("logout")));
        }
    }
}
