using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TRLSwingers.Teams
{
    public class TeamRepository
    {
        private readonly string jsonFile = "../../teams.json";

        public void RefreshTeams(List<string> teams)
        {
            var json = JsonConvert.SerializeObject(teams);

            using (StreamWriter file = new StreamWriter(jsonFile))
            {
                file.Write(json);
            }
        }

        public List<string> GetTeams()
        {
            using (StreamReader file = new StreamReader(jsonFile))
            {
                var json = file.ReadToEnd();
                return JsonConvert.DeserializeObject<List<string>>(json);
            }
        }
    }
}
