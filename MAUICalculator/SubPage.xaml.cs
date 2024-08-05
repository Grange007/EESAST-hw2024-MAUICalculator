namespace MAUICalculator;

public partial class SubPage : ContentPage
{
	public SubPage()
	{
		InitializeComponent();
	}


    // 定义一些变量来存储当前输入的数字，当前选择的运算符，以及上一次计算的结果
    private double currentNumber = 0;
    private double lastNumber = 0;
    private string currentOperator = "";
    private bool isResult = false;

    private long Factorial(int num)
    {
        if (num == 1)
            return 1;
        else
            return Factorial(num - 1) * num;
    }

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
        currentNumber = double.Parse(displayLabel.Text);
    }

    private void OnDelClicked(object sender, EventArgs e)
    {
        if (isResult)
        {// 上一个是 =
            displayLabel.Text = "";
            isResult = false;
            currentNumber = 0;
        }
        if (currentOperator != "")
        { // 输入运算符
            currentOperator = "";
            isResult = false;
        }
        else
        { // 输入数字字符
            currentNumber = 0;
            isResult = false;
        }
    }

    // 定义OnOperatorClicked方法来处理运算符按钮点击事件
    private void OnOperatorClicked(object sender, EventArgs e)
    {
        // 获取按钮的文本值
        var button = sender as Button;
        var op = button.Text;

        // 如果当前的运算符不为空，就改变运算符号
        if (currentOperator != "")
        {
            currentOperator = op;
            displayLabel.Text = lastNumber.ToString();
            isResult = false;
        }
        else
        {
            // 否则，就将当前输入的数字赋值给上一次计算的结果
            lastNumber = currentNumber;
            currentNumber = 0;
            displayLabel.Text = "0";
            isResult = false;
            currentOperator = op;
        }
    }

    // 定义OnEqualClicked方法来处理等号按钮点击事件
    private void OnEqualClicked(object sender, EventArgs e)
    {
        // 如果当前选择的运算符不为空，就执行上一次选择的运算，并显示结果
        if (currentOperator != "")
        {
            Calculate();
            displayLabel.Text = lastNumber.ToString();
            isResult = true;
            currentOperator = "";
        }
    }

    // 定义OnEqualClicked方法来处理等号按钮点击事件
    private void OnClearClicked(object sender, EventArgs e)
    {
        currentNumber = 0;
        lastNumber = 0;
        currentOperator = "";
        isResult = false;
        displayLabel.Text = lastNumber.ToString();
    }

    // 定义OnFunctionClicked方法处理函数按钮点击时间
    private void OnFunctionClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var func = button.Text;


        if(currentOperator != "")
        { // 不能以运算符为函数自变量 
            displayLabel.Text = "Invalid Input for Function";
            isResult = false;
            currentOperator = "";
        }
        else
        {
            
            switch (func)
            {
                case "lg x":
                    if (currentNumber < 0)
                        displayLabel.Text = "Negative numbers can't have logarithm!";
                    else if (currentNumber == 0)
                        displayLabel.Text = "-∞";
                    else
                        lastNumber = currentNumber;
                        currentNumber = Math.Log10(currentNumber);
                        displayLabel.Text = currentNumber.ToString();
                    break;

                case "ln x":
                    if (currentNumber < 0)
                        displayLabel.Text = "Negative numbers can't have logarithm!";
                    else if (currentNumber == 0)
                        displayLabel.Text = "-∞";
                    else
                        lastNumber = currentNumber;
                        currentNumber = Math.Log(currentNumber);
                        displayLabel.Text = currentNumber.ToString();
                    break;

                case "sin x":
                    lastNumber = currentNumber;
                    currentNumber = Math.Sin(currentNumber);
                    displayLabel.Text = currentNumber.ToString();
                    break;

                case "cos x":
                    lastNumber = currentNumber;
                    currentNumber = Math.Cos(currentNumber);
                    displayLabel.Text = currentNumber.ToString();
                    break;

                case "tan x":
                    lastNumber = currentNumber;
                    currentNumber = Math.Tan(currentNumber);
                    displayLabel.Text = currentNumber.ToString();
                    break;

                case "√x":
                    if(currentNumber < 0)
                    { 
                        displayLabel.Text = "Negativa numbers can't have square root!";
                        break; 
                    }
                    lastNumber = currentNumber;
                    currentNumber = Math.Pow(currentNumber, 1 / 2);
                    displayLabel.Text = currentNumber.ToString();
                    break;

                case "x!":
                    int result = 0;
                    if(int.TryParse(currentNumber.ToString(),out result))
                    {
                        displayLabel.Text = Factorial(result).ToString();
                        lastNumber = currentNumber;
                        currentNumber = result;
                    }
                    else
                    {
                        displayLabel.Text = "Only Integers have Factorial (Elementary)";
                    }
                    break;

                case "arcsin x":

                    lastNumber = currentNumber;
                    currentNumber = Math.Asin(currentNumber);
                    displayLabel.Text = currentNumber.ToString();
                    break;

                case "floor x":

                    lastNumber = currentNumber;
                    currentNumber = Math.Floor(currentNumber);
                    displayLabel.Text = currentNumber.ToString();
                    break;
            }

        }

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
            case "x^y":
                lastNumber = Math.Pow(lastNumber, currentNumber);
                break;

            default:
                break;
        }
        lastNumber = Math.Round(lastNumber, 4);
        currentNumber = lastNumber;
    }
}