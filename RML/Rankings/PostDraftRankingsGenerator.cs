using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TRLSwingers.Rankings
{
    public class PostDraftRankingGenerator
    {
        private readonly ChromeDriver _driver;

        private List<string> TeamPaths = new List<string>
        {
            "http://games.espn.com/ffl/clubhouse?leagueId=127291&teamId=1&seasonId=2018",
            "http://games.espn.com/ffl/clubhouse?leagueId=127291&teamId=3&seasonId=2018",
            "http://games.espn.com/ffl/clubhouse?leagueId=127291&teamId=5&seasonId=2018",
            "http://games.espn.com/ffl/clubhouse?leagueId=127291&teamId=7&seasonId=2018",
            "http://games.espn.com/ffl/clubhouse?leagueId=127291&teamId=8&seasonId=2018",
            "http://games.espn.com/ffl/clubhouse?leagueId=127291&teamId=10&seasonId=2018",
            "http://games.espn.com/ffl/clubhouse?leagueId=127291&teamId=11&seasonId=2018",
            "http://games.espn.com/ffl/clubhouse?leagueId=127291&teamId=14&seasonId=2018",
            "http://games.espn.com/ffl/clubhouse?leagueId=127291&teamId=15&seasonId=2018",
            "http://games.espn.com/ffl/clubhouse?leagueId=127291&teamId=17&seasonId=2018",
            "http://games.espn.com/ffl/clubhouse?leagueId=127291&teamId=18&seasonId=2018",
            "http://games.espn.com/ffl/clubhouse?leagueId=127291&teamId=19&seasonId=2018"
        };

        public PostDraftRankingGenerator(ChromeDriver driver)
        {
            _driver = driver;
            _driver.Manage().Timeouts().PageLoad = new TimeSpan(0, 0, 0, 15);
        }

        public void GenerateRankings()
        {
            foreach (var path in TeamPaths)
            {
                _driver.Navigate().GoToUrl(path);
                _driver.FindElement(By.XPath("//div[@id='playerTableHeader']/ul/li[4]")).Click();
                var postDraftRankings = new List<PostDraftRanking>();

                Thread.Sleep(1000);

                
                var playerElements = _driver.FindElements(By.XPath("//tr[contains(@class, 'pncPlayerRow')]"));
                
                foreach (var playerElement in playerElements)
                {
                    var position = playerElement.FindElement(By.XPath("./td[1]")).Text;
                    var teamNameElementString = _driver.FindElement(By.XPath("//h3[@class='team-name']")).Text.Trim();
                    var teamName = teamNameElementString.Substring(0, teamNameElementString.IndexOf(" ("));

                    if (position != "IR")
                    {
                        var postDraftRanking = new PostDraftRanking();
                        postDraftRanking.TeamName = teamName;

                        if (playerElement.FindElements(By.XPath("./td[@class='playertablePlayerName']")).Count > 0)
                        {
                            postDraftRanking.PlayerPosition = position;
                            postDraftRanking.PlayerName = playerElement.FindElement(By.XPath("./td[@class='playertablePlayerName']")).Text;

                            var projectedPoints = playerElement.FindElement(By.XPath("./td[contains(@class, 'appliedPoints')][1]")).Text;

                            var isDefense = postDraftRanking.PlayerName.Contains("D/ST");
                            postDraftRanking.Projection = isDefense ? 1600m : decimal.Parse(projectedPoints);

                            postDraftRankings.Add(postDraftRanking);
                        }
                    }
                }

                 new PrintPostDraftRankingsService(postDraftRankings).WritePostDraftRankingsFile();
            }
        }
    }
}
