using System;
using System.Collections.Generic;

namespace KettedikPA
{
    class UI
    {
        FileManager fm = new FileManager();
        readonly List<GamingMood> allGamingMoods;
        public List<Game> visibleBecauseOfMood;
        GamingMood activeGamingMood;

        public UI()
        {

            visibleBecauseOfMood = new List<Game>();
            allGamingMoods = new List<GamingMood>()
            {
                new DoNotCareGamingMood(),
                new CheapShitGamingMood(),
                new PickyGamingMood()
            };
            activeGamingMood = allGamingMoods[0];
        }
        public void StartModule()
        {
            bool menu = true;
            while (menu)
            {
                Handle_menu();
                try
                {

                    menu = Choose();
                }
                catch (FormatException e)
                {
                    Console.Clear();
                    Error_message("Wrong format. Only numbers are accepted.", e.ToString() + " \nBack to Main Menu.\n");
                    continue;
                }
            }
        }
        public void Handle_menu()
        {
            string[] options = new string[] { "List of Games", "View Own games",
                "Modify Game", "Finish Game", "Delete Own Game","View Active Mood", "Select Mood"};

            Print_menu("\tMain menu\n", options, "Exit program");
        }
        private bool Choose()
        {
            string option = Console.ReadLine();
            Console.Clear();
            int numberOut;
            bool success = Int32.TryParse(option, out numberOut);
            visibleBecauseOfMood = DropTheLoadToTheVisibleList(fm._listOfGames);
            if (success)
            {
                if (numberOut == 1)
                {
                    bool listing = true;
                    while (listing)
                    {
                        string[] listOfPurchase = new string[] { "Purchase Game" };
                        Console.Clear();
                        Print_menu("\tList of Games\n", listOfPurchase, "Back To Menu");
                        int firstNumber = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();
                        Console.Clear();
                        NiceCLIWriter(visibleBecauseOfMood);
                        if (firstNumber == 1)
                        {
                            Console.Write("Input the title of the game: ");
                            string nameOfGame = Console.ReadLine();
                            Console.WriteLine();
                            Console.Clear();

                            Game game = findGame(visibleBecauseOfMood,nameOfGame);
                            if (game == null)
                            {
                                Console.WriteLine("There's no game like " + nameOfGame + "\nPress any key(except the big power button) to continue.");
                                Console.ReadKey();
                            }
                            else if (game._owned)
                            {
                                Console.WriteLine("You might already have purchased this game..\nPress any key (except the big power button) to continue.");
                                Console.ReadKey();
                            }
                            else
                            {
                                game._owned = true;
                                Console.WriteLine("SUCCesfully purchased " + game.Name + ".\nPress any key (except the big power button) to continue.");
                                Console.ReadKey();
                            }
                            Console.WriteLine();
                            listing = true;
                        }
                        else if (firstNumber == 0)
                        {
                            Console.Clear();
                            listing = false;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("There is no such option.\nPress any key(except the big power button) to continue.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                }
                else if (numberOut == 2)
                {
                    bool owning = true;
                    while (owning)
                    {
                        string[] listOfPurchase = new string[] { "List out Own Games", "Details of One Game" };
                        Print_menu("\tOwned Games\n", listOfPurchase, "Back To Menu");
                        int secondNumber = Convert.ToInt32(Console.ReadLine());
                        if (secondNumber == 1)
                        {

                            Console.Clear();
                            int owned = 0;
                            for (int i = 0; i < visibleBecauseOfMood.Count; i++)
                            {
                                if (visibleBecauseOfMood[i]._owned == true)
                                {
                                    owned++;
                                }
                            }
                            if (owned != 0)
                            {

                                for (int i = 0; i < visibleBecauseOfMood.Count; i++)
                                {
                                    if (visibleBecauseOfMood[i]._owned == true)
                                    {
                                        Console.WriteLine($"{visibleBecauseOfMood[i].Name}");
                                    }
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("You don't own any games yet.\nPress any key(except the big power button) to continue.");
                                Console.ReadKey();
                                Console.Clear();
                            }
                            Console.WriteLine();
                        }
                        else if (secondNumber == 2)
                        {
                            bool ownAnyGames = false;
                            for (int i = 0; i < visibleBecauseOfMood.Count; i++)
                            {
                                if (visibleBecauseOfMood[i]._owned == true)
                                {
                                    ownAnyGames = true;
                                }
                            }
                            if (ownAnyGames)
                            {
                                Console.Write("Choose a game: ");
                                string nameOfGame = Console.ReadLine();
                                Console.WriteLine();
                                bool suchGame = false;
                                Console.Clear();

                                for (int i = 0; i < visibleBecauseOfMood.Count; i++)
                                {
                                    if (visibleBecauseOfMood[i].Name.ToLower() == nameOfGame.ToLower() && visibleBecauseOfMood[i]._owned == true)
                                    {
                                        suchGame = true;
                                        Console.WriteLine($"Name:{visibleBecauseOfMood[i].Name}\nRelease year:{visibleBecauseOfMood[i]._releaseYear}\n" +
                                            $"Price:{visibleBecauseOfMood[i].Price}\nFinished:{visibleBecauseOfMood[i]._finished}\n");
                                    }
                                }
                                if (suchGame != true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("There's no game like " + nameOfGame + ", or you don't own it yet. \nPress any key(except the big power button) to continue.");
                                    Console.ReadKey();
                                    Console.Clear();

                                }
                                owning = true;
                            }
                            else if (!ownAnyGames)
                            {
                                Console.Clear();

                                Console.Write("You gotta purchase some games first!\nPress any key(except the big power button) to continue.");
                                Console.ReadKey();
                                Console.Clear();


                            }

                        }
                        else if (secondNumber == 0)
                        {
                            Console.Clear();
                            owning = false;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("There is no such option." + "\nPress any key(except the big power button) to continue.");
                            Console.ReadKey();
                            continue;
                        }
                    }
                }
                else if (numberOut == 3)
                {

                    bool modifying = true;
                    while (modifying)
                    {
                        bool ownAnyGames = false;
                        for (int i = 0; i < visibleBecauseOfMood.Count; i++)
                        {
                            if (visibleBecauseOfMood[i]._owned == true)
                            {
                                ownAnyGames = true;
                            }
                        }
                        if (!ownAnyGames)
                        {
                            Console.WriteLine("You don't own any games to modify.\nPress any key(except the big power button) to continue.");
                            Console.ReadKey();
                            Console.Clear();
                            modifying = false;
                        }
                        else if (ownAnyGames)
                        {
                            Console.WriteLine("Choose a game you would like to modify:");
                            string nameOfOwnedGame = Console.ReadLine();
                            bool notAGame = true;
                            for (int i = 0; i < visibleBecauseOfMood.Count; i++)
                            {
                                if (visibleBecauseOfMood[i].Name.ToLower() == nameOfOwnedGame.ToLower())
                                {
                                    notAGame = false;
                                }
                            }
                            if (!notAGame)
                            {


                                for (int i = 0; i < visibleBecauseOfMood.Count; i++)
                                {
                                    if (visibleBecauseOfMood[i].Name.ToLower() == nameOfOwnedGame.ToLower() && !visibleBecauseOfMood[i]._owned)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("You don't own this game so you can't modify it.\nPress any key(except the big power button) to continue.");
                                        Console.ReadKey();
                                        Console.Clear();
                                        modifying = false;
                                    }

                                    else
                                    {

                                        if (nameOfOwnedGame.ToLower() == visibleBecauseOfMood[i].Name.ToLower())
                                        {
                                            Console.WriteLine("Attribute to change of this game? (Name / Release Year / Price / Owned / Finished)");
                                            string attr = Console.ReadLine();
                                            if (attr.ToLower() == "name")
                                            {
                                                Console.Write("The new name of the game: ");
                                                string newName = Console.ReadLine();
                                                Console.WriteLine($"{visibleBecauseOfMood[i].Name}'s new name is {newName}.");
                                                visibleBecauseOfMood[i].Name = newName;
                                                modifying = false;
                                            }
                                            else if (attr.ToLower() == "release year" || attr.ToLower() == "year")
                                            {
                                                Console.Write("The new release year of the game: ");
                                                string inputYear = Console.ReadLine();
                                                int newYear;
                                                bool parsableYear = Int32.TryParse(inputYear, out newYear);
                                                if (parsableYear)
                                                {
                                                    Console.WriteLine($"{visibleBecauseOfMood[i].Name}'s release year is {newYear}.");
                                                    visibleBecauseOfMood[i]._releaseYear = newYear;
                                                    modifying = false;

                                                }
                                                else
                                                {
                                                    Console.WriteLine("The input must be an integer.");
                                                    modifying = false;

                                                }
                                            }
                                            else if (attr.ToLower() == "price")
                                            {
                                                Console.Write("The new price of the game: ");
                                                string inputPrice = Console.ReadLine();
                                                int newPrice;
                                                bool parsablePrice = Int32.TryParse(inputPrice, out newPrice);
                                                if (parsablePrice)
                                                {
                                                    Console.WriteLine($"{visibleBecauseOfMood[i].Name}'s price is {newPrice}.");
                                                    visibleBecauseOfMood[i].Price = newPrice;
                                                    modifying = false;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("The input must be an integer.");
                                                    modifying = false;

                                                }
                                            }
                                            else if (attr.ToLower() == "owned")
                                            {
                                                Console.Write("The new state of the game: ");
                                                string inputOwned = Console.ReadLine();
                                                bool newOwned;
                                                bool parsableOwned = Boolean.TryParse(inputOwned, out newOwned);
                                                if (parsableOwned)
                                                {
                                                    Console.WriteLine($"{visibleBecauseOfMood[i].Name}'s price is {newOwned}.");
                                                    visibleBecauseOfMood[i]._owned = newOwned;
                                                    modifying = false;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("The input must be a boolean variable.");
                                                    modifying = false;

                                                }
                                            }
                                            else if (attr.ToLower() == "finished")
                                            {
                                                Console.Write("The new state of the game: ");
                                                string inputFinish = Console.ReadLine();
                                                bool newFinish;
                                                bool parsableFinish = Boolean.TryParse(inputFinish, out newFinish);
                                                if (parsableFinish)
                                                {
                                                    Console.WriteLine($"{visibleBecauseOfMood[i].Name}'s price is {newFinish}.");
                                                    visibleBecauseOfMood[i]._owned = newFinish;
                                                    modifying = false;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("The input must be a boolean variable.");
                                                    modifying = false;

                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("There is no such option like " + attr + ".\nPress any key(except the big power button) to continue.");
                                                Console.ReadKey();
                                                Console.Clear();
                                                modifying = false;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("There is no game like " + nameOfOwnedGame + ".\nPress any key(except the big power button) to continue.");
                                Console.ReadKey();
                                Console.Clear();
                                modifying = false;

                            }
                        }
                    }
                }
                else if (numberOut == 4)
                {
                    bool finishing = true;
                    while (finishing)
                    {
                        bool ownAnyGames = false;
                        for (int i = 0; i < visibleBecauseOfMood.Count; i++)
                        {
                            if (visibleBecauseOfMood[i]._owned == true)
                            {
                                ownAnyGames = true;
                            }
                        }
                        if (!ownAnyGames)
                        {
                            Console.WriteLine("You don't own any games to modify it's finish statement.\nPress any key(except the big power button) to continue.");
                            Console.ReadKey();
                            Console.Clear();

                            finishing = false;
                        }
                        else
                        {
                            Console.Write("Input the title of the game you wish to mark as completed: ");
                            string inputTitle = Console.ReadLine();
                            bool foundTitle = false;
                            for (int i = 0; i < visibleBecauseOfMood.Count; i++)
                            {
                                if (visibleBecauseOfMood[i].Name.ToLower() == inputTitle.ToLower() && visibleBecauseOfMood[i]._finished == false && visibleBecauseOfMood[i]._owned == true)
                                {
                                    foundTitle = true;
                                    visibleBecauseOfMood[i]._finished = true;
                                    Console.WriteLine("You have successfully finished " + visibleBecauseOfMood[i].Name + ".\nPress any key(except the big power button) to continue.");
                                    Console.ReadKey();
                                    Console.Clear();
                                    finishing = false;
                                }
                            }


                            if (!foundTitle)
                            {
                                Console.Clear();
                                Console.WriteLine("There is no such game you own called " + inputTitle + ".\nPress any key(except the big power button) to continue.");
                                Console.ReadKey();
                                Console.Clear();
                                finishing = false;
                            }
                        }
                    }
                }
                else if (numberOut == 5)
                {
                    bool deleting = true;
                    while (deleting)
                    {
                        bool ownAnyGames = false;
                        for (int i = 0; i < visibleBecauseOfMood.Count; i++)
                        {
                            if (visibleBecauseOfMood[i]._owned == true)
                            {
                                ownAnyGames = true;
                            }
                        }
                        if (!ownAnyGames)
                        {
                            Console.WriteLine("You don't own any games to delete.\nPress any key(except the big power button) to continue.");
                            Console.ReadKey();
                            Console.Clear();
                            deleting = false;
                        }
                        else
                        {
                            Console.Write("Input the title of the game you wish to delete: ");
                            string inputTitle = Console.ReadLine();
                            Game game = findGame(visibleBecauseOfMood,inputTitle.ToLower());
                            if (game != null && game._owned == true)
                            {
                                game._owned = false;
                                game._finished = false;
                                Console.WriteLine("You have successfully deleted " + game.Name + ".");
                                System.Threading.Thread.Sleep(1200);
                                Console.Clear();
                                deleting = false;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("There is no such game you own called " + inputTitle + ".\nPress any key(except the big power button) to continue.");
                                Console.ReadKey();
                                Console.Clear();
                                deleting = false;
                            }
                        }
                    }
                }
                else if (numberOut == 6)
                {
                    string activeMood = GetActiveMood();
                    Console.WriteLine($"Active gaming mood: {activeMood}\n");
                }
                else if (numberOut == 86)
                {
                    activeGamingMood = GetInTheMood();
                    string activeName = GetActiveMood();
                    Console.WriteLine($"Your gaming mood is {activeName}.\n");

                }
                else if (numberOut == 0)
                {
                    Console.Write("Saving current state");

                    fm.Save();
                    Console.WriteLine("\n\nSuccessfully saved.");
                    Console.ReadKey();
                    return false;
                }

                else
                {
                    Console.WriteLine("There is no such option.\n");
                }
            }
            else
            {
                Console.WriteLine();
            }
            return true;
        }
        private GamingMood GetInTheMood()
        {
            string activeName = GetActiveMood();
            Console.WriteLine($"Input your gaming mood. The current gaming mood is: {activeName}.\n\nChoose from:\n(0) DoNotCareGamingMood\n" +
                "(1) CheapShitGamingMood\n(2) PickyGamingMood (this one is in progress)");
            string option = Console.ReadLine();
            int numberOut;
            bool success = Int32.TryParse(option, out numberOut);
            if (success && numberOut<=2 && numberOut>=0)
            {
                for (int i = 0; i < allGamingMoods.Count; i++)
                {
                    if (i==numberOut)
                    {
                        Console.Clear();
                        
                        return allGamingMoods[i];
                    }
                }
            }
            Console.Clear();
            Console.WriteLine("Invalid input. Choose a listed number next time. Back to the main menu.");
            return activeGamingMood;
        }
        private string GetActiveMood()
        {
            string activeName = "";
            for (int i = 0; i < activeGamingMood.ToString().Length; i++)
            {
                if (i > 10)
                {
                    activeName += activeGamingMood.ToString()[i];
                }
            }
            return activeName;
        }
        private Game findGame(List<Game> games,string nameOfGame)
        {
            Game game = null;
            for (int i = 0; i < games.Count; i++)
            {
                if (games[i].Name.ToLower() == nameOfGame.ToLower())
                {
                    game = games[i];
                    break;
                }
            }
            return game;
        }
        private List<Game> DropTheLoadToTheVisibleList(List<Game> everyGame)
        {
            List<Game> newList = new List<Game>();
            CheapShitGamingMood cheap = new CheapShitGamingMood();
            if (activeGamingMood==allGamingMoods[0])
            {
                newList = fm._listOfGames;
            }
            else if (activeGamingMood == allGamingMoods[1])
            {
                for (int i = 0; i < fm._listOfGames.Count; i++)
                {
                    if (cheap.WannaPlayWith(fm._listOfGames[i]))
                    {
                        newList.Add(fm._listOfGames[i]);
                    }
                }
            }
            return newList;
        }
        public void Print_menu(string title, string[] list_options, string exit_message)
        {
            Console.WriteLine(title);
            for (int i = 0; i < list_options.Length; i++)
            {
                if (list_options.Length==7 && i==list_options.Length-1)
                {
                    Console.WriteLine($"({(86)}) {list_options[i]}");
                }
                else
                {
                    Console.WriteLine($"({(i + 1)}) {list_options[i]}");
                }
            }
            Console.WriteLine("(0) " + exit_message);
        }
        public void Error_message(string m, string e)
        {
            Console.WriteLine($"{m}.$ {e}");
        }
        public void NiceCLIWriter(List<Game> moodList)
        {
            int lengthOfName = 0;
            int lengthOfPrice = 0;
            int lengthOfRYear = "Release Year".Length;
            int lengthOfOwned = "Owned".Length;
            int lengthOfFin = "Finished".Length;
            int lengthOfGenre = "Genre".Length;

            int j = 0;
            while (j != moodList.Count)
            {

                if (moodList[j].Name.Length > lengthOfName)
                {
                    lengthOfName = moodList[j].Name.Length;
                }
                else if (moodList[j].Price.ToString().Length > lengthOfPrice)
                {
                    lengthOfPrice = moodList[j].Price.ToString().Length;
                }
                else if (moodList[j]._releaseYear.ToString().Length > lengthOfRYear)
                {
                    lengthOfRYear = moodList[j]._releaseYear.ToString().Length;
                }
                else if (moodList[j]._owned.ToString().Length > lengthOfOwned)
                {
                    lengthOfOwned = moodList[j]._owned.ToString().Length;
                }
                else if (moodList[j]._finished.ToString().Length > lengthOfFin)
                {
                    lengthOfFin = moodList[j]._finished.ToString().Length;
                }
                for (int i = 0; i < moodList[j].Genres.Count; i++)
                {
                    if (moodList[j].Genres[i].Length > lengthOfGenre)
                    {
                        lengthOfGenre = moodList[j].Genres[i].Length;
                    }
                }

                j++;
            }
            Console.WriteLine($"|{{0,{lengthOfName + 1}}}|" +
                $"{{1,{lengthOfRYear + 1}}}|{{2,{lengthOfPrice + 1}}}|" +
                $"{{3,{lengthOfOwned + 1}}}|{{4,{ lengthOfGenre + 1}}}",
                "Title", "Release Year", "Price", "Owned", "Genre");
            foreach (Game game in moodList)
            {
                Console.Write($"|{{0,{lengthOfName + 1}}}|" +
                    $"{{1,{lengthOfRYear + 1}}}|{{2,{lengthOfPrice + 1}}}|" +
                    $"{{3,{lengthOfOwned + 1}}}|",
                    game.Name, game._releaseYear, game.Price, game._owned.ToString());
                for (int i = 0; i < game.Genres.Count; i++)
                {
                    if (i == 0)
                    {
                        Console.Write(game.Genres[i]);
                    }
                    else
                    {
                        Console.Write(", " + game.Genres[i]);
                    }
                }
                Console.WriteLine("");
            }
            Console.WriteLine();
        }
    }
}
