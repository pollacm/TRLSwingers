using Newtonsoft.Json;
using TRLSwingers.Teams;

namespace TRLSwingers.Trophies
{
    public class DefensivePlayerOfTheYearTrophy : ITrophy
    {
        public DefensivePlayerOfTheYearTrophy(Team team, string additionalInfo)
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
            return TrophyConstants.DefensivePlayerOfTheYear;
        }

        public string LeaguePageText()
        {
            return "THE " + GetTrophyName().ToUpper() + @" AWARD GOES TO...";
        }

        public string GetTrophyBody()
        {
            var dp = JsonConvert.DeserializeObject<PlayerOfTheWeek>(AdditionalInfo);
            return $"[player#{dp.PlayerId}]{dp.Name.ToUpper()}[/player] ({dp.Team.ToUpper()})" + @"

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