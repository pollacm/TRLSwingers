using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TRLSwingers.Teams
{
    public class TeamBuilder
    {
        private readonly ChromeDriver _driver;
        private readonly int _year;

        public TeamBuilder(ChromeDriver driver, int year)
        {
            _driver = driver;
            _year = year;
        }
        public List<string> BuildTeams()
        {
            _driver.Navigate().GoToUrl($"http://games.espn.com/ffl/standings?leagueId=127291&seasonId={_year}");
            var teamAnchors = _driver.FindElements(By.CssSelector("div.games-fullcol table:nth-child(1) a"));

            var teams = new List<string>();
            foreach (var teamAnchor in teamAnchors)
            {
                teams.Add(teamAnchor.Text);
            }

            return teams;
        }
    }
}
