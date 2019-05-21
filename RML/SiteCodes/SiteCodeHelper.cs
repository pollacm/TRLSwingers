using System.Linq;
using TRLSwingers.PlayerComparer;

namespace TRLSwingers.SiteCodes
{
    public static class SiteCodeHelper
    {
        public static string GetEspnCodeFromTeam(string fullSite)
        {
            return PlayerConstants.SiteCodes.Single(c => c.TeamCode == fullSite).EspnCode;
        }
    }
}
