namespace TRLSwingers.Returners
{
    public class Returner
    {
        public Returner()
        {
            this.YahooPrimaryKickReturner = string.Empty;
            this.YahooSecondaryKickReturner = string.Empty;
            this.YahooTertiaryKickReturner = string.Empty;
            this.YahooPrimaryPuntReturner = string.Empty;
            this.YahooSecondaryPuntReturner = string.Empty;
            this.YahooTertiaryPuntReturner = string.Empty;
            this.EspnPrimaryKickReturner = string.Empty;
            this.EspnSecondaryKickReturner = string.Empty;
            this.EspnTertiaryKickReturner = string.Empty;
            this.EspnPrimaryPuntReturner = string.Empty;
            this.EspnSecondaryPuntReturner = string.Empty;
            this.EspnTertiaryPuntReturner = string.Empty;
        }

        public string YahooPrimaryKickReturner { get; set; }
        public string YahooSecondaryKickReturner { get; set; }
        public string YahooTertiaryKickReturner { get; set; }

        public string YahooPrimaryPuntReturner { get; set; }
        public string YahooSecondaryPuntReturner { get; set; }
        public string YahooTertiaryPuntReturner { get; set; }

        public string EspnPrimaryKickReturner { get; set; }
        public string EspnSecondaryKickReturner { get; set; }
        public string EspnTertiaryKickReturner { get; set; }

        public string EspnPrimaryPuntReturner { get; set; }
        public string EspnSecondaryPuntReturner { get; set; }
        public string EspnTertiaryPuntReturner { get; set; }

        //public string OurladsPrimaryKickReturner { get; set; }
        //public string OurladsSecondaryKickReturner { get; set; }
        //public string OurladsTertiaryKickReturner { get; set; }
        //public string OurladsPrimaryPuntReturner { get; set; }
        //public string OurladsSecondaryPuntReturner { get; set; }
        //public string OurladsTertiaryPuntReturner { get; set; }

        public string Team { get; set; }

        //returner domain
        public bool InCommonBothPrimary => this.InCommonPrimaryKickReturners && this.InCommonPrimaryPuntReturners;

        public bool InCommonAndSamePlayerPrimary => this.InCommonPrimaryKickReturners && this.InCommonPrimaryPuntReturners && EspnPrimaryKickReturner != null && EspnPrimaryPuntReturner != null &&
                                                    EspnPrimaryKickReturner == EspnPrimaryPuntReturner;

        public bool InCommonKickReturners => this.InCommonPrimaryKickReturners && this.InCommonSecondaryKickReturners && InCommonTertiaryKickReturners;
        public bool InCommonPuntReturners => this.InCommonPrimaryPuntReturners && this.InCommonSecondaryPuntReturners && InCommonTertiaryPuntReturners;

        public bool InCommonPrimaryKickReturners => (YahooPrimaryKickReturner == null && EspnPrimaryKickReturner == null) ||
                                                    (YahooPrimaryKickReturner != null && EspnPrimaryKickReturner != null && YahooPrimaryKickReturner == EspnPrimaryKickReturner);

        public bool InCommonSecondaryKickReturners => (YahooSecondaryKickReturner == null && EspnSecondaryKickReturner == null) ||
                                                      (YahooSecondaryKickReturner != null && EspnSecondaryKickReturner != null && YahooSecondaryKickReturner == EspnSecondaryKickReturner);

        public bool InCommonTertiaryKickReturners => (YahooTertiaryKickReturner == null && EspnTertiaryKickReturner == null) ||
                                                     (YahooTertiaryKickReturner != null && EspnTertiaryKickReturner != null && YahooTertiaryKickReturner ==EspnTertiaryKickReturner);

        public bool InCommonPrimaryPuntReturners => (YahooPrimaryPuntReturner == null && EspnPrimaryPuntReturner == null) ||
                                                    (YahooPrimaryPuntReturner != null && EspnPrimaryPuntReturner != null && YahooPrimaryPuntReturner == EspnPrimaryPuntReturner);

        public bool InCommonSecondaryPuntReturners => (YahooSecondaryPuntReturner == null && EspnSecondaryPuntReturner == null) ||
                                                      (YahooSecondaryPuntReturner != null && EspnSecondaryPuntReturner != null && YahooSecondaryPuntReturner == EspnSecondaryPuntReturner);

        public bool InCommonTertiaryPuntReturners => (YahooTertiaryPuntReturner == null && EspnTertiaryPuntReturner == null) ||
                                                     (YahooTertiaryPuntReturner != null && EspnTertiaryPuntReturner != null && YahooTertiaryPuntReturner == EspnTertiaryPuntReturner);
    }
}