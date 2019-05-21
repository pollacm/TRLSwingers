using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TRLSwingers.Teams;

namespace TRLSwingers.Weeks
{
    public class WeekBuilder
    {
        private readonly ChromeDriver _driver;
        private readonly int _weekNumber;
        private readonly int _year;

        public WeekBuilder(ChromeDriver driver, int weekNumber, int year)
        {
            _driver = driver;
            _weekNumber = weekNumber;
            _year = year;
        }
        public Week BuildWeek()
        {
            var week = new Week();
            var scores = new List<Score>();

            _driver.Navigate().GoToUrl($"http://games.espn.com/ffl/leagueoffice?leagueId=127291&seasonId={_year}");
            _driver.WaitUntilElementExists(By.ClassName("games-nav"));

            _driver.Navigate().GoToUrl($"http://games.espn.com/ffl/scoreboard?leagueId=127291&matchupPeriodId={_weekNumber}");
            _driver.WaitUntilElementExists(By.ClassName("ptsBased"));

            var matchups = _driver.FindElements(By.XPath("//table[@class='ptsBased matchup']"));
            foreach (var matchup in matchups)
                scores.Add(BuildScore(matchup));
            week.Scores = scores;
            week.WeekNumber = _weekNumber;

            return week;
        }

        private Score BuildScore(IWebElement matchupElement)
        {
            var score = new Score();
            var teams = matchupElement.FindElements(By.XPath("./tbody//tr"));
            var awayTeam = true;
            foreach (var team in teams)
                if (awayTeam) //new score; build away
                {
                    score = new Score();
                    score.AwayTeam = BuildTeam(team);
                    awayTeam = false;
                }
                else //build home team
                {
                    score.HomeTeam = BuildTeam(team);
                    break;
                }

            return score;
        }

        private Team BuildTeam(IWebElement teamElement)
        {
            var team = new Team();

            team.TeamName = teamElement.FindElement(By.XPath("./td[@class='team']/div[@class='name']/a")).Text;
            team.TeamAbbreviation = teamElement.FindElement(By.XPath("./td[@class='team']/div[@class='name']/span")).Text.Replace("(", "").Replace(")", "");
            team.TeamPoints = decimal.Parse(teamElement.FindElement(By.XPath("./td[contains(@class, 'score')]")).Text);
            team.Win = teamElement.FindElements(By.XPath("./td[2][contains(@class, 'winning')]")).Any();

            return team;
        }
    }
}
