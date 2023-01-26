using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestingWithSeleniumTemplate
{
    public class wwwFranDiazinfoTests : IDisposable
    {
        private IWebDriver _driver;
        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
        }

        [Test]
        public void MainPageHaveThePhotoPosted()
        {
            _driver.Navigate().GoToUrl("http://www.frandiaz.info");
            var tagsImg = _driver.FindElements(By.TagName("img"));
            bool encontrado = false;
            foreach (var tag in tagsImg)
            {
                if (tag.GetAttribute("alt")== "Foto Fran Diaz")
                {
                    encontrado = true; 
                    break;
                }
            }

            Assert.AreEqual(true,encontrado);
        }

        [Test]
        public void MyPageOpenThePortfolio()
        {
            _driver.Navigate().GoToUrl("http://www.frandiaz.info");
            var nav = _driver.FindElement(By.Id("nav"));
            var links = nav.FindElements(By.TagName("a"));
            bool encontrado = false;
            foreach (var link in links)
            {
                if (link.GetDomAttribute("href") == "portfolio.seat.html")
                {
                    _driver.Navigate().GoToUrl("http://www.frandiaz.info/portfolio.seat.html");
                    encontrado = true;
                    break;
                }
            }

            Assert.AreEqual(true, encontrado);
            Assert.AreEqual("Portfolio de Seat",
                _driver.FindElement(By.Id("titulo")).Text);
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
