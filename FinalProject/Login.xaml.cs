namespace FinalProject {
    // ReSharper disable once RedundantExtendsListEntry
    /// <summary>Adds functionality to the relevant XAML content page.</summary>
    public partial class Login : ContentPage {
        /// <summary>
        /// A global private readonly static variable for use
        /// within the <see cref="Login"/> class.
        /// </summary>
        private static string DbPath
            => Path.Join(Environment
                .GetEnvironmentVariable("USERPROFILE"), "creds.db");

        /// <summary>
        /// A constructor for initializing a new
        /// instance of the <see cref="Login"/> class.
        /// </summary>
        public Login() => InitializeComponent();

        /// <summary>
        /// A method that executes when the
        /// <see cref="Button"/> in the
        /// relevant XAML page is clicked.
        /// </summary>
        private async void OnClickedLogIn(object sender, EventArgs ev) {
            // Checks if the user exists in the database.
            var isValid = await new GymManDB(DbPath)
                .CheckUserCredentials(uEmail.Text, Password.Text);

            // Acquires the user from the database.
            var user = await new GymManDB(DbPath)
                .GetUserAsync(uEmail.Text, Password.Text);

            // "switch" statements are faster than "if" statements.
            switch (isValid) {
                case true when user is not null:
                    // If the user found in the database, report login successful.
                    await DisplayAlert(
                        "Login",
                        "Login Successful",
                        "OK"); break;
                default:
                    // If the user not found in the database, report login failed.
                    await DisplayAlert(
                        "Login",
                        "Invalid email or password...",
                        "Try again"); break;
            }
        }

        /// <summary>
        /// A method that is executed when the
        /// user clicks on the "register" section
        /// in the associated XAML page.
        /// </summary>
        private void OnRegisterTapped(object sender, EventArgs ev)
            => Navigation.PushAsync(new SignUp());
    }
}
