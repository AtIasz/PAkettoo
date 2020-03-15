using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;


namespace KettedikPA
{
    class FileManager
    {
        public List<Game> _listOfGames{ get; set; }
        public FileManager()
        {
            LoadFromXML();
        }
        public void LoadFromXML(string fileName = "fbay.xml")
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Game>));
            using (Stream tw = new FileStream("fbay.xml", FileMode.Open,
                FileAccess.Read, FileShare.None))
            {
                _listOfGames = (List<Game>)serializer.Deserialize(tw);
            }
        }

        public void MakeTheGames()
        {
            _listOfGames = new List<Game>();
            List<string> list = new List<string>();
            list.Add("rpg");
            Game game = new Game("Undertale", 2015, (3500), list);
            _listOfGames.Add(game);


            list = new List<string>();
            list.Add("sandbox");
            list.Add("survival");
            list.Add("adventure");
            game = new Game("Minecraft", 2009, (7000), list);
            _listOfGames.Add(game);
            list = new List<string>();
            list.Add("adventure");
            list.Add("tps");
            game = new Game("GTA V", 2015, (12000), list);
            _listOfGames.Add(game);
            list = new List<string>();
            list.Add("rpg");
            game = new Game("The Elder Scrolls: Skyrim", 2011, (12500), list);
            _listOfGames.Add(game);
            list = new List<string>();
            list.Add("tps");
            game = new Game("Tom Clancy's: The Division", 2016, (12500), list);
            _listOfGames.Add(game);
            list = new List<string>();
            list.Add("rpg");
            game = new Game("Final Fantasy XV", 2016, (12500), list);
            _listOfGames.Add(game);
            list = new List<string>();
            list.Add("fps");
            game = new Game("Overwatch", 2015, (7000), list);
            _listOfGames.Add(game);
            list = new List<string>();
            list.Add("rpg");
            list.Add("survival");
            game = new Game("ARK: Survival Evolved", 2015, (16000), list);
            _listOfGames.Add(game);
            list = new List<string>();
            list.Add("stealth");
            list.Add("adventure");
            list.Add("open world");
            game = new Game("Assassin's Creed Odyssey", 2018, (20000), list);
            _listOfGames.Add(game);
            list = new List<string>();
            list.Add("roguelike");
            list.Add("manic shooter");

            game = new Game("Enter the Gungeon", 2016, (11500), list);
            _listOfGames.Add(game);
            list = new List<string>();
            list.Add("hack and slash");
            list.Add("dungeon drawl");
            game = new Game("Diablo III", 2012, (6000), list);
            _listOfGames.Add(game);
            list = new List<string>();
            list.Add("soprt-simulation");
            list.Add("competitive");
            list.Add("football");

            game = new Game("Rocket League", 2015, (7000), list);
            _listOfGames.Add(game);
            list = new List<string>();
            list.Add("adventure");

            game = new Game("Red Dead Redemption 2", 2018, (20000), list);
            _listOfGames.Add(game);
            list = new List<string>();
            list.Add("adventure");
            game = new Game("MONSTER HUNTER: WORLD", 2018, (11500), list);
            _listOfGames.Add(game);
            list = new List<string>();
            list.Add("rpg");
            list.Add("open world");
            game = new Game("The Witcher 3: Wild Hunt", 2015, (14000), list);
            _listOfGames.Add(game);
        }
        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Game>));
            
            using (Stream tw = new FileStream("fbay.xml", FileMode.Create, 
                FileAccess.Write, FileShare.None))
            {
                serializer.Serialize(tw, _listOfGames);
            }
        }
        
    }
}
