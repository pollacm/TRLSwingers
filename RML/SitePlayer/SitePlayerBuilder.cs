using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TRLSwingers.PlayerComparer;

namespace TRLSwingers.SitePlayer
{
    public class SitePlayerBuilder
    {
        private readonly ChromeDriver _driver;

        public SitePlayerBuilder(ChromeDriver driver)
        {
            _driver = driver;
        }
        public List<SitePlayer> BuildSitePlayers()
        {
            var sitePlayers = new List<SitePlayer>();

            var espnUrlTemplate = "http://www.espn.com/nfl/team/depth/_/name/{0}/formation/special-teams";
            var yahooUrlTemplate = "https://sports.yahoo.com/nfl/teams/{0}/roster/";
            var ourladsUrlTemplate = "http://www.ourlads.com/nfldepthcharts/depthchart/{0}";
            var count = 0;
            foreach (var siteCode in PlayerConstants.SiteCodes)
            {
                //yahoo
                _driver.Navigate().GoToUrl(string.Format(yahooUrlTemplate, siteCode.YahooCode));
                _driver.FindElement(By.XPath("//li[contains(.,'Defense')]")).Click();
                _driver.WaitUntilElementExists(By.XPath($"//div/h4[contains(.,'Free Safety')]"));

                foreach (var linebackerPositionsYahoo in PlayerConstants.LinebackerPositionsYahoo)
                {
                    var yahooStrongSafetyElement = _driver.FindElements(By.XPath($"//div/h4[contains(.,'{linebackerPositionsYahoo.Key}')]"));
                    //var test = sitePlayers.Where(s => s.Team == "Dallas Cowboys").ToList();
                    if (yahooStrongSafetyElement.Count == 1)
                    {
                        var yahooSafeties = yahooStrongSafetyElement[0].FindElements(By.XPath("./parent::div/ul/li/div/a"));

                        if (yahooSafeties.Count > 0)
                        {
                            sitePlayers.Add(new SitePlayer(siteCode.TeamCode, PlayerConstants.DepthChartEnum.Starter, linebackerPositionsYahoo.Value, SitePlayer.SiteEnum.Yahoo, yahooSafeties[0].Text));
                        }
                        if (yahooSafeties.Count > 1)
                        {
                            if (yahooStrongSafetyElement[0].Text == string.Empty)
                            {
                                sitePlayers.Add(new SitePlayer(siteCode.TeamCode, PlayerConstants.DepthChartEnum.Starter, linebackerPositionsYahoo.Value, SitePlayer.SiteEnum.Yahoo, yahooSafeties[1].Text));
                            }
                            else
                            {
                                sitePlayers.Add(new SitePlayer(siteCode.TeamCode, PlayerConstants.DepthChartEnum.Secondary, linebackerPositionsYahoo.Value, SitePlayer.SiteEnum.Yahoo, yahooSafeties[1].Text));
                            }
                        }
                        if (yahooSafeties.Count > 2)
                        {
                            sitePlayers.Add(new SitePlayer(siteCode.TeamCode, PlayerConstants.DepthChartEnum.Tertiary, linebackerPositionsYahoo.Value, SitePlayer.SiteEnum.Yahoo, yahooSafeties[2].Text));
                        }
                    }
                }

                _driver.Navigate().GoToUrl(string.Format(espnUrlTemplate, siteCode.EspnCode));
                _driver.FindElement(By.XPath("//div[@id='my-teams-table']/div/ul/li[2]/a")).Click();
                _driver.WaitUntilElementExists(By.XPath($"//table/tbody/tr/td[contains(.,'SS')]"));

                foreach (var linebackerPositionsEspn in PlayerConstants.LinebackerPositionsEspn)
                {
                    var espnStrongSafetyElement = _driver.FindElements(By.XPath($"//table/tbody/tr/td[contains(.,'{linebackerPositionsEspn.Key}')]"));
                    if (espnStrongSafetyElement.Count == 1)
                    {
                        var espnSafties = espnStrongSafetyElement[0].FindElements(By.XPath("./parent::tr/td"));

                        if (espnSafties.Count > 1)
                        {
                            sitePlayers.Add(new SitePlayer(siteCode.TeamCode, PlayerConstants.DepthChartEnum.Starter, linebackerPositionsEspn.Value, SitePlayer.SiteEnum.ESPN, espnSafties[1].Text));
                        }
                        if (espnSafties.Count > 2)
                        {
                            if (espnSafties[0].Text == string.Empty)
                            {
                                sitePlayers.Add(new SitePlayer(siteCode.TeamCode, PlayerConstants.DepthChartEnum.Starter, linebackerPositionsEspn.Value, SitePlayer.SiteEnum.ESPN, espnSafties[2].Text));
                            }
                            else
                            {
                                sitePlayers.Add(new SitePlayer(siteCode.TeamCode, PlayerConstants.DepthChartEnum.Secondary, linebackerPositionsEspn.Value, SitePlayer.SiteEnum.ESPN, espnSafties[2].Text));
                            }
                        }
                        if (espnSafties.Count > 3)
                        {
                            sitePlayers.Add(new SitePlayer(siteCode.TeamCode, PlayerConstants.DepthChartEnum.Tertiary, linebackerPositionsEspn.Value, SitePlayer.SiteEnum.ESPN, espnSafties[3].Text));
                        }
                    }
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

                count++;

                if (count % 5 == 0)
                {
                    Console.WriteLine($"******************************************");
                    Console.WriteLine($"Writing Player Comparer: {count} processed.");
                    Console.WriteLine($"******************************************");
                }
            }

            return sitePlayers;
        }
    }
}
