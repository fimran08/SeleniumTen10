using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace seleniumTen10.Hooks
{
    [Binding]
    public static class DriverHelpFactory
    {
        private static WebDriver _driver;

        public static WebDriver BuildDriver()
        {
            _driver =new ChromeDriver();
            return _driver;
        }

        public static WebElement WaitUntilVisible(this WebDriver driver, By itemSpecifier, TimeSpan waitTimespan)
        {
            var wait = new WebDriverWait(driver, waitTimespan);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

            try
            {
                return (WebElement) WaitForElement(driver, itemSpecifier, wait);
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"WaitForElement timed out loading page: '{driver.Url}'");
                throw;
            }
        }

        private static WebElement WaitForElement(WebDriver driver, By itemSpecifier, WebDriverWait wait)
        {
            var element = wait.Until(_ =>
            {
                var elementToBeDisplayed = driver.FindElement(itemSpecifier);

                return elementToBeDisplayed.Displayed
                    ? elementToBeDisplayed
                    : null;
            });

            return (WebElement) element;
        }

        [AfterFeature()]

        public static void AfterTestRun()
        {
            _driver.Close();
            _driver.Dispose();
           
        }
    }
}