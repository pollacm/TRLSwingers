using System.Collections.Generic;
using System.Linq;
using TRLSwingers.Weeks;

namespace TRLSwingers.PowerRankings
{
    public class PowerRankingGenerator
    {
        private readonly List<Week> _weeks;
        private readonly int _currentWeek;

        public PowerRankingGenerator(List<Week> weeks, int currentWeek)
        {
            _weeks = weeks;
            _currentWeek = currentWeek;
        }

        public List<PowerRanking> GeneratePowerRankings()
        {
            var powerRankings = new List<PowerRanking>();
            //previous power rankings
            for (int week = _currentWeek - 1; week > _currentWeek - 4; week--)
            {
                var currentWeek = _weeks.SingleOrDefault(w => w.WeekNumber == week);

                if(currentWeek != null)
                {
                    foreach (var score in currentWeek.Scores)
                    {
                        //HomeTeam
                        if (!powerRankings.Any(p => p.TeamName == score.HomeTeam.TeamName))
                        {
                            var powerRanking = new PowerRanking();
                            powerRanking.TeamName = score.HomeTeam.TeamName;
                            powerRanking.TeamAbbreviation = score.HomeTeam.TeamAbbreviation;
                            powerRanking.PreviousTotal = score.HomeTeam.GetPointsForWeek;
                            powerRankings.Add(powerRanking);
                        }
                        else
                        {
                            var powerRanking = powerRankings.Single(p => p.TeamName == score.HomeTeam.TeamName);
                            powerRanking.PreviousTotal += score.HomeTeam.GetPointsForWeek;
                        }

                        //AwayTeam
                        if (!powerRankings.Any(p => p.TeamName == score.AwayTeam.TeamName))
                        {
                            var powerRanking = new PowerRanking();
                            powerRanking.TeamName = score.AwayTeam.TeamName;
                            powerRanking.TeamAbbreviation = score.AwayTeam.TeamAbbreviation;
                            powerRanking.PreviousTotal = score.AwayTeam.GetPointsForWeek;
                            powerRankings.Add(powerRanking);
                        }
                        else
                        {
                            var powerRanking = powerRankings.Single(p => p.TeamName == score.AwayTeam.TeamName);
                            powerRanking.PreviousTotal += score.AwayTeam.GetPointsForWeek;
                        }
                    }
                }                
            }

            //current power rankings
            for (int week = _currentWeek; week > _currentWeek - 3; week--)
            {
                var currentWeek = _weeks.SingleOrDefault(w => w.WeekNumber == week);

                if (currentWeek != null)
                {
                    foreach (var score in currentWeek.Scores)
                    {

                        //HomeTeam
                        if (!powerRankings.Any(p => p.TeamName == score.HomeTeam.TeamName))
                        {
                            var powerRanking = new PowerRanking();
                            powerRanking.TeamName = score.HomeTeam.TeamName;
                            powerRanking.TeamAbbreviation = score.HomeTeam.TeamAbbreviation;
                            powerRanking.CurrentTotal = score.HomeTeam.GetPointsForWeek;
                            powerRankings.Add(powerRanking);
                        }
                        else
                        {
                            var powerRanking = powerRankings.Single(p => p.TeamName == score.HomeTeam.TeamName);
                            powerRanking.CurrentTotal += score.HomeTeam.GetPointsForWeek;
                        }

                        //AwayTeam
                        if (!powerRankings.Any(p => p.TeamName == score.AwayTeam.TeamName))
                        {
                            var powerRanking = new PowerRanking();
                            powerRanking.TeamName = score.AwayTeam.TeamName;
                            powerRanking.TeamAbbreviation = score.AwayTeam.TeamAbbreviation;
                            powerRanking.CurrentTotal = score.AwayTeam.GetPointsForWeek;
                            powerRankings.Add(powerRanking);
                        }
                        else
                        {
                            var powerRanking = powerRankings.Single(p => p.TeamName == score.AwayTeam.TeamName);
                            powerRanking.CurrentTotal += score.AwayTeam.GetPointsForWeek;
                        }
                    }
                }
            }

            var point = 0m;
            var rank = 0;
            foreach (var powerRanking in powerRankings.OrderByDescending(p => p.PreviousTotal))
            {
                if(point != powerRanking.PreviousTotal)
                {
                    rank++;                    
                }

                powerRanking.PreviousPowerRanking = rank;
            }

            point = 0m;
            rank = 0;
            foreach (var powerRanking in powerRankings.OrderByDescending(p => p.CurrentTotal))
            {
                if (point != powerRanking.CurrentTotal)
                {
                    rank++;
                }

                powerRanking.CurrentPowerRanking = rank;
            }

            return powerRankings;
        }
    }
}
