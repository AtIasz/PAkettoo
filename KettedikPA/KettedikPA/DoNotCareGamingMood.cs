using System.Collections.Generic;

namespace KettedikPA
{
    class DoNotCareGamingMood : GamingMood
    {
        public bool WannaPlayWith(Game game)
        {
            return true;
        }
    }
}