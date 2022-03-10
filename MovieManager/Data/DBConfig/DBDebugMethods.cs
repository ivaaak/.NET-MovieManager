using MovieManager.Services;
using MovieManager.Services.ServicesContracts;

namespace MovieManager.Data.DBConfig
{
    public class DbDebugMethods
    {
        private IAddToDbService addToDb;

        public DbDebugMethods(IAddToDbService addToDb)
        {
            this.addToDb = addToDb;
        }

        public static void CheckDbInitialized(MovieContext context)
        {
            context.Database.EnsureCreated();
            Console.WriteLine("Context is Initialized and DB exists!");
        }


        //clear tables for debug
        public static void ClearMovieLists()
        {
            var context = new MovieContext();

            context.Playlists.RemoveRange(context.Playlists);
            Console.WriteLine("Deleted all data in the table CurrentMovies");

            context.SaveChangesAsync();
            context.Dispose();
        }

       
        //fill tables for debug
        public static void FillMovies(SearchMethodsService searchMethods, AddToDbService addToDb)
        {
			System.Timers.Timer t = new System.Timers.Timer();
            t.Start();
            var context = new MovieContext();
            searchMethods.SearchMovieTitleAndSaveToDb("blade runner", addToDb);
            searchMethods.SearchMovieTitleAndSaveToDb("fargo", addToDb);
            searchMethods.SearchMovieTitleAndSaveToDb("the ghost and the darkness", addToDb);
            searchMethods.SearchMovieTitleAndSaveToDb("fight club", addToDb);
            searchMethods.SearchMovieTitleAndSaveToDb("inherent vice", addToDb);
            searchMethods.SearchMovieTitleAndSaveToDb("from russia with", addToDb);
            searchMethods.SearchMovieTitleAndSaveToDb("you were never really here", addToDb);
            searchMethods.SearchMovieTitleAndSaveToDb("le samourai", addToDb);
            searchMethods.SearchShowTitleAndSaveToDb("ozark", addToDb);
            searchMethods.SearchShowTitleAndSaveToDb("primal", addToDb);
            searchMethods.SearchShowTitleAndSaveToDb("fargo", addToDb);
            searchMethods.SearchShowTitleAndSaveToDb("vikings", addToDb);
            searchMethods.SearchMovieTitleAndSaveToDb("primal", addToDb);
            searchMethods.SearchMovieTitleAndSaveToDb("deer", addToDb);
            searchMethods.SearchMovieTitleAndSaveToDb("batman", addToDb);

            t.Stop();
            Console.WriteLine($"Filled the table Movies with example data. Time elapsed: {t.Interval}");

            context.SaveChanges();
            context.Dispose();
        }



        private static void PrintAndExportEntityToFile(string entityOutput, string outputPath)
        {
            Console.WriteLine(entityOutput);
            File.WriteAllText(outputPath, entityOutput.TrimEnd());
        }


        private static string GetProjectDirectory()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var directoryName = Path.GetFileName(currentDirectory);
            var relativePath = directoryName.StartsWith("netcoreapp") ? @"../../../" : string.Empty;

            return relativePath;
        }


        public static void PrintJsonTxt()
        {
            var jsonPath = "D:\\Softuni\\WEB PROJ IDEA\\MOVI\\Backend C# EF\\MovieManager\\Movies\\JSONstring.txt";
            string json = System.IO.File.ReadAllText(jsonPath);
            Console.WriteLine(json);
        }
    }
}
