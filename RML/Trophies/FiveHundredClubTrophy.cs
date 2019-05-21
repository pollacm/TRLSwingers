﻿using TRLSwingers.Teams;

namespace TRLSwingers.Trophies
{
    public class FiveHundredClubTrophy : ITrophy
    {
        public FiveHundredClubTrophy(Team team, string additionalInfo)
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
            return TrophyConstants.Da500Club;
        }

        public string LeaguePageText()
        {
            return "THE " + GetTrophyName().ToUpper();
        }

        public string GetTrophyBody()
        {
            return Team.TeamName.ToUpper();
        }

        public string GetHeadline(Team team)
        {
            return $"For putting up {team.TeamPoints} points!!!!!";
        }

        public string GetReason(Team team)
        {
            return string.Empty;
        }
    }
}
