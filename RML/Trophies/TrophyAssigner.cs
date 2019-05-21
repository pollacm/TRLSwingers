using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TRLSwingers.Teams;
using TRLSwingers.Weeks;

namespace TRLSwingers.Trophies
{
    public class TrophyAssigner
    {
        private readonly ChromeDriver _driver;
        private readonly int _year;

        public TrophyAssigner(ChromeDriver driver, int year)
        {
            _driver = driver;
            _year = year;
        }
        //TODO: Add a boolean for assign that gets passed in from user input
        public ITrophy AssignTrophy(Week currentWeek, Team team, ITrophy trophyToAssign)
        {
            var start = 0;
            _driver.Navigate().GoToUrl($"http://games.espn.com/ffl/trophylist?leagueId=127291&start={start}");
            
            //http://games.espn.com/ffl/trophylist?leagueId=127291&start=12

            var foundTrophyLabel = _driver.FindElements(By.XPath("//table/tbody/tr/td/div/div/center/b[contains(.,'" + trophyToAssign.GetTrophyName() + "')]")).Count() == 1;

            if (!foundTrophyLabel)
            {
                start = 12;
                _driver.Navigate().GoToUrl($"http://games.espn.com/ffl/trophylist?leagueId=127291&start={start}");
            }

            _driver.FindElement(By.XPath("//table/tbody/tr/td/div/div/center/b[contains(.,'" + trophyToAssign.GetTrophyName() + "')]/parent::center/parent::div/div/a[contains(.,'Assign')]")).Click();
            _driver.WaitUntilElementExists(By.Id("assignTrophyDiv"));

            var dropdown = _driver.FindElement(By.Id("assignTeamId"));
            var dropdownSelect = new SelectElement(dropdown);
            dropdownSelect.SelectByText(team.TeamName);

            _driver.FindElement(By.Name("headline")).SendKeys(trophyToAssign.GetHeadline(team));
            _driver.FindElement(By.Name("reason")).SendKeys(trophyToAssign.GetReason(team));

            var showcase = _driver.FindElement(By.Id("isShowcase"));
            var showcaseDropdown = new SelectElement(showcase);
            showcaseDropdown.SelectByText("Yes");

            //_driver.FindElement(By.Name("btnSubmit")).Click();
            //_driver.WaitUntilElementExists(By.ClassName("bodyCopy"));

            Thread.Sleep(2000);

            return trophyToAssign;
        }
    }
}
