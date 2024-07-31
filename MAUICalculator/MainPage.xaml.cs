namespace MAUICalculator
{
    public partial class MainPage : ContentPage
    {

        public static MainPage? Current;

        public MainPage()
        {
            InitializeComponent();
            Current = this;
        }

        ~MainPage()
        {
            if (Current == this)
                Current = null;
        }

        // 定义一些变量来存储当前输入的数字，当前选择的运算符，以及上一次计算的结果
        private double currentNumber = 0;
        private bool currentNumberDied = false;
        private double lastNumber = 0;
        private string currentOperator = "";
        private bool isResult = false;

        public void LoadFromData()
        {
            // 同步两个页面的数据
            currentNumber = CalculatorData.currentNumber;
            currentNumberDied = CalculatorData.currentNumberDied;
            lastNumber = CalculatorData.lastNumber;
            currentOperator = CalculatorData.currentOperator;
            isResult = CalculatorData.isResult;
            displayLabel.Text = CalculatorData.displayText;
            historyLabel.Text = CalculatorData.historyText;

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // 同步两个页面的数据
            CalculatorData.currentNumber = currentNumber;
            CalculatorData.currentNumberDied = currentNumberDied;
            CalculatorData.lastNumber = lastNumber;
            CalculatorData.currentOperator = currentOperator;
            CalculatorData.isResult = isResult;
            CalculatorData.displayText = displayLabel.Text;
            CalculatorData.historyText = historyLabel.Text;

            SubPage.Current?.LoadFromData();

        }

        // 定义OnNumberClicked方法来处理数字按钮点击事件
        private void OnNumberClicked(object sender, EventArgs e)
        {
            // 获取按钮的文本值
            var button = sender as Button;
            var number = button.Text;

            // 如果当前显示的是结果，或者是0，或者是旧输入的数字，就清空显示屏
            if (isResult || displayLabel.Text == "0" || currentNumberDied)
            {
                displayLabel.Text = "";
                if (number == ".")
                    displayLabel.Text = "0";
                isResult = false;
                currentNumberDied = false;
            }

            // 将数字追加到显示屏，并更新当前输入的数字
            displayLabel.Text += number;
            //currentOperator = "";
            currentNumber = double.Parse(displayLabel.Text);
        }

        // 定义OnOperatorClicked方法来处理运算符按钮点击事件
        private void OnOperatorClicked(object sender, EventArgs e)
        {
            // 获取按钮的文本值
            var button = sender as Button;
            var op = button.Text;

            // 如果当前的运算符不为空
            if (currentOperator != "")
            {
                // 如果未输入数字，只改变运算符
                if (isResult || displayLabel.Text == "0" || currentNumberDied)
                {
                    // Do nothing.
                }
                // 如果已输入数字，就执行上一次选择的运算，并显示结果
                else
                {
                    Calculate();
                    currentNumber = lastNumber;
                }
            }

            // 如果当前的运算符为空，就将当前输入的数字赋值给上一次计算的结果
            if (currentOperator == "")
            {
                 lastNumber = currentNumber;
                 isResult = false;

            }

            // 将当前选择的运算符赋值给变量，并预备清空当前输入的数字
            currentOperator = op;
            currentNumberDied = true;

            // 更新historyLabel显示当前的计算过程
            historyLabel.Text = $"{lastNumber} {currentOperator} ";
        }

        // 定义OnEqualClicked方法来处理等号按钮点击事件
        private void OnEqualClicked(object sender, EventArgs e)
        {

            // 如果当前选择的运算符不为空，就执行上一次选择的运算，并显示结果
            if (currentOperator != "")
            {
                // 更新historyLabel显示当前的计算过程
                historyLabel.Text = $"{lastNumber} {currentOperator} {currentNumber} = ";

                Calculate();
                displayLabel.Text = lastNumber.ToString();
                isResult = true;
            }
        }

        // 定义OnClearClicked方法来处理清除按钮点击事件
        private void OnClearClicked(object sender, EventArgs e)
        {
            currentNumber = 0;
            lastNumber = 0;
            currentOperator = "";
            isResult = false;
            displayLabel.Text = lastNumber.ToString();
            historyLabel.Text = "";
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
                case "−":
                    lastNumber -= currentNumber;
                    break;
                case "×":
                    lastNumber *= currentNumber;
                    break;
                case "÷":
                    lastNumber /= currentNumber;
                    break;
                default:
                    break;
            }
            lastNumber = Math.Round(lastNumber, 4);
            // currentNumber = lastNumber;
        }

        // 定义OnDeleteClicked方法来处理删除按钮点击事件
        private void OnDeleteClicked(object sender, EventArgs e)
        {
            // 当上一个字符为"="时，清空显示屏但不改变所存储的计算结果
            if (isResult)
            {
                currentNumber = double.Parse(displayLabel.Text);
                historyLabel.Text = displayLabel.Text;
                currentOperator = "";
                isResult = false;
            }
            else
                currentNumber = 0;
            displayLabel.Text = "0";
        }
    }

}
