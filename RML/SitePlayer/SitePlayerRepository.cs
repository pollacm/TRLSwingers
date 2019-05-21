using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TRLSwingers.SitePlayer
{
    public class SitePlayerRepository
    {
        private readonly string jsonFile = "../../sitePlayers.json";

        public void RefreshSitePlayers(List<SitePlayer> sitePlayers)
        {
            var json = JsonConvert.SerializeObject(sitePlayers);

            using (StreamWriter file = new StreamWriter(jsonFile))
            {
                file.Write(json);
            }
        }

        public List<SitePlayer> GetSitePlayers()
        {
            using (StreamReader file = new StreamReader(jsonFile))
            {
                var json = file.ReadToEnd();
                return JsonConvert.DeserializeObject<List<SitePlayer>>(json);
            }
        }
    }
}
