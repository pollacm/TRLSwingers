using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TRLNFL.Slack;
using TRLNFL.Teams;

namespace TRLNFL
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var options = new ChromeOptions();
            ////options.AddArgument("--headless");
            var driver = new ChromeDriver(options);
            driver.NavigateToUrl("https://football.fantasysports.yahoo.com/f1/900565/matchup?week=11&mid1=4&mid2=6");

            var benchToggleExists = driver.FindElements(By.XPath("//a[@id='bench-toggle']")).Any();
            if (benchToggleExists)
            {
                var benchToggle = driver.FindElement(By.XPath("//a[@id='bench-toggle']"));
                benchToggle.Click();
                Thread.Sleep(2000);
            }

            //check version
            var activeElement = FindElements(driver, "redzone-home", true);
            bool activeGame = activeElement.Any();
            //home
            var homeTeam2 = new Team(FindElement(driver, "redzone-home", activeGame).Text);
            var awayTeam2 = new Team(FindElement(driver, "redzone-away", activeGame).Text);

            var starters = FindElements(driver, "starters", activeGame);
            var benchPlayers = FindElements(driver, "bench", activeGame);

            var subs = new Dictionary<string, string>();
            subs.Add("D. Duvernay", "C. Olave");
            subs.Add("D. Pierce", "E. Elliott");
            subs.Add("J. Hargrave", "B. Wagner");
            subs.Add("A.J. Brown", "D. Carr");

            foreach (var starter in starters)
            {
                var rmlPlayer = new RmlPlayer.RmlPlayer();

                var hasName = FindElements(driver, "name-home", activeGame, starter);

                if (hasName.Any())
                {
                    //home starter
                    rmlPlayer.Name = FindElement(driver, "name-home", activeGame, starter).Text;
                    var projectionExists = FindElements(driver, "projection-home", activeGame, starter).Any();
                    if (projectionExists)
                    {
                        rmlPlayer.Projection = Convert.ToDecimal(FindElement(driver, "projection-home", activeGame, starter).Text);
                    }
                    else
                    {
                        rmlPlayer.Projection = 0;
                    }

                    var pointsExist = FindElements(driver, "points-home", activeGame, starter);
                    if (pointsExist.Any())
                    {
                        rmlPlayer.Points = Convert.ToDecimal(FindElement(driver, "points-home", activeGame, starter).Text);
                    }
                    else
                    {
                        rmlPlayer.Points = (decimal)0;
                    }

                    rmlPlayer.Position = FindElement(driver, "position-home", activeGame, starter).Text.Split('-')[1].Trim();
                    rmlPlayer.Starter = true;
                    homeTeam2.Players.Add(rmlPlayer);
                }

                hasName = FindElements(driver, "name-away", activeGame, starter);
                if (hasName.Any())
                {
                    //away starter
                    rmlPlayer = new RmlPlayer.RmlPlayer();
                    rmlPlayer.Name = FindElement(driver, "name-away", activeGame, starter).Text;

                    var projectionExists = FindElements(driver, "projection-away", activeGame, starter).Any();
                    if (projectionExists)
                    {
                        rmlPlayer.Projection = Convert.ToDecimal(FindElement(driver, "projection-away", activeGame, starter).Text);
                    }
                    else
                    {
                        rmlPlayer.Projection = 0;
                    }

                    var pointsExist = FindElements(driver, "points-away", activeGame, starter);
                    if (pointsExist.Any())
                    {
                        rmlPlayer.Points = Convert.ToDecimal(FindElement(driver, "points-away", activeGame, starter).Text);
                    }
                    else
                    {
                        rmlPlayer.Points = (decimal)0;
                    }
                    rmlPlayer.Position = FindElement(driver, "position-away", activeGame, starter).Text.Split('-')[1].Trim();
                    rmlPlayer.Starter = true;
                    awayTeam2.Players.Add(rmlPlayer);
                }

            }

            foreach (var benchPlayer in benchPlayers)
            {
                var hasName = FindElements(driver, "name-home", activeGame, benchPlayer);
                if (hasName.Any())
                {
                    var rmlPlayer = new RmlPlayer.RmlPlayer();
                    //home bench
                    rmlPlayer.Name = FindElement(driver, "name-home", activeGame, benchPlayer).Text;

                    var projectionExists = FindElements(driver, "projection-home", activeGame, benchPlayer).Any();
                    if (projectionExists)
                    {
                        rmlPlayer.Projection = Convert.ToDecimal(FindElement(driver, "projection-home", activeGame, benchPlayer).Text);
                    }
                    else
                    {
                        rmlPlayer.Projection = 0;
                    }

                    var pointsExist = FindElements(driver, "points-home", activeGame, benchPlayer);
                    if (pointsExist.Any())
                    {
                        rmlPlayer.Points = Convert.ToDecimal(FindElement(driver, "points-home", activeGame, benchPlayer).Text);
                    }
                    else
                    {
                        rmlPlayer.Points = (decimal)0;
                    }

                    rmlPlayer.Position = FindElement(driver, "position-home", activeGame, benchPlayer).Text.Split('-')[1].Trim();
                    rmlPlayer.Starter = false;
                    homeTeam2.Players.Add(rmlPlayer);
                }

                hasName = FindElements(driver, "name-away", activeGame, benchPlayer);
                if (hasName.Any())
                {
                    var rmlPlayer = new RmlPlayer.RmlPlayer();
                    //away bench
                    rmlPlayer.Name = FindElement(driver, "name-away", activeGame, benchPlayer).Text;

                    var projectionExists = FindElements(driver, "projection-away", activeGame, benchPlayer).Any();
                    if (projectionExists)
                    {
                        rmlPlayer.Projection = Convert.ToDecimal(FindElement(driver, "projection-away", activeGame, benchPlayer).Text);
                    }
                    else
                    {
                        rmlPlayer.Projection = 0;
                    }

                    var pointsExist = FindElements(driver, "points-away", activeGame, benchPlayer);
                    if (pointsExist.Any())
                    {
                        rmlPlayer.Points = Convert.ToDecimal(FindElement(driver, "points-away", activeGame, benchPlayer).Text);
                    }
                    else
                    {
                        rmlPlayer.Points = (decimal)0;
                    }

                    rmlPlayer.Position = FindElement(driver, "position-away", activeGame, benchPlayer).Text.Split('-')[1].Trim();
                    rmlPlayer.Starter = false;
                    awayTeam2.Players.Add(rmlPlayer);
                }
            }

            //sub players
            homeTeam2.SubPlayers(subs);
            homeTeam2.CalculatePoints();

            awayTeam2.SubPlayers(subs);
            awayTeam2.CalculatePoints();

            ////home
            //var homeTeam = new Team("Dalvin");
            
            ////qb - me
            //homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Hurts", Projection = (decimal?) 171.93, Points = (decimal) 141.9, SubPoints = (decimal?) null, SamePosition = true, SubName = "" });
            ////wr - me
            //homeTeam.Players.Add(new RmlPlayer.RmlPlayer{ Name = "Olave", Projection = (decimal?)null, Points = (decimal)59, SubPoints = (decimal?)148.00, SamePosition = true, SubName = "Hopkins" }); 
            ////rb - me
            //homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Cook", Projection = (decimal?)null, Points = (decimal)218.6, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            ////te - me
            //homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Schultz", Projection = (decimal?)null, Points = (decimal)107, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            ////wrt1 - me
            //homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Pierce", Projection = (decimal?)null, Points = (decimal)139.8, SubPoints = (decimal?)null, SamePosition = true, SubName = ""});
            ////wrt2 - me
            //homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Connor", Projection = (decimal?)null, Points = (decimal)143.4, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            ////sup - me
            //homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Spiller", Projection = (decimal?)null, Points = (decimal)4.6, SubPoints = (decimal?)165.4, SamePosition = true, SubName = "Foreman" });
            ////k - me
            //homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Myers", Projection = (decimal?)null, Points = (decimal)66.83, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            ////d - me
            //homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Walker", Projection = (decimal?)null, Points = (decimal)99, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            ////db - me
            //homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Elliot", Projection = (decimal?)null, Points = (decimal)15, SubPoints = (decimal?)81, SamePosition = true, SubName = "Kearse" });
            ////dl - me
            //homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Hunter", Projection = (decimal?)null, Points = (decimal)75, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            ////lb1 - me
            //homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Foye", Projection = (decimal?)null, Points = (decimal)105, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            ////lb2 - me
            //homeTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Long", Projection = (decimal?)null, Points = (decimal)85, SubPoints = (decimal?)190, SamePosition = true, SubName = "Shaq" });
            //homeTeam.CalculatePoints();


            ////away
            //var awayTeam = new Team("Opponent");
            ////qb - opponent
            //awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Tua", Projection = (decimal?)162.19, Points = (decimal)162.19, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            ////wr - opponent
            //awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Palmer", Projection = (decimal?)null, Points = (decimal)63, SubPoints = (decimal?)220.00, SamePosition = true, SubName = "Kirk" });
            ////rb - opponent
            //awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Williams", Projection = (decimal?)null, Points = (decimal)85.4, SubPoints = (decimal?)183.00, SamePosition = true, SubName = "Wilson" });
            ////te - opponent
            //awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Ertz", Projection = (decimal?)null, Points = (decimal)20, SubPoints = (decimal?)200, SamePosition = false, SubName = "Mclaurin" });
            ////wrt1 - opponent
            //awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Hubbard", Projection = (decimal?)null, Points = (decimal)16.00, SubPoints = (decimal?)128, SamePosition = true, SubName = "Herbert" });
            ////wrt2 - opponent
            //awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Moore", Projection = (decimal?)null, Points = (decimal)51.00, SubPoints = (decimal?)153.8, SamePosition = true, SubName = "Toney" });
            ////sup - opponent
            //awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Geno", Projection = (decimal?)null, Points = (decimal)148.81, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            ////k - opponent
            //awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Gano", Projection = (decimal?)null, Points = (decimal)66.83, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            ////d - opponent
            //awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Garrett", Projection = (decimal?)null, Points = (decimal)23, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            ////db - opponent
            //awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Grant", Projection = (decimal?)null, Points = (decimal)147, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            ////dl - opponent
            //awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Crosby", Projection = (decimal?)null, Points = (decimal)153, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            ////lb1 - opponent
            //awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Wagner", Projection = (decimal?)null, Points = (decimal)145, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            ////lb2 - opponent
            //awayTeam.Players.Add(new RmlPlayer.RmlPlayer { Name = "Collins", Projection = (decimal?)null, Points = (decimal)96, SubPoints = (decimal?)null, SamePosition = true, SubName = "" });
            //awayTeam.CalculatePoints();
            var x = 1;



            //Thread.Sleep(5000);

            //driver.Navigate().GoToUrl($"{RosterPage}");

            //driver.Close();
            //Refresh Pitcher Names
        }

        private static IWebElement FindElement(ChromeDriver driver, string element, bool active, IWebElement additionalPath = null)
        {
            switch (element.ToLower())
            {
                case "redzone-home":
                    return active ? driver.FindElement(By.XPath("//div[@class='RedZone']/div/div/div[1]/div/div/a")) : driver.FindElement(By.XPath("//section[@id='matchup-header']/div/div[1]/div[1]/div/div[2]/div[1]/a"));
                case "redzone-away":
                    return active ? driver.FindElement(By.XPath("//div[@class='RedZone']/div/div/div[3]/div/div/a")) : driver.FindElement(By.XPath("//section[@id='matchup-header']/div/div[1]/div[3]/div/div[2]/div[1]/a"));
                case "name-home":
                    return active ? additionalPath.FindElement(By.XPath("./td[2]/div/a")) : additionalPath.FindElement(By.XPath("./td[2]/div/div/div/div[1]/a"));
                case "projection-home":
                    return active ? additionalPath.FindElement(By.XPath("./td[3]/div/span/span")) : additionalPath.FindElement(By.XPath("./td[3]/div/div"));
                case "points-home":
                    return active ? additionalPath.FindElement(By.XPath("./td[4]/div/span/div/span/span")) : additionalPath.FindElement(By.XPath("./td[4]/div/a"));
                case "position-home":
                    return active ? additionalPath.FindElement(By.XPath("./td[2]/div/span")) : additionalPath.FindElement(By.XPath("./td[2]/div/div/div[1]/div/span"));
                case "name-away":
                    return active ? additionalPath.FindElement(By.XPath("./td[8]/div/a")) : additionalPath.FindElement(By.XPath("./td[10]/div/div/div/div[1]/a"));
                case "projection-away":
                    return active ? additionalPath.FindElement(By.XPath("./td[7]/div/span/span")) : additionalPath.FindElement(By.XPath("./td[9]/div/div"));
                case "points-away":
                    return active ? additionalPath.FindElement(By.XPath("./td[6]/div/span/div/span/span")) : additionalPath.FindElement(By.XPath("./td[8]/div/a"));
                case "position-away":
                    return active ? additionalPath.FindElement(By.XPath("./td[8]/div/span")) : additionalPath.FindElement(By.XPath("./td[10]/div/div/div[1]/div/span"));
                default:
                    return driver.FindElement(By.XPath(""));
            }
        }

        private static ReadOnlyCollection<IWebElement> FindElements(ChromeDriver driver, string element, bool active, IWebElement additionalPath = null)
        {
            switch (element.ToLower())
            {
                case "redzone-home":
                    return active ? driver.FindElements(By.XPath("//div[@class='RedZone']/div/div/div[1]/div/div/a")) : driver.FindElements(By.XPath(""));
                case "starters":
                    return active ? driver.FindElements(By.XPath("//table[1]/tbody/tr")) : driver.FindElements(By.XPath("//table[@id='statTable1']/tbody/tr"));
                case "bench":
                    return active ? driver.FindElements(By.XPath("//table[2]/tbody/tr")) : driver.FindElements(By.XPath("//table[@id='statTable2']/tbody/tr"));
                case "name-home":
                    return active ? additionalPath.FindElements(By.XPath("./td[2]/div/a")) : additionalPath.FindElements(By.XPath("./td[2]/div/div/div/div[1]/a"));
                case "projection-home":
                    return active ? additionalPath.FindElements(By.XPath("./td[3]/div/span/span")) : additionalPath.FindElements(By.XPath("./td[3]/div/div"));
                case "points-home":
                    return active ? additionalPath.FindElements(By.XPath("./td[4]/div/span/div/span/span")) : additionalPath.FindElements(By.XPath("./td[4]/div/a"));
                case "name-away":
                    return active ? additionalPath.FindElements(By.XPath("./td[8]/div/a")) : additionalPath.FindElements(By.XPath("./td[10]/div/div/div/div[1]/a"));
                case "points-away":
                    return active ? additionalPath.FindElements(By.XPath("./td[6]/div/span/div/span/span")) : additionalPath.FindElements(By.XPath("./td[8]/div/a"));
                case "projection-away":
                    return active ? additionalPath.FindElements(By.XPath("./td[7]/div/span/span")) : additionalPath.FindElements(By.XPath("./td[9]/div/div"));
                default:
                    return driver.FindElements(By.XPath(""));
            }
        }
    }
}