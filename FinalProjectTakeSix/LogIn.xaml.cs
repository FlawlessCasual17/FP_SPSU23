namespace FinalProjectTakeSix {
    public partial class LogIn : ContentPage {
        public LogIn() { InitializeComponent(); }

        private void OnClickLogIn(object sender, EventArgs e) {
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;

            var loginSuccess = CheckCredentials(username, password);

            SuccessLabel.Text = loginSuccess ? "Login successful!" : "Invalid username or password.";
        }

        private bool CheckCredentials(string username, string password) {
            var member = DbService.Instance.GetMemberByUsernameAndPassword(username, password);
            return member != null;
        }
    }
}
