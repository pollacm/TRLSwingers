using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TRLSwingers.Rankings
{
    public class RankingGenerator
    {
        private readonly ChromeDriver _driver;
        private readonly int _year;

        public RankingGenerator(ChromeDriver driver, int year)
        {
            _driver = driver;
            _year = year;
        }

        public List<Ranking> GenerateRankings()
        {
            var rankings = new List<Ranking>();
            int i = 1;

            //TODO: Once the season starts change this to 12
            while (rankings.Count != 10)
            {
                _driver.Navigate().GoToUrl($"http://games.espn.com/ffl/clubhouse?leagueId=127291&teamId={i}&seasonId={_year}");

                var teamNameElementString = _driver.FindElement(By.XPath("//h3[@class='team-name']")).Text.Trim();
                var teamName = teamNameElementString.Substring(0, teamNameElementString.IndexOf(" ("));

                if (!rankings.Any(r => r.Team == teamName))
                { 
                    var ranking = new Ranking();
                    ranking.Team = teamName;
                    var recordElementString = _driver.FindElement(By.XPath("//div[@class='games-univ-mod4']/h4")).Text.Trim();
                    recordElementString = recordElementString.Substring(8, recordElementString.Length - 8);
                    var record = recordElementString.Substring(0, recordElementString.IndexOf(" (")).Split('-');

                    if (record.Length == 3)
                    {
                        ranking.Draws = int.Parse(record[2]);
                    }

                    ranking.Wins = Int32.Parse(record[0]);
                    ranking.Loses = Int32.Parse(record[1]);
                    var rankElementString = _driver.FindElement(By.XPath("//div[@class='games-univ-mod4']/h4/em")).Text.Trim().TrimStart('(').TrimEnd(')');
                    ranking.Rank = int.Parse(rankElementString.Substring(0, rankElementString.Length - 2));
                    ranking.Id = i;
                    ranking.Division = _driver.FindElement(By.XPath("//div[@class='games-univ-mod1']/p/strong")).Text;

                    rankings.Add(ranking);
                }

                i++;
            }

            return rankings;
        }
    }
}
