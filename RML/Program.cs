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
using TRLSwingers.Pitchers;
using TRLSwingers.Slack;

namespace TRLSwingers
{
    internal class Program
    {
        private static readonly string RosterPage = "https://baseball.fantasysports.yahoo.com/b1/125071/7";
        private static readonly string PlayerPage = "https://baseball.fantasysports.yahoo.com/b1/125071/players?status=A&pos=S_P&cut_type=33&stat1=S_AL30&myteam=0&sort=PTS&sdir=1";
        private static List<string> AddPlayerPages = new List<string>
        {
            "https://baseball.fantasysports.yahoo.com/b1/125071/addplayer?apid=7599",
            "https://baseball.fantasysports.yahoo.com/b1/125071/addplayer?apid=7701",
            "https://baseball.fantasysports.yahoo.com/b1/125071/addplayer?apid=10597"
        };
        private static List<string> PitchersToNotReplace = new List<string>()
        {
            "Berríos",
            "Verlander",
            "Berríos",
            "Clevinger",
            "Montas",
            "Lynn",
            "Giolito"
        };
        private static readonly string EmailAddressForLogin = "pollacm";

        //private static readonly string RosterPage = "https://baseball.fantasysports.yahoo.com/b1/189961/1";
        //private static readonly string PlayerPage = "https://baseball.fantasysports.yahoo.com/b1/189961/players?status=A&pos=S_P&cut_type=33&stat1=S_AL30&myteam=0&sort=AR&sdir=1";
        //private static List<string> AddPlayerPages = new List<string>
        //{
        //    "https://baseball.fantasysports.yahoo.com/b1/189961/addplayer?apid=10131",
        //    "https://baseball.fantasysports.yahoo.com/b1/189961/addplayer?apid=8270"
        //};
        //private static List<string> PitchersToNotReplace = new List<string>()
        //{
        //    "Verlander",
        //    "Berríos",
        //    "Clevinger",
        //    "Jansen",
        //    "Montas",
        //    "Lynn",
        //    "Giolito",
        //    "Jansen",
        //    "Doolittle"
        //};
        private static void Main(string[] args)
        {
            var options = new ChromeOptions();
            //options.AddArgument("--headless");
            var driver = new ChromeDriver(options);

            //new ReturnerBuilder(driver).GenerateReturners();

            //login
            driver.Navigate().GoToUrl($"https://login.yahoo.com");
            driver.WaitUntilElementExists(By.XPath("//input[@id='login-username']"));

            var usernameField = driver.FindElement(By.XPath("//input[@id='login-username']"));
            usernameField.SendKeys(EmailAddressForLogin);

            var nextButton = driver.FindElement(By.XPath("//input[@id='login-signin']"));
            nextButton.Submit();

            driver.WaitUntilElementExists(By.XPath("//input[@id='login-passwd']"));

            var passwordField = driver.FindElement(By.XPath("//input[@id='login-passwd']"));
            passwordField.SendKeys("grip1334");
            passwordField.SendKeys(Keys.Enter);
            Thread.Sleep(5000);

            driver.Navigate().GoToUrl($"{RosterPage}");
            var canAddPlayers = false;
            var currentDateText = DateTime.Now.ToString("MMM dd");

            while (!canAddPlayers)
            {
                var defaultDateText = driver.FindElement(By.XPath("//span[contains(@class, 'flyout-title')]")).Text;
                if (defaultDateText.Contains(currentDateText))
                {
                    canAddPlayers = true;
                }
                else
                {
                    Thread.Sleep(new TimeSpan(0, 0, 0, 5));
                    driver.Navigate().GoToUrl($"{RosterPage}");
                }
            }

            //AddPitchersDirect(driver);

            var pitcher1 = new Pitcher
            {
                Name = "Marco Gonzales"
            };

            AddPitcher(driver, pitcher1);

            var pitcher4 = new Pitcher
            {
                Name = "Steven Matz"
            };

            AddPitcher(driver, pitcher4);

            var pitcher2 = new Pitcher
            {
                Name = "Andrew Heaney"
            };

            AddPitcher(driver, pitcher2);
            

            var pitcher5 = new Pitcher
            {
                Name = "Brad Keller"
            };

            AddPitcher(driver, pitcher5);

            var pitcher3 = new Pitcher
            {
                Name = "Trevor Williams"
            };

            AddPitcher(driver, pitcher3);

            var pitcher6 = new Pitcher
            {
                Name = "Zach Davies"
            };

            AddPitcher(driver, pitcher6);

            //driver.Navigate().GoToUrl($"{PlayerPage}");
            //var dataPitchers = driver.FindElements(By.XPath("//table[contains(@class, 'Table-interactive')]/tbody/tr"));
            //var pitchers = new List<Pitchers.Pitcher>();
            //var totalPitchers = dataPitchers.Count > 10 ? 10 : dataPitchers.Count;
            //for (var i = 0; i < totalPitchers; i++)
            //{
            //    //table[contains(@class, 'Table-interactive')]/tbody/tr[1]/td[2]
            //    var dataPitcher = dataPitchers[i];
            //    var pitcher = new Pitchers.Pitcher();

            //    //table[contains(@class, 'Table-interactive')]/tbody/tr[1]/td[2]/div/div/div[contains(@class, 'ysf-player-name')]/a
            //    pitcher.Name = dataPitcher.FindElement(By.XPath("./td[2]/div/div/div[contains(@class, 'ysf-player-name')]/a")).Text;
            //    pitcher.Position = dataPitcher.FindElement(By.XPath("./td[2]/div/div/div[contains(@class, 'ysf-player-name')]/span")).Text;
            //    //table[contains(@class, 'Table-interactive')]/tbody/tr[1]/td[8]/div/span

            //    var averageDatas = dataPitcher.FindElements(By.XPath("./td[8]/div/span"));
            //    IWebElement averageData;

            //    //FOR TESTING
            //    if (averageDatas.Any())
            //    {
            //        averageData = averageDatas.First();
            //    }
            //    else
            //    {
            //        averageData = dataPitcher.FindElement(By.XPath("./td[9]/div"));
            //    }

            //    pitcher.Average = double.TryParse(averageData.Text, out var average) ? average : 0;

            //    var playerNote = dataPitcher.FindElement(By.XPath("./td[2]/div/div/span/a"));

            //    ClickPlayerNote(playerNote);

            //    //Thread.Sleep(new TimeSpan(0, 0, 0, 0, 500));

            //    var stars = driver.FindElements(By.XPath("//div[(contains(@class, 'yui3-ysplayernote-surround-R')) and not(contains(@class, 'yui3-ysplayernote-hidden'))]/div/div[2]/div/div/div/div/div/div/div/p[contains(@class, 'rating-value')]/span[contains(@class, 'F-negative')]"));
            //    //var availableStars = driver.FindElements(By.XPath("//div[(contains(@class, 'yui3-ysplayernote-surround-R')) and not(contains(@class, 'yui3-ysplayernote-hidden'))]/div/div[2]/div/div/div/div/div/div/div/p[contains(@class, 'rating-value')]/span[contains(@class, 'F-icon')]"));
            //    var timesWaitingForPlayerNoteToOpen = 0;
            //    var getStarsCount = 0;
            //    while (stars.Count == 0)
            //    {
            //        Thread.Sleep(new TimeSpan(0, 0, 0, 0, 50));
            //        stars = driver.FindElements(By.XPath("//div[(contains(@class, 'yui3-ysplayernote-surround-R')) and not(contains(@class, 'yui3-ysplayernote-hidden'))]/div/div[2]/div/div/div/div/div/div/div/p[contains(@class, 'rating-value')]/span[contains(@class, 'F-negative')]"));
            //        //availableStars = driver.FindElements(By.XPath("//div[(contains(@class, 'yui3-ysplayernote-surround-R')) and not(contains(@class, 'yui3-ysplayernote-hidden'))]/div/div[2]/div/div/div/div/div/div/div/p[contains(@class, 'rating-value')]/span[contains(@class, 'F-icon')]"));

            //        timesWaitingForPlayerNoteToOpen++;
            //        if (timesWaitingForPlayerNoteToOpen % 5 == 0)
            //        {
            //            ClickPlayerNote(playerNote);
            //        }

            //        getStarsCount++;

            //        if (getStarsCount == 10)
            //        {
            //            break;
            //        }
            //    }
            //    pitcher.Stars = stars.Count;

            //    pitchers.Add(pitcher);
            //}

            //pitchers = pitchers.OrderByDescending(p => p.Value).ToList();

            //var pitchersSwapped = 0;
            //foreach (var pitcher in pitchers)
            //{
            //    if (AddPitcher(driver, pitcher))
            //    {
            //        pitchersSwapped++;
            //    }

            //    if (pitchersSwapped == 2)
            //    {
            //        break;
            //    }
            //}

            ////var reliefPitcher = pitchers.FirstOrDefault(p => !p.Added && p.Position.Contains("RP"));
            ////AddPitcher(driver, reliefPitcher);

            //WriteOutSlackMessage(pitchers);
            //start active players
            driver.Navigate().GoToUrl($"{RosterPage}");
            driver.FindElement(By.XPath("//a[contains(@class, 'start-active-players')]")).Click();

            Thread.Sleep(5000);

            driver.Navigate().GoToUrl($"{RosterPage}");

            driver.Close();
            //Refresh Pitcher Names
        }

