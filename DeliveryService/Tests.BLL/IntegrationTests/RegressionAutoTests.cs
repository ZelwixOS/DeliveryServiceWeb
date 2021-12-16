using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;


namespace Tests.BLL.IntegrationTests
{
    public class RegressionAutoTests
    {
        const string baseUrl = "http://localhost:5000";

        [Fact]
        public void IsAuntethicatedOff_NotAutentificated_EmptyMessage()
        {
            using IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            string expected = "\"message\":\"\"";

            driver.Navigate().GoToUrl($"{baseUrl}/api/Account/isAuthenticated");
            string content = driver.PageSource;

            Assert.Contains(expected, content);
        }

        [Fact]
        public void Role_NotAutentificated_EmptyPage()
        {
            using IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            string expected = "<html><head></head><body></body></html>";

            driver.Navigate().GoToUrl($"{baseUrl}/api/Account/Role");
            string content = driver.PageSource;

            Assert.Equal(expected, content);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void GetOrder_CorrectId_Forbidden(int orderId)
        {
            using IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            string expected = $"\"id\":{orderId}";

            driver.Navigate().GoToUrl($"{baseUrl}/api/orders/{orderId}");
            string content = driver.PageSource;

            Assert.Contains(expected, content);
        }
    }
}
