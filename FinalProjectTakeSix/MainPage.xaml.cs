namespace FinalProjectTakeSix {
    public partial class MainPage : ContentPage {
        public MainPage() { InitializeComponent(); }

        private void OnClickSignUp(object sender, EventArgs e) { Shell.Current.GoToAsync("//SignUp"); }

        private void OnClickLogIn(object sender, EventArgs e) { Shell.Current.GoToAsync("//LogIn"); }
    }
}