        private static void AddPitchersDirect(ChromeDriver driver)
        {
            foreach (var pitcherDirect in AddPlayerPages)
            {
                driver.Navigate().GoToUrl($"{pitcherDirect}");

                var hasPitchersThatCanBeRemoved = ClickButtonToRemovePitcher(driver);

                if (hasPitchersThatCanBeRemoved)
                {
                    var addedRemovePitcher = false;
                    var addedRemovePitcherCount = 0;
                    var totalCount = 0;
                    while (!addedRemovePitcher)
                    {
                        if (totalCount == 100)
                        {
                            break;
                        }
                        if (addedRemovePitcherCount % 5 == 0)
                        {
                            ClickButtonToRemovePitcher(driver);
                        }

                        try
                        {
                            driver.FindElement(By.XPath("//input[@id='submit-add-drop-button']")).Submit();
                            addedRemovePitcher = true;
                        }
                        catch
                        {
                            Thread.Sleep(new TimeSpan(0, 0, 0, 0, 500));
                        }

                        addedRemovePitcherCount++;
                        totalCount++;
                        Thread.Sleep(new TimeSpan(0, 0, 0, 0, 500));
                    }
                }
            }
        }

        private static bool AddPitcher(ChromeDriver driver, Pitcher pitcher)
        {
            if (pitcher.LastName == string.Empty)
                return false;

            var pitcherSwapped = false;
            driver.Navigate().GoToUrl($"{PlayerPage}");
            //find that pitcher
            var addPitcherButtons =
                driver.FindElements(By.XPath($"//table[contains(@class, 'Table-interactive')]/tbody/tr/td[2]/div/div/div/a[contains(text(), '{pitcher.LastName}')]/parent::div/parent::div/parent::div/parent::td/parent::tr/td[3]/div/a"));
            if (addPitcherButtons.Any())
            {
                var addPitchersButtonSuccess = false;
                while (!addPitchersButtonSuccess)
                {
                    try
                    {
                        addPitcherButtons.First().Click();
                        addPitchersButtonSuccess = true;
                    }
                    catch
                    {
                        Thread.Sleep(new TimeSpan(0, 0, 0, 0, 500));
                    }
                }

                var hasPitchersThatCanBeRemoved = ClickButtonToRemovePitcher(driver);

                if (hasPitchersThatCanBeRemoved)
                {
                    var addedRemovePitcher = false;
                    var addedRemovePitcherCount = 1;
                    while (!addedRemovePitcher)
                    {
                        if (addedRemovePitcherCount % 5 == 0)
                        {
                            ClickButtonToRemovePitcher(driver);
                        }

                        try
                        {
                            driver.FindElement(By.XPath("//input[@id='submit-add-drop-button']")).Submit();
                            Thread.Sleep(new TimeSpan(0, 0, 0, 1));
                            addedRemovePitcher = true;
                        }
                        catch
                        {
                            Thread.Sleep(new TimeSpan(0, 0, 0, 0, 500));
                        }

                        addedRemovePitcherCount++;
                    }

                    var confirmationMessage = driver.FindElements(By.XPath("//div[contains(@class,'Alert-confirmation')]"));
                    if (confirmationMessage.Any())
                    {
                        pitcherSwapped = true;
                        pitcher.Added = true;
                    }
                }
            }

            return pitcherSwapped;
        }

