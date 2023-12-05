namespace FinalProject {
    /// <summary>A class for building the MAUI application.</summary>
    public static class MauiProgram {
        /// <summary>
        /// Adds the required fonts and
        /// builds the MAUI application.
        /// </summary>
        public static MauiApp CreateMauiApp()
            => MauiApp.CreateBuilder().UseMauiApp<App>()
                .ConfigureFonts(fonts => {
                    fonts.AddFont(
                        "OpenSans-Regular.ttf",
                        "OpenSansRegular");
                    fonts.AddFont(
                        "OpenSans-Semibold.ttf",
                        "OpenSansSemibold");
                })
                .Build();
    }
}
