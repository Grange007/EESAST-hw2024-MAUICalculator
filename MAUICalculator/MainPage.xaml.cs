namespace MAUICalculator
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        // 定义一些变量来存储当前输入的数字，当前选择的运算符，以及上一次计算的结果
        //private double currentNumber = 0;
        //private double lastNumber = 0;
        //private string currentOperator = "";
        //private bool isResult = false;

        // 定义OnNumberClicked方法来处理数字按钮点击事件
        private void OnNumberClicked(object sender, EventArgs e)
        {
            // 获取按钮的文本值
            var button = sender as Button;
            var number = button.Text;

            // 如果当前显示的是结果，或者是0，就清空显示屏
            if (CalculatorState.isResult || displayLabel.Text == "0")
            {
                displayLabel.Text = "";
                if (number == ".")
                    displayLabel.Text = "0";
                CalculatorState.isResult = false;
            }

            // 将数字追加到显示屏，并更新当前输入的数字
            displayLabel.Text += number;
            CalculatorState.currentNumber = double.Parse(displayLabel.Text);
        }

        // 定义OnOperatorClicked方法来处理运算符按钮点击事件
        private void OnOperatorClicked(object sender, EventArgs e)
        {
            // 获取按钮的文本值
            var button = sender as Button;
            var op = button.Text;

            // 如果当前的运算符不为空，根据最后输入的运算符进行更新，不进行运算
            if (CalculatorState.currentOperator != "")
            {
                //Calculate();
                //displayLabel.Text = lastNumber.ToString();
                //isResult = true;
                CalculatorState.currentOperator = op;
            }
            else
            {
                // 否则，就将当前输入的数字赋值给上一次计算的结果
                CalculatorState.lastNumber = CalculatorState.currentNumber;
                displayLabel.Text = "0";
                CalculatorState.isResult = false;
            }

            // 将当前选择的运算符赋值给变量，并清空当前输入的数字
            CalculatorState.currentOperator = op;
        }

        // 定义OnEqualClicked方法来处理等号按钮点击事件
        private void OnEqualClicked(object sender, EventArgs e)
        {
            // 如果当前选择的运算符不为空，就执行上一次选择的运算，并显示结果
            if (CalculatorState.currentOperator != "")
            {
                Calculate();
                displayLabel.Text = CalculatorState.lastNumber.ToString();
                CalculatorState.isResult = true;
                CalculatorState.currentOperator = "";
            }
        }

        // 定义OnClearClicked方法来处理等号按钮点击事件
        private void OnClearClicked(object sender, EventArgs e)
        {
            CalculatorState.currentNumber = 0;
            CalculatorState.lastNumber = 0;
            CalculatorState.currentOperator = "";
            CalculatorState.isResult = false;
            displayLabel.Text = CalculatorState.lastNumber.ToString();
        }

        // 定义Calculate方法来执行运算逻辑
        private void Calculate()
        {
            // 根据当前选择的运算符，对上一次计算的结果和当前输入的数字进行相应的运算，并更新上一次计算的结果
            try
            {
                switch (CalculatorState.currentOperator)
                {
                    case "ln":
                        if (CalculatorState.currentNumber <= 0)
                            throw new ArgumentOutOfRangeException("Logarithm of non-positive numbers is undefined.");
                        CalculatorState.lastNumber = Math.Log(CalculatorState.currentNumber);
                        break;

                    case "x^y":
                        CalculatorState.lastNumber = Math.Pow(CalculatorState.lastNumber, CalculatorState.currentNumber);
                        break;

                    case "sin":
                        CalculatorState.lastNumber = Math.Sin(CalculatorState.currentNumber);
                        break;

                    case "cos":
                        CalculatorState.lastNumber = Math.Cos(CalculatorState.currentNumber);
                        break;

                    case "+":
                        CalculatorState.lastNumber += CalculatorState.currentNumber;
                        break;
                    case "-":
                        CalculatorState.lastNumber -= CalculatorState.currentNumber;
                        break;
                    case "*":
                        CalculatorState.lastNumber *= CalculatorState.currentNumber;
                        break;
                    case "/":
                        CalculatorState.lastNumber /= CalculatorState.currentNumber;
                        break;

                    default:
                        throw new InvalidOperationException("Invalid operator");
                }

                CalculatorState.lastNumber = Math.Round(CalculatorState.lastNumber, 4);
                CalculatorState.currentNumber = CalculatorState.lastNumber;
            }
            catch (Exception ex)
            {
                CalculatorState.lastNumber = -99999;
                CalculatorState.currentNumber = 0;
            }
        }

        /// <summary>
        /// 定义DEL按键所执行的命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDeleteClicked(object sender, EventArgs e)
        {
            //            displayLabel.Text = $@"
            //currentN = {currentNumber},lastN = {lastNumber},currentOp = {currentOperator},isResult = {isResult}
            //";
            // 当上一个字符为`=`时，清空显示屏但不改变`lastNumber`的值
            if (CalculatorState.isResult) {
                displayLabel.Text = "";
            }
            else
            {
                // 简化考虑：
                // (1) 上一个字符为运算符 <=> 屏幕显示为"0"
                // (2) 上一个字符为数字 <=> 屏幕显示为非零数字
                if (displayLabel.Text == "0")
                {
                    CalculatorState.currentOperator = "";
                }
                else if (displayLabel.Text != "") 
                {
                    // 若上一个字符为数字 从屏幕显示字符串中删除最后一个字符
                    displayLabel.Text = displayLabel.Text.Remove(
                            displayLabel.Text.Length - 1, 1
                        );
                    //  然后转化为浮点数 更新currentNumber 
                    if (displayLabel.Text != "")
                        CalculatorState.currentNumber = double.Parse(displayLabel.Text);
                    else
                        CalculatorState.currentNumber = 0;
                }

            }
        }


        // 切换时保证显示的内容的一致
        protected override void OnAppearing()
        {
            base.OnAppearing();
            displayLabel.Text = CalculatorState.currentNumber.ToString(); // 或根据需要显示的内容更新
        }

    }

}
