using System;
using NLog.Web;
using System.IO;
using System.Collections;

namespace MovieLibraryDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\nlog.config";

            // create instance of Logger
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();
            // logs program has started
            logger.Info("Program has started");
            // file variable for movies file
            var file = "movies.csv";
            // Menu to direct user to path
            Console.WriteLine("Enter 1 to read data.");
            Console.WriteLine("Enter 2 to add data.");
            Console.WriteLine("Enter any key to exit.");
            // stores user input to proper path
            string input = Console.ReadLine();

            // If statement for reading data
            if (input == "1"){
                // If file exists, read file
                if (File.Exists(file)){
                    StreamReader sr = new StreamReader(file);
                    // while loop to go for as long as there is still data to read
                    while(!sr.EndOfStream){
                        // stores each line into variable
                        String line = sr.ReadLine();
                        // creates array and splits info by comma
                        // arr[0] = ID
                        // arr[1] = Title
                        // arr[2] = Genres
                        String [] arr = line.Split(',');
                        // 3 Arraylists for each variable needed to display
                        ArrayList movies = new ArrayList();
                        ArrayList movieID = new ArrayList();
                        ArrayList genres = new ArrayList();

                        // sets moves equal to arr[1] to use replace method
                        string splitMovies = arr[1];
                        // sets genres equalt to arr[2] to use replace method
                        string splitGenres = arr[2];
                        // adds ID into correct Arraylist
                        movieID.Add(arr[0]);
                        // Replaces any unneccessary characters with space for formatting
                        movies.Add(splitMovies.Replace('"', ' '));
                        genres.Add(splitGenres.Replace('|', ' '));
                        
                        // Displays information and formats them 3 spaces over
                        Console.WriteLine($"ID: {movieID[0], 3}");
                        Console.WriteLine($"Title: {movies[0], 3}");
                        Console.WriteLine($"Genres: {genres[0], 3}\n");
                        
                        // loop for testing each Arraylist

                      /*  foreach(string element in movies){
                            Console.WriteLine(element);
                        } */       

                    }
                } 

                // else if statement to add data to file
            } else if (input == "2"){
                // initializes StreamWriter and sets append to true to prevent overwriting
                StreamWriter sw = new StreamWriter(file, append:true);
                // asks for user to enter movie ID 
                Console.WriteLine("Enter Movie ID");
                // Exception handling to make sure they enter numbers
                // stores it in variable called id
                int id;
                if(!int.TryParse(Console.ReadLine(), out id)){
                    throw new Exception("The ID must be numbers only");
                }
                // asks user to enter movie title
                Console.WriteLine("Enter Movie Title");
                // stores it in variable called title
                string title = Console.ReadLine();
                // asks user to enter genre
                Console.WriteLine("Enter Genres");
                // stores it in variable called genres
                string genres = Console.ReadLine();
                // writes each piece of information to file 
                sw.WriteLine("{0}, {1}, {2}", id, title, genres);
                // closes stream writer
                sw.Close();

                // logger to warn if file does not exist
            } else {logger.Warn("file does not exist. {file}", file);}
            // logs that program has ended
            logger.Info("Program has ended");
        }
    }
}
