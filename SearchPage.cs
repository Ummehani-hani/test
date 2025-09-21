using OpenQA.Selenium;

namespace FinalProject
{
    public class SearchPage : BasePage
    {
        public void SearchHotel()
        {
            driver.FindElement(By.Id("location")).SendKeys("Paris");
            driver.FindElement(By.Id("hotels")).SendKeys("Hotel Sunshine");
            driver.FindElement(By.Id("room_type")).SendKeys("Standard");
            driver.FindElement(By.Id("room_nos")).SendKeys("1 - One");

            driver.FindElement(By.Id("datepick_in")).Clear();
            driver.FindElement(By.Id("datepick_in")).SendKeys("30/08/2025");

            driver.FindElement(By.Id("datepick_out")).Clear();
            driver.FindElement(By.Id("datepick_out")).SendKeys("31/08/2025");

            driver.FindElement(By.Id("adult_room")).SendKeys("1 - One");
            driver.FindElement(By.Id("child_room")).SendKeys("0 - None");

            driver.FindElement(By.Id("Submit")).Click();
        }
    }
}
