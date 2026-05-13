using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace DatesAndStuff.Web.Tests;

[TestFixture]
public class BlazeDemoTests
{
    private IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }

    [Test]
    public void BlazeDemo_MexicoCityToDublin_ShouldHaveAtLeastThreeFlights()
    {
        driver.Navigate().GoToUrl("https://blazedemo.com/");

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        var fromPort = wait.Until(d => d.FindElement(By.Name("fromPort")));
        new SelectElement(fromPort).SelectByText("Mexico City");

        var toPort = wait.Until(d => d.FindElement(By.Name("toPort")));
        new SelectElement(toPort).SelectByText("Dublin");

        driver.FindElement(By.CssSelector("input[type='submit']")).Click();

        var flights = wait.Until(d =>
        {
            var rows = d.FindElements(By.CssSelector("table tbody tr"));
            return rows.Count > 0 ? rows : null;
        });

        flights.Count.Should().BeGreaterThanOrEqualTo(3);
    }
}