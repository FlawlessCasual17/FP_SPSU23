namespace FinalProject {
    // ReSharper disable once RedundantExtendsListEntry
    public partial class MainPage : ContentPage {
        public MainPage() => InitializeComponent();

        private void OnClickLogIn(object sender, EventArgs e)
            => Navigation.PushAsync(new Login());
    }
}
