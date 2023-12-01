namespace FinalProjectV9 {
    public partial class Login : ContentPage {
        public Login() { InitializeComponent(); }

        private void OnClickedLogIn(object sender, EventArgs e) {
            //add something here to navigate to gym management page
        }

        private void OnRegisterTapped(object sender, EventArgs e) => Navigation.PushAsync(new SignIn());
    }
}
