namespace FinalProject {
    // ReSharper disable once RedundantExtendsListEntry
    /// <summary>
    /// A class that adds functionality to the
    /// relevant XAML content page.
    /// </summary>
    public partial class MainPage : ContentPage {
        /// <summary>
        /// A constructor for initializing a new
        /// instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage() => InitializeComponent();

        /// <summary>
        /// A method that executes when the
        /// <see cref="Button"/> in the
        /// relevant XAML page is clicked.
        /// </summary>
        private void OnClickLogIn(object sender, EventArgs e)
            => Navigation.PushAsync(new Login());
    }
}
