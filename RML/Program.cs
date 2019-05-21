using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TRLSwingers
{
    internal class Program
    {
        private static readonly int year = 2018;
        private static readonly int week = 11;

        private static List<string> PitchersToNotReplace = new List<string>()
        {
            "Verlander",
            "Berríos"
        };

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
            usernameField.SendKeys("pollacm");

            var nextButton = driver.FindElement(By.XPath("//input[@id='login-signin']"));
            nextButton.Submit();

            driver.WaitUntilElementExists(By.XPath("//input[@id='login-passwd']"));

            var passwordField = driver.FindElement(By.XPath("//input[@id='login-passwd']"));
            passwordField.SendKeys("grip1334");
            passwordField.SendKeys(Keys.Enter);
            Thread.Sleep(10000);

            driver.Navigate().GoToUrl($"https://baseball.fantasysports.yahoo.com/b1/125071/7");
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
                    Thread.Sleep(new TimeSpan(0, 0, 0, 10));
                    driver.Navigate().GoToUrl($"https://baseball.fantasysports.yahoo.com/b1/125071/7");
                }
            }

            driver.Navigate().GoToUrl($"https://baseball.fantasysports.yahoo.com/b1/125071/players?status=A&pos=S_P&cut_type=33&stat1=S_AL30&myteam=0&sort=PTS&sdir=1");
            var dataPitchers = driver.FindElements(By.XPath("//table[contains(@class, 'Table-interactive')]/tbody/tr"));
            var pitchers = new List<Pitchers.Pitcher>();
            var totalPitchers = dataPitchers.Count > 10 ? 10 : dataPitchers.Count;
            for (var i = 0; i < totalPitchers; i++)
            {
                //table[contains(@class, 'Table-interactive')]/tbody/tr[1]/td[2]
                var dataPitcher = dataPitchers[i];
                var pitcher = new Pitchers.Pitcher();

                //table[contains(@class, 'Table-interactive')]/tbody/tr[1]/td[2]/div/div/div[contains(@class, 'ysf-player-name')]/a
                pitcher.Name = dataPitcher.FindElement(By.XPath("./td[2]/div/div/div[contains(@class, 'ysf-player-name')]/a")).Text;
                //table[contains(@class, 'Table-interactive')]/tbody/tr[1]/td[8]/div/span
                pitcher.Average = double.TryParse(dataPitcher.FindElement(By.XPath("./td[8]/div/span")).Text, out var average) ? average : 0;

                var playerNote = dataPitcher.FindElement(By.XPath("./td[2]/div/div/span/a"));
                playerNote.Click();

                Thread.Sleep(new TimeSpan(0, 0, 0, 0, 500));

                var stars = driver.FindElements(By.XPath("//p[contains(@class, 'rating-value')]/span[contains(@class, 'F-negative')]"));
                pitcher.Average = stars.Count;

                pitchers.Add(pitcher);
            }

            pitchers = pitchers.OrderByDescending(p => p.Value).ToList();

            var pitchersSwapped = 0;
            foreach (var pitcher in pitchers)
            {
                driver.Navigate().GoToUrl($"https://baseball.fantasysports.yahoo.com/b1/125071/players?status=A&pos=S_P&cut_type=33&stat1=S_AL30&myteam=0&sort=PTS&sdir=1");
                //find that pitcher
                dataPitchers = driver.FindElements(By.XPath("//table[contains(@class, 'Table-interactive')]/tbody/tr"));
                for (var i = 0; i < totalPitchers; i++)
                {
                    var dataPitcher = dataPitchers[i];

                    //table[contains(@class, 'Table-interactive')]/tbody/tr[1]/td[2]/div/div/div[contains(@class, 'ysf-player-name')]/a
                    var dataPitcherName = dataPitcher.FindElement(By.XPath("./td[2]/div/div/div[contains(@class, 'ysf-player-name')]/a")).Text;
                    if (pitcher.Name == dataPitcherName && !PitchersToNotReplace.Contains(dataPitcherName))
                    {
                        //click add
                        dataPitcher.FindElement(By.XPath("./td[3]/div/a")).Click();

                        //click drop for pitcher to drop
                        //table[@id='statTable1']/tbody/tr[1]/td[3]/div/div/div/span
                        var dataMyPitchers = driver.FindElements(By.XPath("//table[@id='statTable-drop-2']/tbody/tr"));
                        foreach (var dataMyPitcher in dataMyPitchers)
                        {
                            var isPitcher = dataMyPitcher.FindElement(By.XPath("./td[2]/div/div/div/div/span")).Text.Contains(" - SP");
                            var dataMyPitcherName = dataMyPitcher.FindElement(By.XPath("./td[2]/div/div/div/div/a")).Text;
                            var pitcherLastName = dataMyPitcherName.Split(' ').Last();
                            var isAlreadyInList = PitchersToNotReplace.Contains(pitcherLastName);

                            if (isPitcher && !isAlreadyInList)
                            {
                                dataMyPitcher.FindElement(By.XPath("./td[1]/div/button")).Click();

                                //driver.FindElement(By.XPath("//input[@id='submit-add-drop-button']")).Submit();

                                var pitcherToBeAddedLastName = dataPitcherName.Split(' ').Last();
                                PitchersToNotReplace.Add(pitcherToBeAddedLastName);
                            }
                        }
                    }
                }

                pitchersSwapped++;
                if (pitchersSwapped == 2)
                {
                    break;
                }
            }

            //start active players
            driver.Navigate().GoToUrl($"https://baseball.fantasysports.yahoo.com/b1/125071/7");
            driver.FindElement(By.XPath("//a[contains(@class, 'start-active-players')]")).Click();
        }

        //need to add to slack channel and send updates for adds
    }
}