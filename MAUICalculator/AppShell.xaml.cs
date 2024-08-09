namespace MAUICalculator
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }
        public static class CalculatorState
        {
            public static double lastnumber = 0;
            public static string displaytext = "";
        }
    }


}
