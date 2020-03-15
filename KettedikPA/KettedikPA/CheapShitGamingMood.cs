using System.Collections.Generic;

namespace KettedikPA
{
    class CheapShitGamingMood : GamingMood
    {
        public bool WannaPlayWith(Game game)
        {
            if (game.Price<9000)
            {
                return true;
            }
            return false;
            
        }
    }
}