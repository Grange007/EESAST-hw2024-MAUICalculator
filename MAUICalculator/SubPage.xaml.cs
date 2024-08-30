namespace MAUICalculator
{
    public partial class SubPage : ContentPage
    {
        private double currentNumber;
        private double lastNumber;
        private string currentOperator;
        private bool isResult;
        private MainPage _mainPage;
        public SubPage(MainPage mainPage)
        {
            InitializeComponent();
            // 在页面加载时，保留MainPage的状态
            _mainPage = mainPage;
            currentNumber = _mainPage.currentNumber;
            lastNumber = _mainPage.lastNumber;
            currentOperator = _mainPage.currentOperator;
            isResult = _mainPage.isResult;
            displayLabel.Text = _mainPage.GetDisplayText();
        }

        private void OnNumberClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var number = button.Text;

            if (isResult || displayLabel.Text == "0")
            {
                displayLabel.Text = "";
                isResult = false;
            }

            displayLabel.Text += number;
            currentNumber = double.Parse(displayLabel.Text);
        }

        private void OnOperatorClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var op = button.Text;

            if (isResult || currentOperator != "")
            {
                currentOperator = op;
                isResult = false;
                return;
            }

            if (!string.IsNullOrEmpty(currentOperator))
            {
                Calculate();
                displayLabel.Text = lastNumber.ToString();
                isResult = true;
            }
            else
            {
                lastNumber = currentNumber;
                displayLabel.Text = "0";
                isResult = false;
            }

            currentOperator = op;
        }

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
            }
            lastNumber = Math.Round(lastNumber, 4);
            currentNumber = lastNumber;
        }

        private void OnPowerClicked(object sender, EventArgs e)
        {
            lastNumber = Math.Pow(lastNumber, currentNumber);
            displayLabel.Text = lastNumber.ToString();
            isResult = true;
        }

        private void OnSqrtClicked(object sender, EventArgs e)
        {
            lastNumber = Math.Sqrt(currentNumber);
            displayLabel.Text = lastNumber.ToString();
            isResult = true;
        }

        private void OnLogClicked(object sender, EventArgs e)
        {
            lastNumber = Math.Log10(currentNumber);
            displayLabel.Text = lastNumber.ToString();
            isResult = true;
        }

        private void OnLnClicked(object sender, EventArgs e)
        {
            lastNumber = Math.Log(currentNumber);
            displayLabel.Text = lastNumber.ToString();
            isResult = true;
        }

        private void OnFactorialClicked(object sender, EventArgs e)
        {
            lastNumber = Factorial((int)currentNumber);
            displayLabel.Text = lastNumber.ToString();
            isResult = true;
        }

        private double Factorial(int n)
        {
            if (n <= 1) return 1;
            return n * Factorial(n - 1);
        }

        private void OnSinClicked(object sender, EventArgs e)
        {
            lastNumber = Math.Sin(currentNumber);
            displayLabel.Text = lastNumber.ToString();
            isResult = true;
        }

        private void OnCosClicked(object sender, EventArgs e)
        {
            lastNumber = Math.Cos(currentNumber);
            displayLabel.Text = lastNumber.ToString();
            isResult = true;
        }

        private void OnTanClicked(object sender, EventArgs e)
        {
            lastNumber = Math.Tan(currentNumber);
            displayLabel.Text = lastNumber.ToString();
            isResult = true;
        }

        private void OnPiClicked(object sender, EventArgs e)
        {
            currentNumber = Math.PI;
            displayLabel.Text = currentNumber.ToString();
        }

        private void OnEClicked(object sender, EventArgs e)
        {
            currentNumber = Math.E;
            displayLabel.Text = currentNumber.ToString();
        }

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

        private void OnDelClicked(object sender, EventArgs e)
        {
            if (isResult)
            {
                displayLabel.Text = "0";
                isResult = false;
                return;
            }

            if (!string.IsNullOrEmpty(displayLabel.Text))
            {
                displayLabel.Text = displayLabel.Text.Substring(0, displayLabel.Text.Length - 1);

                if (string.IsNullOrEmpty(displayLabel.Text))
                {
                    displayLabel.Text = "0";
                }

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

        private void OnClearClicked(object sender, EventArgs e)
        {
            currentNumber = 0;
            lastNumber = 0;
            currentOperator = "";
            isResult = false;
            displayLabel.Text = "0";
        }
    }
}
