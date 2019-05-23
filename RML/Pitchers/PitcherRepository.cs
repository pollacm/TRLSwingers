using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TRLSwingers.Pitchers
{
    public class PitcherRepository
    {
        private readonly string jsonFile = "../../Pitchers.json";

        public void RefreshPitchers(List<string> pitchers)
        {
            var json = string.Join("|", pitchers);

            using (StreamWriter file = new StreamWriter(jsonFile))
            {
                file.Write(json);
            }
        }

        public List<string> GetPitchers()
        {
            using (StreamReader file = new StreamReader(jsonFile))
            {
                var json = file.ReadToEnd();
                return json.Split('|').ToList();
            }
        }
    }
}
