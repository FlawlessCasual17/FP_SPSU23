namespace FinalProject {
    public partial class SignUp {
        public SignUp() => InitializeComponent();

        private async void OnClickedSignUp(object sender, EventArgs ev) {
            // Gets the database path.
            var dbPath = Path.Join(
                Environment.GetEnvironmentVariable("USERPROFILE"), "creds.db");
            // Creates a new database.
            var database = new GymManDB(dbPath);

            // Creates a new gym member.
            var member = new GymMember {
                Email = uEmail.Text,
                Password = Password.Text
            };

            // Saves the user to the database.
            await database.SaveUserAsync(member);
            // Displays a success message to inform the user.
            await DisplayAlert(
                "Success",
                "You have successfully registered your account.",
                "OK");
        }
    }
}
