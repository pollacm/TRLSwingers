using System;
using System.Diagnostics;

namespace TRLNFL.RmlPlayer
{
    public class RmlPlayer
    {
        public string Name { get; set; }
        public string SubName { get; set; }
        public decimal Points { get; set; }
        public string Position { get; set; }
        public decimal? SubPoints { get; set; }
        public decimal? Projection { get; set; }
        public Boolean SamePosition { get; set; }
        public Boolean Starter { get; set; }

        public decimal PointWithSub
        {
            get
            {
                if (SubPoints == null)
                {
                    return Points;
                }
                else if (SubPoints != null && SamePosition)
                {
                    return SubPoints.Value;
                }
                else if (SubPoints != null && !SamePosition)
                {
                    return (decimal)(((SubPoints.Value + Points) / 2));
                }

                return 0;

            }
        }

        public decimal PointWithSubDifference => PointWithSub - Points;

        public override string ToString()
        {
            Debug.WriteLine("{0, -20}\t{1, -7}\t{2,-20}\t{3,-7}", Name, Points, SubName != string.Empty ? "(" + PointWithSubDifference + " - " + SubName + ")": "", PointWithSub);
            return "";
        }

    }
}
