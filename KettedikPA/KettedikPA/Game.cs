using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KettedikPA 
{
    public class Game : object
    {
        public string Name { get; set; }
        public int _releaseYear { get; set; }
        public double Price { get; set; }
        public List<string> Genres { get; set; }
        
        public bool _finished { get; set; }
        public bool _owned { get; set; }
        public Game()
        {

        }
        public Game(String name, int rYear, double price, List<string> genres, bool f = false, bool o=false)
        {
            Int32 a = 13;
            Name = name;
            _releaseYear = rYear;
            Price = price;
            Genres = genres;
            _finished = f;
            _owned = o;
        }
    }
}
