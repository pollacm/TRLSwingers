using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TRLSwingers.PlayerComparer;

namespace TRLSwingers.Returners
{
    public class ReturnerBuilder
    {
        private readonly ChromeDriver _driver;

        public ReturnerBuilder(ChromeDriver driver)
        {
            _driver = driver;
        }

        public void GenerateReturners()
        {
            var returners = new List<Returner>();
            var espnUrlTemplate = "http://www.espn.com/nfl/team/depth/_/name/{0}/formation/special-teams";
            var yahooUrlTemplate = "https://sports.yahoo.com/nfl/teams/{0}/roster/";
            var ourladsUrlTemplate = "http://www.ourlads.com/nfldepthcharts/depthchart/{0}";
            var count = 0;
            foreach (var siteCode in PlayerConstants.SiteCodes)
            {
                //yahoo
                _driver.NavigateToUrl(string.Format(yahooUrlTemplate, siteCode.YahooCode));
                _driver.FindElement(By.XPath("//li[contains(.,'Specialists')]")).Click();
                Thread.Sleep(1000);

                var returner = new Returner();
                returner.Team = siteCode.TeamCode;

                //yahoo
                _driver.Navigate().GoToUrl(string.Format(yahooUrlTemplate, siteCode.YahooCode));
                _driver.FindElement(By.XPath("//li[contains(.,'Specialists')]")).Click();
                Thread.Sleep(1000);

                var yahooKickReturnersElement = _driver.FindElements(By.XPath("//div/h4[contains(.,'Kick Returner')]"));
                if (yahooKickReturnersElement.Count == 1)
                {
                    var yahooKickReturners = yahooKickReturnersElement[0].FindElements(By.XPath("./parent::div/ul/li/div/a"));

                    if (yahooKickReturners.Count > 0)
                        returner.YahooPrimaryKickReturner = yahooKickReturners[0].Text;

                    if (yahooKickReturners.Count > 1)
                    {
                        if (yahooKickReturners[0].Text == string.Empty)
                            returner.YahooPrimaryKickReturner = yahooKickReturners[1].Text;
                        else
                            returner.YahooSecondaryKickReturner = yahooKickReturners[1].Text;
                    }

                    if (yahooKickReturners.Count > 2)
                        returner.YahooTertiaryKickReturner = yahooKickReturners[2].Text;
                }

                var yahooPuntReturnersElement = _driver.FindElements(By.XPath("//div/h4[contains(.,'Punt Returner')]"));
                if (yahooPuntReturnersElement.Count == 1)
                {
                    var yahooPuntReturners = yahooPuntReturnersElement[0].FindElements(By.XPath("./parent::div/ul/li/div/a"));

                    if (yahooPuntReturners.Count > 0)
                        returner.YahooPrimaryPuntReturner = yahooPuntReturners[0].Text;

                    if (yahooPuntReturners.Count > 1)
                    {
                        if (yahooPuntReturners[0].Text == string.Empty)
                            returner.YahooPrimaryPuntReturner = yahooPuntReturners[1].Text;
                        else
                            returner.YahooSecondaryPuntReturner = yahooPuntReturners[1].Text;
                    }

                    if (yahooPuntReturners.Count > 2)
                        returner.YahooTertiaryPuntReturner = yahooPuntReturners[2].Text;
                }

                //espn
                _driver.Navigate().GoToUrl(string.Format(espnUrlTemplate, siteCode.EspnCode));
                var espnKickReturnersElement = _driver.FindElements(By.XPath("//table/tbody/tr/td[contains(.,'KR')]"));
                if (espnKickReturnersElement.Count == 1)
                {
                    var espnKickReturners = espnKickReturnersElement[0].FindElements(By.XPath("./parent::tr/td"));

                    if (espnKickReturners.Count > 1)
                        returner.EspnPrimaryKickReturner = espnKickReturners[1].Text;

                    if (espnKickReturners.Count > 2)
                    {
                        if (espnKickReturners[0].Text == string.Empty)
                            returner.EspnPrimaryKickReturner = espnKickReturners[2].Text;
                        else
                            returner.EspnSecondaryKickReturner = espnKickReturners[2].Text;
                    }

                    if (espnKickReturners.Count > 3)
                        returner.EspnTertiaryKickReturner = espnKickReturners[3].Text;
                }

                var espnPuntReturnersElement = _driver.FindElements(By.XPath("//table/tbody/tr/td[contains(.,'PR')]"));
                if (espnPuntReturnersElement.Count == 1)
                {
                    var espnPuntReturners = espnPuntReturnersElement[0].FindElements(By.XPath("./parent::tr/td"));

                    if (espnPuntReturners.Count > 1)
                        returner.EspnPrimaryPuntReturner = espnPuntReturners[1].Text;

                    if (espnPuntReturners.Count > 2)
                    {
                        if (espnPuntReturners[0].Text == string.Empty)
                            returner.EspnPrimaryPuntReturner = espnPuntReturners[2].Text;
                        else
                            returner.EspnSecondaryPuntReturner = espnPuntReturners[2].Text;
                    }

                    if (espnPuntReturners.Count > 3)
                        returner.EspnTertiaryPuntReturner = espnPuntReturners[3].Text;
                }

                //ourlads
                //_driver.Navigate().GoToUrl(string.Format(ourladsUrlTemplate, siteCode.OurladsCode));
                //var ourladsKickReturnersElement = _driver.FindElements(By.XPath("//table/tbody/tr/td[contains(.,'KR')]"));
                //if (ourladsKickReturnersElement.Count == 1)
                //{
                //    var ourladsKickReturners = ourladsKickReturnersElement[0].FindElements(By.XPath("./parent::tr/td"));

                //    var splitReturnerName = ourladsKickReturners[2].Text.Split(new string[] { ", " }, StringSplitOptions.None);
                //    if (splitReturnerName.Length > 1)
                //    {
                //        returner.OurladsPrimaryKickReturner = splitReturnerName[1] + " " + splitReturnerName[0];
                //    }

                //    splitReturnerName = ourladsKickReturners[4].Text.Split(new string[] { ", " }, StringSplitOptions.None);
                //    if (splitReturnerName.Length > 1)
                //    {
                //        returner.OurladsPrimaryKickReturner = splitReturnerName[1] + " " + splitReturnerName[0];
                //    }

                //    splitReturnerName = ourladsKickReturners[6].Text.Split(new string[] { ", " }, StringSplitOptions.None);
                //    if (splitReturnerName.Length > 1)
                //    {
                //        returner.OurladsTertiaryKickReturner = splitReturnerName[1] + " " + splitReturnerName[0];
                //    }
                //}

                //var ourladsPuntReturnersElement = _driver.FindElements(By.XPath("//table/tbody/tr/td[contains(.,'PR')]"));
                //if (ourladsPuntReturnersElement.Count == 1)
                //{
                //    var ourladsPuntReturners = ourladsPuntReturnersElement[0].FindElements(By.XPath("./parent::tr/td"));

                //    var splitReturnerName = ourladsPuntReturners[2].Text.Split(new string[] { ", " }, StringSplitOptions.None);
                //    if (splitReturnerName.Length > 1)
                //    {
                //        returner.OurladsPrimaryPuntReturner = splitReturnerName[1] + " " + splitReturnerName[0];
                //    }

                //    splitReturnerName = ourladsPuntReturners[4].Text.Split(new string[] { ", " }, StringSplitOptions.None);
                //    if (splitReturnerName.Length > 1)
                //    {
                //        returner.OurladsPrimaryPuntReturner = splitReturnerName[1] + " " + splitReturnerName[0];
                //    }

                //    splitReturnerName = ourladsPuntReturners[6].Text.Split(new string[] { ", " }, StringSplitOptions.None);
                //    if (splitReturnerName.Length > 1)
                //    {
                //        returner.OurladsTertiaryPuntReturner = splitReturnerName[1] + " " + splitReturnerName[0];
                //    }
                //}

                returners.Add(returner);

                count++;

                if (count % 5 == 0)
                {
                    Console.WriteLine($"******************************************");
                    Console.WriteLine($"Writing Returners: {count} processed.");
                    Console.WriteLine($"******************************************");
                }
            }

            //Console.WriteLine(returners);

            Console.WriteLine("Writing returner File:");

            new PrintReturnerService(returners).WriteReturnerFile();
            Console.WriteLine("Writing returner File COMPLETE");
        }
    }
}