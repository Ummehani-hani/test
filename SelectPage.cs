using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace FinalProject
{
    public class SelectPage : BasePage
    {
        public void SelectHotel()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("radiobutton_0")));
        
            driver.FindElement(By.Id("radiobutton_0")).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("continue")));
         
            driver.FindElement(By.Id("continue")).Click();

           
            wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//td[contains(text(),'Book A Hotel')]")));
        }
    }
}
