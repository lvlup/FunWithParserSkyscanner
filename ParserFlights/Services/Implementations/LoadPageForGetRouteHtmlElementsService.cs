using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ParserFlights.Services.Interfaces;

namespace ParserFlights.Services.Implementations
{
    public class LoadPageForGetRouteHtmlElementsService : ILoadPageForGetRouteHtmlElementsService
    {
        private readonly IWebDriver webdriver;
        private readonly WebDriverWait waitDriver;

        public LoadPageForGetRouteHtmlElementsService(IWebDriver driver)
        {
            webdriver = driver;
            waitDriver = new WebDriverWait(webdriver, TimeSpan.FromSeconds(60));
        }

        public void WaitLoadPage(string url)
        {
            //ставим таймаут на загрузку страницы
            webdriver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(60));
            //переходим на страницу
            webdriver.Navigate().GoToUrl(url);

            waitDriver.IgnoreExceptionTypes(typeof(NoSuchElementException));
            //ждем пока появится прогресс бар загрузки билетов, так как возможна ситуция, при загрузки страницы будет высвечиваться выполнение поиска
            waitDriver.Until<IWebElement>(
                (d) => d.FindElement(By.CssSelector("#cols-header > div > div.day-list-progress")));
            //ждем пока исчезнет прогресс бар загрузки билетов-это будет означать что все возможные билеты вывелись на экран
            waitDriver.Until(
                ExpectedConditions.InvisibilityOfElementLocated(
                    By.CssSelector("#cols-header > div > div.day-list-progress")));
        }


        public string GetSummoryInnerHtml()
        {
            //забираем хтмл код первого элемента из списка-самый дешевый
            return webdriver.FindElement(
                  By.CssSelector("#day-section > div > div.day-main-content > ul > li:nth-child(1) > article"))
                  .GetAttribute("innerHTML");
        }

        public string GetDetailsInnerHtml()
        {
            //нажимаем на детали, чтобы получить номера рейсов
            webdriver.FindElement(
                By.CssSelector(
                    "#day-section > div > div.day-main-content > ul > li:nth-child(1) > article > div.mainquote-cba.clearfix > button"))
                .Click();
            //ждем пока отобразится 
            waitDriver.Until<IWebElement>((d) => d.FindElement(By.CssSelector("#details-panel")));
            //забираем хтмл код
            return webdriver.FindElement(By.CssSelector("#details-panel")).GetAttribute("innerHTML");
        }

        public void Dispose()
        {
            webdriver.Quit();
        }
    }
}
