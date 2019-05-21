using TRLSwingers.Teams;

namespace TRLSwingers.Trophies
{
    public class BottomRankedSeasonTrophy : ITrophy
    {
        public BottomRankedSeasonTrophy(Team team, string additionalInfo)
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
            return TrophyConstants.Number12Ranked;
        }

        public string LeaguePageText()
        {
            return GetTrophyName().ToUpper() + @" AWARD GOES TO...";
        }

        public string GetTrophyBody()
        {
            return Team.TeamName.ToUpper();
        }

        public string GetHeadline(Team team)
        {
            return $"For finishing the season in last place!!!!!";
        }

        public string GetReason(Team team)
        {
            return string.Empty;
        }
    }
}
