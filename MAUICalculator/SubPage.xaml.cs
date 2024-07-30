using System;
namespace MAUICalculator;

public partial class SubPage : ContentPage
{
    public SubPage()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        displayLabel.Text = Channel.text;
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Channel.text = displayLabel.Text;
    }

    // 定义OnNumberClicked方法来处理数字按钮点击事件
    private void OnNumberClicked(object sender, EventArgs e)
    {
        // 获取按钮的文本值
        var button = sender as Button;
        var number = button.Text;
        Channel.type = 1;

        // 如果当前显示的是结果，或者是0，就清空显示屏
        if (Channel.isResult || displayLabel.Text == "0")
        {
            displayLabel.Text = "";
            if (number == ".")
                displayLabel.Text = "0";
            Channel.isResult = false;
        }

        // 将数字追加到显示屏，并更新当前输入的数字
        displayLabel.Text += number;
        if (number == "e")
            Channel.currentNumber = Math.E;
        else if (number == "pi")
            Channel.currentNumber = Math.PI;
        else
            Channel.currentNumber = double.Parse(displayLabel.Text);
        Channel.flag = 1;
    }

    // 定义OnOperatorClicked方法来处理运算符按钮点击事件
    private void OnOperatorClicked(object sender, EventArgs e)
    {
        // 获取按钮的文本值
        var button = sender as Button;
        var op = button.Text;
        Channel.type = 2;

        // 如果当前的运算符不为空，就执行上一次选择的运算，并显示结果
        if (Channel.currentOperator != "" && Channel.flag != 0)
        {
            Calculate();
            displayLabel.Text = Channel.lastNumber.ToString();
            Channel.isResult = true;
        }
        else
        {
            // 否则，就将当前输入的数字赋值给上一次计算的结果
            Channel.flag = 0;
            Channel.lastNumber = Channel.currentNumber;
            displayLabel.Text = "0";
            Channel.isResult = false;
        }

        // 将当前选择的运算符赋值给变量，并清空当前输入的数字
        Channel.currentOperator = op;
    }

    private void OnMonoOperatorClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var op = button.Text;
        Channel.flag = 1;
        Channel.lastNumber = Channel.currentNumber;
        displayLabel.Text = "0";
        Channel.isResult = false;
        Channel.currentOperator = op;
    }

    // 定义OnEqualClicked方法来处理等号按钮点击事件
    private void OnEqualClicked(object sender, EventArgs e)
    {
        // 如果当前选择的运算符不为空，就执行上一次选择的运算，并显示结果
        Channel.type = 3;
        if (Channel.currentOperator != "")
        {
            Calculate();
            displayLabel.Text = Channel.lastNumber.ToString();
            Channel.isResult = true;
            Channel.currentOperator = "";
        }
    }

    // 定义OnClearClicked方法来处理AC按钮点击事件
    private void OnClearClicked(object sender, EventArgs e)
    {
        Channel.currentNumber = 0;
        Channel.lastNumber = 0;
        Channel.currentOperator = "";
        Channel.isResult = false;
        displayLabel.Text = Channel.lastNumber.ToString();
    }

    // 定义Calculate方法来执行运算逻辑
    private void Calculate()
    {
        // 根据当前选择的运算符，对上一次计算的结果和当前输入的数字进行相应的运算，并更新上一次计算的结果
        switch (Channel.currentOperator)
        {
            case "+":
                Channel.lastNumber += Channel.currentNumber;
                break;
            case "-":
                Channel.lastNumber -= Channel.currentNumber;
                break;
            case "*":
                Channel.lastNumber *= Channel.currentNumber;
                break;
            case "/":
                Channel.lastNumber /= Channel.currentNumber;
                break;
            case "lg":
                Channel.lastNumber = Math.Log10(Channel.lastNumber);
                break;
            case "ln":
                Channel.lastNumber = Math.Log(Channel.lastNumber);
                break;
            case "sin":
                Channel.lastNumber = Math.Sin(Channel.lastNumber);
                break;
            case "cos":
                Channel.lastNumber = Math.Cos(Channel.lastNumber);
                break;
            case "tan":
                Channel.lastNumber = Math.Tan(Channel.lastNumber);
                break;
            case "sqrt":
                Channel.lastNumber = Math.Sqrt(Channel.lastNumber);
                break;
            case "pow":
                Channel.lastNumber = Math.Pow(Channel.lastNumber, Channel.currentNumber);
                break;
            case "!":
                for (int iteration = (int)(Math.Floor(Channel.lastNumber) - 1); iteration > 0; --iteration)
                {
                    Channel.lastNumber *= iteration;
                }
                break;
            default:
                break;
        }
        Channel.lastNumber = Math.Round(Channel.lastNumber, 4);
        Channel.currentNumber = Channel.lastNumber;
    }

    // 定义OnDELClicked方法来处理DEL按钮点击事件
    private void OnDELClicked(object sender, EventArgs e)
    {
        if (Channel.type == 1)
        {
            Channel.currentNumber = 0;
            displayLabel.Text = "0";
        }
        else if (Channel.type == 2)
            Channel.currentOperator = "";
        else
            displayLabel.Text = "0";
    }
}