using TRLSwingers.PlayerComparer;

namespace TRLSwingers.SitePlayer
{
    public class SitePlayer
    {
        public SitePlayer(string teamCode, PlayerConstants.DepthChartEnum depthChartPosition, PositionEnum position, SiteEnum site, string name)
        {
            Team = teamCode;
            DepthChart = depthChartPosition;
            Position = position;
            Site = site;
            Name = name;
        }
        public string Team { get; set; }
        public string Name { get; set; }
        public PositionEnum Position { get; set; }
        public SiteEnum Site { get; set; }
        public PlayerConstants.DepthChartEnum DepthChart { get; set; }

        public enum PositionEnum
        {
            FS = 0,
            SS = 1,
            MLB = 2,
            ILB = 3,
            SLB = 4,
            WLB = 5,
            OLB = 6
        }

        public enum SiteEnum
        {
            ESPN = 0,
            Yahoo = 1
        }
    }
}