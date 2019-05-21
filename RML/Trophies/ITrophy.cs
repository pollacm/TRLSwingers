using TRLSwingers.Teams;

namespace TRLSwingers.Trophies
{
    public interface ITrophy
    {
        Team Team { get; }
        string AdditionalInfo { get; }
        bool BuildTogether { get; }

        string GetHeadline(Team team);
        string GetReason(Team team);
        string GetTrophyName();
        string LeaguePageText();
        string GetTrophyBody();
    }
}