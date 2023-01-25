using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestingWithSeleniumTemplate
{
    public class GoogleTests : IDisposable
    {
        private IWebDriver _driver;
        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
        }

        [Test]
        public void TestGoogle()
        {
            _driver.Navigate().GoToUrl("http://www.google.com");
            _driver.FindElement(By.Id("L2AGLb")).Click();
            //Wait(2000);
            Assert.AreEqual("Google",
              _driver.FindElement(By.TagName("img")).GetAttribute("alt"));

        }
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        public void Wait(int miliseconds)
        {
            System.Threading.Thread.Sleep(miliseconds);
        }

        public void TakeScreenShoot()
        {
            var screenImage = System.IO.Path.Combine
            (TestContext.CurrentContext.TestDirectory,
             Guid.NewGuid().ToString() + ".png");
            ((ITakesScreenshot)_driver).GetScreenshot()
                .SaveAsFile(screenImage, ScreenshotImageFormat.Png);
            //Agrega la imagen al contexto de la prueba para que sea
            //mostrada posteriormente como attachment
            //en Azure DevOps
            TestContext.AddTestAttachment(screenImage);
        }
    }

}
