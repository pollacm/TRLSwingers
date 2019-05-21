namespace TRLSwingers.Teams
{
    public class Team
    {
        public string TeamName { get; set; }
        public string TeamAbbreviation { get; set; }
        public decimal TeamPoints { get; set; }
        public bool Win { get; set; }

        public decimal GetPointsForWeek 
        {
            get
            {
                return TeamPoints + (Win ? 50 : 0);
            }
        }
    }
}
