using Newtonsoft.Json;
using TRLSwingers.Standings;
using TRLSwingers.Teams;

namespace TRLSwingers.Trophies
{
    public class HighestScoringSeasonTrophy : ITrophy
    {
        public HighestScoringSeasonTrophy(Team team, string additionalInfo)
        {
            Team = team;
            AdditionalInfo = additionalInfo;
            BuildTogether = true;
        }
        public Team Team { get; set; }
        public string AdditionalInfo { get; }
        public bool BuildTogether { get; }

        public string GetTrophyName()
        {
            return TrophyConstants.HighestScoringTeamOfTheYear;
        }

        public string LeaguePageText()
        {
            return "THE " + GetTrophyName().ToUpper() + @" AWARD GOES TO...";
        }

        public string GetTrophyBody()
        {
            return Team.TeamName.ToUpper();
        }

        public string GetHeadline(Team team)
        {
            var standing = JsonConvert.DeserializeObject<Standing>(AdditionalInfo);
            return $"For putting up a total of {standing.PointsFor} throughout the season!!!!!";
        }

        public string GetReason(Team team)
        {
            return string.Empty;
        }
    }
}
