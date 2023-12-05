using SQLite;

namespace FinalProject {
    // ReSharper disable once RedundantExtendsListEntry
    /// <summary>Adds functionality to the relevant XAML content page.</summary>
    public partial class Login : ContentPage {
        /// <summary>
        /// A global private readonly variable for use
        /// within the <see cref="Login"/> class.
        /// </summary>
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly SQLiteAsyncConnection database;

        /// <summary>
        /// A constructor for initializing a new
        /// instance of the <see cref="Login"/> class.
        /// </summary>
        public Login() {
            InitializeComponent();
            var userDir = Environment.GetEnvironmentVariable("USERPROFILE");
            var dbPath = Path.Join(userDir, "creds.db");
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<GymMember>().Wait();
        }

        /// <summary>
        /// A method that executes when the
        /// <see cref="Button"/> in the
        /// relevant XAML content is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        private async void OnClickedLogIn(object sender, EventArgs ev) {
            var user = await new GymManDB().GetUserAsync(UEmail.Text, Password.Text);

            switch (user) {
                case null:
                    // User found in the database, login successful.
                    await DisplayAlert(
                        "Login", "Login Successful",
                        "OK");
                    break;
                    // Navigate to the next page here.
                default:
                    // User not found in the database, login failed.
                    await DisplayAlert(
                        "Login", "Invalid email or password...",
                        "Try again");
                    break;
            }
        }

        private void OnRegisterTapped(object sender, EventArgs e)
            => Navigation.PushAsync(new SignUp());
    }
}
