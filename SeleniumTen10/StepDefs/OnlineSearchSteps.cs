using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using seleniumTen10.Hooks;
using Shouldly;
using TechTalk.SpecFlow;

namespace SeleniumTen10.StepDefs
{
    [Binding]
    public class OnlineSearchSteps
    {
        private const int TimeoutInSeconds = 90;
        private WebDriver _driver;
        private String _selectedLang ;

        public OnlineSearchSteps()
        {
            _driver = DriverHelpFactory.BuildDriver();
        }

        [Given(@"user naviogates to url '(.*)'")]
        public void GivenUserNaviogatesToUrl(string url)
        {
            _driver.Url = url;
        }
        
        [When(@"user enters '(.*)' in serach field")]
        public void WhenUserEntersInSerachField(string searchText)
        {
            var searchElementField = _driver.WaitUntilVisible(By.Id("searchInput"), TimeSpan.FromSeconds(TimeoutInSeconds));
            searchElementField.SendKeys(searchText);
        }

        [When(@"selected the '(.*)' language from language drop down")]
        public void WhenSelectedTheLanguageFromLanguageDropDown(string Lang)
        {
            var searchElementField = _driver.FindElement(By.Id("searchLanguage"));
            var selectLang = new SelectElement(searchElementField);
            selectLang.SelectByText(Lang);
            WebElement option = (WebElement)selectLang.SelectedOption;
            _selectedLang = option.GetAttribute("Value");

        }

        [When(@"click on the search button")]
        public void WhenClickOnTheSearchButton()
        {
            var search = _driver.FindElement(By.CssSelector("button[type='submit'"));
                search.Click();
        }


        [Then(@"search result page will be displayed")]
        public void ThenSearchResultPageWillBeDisplayed()
        {
            var searchresult = _driver.WaitUntilVisible(By.XPath("//*[@id=\"firstHeading\"]"), TimeSpan.FromSeconds(TimeoutInSeconds));
            searchresult.ShouldNotBeNull();

            var pageLang = _driver.FindElement(By.XPath("/html"));
            string lang = pageLang.GetAttribute("lang");
            lang.ShouldBe(_selectedLang);

        }
    }
}
