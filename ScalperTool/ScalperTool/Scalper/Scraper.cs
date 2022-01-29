using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ScalperTool.Scalper.Interfaces;
using ScalperTool.Settings;
using System;
using System.IO;
using System.Reflection;

namespace ScalperTool.Scraper
{
    public class Scraper: IPlaystationFiveScalper
    {
        private readonly ElgigantenSettings _elgigantenSettings;
        private readonly MediaMarktSettings _mediaMarktSettings;
        private readonly NetonnetSettings _netonnetSettings;
        private readonly ChromeDriverSettings _chromeDriverSettings;

        public Scraper(
            IOptions<ElgigantenSettings> elgigantenSettings,
            IOptions<MediaMarktSettings> mediaMarktSettings,
            IOptions<NetonnetSettings> netonnetSettings,
            IOptions<ChromeDriverSettings> chromeDriverSettings)
        {
            _elgigantenSettings = elgigantenSettings.Value;
            _mediaMarktSettings = mediaMarktSettings.Value;
            _netonnetSettings = netonnetSettings.Value;
            _chromeDriverSettings = chromeDriverSettings.Value;

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            ChromeOptions options = new ChromeOptions();

            options.AddArguments(new string[]
            {
                _chromeDriverSettings.StartMaximized,
                _chromeDriverSettings.DisableBlinkFeaturesAutomationControlled,
                _chromeDriverSettings.UserAgent,
                _chromeDriverSettings.Headless
            });

            ChromeDriver = new ChromeDriver(path,options);
        }

        public ChromeDriver ChromeDriver { get; set; }

        public bool InStockOrBookableMediaMarkt()
        {
            var status = ExtractInnerHtmlValueFromClass("price-sidebar", _mediaMarktSettings.PlaystationFiveDiscUrl);

            return !status.Contains("Ej i webblager");

        }

        public bool AcceptOrdersElgiganten()
        {
            var status = ExtractInnerHtmlValueFromClass("add-to-basket", _elgigantenSettings.PlaystationFiveDiscUrl);

            return status.Contains("Lägg i kundvagn");
        }

        public bool InStockOrBookableNetonnet()
        {
            var status = ExtractInnerHtmlValueFromId("BuyButton_ProductPageStandard_1021423", _netonnetSettings.PlaystationFiveDiscUrl);

            return !status.Contains("Elementet fanns inte");

        }

        private string ExtractInnerHtmlValueFromClass(string className, string url)
        {
            ChromeDriver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 5);
            ChromeDriver.Navigate().GoToUrl(url);
            IWebElement webElement;

            try
            {
                webElement = ChromeDriver.FindElement(By.ClassName(className));
            }
            catch (NoSuchElementException)
            {
                return "Elementet fanns inte";
            }
            
            return webElement.GetAttribute("innerHTML");
        }
        private string ExtractInnerHtmlValueFromId(string id, string url)
        {
            ChromeDriver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 5);
            ChromeDriver.Navigate().GoToUrl(url);
            IWebElement webElement;

            try
            {
                webElement = ChromeDriver.FindElement(By.Id(id));
            }
            catch (NoSuchElementException)
            {
                return "Elementet fanns inte";
            }

            return webElement.GetAttribute("innerHTML");
        }
    }
}
