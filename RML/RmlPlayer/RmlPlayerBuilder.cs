using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TRLSwingers.RmlPlayer
{
    public class RmlPlayerBuilder
    {
        private readonly ChromeDriver _driver;
        private readonly int _year;

        public RmlPlayerBuilder(ChromeDriver driver, int year)
        {
            _driver = driver;
            _year = year;
        }

        public List<RmlPlayer> BuildRmlPlayers()
        {
            var playerTypes = new List<string>
            {
                "CB",
                "DL"
            };

            var rmlPlayers = new List<RmlPlayer>();
            foreach (var playerType in playerTypes)
            {
                _driver.Navigate().GoToUrl($"http://games.espn.com/ffl/freeagency?leagueId=127291&teamId=8&seasonId={_year}");
                var opLink = _driver.FindElement(By.XPath($"//ul[@class='filterToolsOptionSet']/li/a[contains(.,'{playerType}')]"));
                opLink.Click();

                System.Threading.Thread.Sleep(2000);
                var nextLink = _driver.FindElements(By.XPath("//div[@class='paginationNav']/a[contains(., 'NEXT')]"));

                while (nextLink.Count == 1)
                {
                    nextLink = _driver.FindElements(By.XPath("//div[@class='paginationNav']/a[contains(., 'NEXT')]"));
                    var rmlPlayerRows = _driver.FindElements(By.XPath("//tr[contains(@class, 'pncPlayerRow')]"));

                    foreach (var rmlPlayerRow in rmlPlayerRows)
                    {
                        var rmlPlayer = new RmlPlayer();
                        //TODO: Need to check if the first Position is the one we are looking for (i.e. S, CB => CB)
                        //Chandler Jones, Ari LB, DE, EDR
                        try
                        {
                            rmlPlayer.Team = rmlPlayerRow.FindElement(By.XPath("./td[@class='playertablePlayerName']")).Text.Split(new string[] { ", " }, StringSplitOptions.None)[1].Split(' ')[0];
                            rmlPlayer.Name = rmlPlayerRow.FindElement(By.XPath("./td[@class='playertablePlayerName']/a")).Text;
                            rmlPlayer.PreviousRank = int.TryParse(rmlPlayerRow.FindElement(By.XPath("./td[@class='playertableData'][1]")).Text, out _) ?
                                int.Parse(rmlPlayerRow.FindElement(By.XPath("./td[@class='playertableData'][1]")).Text) : -1;
                            rmlPlayer.PreviousPoints = decimal.TryParse(rmlPlayerRow.FindElement(By.XPath("./td[contains(@class,'playertableStat')][1]")).Text, out _) ?
                                decimal.Parse(rmlPlayerRow.FindElement(By.XPath("./td[contains(@class,'playertableStat')][1]")).Text) : -10;
                            rmlPlayer.PreviousAverage = decimal.TryParse(rmlPlayerRow.FindElement(By.XPath("./td[contains(@class,'playertableStat')][2]")).Text, out _) ?
                                decimal.Parse(rmlPlayerRow.FindElement(By.XPath("./td[contains(@class,'playertableStat')][2]")).Text) : -10;
                            rmlPlayer.Positions = ParsePositionFromElement(rmlPlayerRow.FindElement(By.XPath("./td[@class='playertablePlayerName']")).Text, rmlPlayer.Name.Length);
                        }
                        catch
                        {
                            System.Threading.Thread.Sleep(30000);
                            rmlPlayer.Team = rmlPlayerRow.FindElement(By.XPath("./td[@class='playertablePlayerName']")).Text.Split(new string[] { ", " }, StringSplitOptions.None)[1].Split(' ')[0];
                            rmlPlayer.Name = rmlPlayerRow.FindElement(By.XPath("./td[@class='playertablePlayerName']/a")).Text;
                            rmlPlayer.PreviousRank = int.TryParse(rmlPlayerRow.FindElement(By.XPath("./td[@class='playertableData'][1]")).Text, out _) ?
                                int.Parse(rmlPlayerRow.FindElement(By.XPath("./td[@class='playertableData'][1]")).Text) : -1;
                            rmlPlayer.PreviousPoints = decimal.TryParse(rmlPlayerRow.FindElement(By.XPath("./td[contains(@class,'playertableStat')][1]")).Text, out _) ?
                                decimal.Parse(rmlPlayerRow.FindElement(By.XPath("./td[contains(@class,'playertableStat')][1]")).Text) : -10;
                            rmlPlayer.PreviousAverage = decimal.TryParse(rmlPlayerRow.FindElement(By.XPath("./td[contains(@class,'playertableStat')][2]")).Text, out _) ?
                                decimal.Parse(rmlPlayerRow.FindElement(By.XPath("./td[contains(@class,'playertableStat')][2]")).Text) : -10;
                            rmlPlayer.Positions = ParsePositionFromElement(rmlPlayerRow.FindElement(By.XPath("./td[@class='playertablePlayerName']")).Text, rmlPlayer.Name.Length);
                        }

                        rmlPlayers.Add(rmlPlayer);
                    }

                    if (nextLink.Count == 1)
                    {
                        nextLink[0].Click();
                        System.Threading.Thread.Sleep(5000);
                    }
                }
            }

            return rmlPlayers;
        }

        private string ParsePositionFromElement(string text, int lengthOfName)
        {
            var teamAndPositions = text.Substring(lengthOfName, text.Length - lengthOfName).Substring(2);
            var indexOfTeam = teamAndPositions.IndexOf(' ');
            var positions = teamAndPositions.Substring(indexOfTeam, teamAndPositions.Length - indexOfTeam).Substring(1);

            return positions;
        }
    }
}
