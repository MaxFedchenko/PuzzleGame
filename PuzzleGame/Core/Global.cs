namespace PuzzleGame.Core
{
    public static class Global
    {
        public static IConfigurationRoot AppSettings;

        static Global() 
        {
            AppSettings = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
        }
    }
}
