using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;

namespace TRLSwingers.Weeks
{
    public class WeekRepository
    {
        private readonly string jsonFile = "../../weeks.json";

        public void RefreshWeeks(List<Week> weeks)
        {
            var json = JsonConvert.SerializeObject(weeks);

            using (StreamWriter file = new StreamWriter(jsonFile))
            {
                file.Write(json);
            }
        }

        public List<Week> GetWeeks()
        {
            using (StreamReader file = new StreamReader(jsonFile))
            {
                var json = file.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Week>>(json);
            }
        }

        public Week GetWeek(ChromeDriver driver, int week, int year)
        {
            var weeks = GetWeeks();
            if (weeks.Any(w => w.WeekNumber == week))
            {
                return weeks.Single(w => w.WeekNumber == week);
            }
            else
            {
                var builtWeek = new WeekBuilder(driver, week, year).BuildWeek();
                weeks.Add(builtWeek);
                RefreshWeeks(weeks.OrderBy(w => w.WeekNumber).ToList());

                return builtWeek;
            }
        }
    }
}
