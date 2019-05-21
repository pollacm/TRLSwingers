namespace TRLSwingers.SiteCodes
{
    public class SiteCode
    {
        public SiteCode(string teamCode, string espnCode, string yahooCode, string ourladsCode)
        {
            this.TeamCode = teamCode;
            this.EspnCode = espnCode;
            this.YahooCode = yahooCode;
            this.OurladsCode = ourladsCode;
        }

        public string TeamCode { get; set; }
        public string EspnCode { get; set; }
        public string YahooCode { get; set; }
        public string OurladsCode { get; set; }
    }

}