namespace FinalProjectV9 {
    public partial class MainPage : ContentPage {

        public MainPage() { InitializeComponent(); }

        private void OnClickLogIn(object sender, EventArgs e) => Navigation.PushAsync(new Login());
    }
}
