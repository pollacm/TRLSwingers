using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TRLSwingers.Returners
{
    public class PrintReturnerService
    {
        private List<Returner> _returners;
        private string returnerFile = @"E:\Dropbox\Private\Fantasy\RML\2018\ReturnersGenerated2.txt";

        public PrintReturnerService(List<Returner> returners)
        {
            _returners = returners;
        }

        public void WriteReturnerFile()
        {
            using (StreamWriter file = new StreamWriter(returnerFile))
            {
                PrintHeader(file);

                foreach (var returner in _returners)
                {
                    PrintLine(file, returner);
                }
            }
        }

        private void PrintLine(StreamWriter file, Returner returner)
        {
            file.Write(returner.Team);
            for (int i = 0; i < (6 - (int)(returner.Team.ToArray().Count() / 4)); i++)
                file.Write("\t");

            file.Write(returner.YahooPrimaryKickReturner);
            for (int i = 0; i < (6 - (int)(returner.YahooPrimaryKickReturner.ToArray().Count() / 4)); i++)
                file.Write("\t");

            file.Write(returner.YahooPrimaryPuntReturner);
            for (int i = 0; i < (8 - (int)(returner.YahooPrimaryPuntReturner.ToArray().Count() / 4)); i++)
                file.Write("\t");

            file.Write(returner.EspnPrimaryKickReturner);
            for (int i = 0; i < (8 - (int)(returner.EspnPrimaryKickReturner.ToArray().Count() / 4)); i++)
                file.Write("\t");

            file.Write(returner.EspnPrimaryPuntReturner);
            for (int i = 0; i < (7 - (int)(returner.EspnPrimaryPuntReturner.ToArray().Count() / 4)); i++)
                file.Write("\t");

            if (returner.InCommonBothPrimary)
            {
                file.Write("***\t\t");
            }

            //"Player".Dump();
            //returner.EspnPrimaryKickReturner.Dump();
            //"Split Player".Dump();
            //returner.EspnPrimaryKickReturner.Split(' ').Last().Dump();        
            //"Is Set".Dump();
            //returner.YahooPrimaryKickReturner.Contains(returner.EspnPrimaryKickReturner.Split(' ').Last()).Dump();
            //"Is Set Real".Dump();
            //returner.InCommonKickReturners.Dump();
            //returner.Dump();

            if (returner.InCommonAndSamePlayerPrimary)
            {
                file.Write("***");
            }

            file.WriteLine();
        }

        private void PrintHeader(StreamWriter file)
        {
            file.WriteLine("\t\t\t\t\t\tYahoo\t\t\t\t\t\t\t\t\t\t\t\t\tESPN");
            file.WriteLine("TEAM\t\t\t\t\tKR\t\t\t\t\t\tPR\t\t\t\t\t\t\t\tKR\t\t\t\t\t\t\t\tPR");
            file.WriteLine("\t\t\t\t\t\t--------------------\t-------------------------\t\t------------------------------\t-------------------------");
        }
    }
}