
namespace MAUICalculator
{
    public static class CalculatorState
    {
        public static double currentNumber { get; set; } = 0;
        public static double lastNumber { get; set; } = 0;
        public static string currentOperator { get; set; } = "";
        public static bool isResult { get; set; }=false;
    }
}
