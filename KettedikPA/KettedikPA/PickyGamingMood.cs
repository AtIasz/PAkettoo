using System;
using System.Collections.Generic;
using System.Text;

namespace KettedikPA
{
    class PickyGamingMood : GamingMood
    {
        public bool WannaPlayWith(Game game)
        {
            Console.WriteLine("FaszrákPickyGM");
            string input = Console.ReadLine();
            bool thereIs = false;
            foreach (Genre genre in (Genre[]) Enum.GetValues(typeof(Genre)))
            {
                if (genre.ToString() == input.ToLower())
                {

                    thereIs= true;
                }
            }
            if (thereIs)
            {
                FileManager fm = new FileManager();
                SameGenre(fm._listOfGames, input);
            }
            

            return false;
        }
        
        public List<Game> SameGenre(List<Game>games,string genrei)
        {

            List<Game> oneGenre = new List<Game>();
            for (int i = 0; i < games.Count; i++)
            {
                for (int j = 0; j < games[i].Genres.Count; j++)
                {
                    if (games[i].Genres[j]==genrei.ToLower())
                    {
                        oneGenre.Add(games[i]);
                    }
                }
            }
            return oneGenre;
        }
        enum Genre
        {
            rpg,sandbox,survival,adventure,tps,fps,
            stealth,openWorld,roguelike,shooter,
            hack_N_slash,sport,competitve,football,
        }
    }
}
