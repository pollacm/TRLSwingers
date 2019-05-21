using Newtonsoft.Json;
using TRLSwingers.Teams;

namespace TRLSwingers.Trophies
{
    public class AfcDivisionChampionshipTrophy : ITrophy
    {
        public AfcDivisionChampionshipTrophy(Team team, string additionalInfo)
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
            return TrophyConstants.AfcChamp;
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
            var op = JsonConvert.DeserializeObject<PlayerOfTheWeek>(AdditionalInfo);
            return $"For winning the AFC Division!!!!!";
        }

        public string GetReason(Team team)
        {
            return string.Empty;
        }
    }
}
