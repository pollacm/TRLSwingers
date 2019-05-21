using System.Collections.Generic;
using System.Linq;

namespace TRLSwingers.Trophies
{
    public class TrophyWriter
    {
        private readonly List<ITrophy> _trophies;

        public TrophyWriter(List<ITrophy> trophies)
        {
            _trophies = trophies;
        }

        public string GetTrophyTextForLeaguePage()
        {
            var trophyText = WriteTrophyText(_trophies.OfType<OffensivePlayerOfTheWeekTrophy>());
            trophyText += WriteTrophyText(_trophies.OfType<DefensivePlayerOfTheWeekTrophy>());
            trophyText += WriteTrophyText(_trophies.OfType<SevenHundredClubTrophy>());
            trophyText += WriteTrophyText(_trophies.OfType<SixHundredClubTrophy>());
            trophyText += WriteTrophyText(_trophies.OfType<FiveHundredClubTrophy>());
            trophyText += WriteTrophyText(_trophies.OfType<BallerOfTheWeekTrophy>());
            trophyText += WriteTrophyText(_trophies.OfType<LoserOfTheWeekTrophy>());
            trophyText += WriteTrophyText(_trophies.OfType<HighestScoringSeasonTrophy>());
            trophyText += WriteTrophyText(_trophies.OfType<TopRankedSeasonTrophy>());
            trophyText += WriteTrophyText(_trophies.OfType<OffensivePlayerOfTheYearTrophy>());
            trophyText += WriteTrophyText(_trophies.OfType<DefensivePlayerOfTheYearTrophy>());
            trophyText += WriteTrophyText(_trophies.OfType<NfcDivisionChampionshipTrophy>());
            trophyText += WriteTrophyText(_trophies.OfType<AfcDivisionChampionshipTrophy>());
            trophyText += WriteTrophyText(_trophies.OfType<BottomRankedSeasonTrophy>());

            return trophyText;
        }

        private string WriteTrophyText(IEnumerable<ITrophy> trophies)
        {
            var trophyText = string.Empty;
            if (trophies.Any())
            {
                trophyText = trophies.First().LeaguePageText() + @"
                ";
                if (trophies.First().BuildTogether)
                {
                    trophyText += string.Join(", ", trophies.Select(t => t.GetTrophyBody()));
                }
                else
                {
                    foreach (var trophy in trophies)
                    {
                        trophyText += trophy.GetTrophyBody();
                        trophyText += @"

                        ";
                    }
                }

                trophyText += @"


                ";

            }

            return trophyText;
        }
    }
}
