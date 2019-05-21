using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TRLSwingers.Rankings
{
    public class PrintPostDraftRankingsService
    {
        private List<PostDraftRanking> _postDraftRankings;
        //private string postDraftRankingFile = @"E:\Dropbox\Private\Fantasy\RML\2018\PostDraftRankings2.txt";
        private string postDraftRankingFile = @"C:\Users\cxp6696\Dropbox\Private\Fantasy\RML\2019\PostDraftRankings2.txt";

        public PrintPostDraftRankingsService(List<PostDraftRanking> postDraftRankings)
        {
            _postDraftRankings = postDraftRankings;
        }

        public void WritePostDraftRankingsFile()
        {
            using (StreamWriter file = new StreamWriter(postDraftRankingFile, true))
            {
                PrintTeam(file, _postDraftRankings.First().TeamName);

                var firstBench = false;

                foreach (var postDraftRanking in _postDraftRankings)
                {
                    if (postDraftRanking.PlayerPosition.ToLower() == "bench" && !firstBench)
                    {
                        PrintTotals(file, _postDraftRankings.Where(r => r.PlayerPosition != "Bench").ToList());
                        file.WriteLine("-----------------");
                        file.WriteLine();
                        firstBench = true;
                    }
                    PrintLine(file, postDraftRanking);
                }

                PrintTotals(file, _postDraftRankings.Where(r => r.PlayerPosition == "Bench").ToList());

                file.WriteLine();
                file.WriteLine();
                file.WriteLine("----------------------------");
            }
        }

        private void PrintLine(StreamWriter file, PostDraftRanking ranking)
        {
            file.WriteLine(ranking.PlayerName);
            file.WriteLine($"{ranking.Projection}\t\t{ranking.WeeklyProjection}\t\t\t");
            file.WriteLine();
        }

        private void PrintTotals(StreamWriter file, List<PostDraftRanking> rankings)
        {
            decimal totalProjection = 0;
            decimal totalWeeklyProjection = 0;
            foreach (var ranking in rankings)
            {
                totalProjection += ranking.Projection;
                totalWeeklyProjection += ranking.WeeklyProjection;
            }
            file.WriteLine($"{totalProjection}\t\t{totalWeeklyProjection}\t\t\t");
            file.WriteLine();
        }

        private void PrintTeam(StreamWriter file, string team)
        {
            file.WriteLine("------------------------------------");
            file.WriteLine(team.ToUpper());
            file.WriteLine("------------------------------------");
            file.WriteLine("\n");
        }
    }
}