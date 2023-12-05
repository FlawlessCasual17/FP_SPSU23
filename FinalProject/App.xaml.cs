namespace FinalProject {
    // ReSharper disable once RedundantExtendsListEntry
    /// <summary>
    /// A class that adds functionality to the
    /// relevant XAML content page.
    /// </summary>
    public partial class App : Application {
        /// <summary>
        ///  A constructor for initializing a new
        ///  instance of the <see cref="App"/> class.
        /// </summary>
        public App() { InitializeComponent(); MainPage = new AppShell(); }
    }
}
