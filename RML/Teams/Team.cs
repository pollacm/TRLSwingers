using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Xml.Schema;

namespace TRLNFL.Teams
{
    public class Team
    {
        public Team(string teamName)
        {
            TeamName = teamName;
            Players = new List<RmlPlayer.RmlPlayer>();
        }

        public string TeamName { get; set; }
        public decimal TeamPoints { get; set; }
        public decimal ProjectedPoints { get; set; }
        public List<RmlPlayer.RmlPlayer> Players { get; set; }

        public string CalculatePoints()
        {
            var calculatedPoints = string.Empty;
            Debug.WriteLine(TeamName);
            Debug.WriteLine("----------------------------------------");
            foreach (var player in Players.Where(p => p.Starter))
            {
                TeamPoints += player.PointWithSub;
                if (player.Points == 0 && player.Projection.HasValue)
                {
                    ProjectedPoints += player.Projection.Value;
                }

                var x = player.ToString();
            }
            Debug.WriteLine("----------------------------------------");
            Debug.WriteLine($"TOTAL: {TeamPoints} {(ProjectedPoints > 0 ? " + (" + ProjectedPoints + ") = " + (TeamPoints + ProjectedPoints) : "")}");
            Debug.WriteLine("----------------------------------------");
            Debug.WriteLine("----------------------------------------");

            return calculatedPoints;
        }

        public void SubPlayers(Dictionary<string, string> subs)
        {
            foreach (var player in Players.Where(p => p.Starter))
            {
                if (subs.ContainsKey(player.Name))
                {
                    var subPlayer = subs.FirstOrDefault(s => s.Key == player.Name);
                    if (subPlayer.Key != string.Empty && subPlayer.Value != string.Empty)
                    {
                        var actualPlayerToSub = Players.FirstOrDefault(p => p.Name == subPlayer.Value);
                        if (actualPlayerToSub != null)
                        {
                            player.SubName = actualPlayerToSub.Name;
                            player.SubPoints = actualPlayerToSub.Points;
                            player.SamePosition = actualPlayerToSub.Position.Contains(player.Position) || player.Position.Contains(actualPlayerToSub.Position);
                        }
                    }
                }
            }
        }
    }
}