        private static void ClickPlayerNote(IWebElement playerNote)
        {
            var completedNoteOpen = false;
            while (!completedNoteOpen)
            {
                try
                {
                    playerNote.Click();
                    completedNoteOpen = true;
                }
                catch
                {
                    Thread.Sleep(new TimeSpan(0, 0, 0, 0, 250));
                }
            }
        }

        private static bool ClickButtonToRemovePitcher(ChromeDriver driver)
        {
            var hasPitchersThatCanBeRemoved = false;
            var containsStringWhenRemovingPitchers = string.Empty;
            var firstRemovedPitcherDone = false;
            foreach (var pitcherToRemove in PitchersToNotReplace)
            {
                if (!firstRemovedPitcherDone)
                {
                    containsStringWhenRemovingPitchers += $"contains(text(), '{pitcherToRemove}')";
                }
                else
                {
                    containsStringWhenRemovingPitchers += $" or contains(text(), '{pitcherToRemove}')";
                }

                firstRemovedPitcherDone = true;
            }
            //table[@id='statTable-drop-2']/tbody/tr/td[2]/div/div/div/div/a/parent::div/parent::div/parent::div/parent::div/parent::td/div/div/div[2]/div/span[not(span)]/parent::div/parent::div/parent::div/parent::div/parent::td/parent::tr

            //table[@id='statTable-drop-2']/tbody/tr/td[2]/div/div/div/div/a[contains(text(), 'Bundy') or contains(text(), 'Richards')]/parent::div/parent::div/parent::div/parent::div/parent::td/parent::tr/td[2]/div/div/div/div/a/parent::div/parent::div/parent::div/parent::div/parent::td/div/div/div[2]/div/span[not(span)]/parent::div/parent::div/parent::div/parent::div/parent::td/parent::tr/td[1]/div/button


            //(contains(text(), 'Verlander') or contains(text(), 'Berríos') or contains(text(), 'Vázquez') or contains(text(), 'Jansen') or contains(text(), 'Clevinger')
            //table[@id='statTable-drop-2']/tbody/tr/td[2]/div/div/div/div/a/parent::div/parent::div/parent::div/parent::div/parent::td/div/div/div[2]/div/span[not(span)]/parent::div/parent::div/parent::div/parent::div/parent::td/parent::tr/td[2]/div/div/div/div/a[not({containsStringWhenRemovingPitchers})]

            //table[@id='statTable-drop-2']/tbody/tr/td[2]/div/div/div/div/a/parent::div/parent::div/parent::div/parent::div/parent::td/div/div/div[2]/div/span[not(span)]/parent::div/parent::div/parent::div/parent::div/parent::td/parent::tr/td[2]/div/div/div/div/a[not(contains(text(), 'Verlander'))]/parent::div/parent::div/parent::div/parent::div/parent::td/parent::tr/td[1]/div/button
            //var selector = $"//table[@id='statTable-drop-2']/tbody/tr/td[2]/div/div/div/div/a[{containsStringWhenRemovingPitchers}]/parent::div/parent::div/parent::div/parent::div/parent::td/parent::tr/td[1]/div/button";
            var selector = $"//table[@id='statTable-drop-2']/tbody/tr/td[2]/div/div/div/div/a/parent::div/parent::div/parent::div/parent::div/parent::td/div/div/div[2]/div/span[not(span)]/parent::div/parent::div/parent::div/parent::div/parent::td/parent::tr/td[2]/div/div/div/div/a[not({containsStringWhenRemovingPitchers})]/parent::div/parent::div/parent::div/parent::div/parent::td/parent::tr/td[1]/div/button";
            //table[@id='statTable-drop-2']/tbody/tr/td[2]/div/div/div/div/a/parent::div/parent::div/parent::div/parent::div/parent::td/div/div/div[2]/div/span[not(span)]/parent::span/parent::div/parent::div/parent::div
            ReadOnlyCollection<IWebElement> buttonsToRemovePitcher = driver.FindElements(By.XPath($"{selector}"));
            if (buttonsToRemovePitcher.Any())
            {
                var addedRemovePitcher = false;
                Thread.Sleep(new TimeSpan(0, 0, 0, 1));
                while (!addedRemovePitcher)
                {
                    try
                    {
                        buttonsToRemovePitcher.First().Click();
                        addedRemovePitcher = true;
                    }
                    catch
                    {
                        Thread.Sleep(new TimeSpan(0, 0, 0, 0, 250));
                    }
                }

                hasPitchersThatCanBeRemoved = true;
            }

            return hasPitchersThatCanBeRemoved;
        }

        private static void WriteOutSlackMessage(List<Pitcher> pitchers)
        {
            int rank = 1;
            SlackClient client = new SlackClient();
            StringBuilder message = new StringBuilder();
            foreach (var pitcher in pitchers)
            {
                message.Append($"RANK:\t{rank}\t{pitcher}\n");
                rank++;
            }

            client.PostMessage(message.ToString());
        }
    }
}