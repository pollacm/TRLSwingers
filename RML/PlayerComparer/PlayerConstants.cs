using System.Collections.Generic;
using TRLSwingers.SiteCodes;

namespace TRLSwingers.PlayerComparer
{
    public static class PlayerConstants
    {
        public enum DepthChartEnum
        {
            Starter = 0,
            Secondary = 1,
            Tertiary = 2
        }

        public static Dictionary<string, SitePlayer.SitePlayer.PositionEnum> LinebackerPositionsYahoo = new Dictionary<string, SitePlayer.SitePlayer.PositionEnum>
        {
            { "Strong Safety", SitePlayer.SitePlayer.PositionEnum.SS},
            { "Free Safety", SitePlayer.SitePlayer.PositionEnum.FS},
            { "Left LineBacker", SitePlayer.SitePlayer.PositionEnum.WLB},
            { "Middle Linebacker", SitePlayer.SitePlayer.PositionEnum.MLB},
            { "Right Linebacker", SitePlayer.SitePlayer.PositionEnum.SLB},
            { "Left Outside Linebacker", SitePlayer.SitePlayer.PositionEnum.OLB},
            { "Right Outside Linebacker", SitePlayer.SitePlayer.PositionEnum.OLB},
            { "Left Inside Linebacker", SitePlayer.SitePlayer.PositionEnum.ILB},
            { "Right Inside Linebacker", SitePlayer.SitePlayer.PositionEnum.ILB}
        };

        public static Dictionary<string, SitePlayer.SitePlayer.PositionEnum> LinebackerPositionsEspn = new Dictionary<string, SitePlayer.SitePlayer.PositionEnum>
        {
            { "SS", SitePlayer.SitePlayer.PositionEnum.SS},
            { "FS", SitePlayer.SitePlayer.PositionEnum.FS},
            { "WLB", SitePlayer.SitePlayer.PositionEnum.WLB},
            { "MLB", SitePlayer.SitePlayer.PositionEnum.MLB},
            { "SLB", SitePlayer.SitePlayer.PositionEnum.SLB},
            { "LOLB", SitePlayer.SitePlayer.PositionEnum.OLB},
            { "ROLB", SitePlayer.SitePlayer.PositionEnum.OLB},
            { "LILB", SitePlayer.SitePlayer.PositionEnum.ILB},
            { "RILB", SitePlayer.SitePlayer.PositionEnum.ILB}
        };

        public static List<SiteCode> SiteCodes = new List<SiteCode>
        {
            new SiteCode("Buffalo Bills", "buf", "buf", "buf"),
            new SiteCode("Dallas Cowboys", "dal", "dal", "dal"),
            new SiteCode("Miami Dolphins", "mia", "mia", "mia"),
            new SiteCode("New York Giants", "nyg", "nyg", "nyg"),
            new SiteCode("New England Patriots", "ne", "nwe", "ne"),
            new SiteCode("Philadelphia Eagles", "phi", "phi", "phi"),
            new SiteCode("New York Jets", "nyj", "nyj", "nyj"),
            new SiteCode("Washington Redskins", "wsh", "was", "was"),

            new SiteCode("Denver Broncos", "den", "den", "den"),
            new SiteCode("Arizona Cardinals", "ari", "ari", "arz"),
            new SiteCode("Kansas City Chiefs", "kc", "kan", "kc"),
            new SiteCode("Los Angeles Rams", "lar", "lar", "ram"),
            new SiteCode("Los Angeles Chargers", "lac", "lac", "sd"),
            new SiteCode("San Fransisco 49ers", "sf", "sfo", "sf"),
            new SiteCode("Oakland Raiders", "oak", "oak", "oak"),
            new SiteCode("Seattle Seahawks", "sea", "sea", "sea"),

            new SiteCode("Baltimore Ravens", "bal", "bal", "bal"),
            new SiteCode("Chicago Bears", "chi", "chi", "chi"),
            new SiteCode("Cincinnati Bengals", "cin", "cin", "cin"),
            new SiteCode("Detroit Lions", "det", "det", "det"),
            new SiteCode("Cleveland Browns", "cle", "cle", "cle"),
            new SiteCode("Green Bay Packers", "gb", "gnb", "gb"),
            new SiteCode("Pittsburg Steelers", "pit", "pit", "pit"),
            new SiteCode("Minnesota Vikings", "min", "min", "min"),

            new SiteCode("Houston Texans", "hou", "hou", "hou"),
            new SiteCode("Atlanta Falcons", "atl", "atl", "atl"),
            new SiteCode("Indianapolis Colts", "ind", "ind", "ind"),
            new SiteCode("Carolina Panthers", "car", "car", "car"),
            new SiteCode("Jacksonville Jaguars", "jax", "jac", "jax"),
            new SiteCode("New Orleans Saints", "no", "nor", "no"),
            new SiteCode("Tennessee Titans", "ten", "ten", "ten"),
            new SiteCode("Tampa Bay Buccaneers", "tb", "tam", "tb")
        };
    }
}
