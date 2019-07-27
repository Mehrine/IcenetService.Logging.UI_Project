using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    class SeleniumTesting
    {
        IWebDriver driver = new ChromeDriver();

        [Test]
        public void WhenINavigateToTheLogViewerPage_IShouldSeeThatTheTelerikGrid_Has5LogItems()
        {
            var driver = new ChromeDriver(); //opens the ChromeDriver
            driver.Url = "http://localhost/LogViewer/Home/Index";
            
            var table = driver.FindElementById("MyGrid");

            var rows = table.FindElements(By.CssSelector("tr")); //finds the rows
            Assert.That(rows.Count, Is.EqualTo(6));
        }

        [Test]
        public void TheLogCategoryShouldSelectACategory_AndHitTheFilterButton()
        {
            var driver = new ChromeDriver(); //opens the ChromeDriver
            driver.Url = "http://localhost/LogViewer/Home/Index";

            new SelectElement(driver.FindElement(By.Id("Filter_LogCategory"))).SelectByText("Info");

            driver.FindElementById("filterButton").Click();
        }

        [Test]
        public void TheSearchTextBoxShould_SearchForSomeTextAnd_ThenClickTheFilterButton()
        {
            var driver = new ChromeDriver(); //opens the ChromeDriver
            driver.Url = "http://localhost/LogViewer/Home/Index";

            IWebElement searchTextBox = driver.FindElement(By.Id("Filter_SearchText"));

            searchTextBox.Clear();
            searchTextBox.SendKeys("Checking");

            driver.FindElementById("filterButton").Click();
        }

        [Test]
        public void TheDatePickerShould_SelectADate_AndThenClickTheFilterButton()
        {
            var driver = new ChromeDriver(); //opens the ChromeDriver
            driver.Url = "http://localhost/LogViewer/Home/Index";
            
            IWebElement datePicker = driver.FindElement(By.Id("Filter_DateFrom"));

            datePicker.Clear();
            datePicker.SendKeys("2018-2-13");

            driver.FindElementById("filterButton").Click();
        }

        //use selenium to test the grid to check how much "check", logcategories are there with this search
        //and assert it accordingly but first write a feature file

        [Test]
        public void TheDatePickerShould_SelectADate_AndThenClickTheFilterButton_AndThenTheGridShouldClickTheReset_ButtonAndResetTheGrid()
        {
            var driver = new ChromeDriver(); //opens the ChromeDriver
            driver.Url = "http://localhost/LogViewer/Home/Index";

            IWebElement datePicker = driver.FindElement(By.Id("Filter_DateFrom"));

            datePicker.Clear();
            datePicker.SendKeys("2018-2-7");

            driver.FindElementById("filterButton").Click();

            driver.FindElementById("resetButton").Click();
        }
    }
}




//the selenium tests must test the search text, the datepicker etc. it should do everything a user would do but automically without
//clicking on it...
//for the specflow, after creating the feature file for the automated tests once its been "binded", it will need to be changed accordingly
//and asserted accordingly