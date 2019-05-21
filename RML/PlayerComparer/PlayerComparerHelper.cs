//using System.Collections.Generic;
//using System.Linq;

//namespace RML.PlayerComparer
//{
//    public class PlayerComparerHelper
//    {
//        private readonly List<SitePlayer> _sitePlayers;

//        public PlayerComparerHelper(List<SitePlayer> sitePlayers)
//        {
//            _sitePlayers = sitePlayers;
//        }

//        public SitePlayer PlayerInUpgradedPosition(RmlPlayer rmlPlayer)
//        {
//            return _sitePlayers.SingleOrDefault(c => c.Name == rmlPlayer.Name && (c.Position == SitePlayer.PositionEnum.FS || c.Position == SitePlayer.PositionEnum.SS));
//        }
//    }
//}
