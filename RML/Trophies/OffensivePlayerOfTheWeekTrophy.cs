using Newtonsoft.Json;
using TRLSwingers.Teams;

namespace TRLSwingers.Trophies
{
    public class OffensivePlayerOfTheWeekTrophy : ITrophy
    {
        public OffensivePlayerOfTheWeekTrophy(Team team, string additionalInfo)
        {
            Team = team;
            AdditionalInfo = additionalInfo;
            BuildTogether = false;
        }
        public Team Team { get; set; }
        public string AdditionalInfo { get; }
        public bool BuildTogether { get; }

        public string GetTrophyName()
        {
            return TrophyConstants.OffensivePlayerOfTheWeek;
        }

        public string LeaguePageText()
        {
            return GetTrophyName().ToUpper();
        }

        public string GetTrophyBody()
        {
            var op = JsonConvert.DeserializeObject<PlayerOfTheWeek>(AdditionalInfo);
            return $"[player#{op.PlayerId}]{op.Name.ToUpper()}[/player] ({op.Team.ToUpper()})" + @"

                [image]<update>[/image]";
        }

        public string GetHeadline(Team team)
        {
            var op = JsonConvert.DeserializeObject<PlayerOfTheWeek>(AdditionalInfo);
            return $"{op.Name.ToUpper()} - {op.Points} POINTS!!!!!";
        }

        public string GetReason(Team team)
        {
            return string.Empty;
        }
    }
}