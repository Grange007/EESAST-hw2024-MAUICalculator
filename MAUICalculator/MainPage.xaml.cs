namespace MAUICalculator
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        public MainPage()
        {
            InitializeComponent();
            RestoreState();
        }

        private void RestoreState()
        {
            displayLabel.Text = AppShell.CalculatorState.displaytext;
            lastNumber = AppShell.CalculatorState.lastnumber;
        }

        private void SaveState()
        {
            AppShell.CalculatorState.displaytext = displayLabel.Text;
            AppShell.CalculatorState.lastnumber = lastNumber;
        }

        // 定义一些变量来存储当前输入的数字，当前选择的运算符，以及上一次计算的结果
        private double currentNumber = 0;
        private double lastNumber = 0;
        private string currentOperator = "";
        private bool isResult = false;
        // 定义OnNumberClicked方法来处理数字按钮点击事件
        private void OnNumberClicked(object sender, EventArgs e)
        {
            // 获取按钮的文本值
            var button = sender as Button;
            var number = button.Text;

            // 如果当前显示的是结果，或者是0，就清空显示屏
            if (isResult || displayLabel.Text == "0")
            {
                displayLabel.Text = "";
                if (number == ".")
                    displayLabel.Text = "0";
                isResult = false;
            }

            // 将数字追加到显示屏，并更新当前输入的数字
            displayLabel.Text += number;
            currentNumber = double.Parse(number);
            SaveState();
        }

        private void OnDelClicked(object sender, EventArgs e)
        {
            if (isResult)
            {// 上一个是 =
                displayLabel.Text = "";
                isResult = false;
                currentNumber = 0;
            }
            if(currentOperator != "")
            { // 输入运算符
                currentOperator = "";
                isResult=false;
            }
            else
            { // 输入数字字符
                currentNumber = 0;
                isResult = false;
            }
            SaveState();
        }

        // 定义OnOperatorClicked方法来处理运算符按钮点击事件
        private void OnOperatorClicked(object sender, EventArgs e)
        {
            RestoreState();
            // 获取按钮的文本值
            var button = sender as Button;
            var op = button.Text;

            // 如果当前的运算符不为空，就改变运算符号
            if (currentOperator != "")
            {
                currentOperator = op;
                displayLabel.Text = lastNumber.ToString() + op;
                isResult = false;
            }
            else
            {
                // 否则，就将当前输入的数字赋值给上一次计算的结果
                lastNumber = currentNumber;
                currentNumber = 0;
                displayLabel.Text = lastNumber.ToString() + op;
                isResult = false;
                currentOperator = op;
            }
            SaveState();
        }

        // 定义OnEqualClicked方法来处理等号按钮点击事件
        private void OnEqualClicked(object sender, EventArgs e)
        {
            RestoreState();
            // 如果当前选择的运算符不为空，就执行上一次选择的运算，并显示结果
            if (currentOperator != "")
            {
                Calculate();
                displayLabel.Text = lastNumber.ToString();
                isResult = true;
                currentOperator = "";
            }
            SaveState();
        }

        // 定义OnEqualClicked方法来处理等号按钮点击事件
        private void OnClearClicked(object sender, EventArgs e)
        {
            currentNumber = 0;
            lastNumber = 0;
            currentOperator = "";
            isResult = false;
            displayLabel.Text = lastNumber.ToString();
            SaveState();
        }

        // 定义Calculate方法来执行运算逻辑
        private void Calculate()
        {
            // 根据当前选择的运算符，对上一次计算的结果和当前输入的数字进行相应的运算，并更新上一次计算的结果
            switch (currentOperator)
            {
                case "+":
                    lastNumber += currentNumber;
                    break;
                case "-":
                    lastNumber -= currentNumber;
                    break;
                case "*":
                    lastNumber *= currentNumber;
                    break;
                case "/":
                    lastNumber /= currentNumber;
                    break;
                default:
                    break;
            }
            lastNumber = Math.Round(lastNumber, 4);
            currentNumber = lastNumber;
        }
    }
}
