namespace MAUICalculator
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        public double currentNumber = 0;
        public double lastNumber = 0;
        public string currentOperator = "";
        public bool isResult = false;
        //public static Label displayLabel;
        public MainPage()
        {
            InitializeComponent();
        }

        public string GetDisplayText()
        {
            return displayLabel.Text;
        }
        // 定义一些变量来存储当前输入的数字，当前选择的运算符，以及上一次计算的结果


        // 定义OnNumberClicked方法来处理数字按钮点击事件
        private void OnNumberClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var number = button.Text;

            if (isResult || displayLabel.Text == "0")
            {
                displayLabel.Text = "";
                if (number == ".")
                    displayLabel.Text = "0";
                isResult = false;
            }

            displayLabel.Text += number;
            currentNumber = double.Parse(displayLabel.Text);
        }

        // 定义OnOperatorClicked方法来处理运算符按钮点击事件
        private void OnOperatorClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var op = button.Text;

            // 如果当前显示的是结果，或者没有数字输入，不进行运算，只更新当前运算符
            if (isResult || currentOperator != "")
            {
                currentOperator = op;
                isResult = false;
                return;
            }

            // 如果当前的运算符不为空，并且有有效的数字输入，才执行计算
            if (!string.IsNullOrEmpty(currentOperator))
            {
                Calculate();
                displayLabel.Text = lastNumber.ToString();
                isResult = true;
            }
            else
            {
                // 否则，将当前输入的数字赋值给上一次计算的结果
                lastNumber = currentNumber;
                displayLabel.Text = "0";
                isResult = false;
            }

            // 更新当前选择的运算符
            currentOperator = op;
        }


        // 定义OnEqualClicked方法来处理等号按钮点击事件
        private void OnEqualClicked(object sender, EventArgs e)
        {
            if (currentOperator != "")
            {
                Calculate();
                displayLabel.Text = lastNumber.ToString();
                isResult = true;
                currentOperator = "";
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
        }

        // 定义OnDelClicked方法来处理DEL按钮点击事件
        private void OnDelClicked(object sender, EventArgs e)
        {
            // 如果显示的是结果，则清空显示但保留lastNumber
            if (isResult)
            {
                displayLabel.Text = "0";
                isResult = false;
                return;
            }

            // 如果显示不为空，删除最后一个字符
            if (!string.IsNullOrEmpty(displayLabel.Text))
            {
                displayLabel.Text = displayLabel.Text.Substring(0, displayLabel.Text.Length - 1);

                // 如果删除后显示为空，则重置为0
                if (string.IsNullOrEmpty(displayLabel.Text))
                {
                    displayLabel.Text = "0";
                }

                // 更新currentNumber
                if (double.TryParse(displayLabel.Text, out double number))
                {
                    currentNumber = number;
                }
                else
                {
                    currentNumber = 0;
                }
            }
        }

        // 定义Calculate方法来执行运算逻辑
        private void Calculate()
        {
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
