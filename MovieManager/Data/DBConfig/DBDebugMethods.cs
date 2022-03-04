using MovieManager.Services;

namespace MovieManager.Data.DBConfig
{
    public class DbDebugMethods
    {
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
        public static void FillMovies()
        {
			System.Timers.Timer t = new System.Timers.Timer();
            t.Start();
            var context = new MovieContext();
            SearchMethods.SearchMovieTitleAndSaveToDb("blade runner");
            SearchMethods.SearchMovieTitleAndSaveToDb("fargo");
            SearchMethods.SearchMovieTitleAndSaveToDb("the ghost and the darkness");
            SearchMethods.SearchMovieTitleAndSaveToDb("fight club");
            SearchMethods.SearchMovieTitleAndSaveToDb("inherent vice");
            SearchMethods.SearchMovieTitleAndSaveToDb("from russia with");
            SearchMethods.SearchMovieTitleAndSaveToDb("you were never really here");
            SearchMethods.SearchMovieTitleAndSaveToDb("le samourai");
            SearchMethods.SearchShowTitleAndSaveToDb("ozark");
            SearchMethods.SearchShowTitleAndSaveToDb("primal");
            SearchMethods.SearchShowTitleAndSaveToDb("fargo");
            SearchMethods.SearchShowTitleAndSaveToDb("vikings");
            SearchMethods.SearchMovieTitleAndSaveToDb("primal");
            SearchMethods.SearchMovieTitleAndSaveToDb("deer");
            SearchMethods.SearchMovieTitleAndSaveToDb("batman");

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
