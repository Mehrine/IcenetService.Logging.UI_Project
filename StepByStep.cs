using System;
using System.Web.Mvc;
using TechTalk.SpecFlow;
using Icenet.Service.Logging.UI.Controllers;
using Icenet.Service.Logging.UI.Models;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace SpecFlowTest
{
    [Binding]
    public class StepByStep
    {
        private const string LogFilterModel = "getSet";
        private const string ActionResult = "resultT";
        private ChromeDriver _driver;

        public ChromeDriver Driver => _driver ?? (_driver = new ChromeDriver());


        private LogFilterModel PrivateMethodThatGetsTheGetSetProperties()
        {
            LogFilterModel get = null;
            if (ScenarioContext.Current.ContainsKey(LogFilterModel))
                get = (LogFilterModel) ScenarioContext.Current[LogFilterModel];

            if (get == null)
            {
                get = new LogFilterModel();
                ScenarioContext.Current[LogFilterModel] = get;
            }
            return get;
        }

        #region NON selenium steps
     
        [Given(@"logCategory is set to '(.*)'")]
        public void GivenLogCategoryIsInfo(string logCategory)
        {
            var get = PrivateMethodThatGetsTheGetSetProperties();
            get.LogCategory = logCategory;
        }


        [Given(@"DateFrom is (.*)")]
        public void MakingSureTheDateIsTheSameAsTheGivenDate(DateTime allocatedDate)
        {
            var get = PrivateMethodThatGetsTheGetSetProperties();
            get.DateFrom = allocatedDate;
        }


        [When(@"i have filtered the LogCategory")]
        public void WhenIHaveFilteredTheLogCategory()
        {
            var logModel = new LogViewModel();
            var controller = new HomeController();

            var filter = (LogFilterModel)ScenarioContext.Current[LogFilterModel];
            logModel.Filter = filter;

            var actionResult = controller.GetFilteredLogItems(logModel);
            ScenarioContext.Current[ActionResult] = actionResult;
        }


        [Then(@"the result should contain (.*) items")]
        public void ThenTheResultShouldContainItems(int expectedItemCount)
        {
            var actionResult = (ActionResult)ScenarioContext.Current[ActionResult];
            var viewResult = (ViewResult)actionResult;
            var logViewModel = (LogViewModel)viewResult.Model;
            var count = logViewModel.LogItems.Count;

            Assert.That(count, Is.EqualTo(expectedItemCount));
        }
        #endregion





        #region Selenium Steps

        [Given(@"When the page navigates to the LogViewer page")]
        public void GivenWhenThePageNavigatesToTheLogViewerPage()
        {
            Driver.Url = "http://localhost/LogViewer/Home/Index";

        }

        [Given(@"LogCategory is set to '(.*)'")]
        public void GivenLogCategoryItSetToInfo(string logCategory)
        {
            new SelectElement(Driver.FindElement(By.Id("Filter_LogCategory"))).SelectByText(logCategory);
            Driver.FindElementById("filterButton").Click();
        }

        

        [Given(@"Search text is set to Info")]
        public void GivenSearchTextIsSetToInfo()
        {
            IWebElement searchTextBox = Driver.FindElement(By.Id("Filter_SearchText"));

            searchTextBox.Clear();
            searchTextBox.SendKeys("Checking");
        }


        [When(@"I press the filter button")]
        public void WhenIPressTheFilterButton()
        {
            Driver.FindElementById("filterButton").Click();
        }
        

        [Then(@"the result should contain (.*) log categories")]
        public void ThenTheResultShouldContainLogCategories(int number)
        {
            var gridContent = Driver.FindElement(By.ClassName("t-grid-content"));
            var rows = gridContent.FindElements(By.TagName("tr"));
          
            Assert.That(rows.Count, Is.EqualTo(number));
            
            //foreach (var row in rows)
            //{
            //    var rowTds = row.FindElements(By.TagName("td"));
            //    foreach (var td in rowTds)
            //    {
            //        var a = td.FindElement(By.TagName("a"));
            //        Console.WriteLine("HREF: " + a.GetAttribute("href"));
            //    }
          //  }
        } 
          #endregion
    }
}



//google how to get rows out of selenium

//to check how many there are in the grid, to test it aginst the original number of items then
//assert it to how many and what its equal to....


//read up on selenium web driver...
//start selenium testing

    //install selenium on the "specflow test", file and copy the content
    //into a new class inside the "specflow test", project
    //write the steps down for the selenium automated testing in the step by step file 
    //so that the steps are created
