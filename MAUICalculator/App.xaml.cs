namespace MAUICalculator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);

            if (MainPage != null)
            {
                var width = (int)MainPage.WidthRequest;
                var height = (int)MainPage.HeightRequest;

                window.Width = width;
                window.Height = height;
            }

            return window;
        }
    }
}
