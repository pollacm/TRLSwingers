using TRLSwingers.Teams;

namespace TRLSwingers.Trophies
{
    public class LoserOfTheWeekTrophy : ITrophy
    {
        public LoserOfTheWeekTrophy(Team team, string additionalInfo)
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
            return TrophyConstants.LargestMarginWeekLoser;
        }

        public string LeaguePageText()
        {
            return "THE " + GetTrophyName().ToUpper() + @" AWARD";
        }

        public string GetTrophyBody()
        {
            return Team.TeamName.ToUpper();
        }

        public string GetHeadline(Team team)
        {
            return $"For losing by {AdditionalInfo} points!!!!!";
        }

        public string GetReason(Team team)
        {
            return string.Empty;
        }
    }
}