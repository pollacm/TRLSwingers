using System.Linq;

namespace TRLSwingers.Pitchers
{
    public class Pitcher
    {
        public string Name { get; set; }
        public int Stars { get; set; }
        public double Average { get; set; }

        public double Value
        {
            get { return Average + (Stars * .5); }
        }

        public string LastName => Name.Split(' ').Last();
    }
}
