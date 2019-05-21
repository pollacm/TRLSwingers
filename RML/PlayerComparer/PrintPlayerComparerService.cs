using System.Collections.Generic;
using System.IO;
using System.Linq;
using TRLSwingers.SiteCodes;

namespace TRLSwingers.PlayerComparer
{
    public class PrintPlayerComparerService
    {
        private readonly List<SitePlayer.SitePlayer> _sitePlayers;

        //private string returnerFile = @"C:\Users\cxp6696\Dropbox\Private\Fantasy\RML\2018\PlayerComparerGenerated2.txt";
        private string returnerFile = @"E:\Dropbox\Private\Fantasy\RML\2018\PlayerComparerGenerated2.txt";
        private readonly List<RmlPlayer.RmlPlayer> _rmlPlayers;

        public PrintPlayerComparerService(List<SitePlayer.SitePlayer> sitePlayers, List<RmlPlayer.RmlPlayer> rmlPlayers)
        {
            _rmlPlayers = rmlPlayers;
            _sitePlayers = sitePlayers;
        }

        public void WritePlayerComparerFile()
        {
            using (StreamWriter file = new StreamWriter(returnerFile))
            {
                PrintHeader(file);
                foreach (var sitePlayer in _sitePlayers)
                {
                    var sitePlayerTeamCodeForEspn = SiteCodeHelper.GetEspnCodeFromTeam(sitePlayer.Team);
                    var rmlPlayers = _rmlPlayers.Where(p => p.Name == sitePlayer.Name && p.Team.ToUpper() == sitePlayerTeamCodeForEspn.ToUpper()).ToList();
                    if (rmlPlayers.Any())
                    {
                        foreach (var rmlPlayer in rmlPlayers)
                        {
                            PrintLine(file, rmlPlayer, sitePlayer);
                        }
                    }
                }
            }
        }

        private void PrintLine(StreamWriter file, RmlPlayer.RmlPlayer rmlPlayer, SitePlayer.SitePlayer sitePlayer)
        {
            file.Write(rmlPlayer.Team);
            for (int i = 0; i < (4 - (int)(rmlPlayer.Team.ToArray().Count() / 4)); i++)
                file.Write("\t");

            file.Write(rmlPlayer.Name);
            for (int i = 0; i < (7 - (int)(rmlPlayer.Name.ToArray().Count() / 4)); i++)
                file.Write("\t");

            file.Write(sitePlayer.Position);
            for (int i = 0; i < (3 - (int)(sitePlayer.Position.ToString().ToArray().Count() / 4)); i++)
                file.Write("\t");

            file.Write(sitePlayer.DepthChart);
            for (int i = 0; i < (3 - (int)(sitePlayer.DepthChart.ToString().ToArray().Count() / 4)); i++)
                file.Write("\t");

            file.Write(sitePlayer.Site);
            for (int i = 0; i < (3 - (int)(sitePlayer.Site.ToString().ToArray().Count() / 4)); i++)
                file.Write("\t");

            file.Write(rmlPlayer.PreviousRank);
            for (int i = 0; i < (3 - (int)(rmlPlayer.PreviousRank.ToString().ToArray().Count() / 4)); i++)
                file.Write("\t");

            file.Write(rmlPlayer.PreviousAverage);
            for (int i = 0; i < (3 - (int)(rmlPlayer.PreviousAverage.ToString().ToArray().Count() / 4)); i++)
                file.Write("\t");

            var espnPlayer = _sitePlayers.Any(p => p.Name == rmlPlayer.Name && p.Site == SitePlayer.SitePlayer.SiteEnum.ESPN);
            var espnPlayerDisplay = espnPlayer ? "**" : "";

            file.Write(espnPlayerDisplay);
            for (int i = 0; i < (3 - (int)(espnPlayerDisplay.ToString().ToArray().Count() / 4)); i++)
                file.Write("\t");

            var yahooPlayer = _sitePlayers.Any(p => p.Name == rmlPlayer.Name && p.Site == SitePlayer.SitePlayer.SiteEnum.Yahoo);
            var yahooPlayerDisplay = yahooPlayer ? "**" : "";
            file.Write(yahooPlayerDisplay);
            for (int i = 0; i < (3 - (int)(yahooPlayerDisplay.ToString().ToArray().Count() / 4)); i++)
                file.Write("\t");

            var inBothSites = espnPlayer && yahooPlayer ? "****" : "";
            file.Write(inBothSites);
            for (int i = 0; i < (3 - (int)(inBothSites.ToString().ToArray().Count() / 4)); i++)
                file.Write("\t");

            file.WriteLine();
        }

        private void PrintHeader(StreamWriter file)
        {
            file.WriteLine("TEAM\t\t\tNAME\t\t\t\t\t\tPOSITION\tDEPTH\t\tSITE\t\tRANK\t\tPOINTS\t\tESPN\t\tYAHOO\t\tBOTH");
        }
    }
}