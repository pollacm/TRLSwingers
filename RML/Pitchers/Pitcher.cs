using System.Linq;

namespace TRLSwingers.Pitchers
{
    public class Pitcher
    {
        public string Name { get; set; }
        public int Stars { get; set; }
        public double Average { get; set; }
        public bool Added { get; set; }
        public string Position { get; set; }
        public double Value
        {
            get { return Average + (Stars * .5); }
        }

        public string LastName => Name.Split(' ').Last();

        public override string ToString()
        {
            return $"{Name}:\tSTARS:\t{Stars}:\tAVERAGE:\t{Average}\tVALUE:{Value}\tADDED:{(Added ? "***************" : "--")}";
        }
    }
}
