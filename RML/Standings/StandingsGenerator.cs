using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TRLSwingers.Standings
{
    public class StandingsGenerator
    {
        private readonly ChromeDriver _driver;
        private readonly int _year;

        public StandingsGenerator(ChromeDriver driver, int year)
        {
            _driver = driver;
            _year = year;
        }

        public List<Standing> GenerateStandings()
        {
            var standings = new List<Standing>();

            _driver.Navigate().GoToUrl($"http://games.espn.com/ffl/standings?leagueId=127291&seasonId={_year}");
            standings.AddRange(BuildDivisionStandings("//table[2]"));
            standings.AddRange(BuildDivisionStandings("//table[3]"));

            return standings;
        }

        public List<Standing> BuildDivisionStandings(string tableName)
        {
            var standings = new List<Standing>();
            var divisionTable = _driver.FindElement(By.XPath(tableName));
            var standingRows = divisionTable.FindElements(By.XPath("./tbody/tr[contains(@class, 'bodyCopy')]"));
            var divisionRow = divisionTable.FindElement(By.XPath("./tbody/tr[contains(@class, 'tableHead')]")).Text;

            foreach (var standingRow in standingRows)
            {
                var standing = new Standing();
                standing.Division = divisionRow;
                standing.Team = standingRow.FindElement(By.XPath("./td/a")).Text.Split(new string[] { " (" }, StringSplitOptions.None)[0].Trim();
                standing.PointsFor = decimal.Parse(standingRow.FindElement(By.XPath("./td[@class='sortablePF']")).Text);

                standings.Add(standing);
            }

            return standings;
        }
    }
}
