namespace MAUICalculator;

public partial class SubPage : ContentPage
{
	public SubPage()
	{
		InitializeComponent();
        displayLabel.Text = MainPage.Source.Display;
        lastNumberS.Text = MainPage.Source.LastNumS;
    }
    //两个实现与主界面共享的函数
    protected override void OnAppearing()
    {
        base.OnAppearing();
        displayLabel.Text = MainPage.Source.Display;
        lastNumberS.Text = MainPage.Source.LastNumS;
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        MainPage.Source.Display = displayLabel.Text;
        MainPage.Source.LastNumS = lastNumberS.Text;
    }
    // 定义OnNumberClicked方法来处理数字按钮点击事件
    private void OnNumberClicked(object sender, EventArgs e)
    {
        //阶乘只能有左侧数
        if(MainPage.Source.CurrentOperator != "!")
        {
            // 获取按钮的文本值
            var button = sender as Button;
            var number = button.Text;
            // 如果当前显示的是结果，或者是0，就相当于按下了AC后再输入
            if (MainPage.Source.IsResult || displayLabel.Text == "0")
            {
                OnClearClicked(sender, e);
                displayLabel.Text = "";
                if (number == ".")
                    displayLabel.Text = "0";
            }
            // 特殊字符后不可以跟数字
            if (displayLabel.Text != "π" && displayLabel.Text != "e")
            {
                if (!MainPage.Source.IsNext)
                {
                    displayLabel.Text += number;
                    //特殊字符前不可以跟数字
                    if (number == "π" || number == "e")
                        displayLabel.Text = number;
                }
                //如果在运算符右边，就额外赋值
                else
                {
                    if (MainPage.Source.RhsNumberS != "π" && MainPage.Source.RhsNumberS != "e")
                    {
                        MainPage.Source.RhsNumberS += number;
                        displayLabel.Text += number;
                        if (number == "π" || number == "e")
                        {
                            displayLabel.Text = displayLabel.Text.Replace(MainPage.Source.RhsNumberS, ""); displayLabel.Text += number;
                            MainPage.Source.RhsNumberS = number;

                        }
                    }
                }
            }
        }
    }

    // 定义OnOperatorClicked方法来处理运算符按钮点击事件
    private void OnOperatorClicked(object sender, EventArgs e)
    {
        // 获取按钮的文本值
        var button = sender as Button;
        var op = button.Text;
        // 如果没有右侧数，就按双目运算来
        if (MainPage.Source.RhsNumberS == "")
        {
            // 如果当前的运算符不为空，就删去之前的字符
            if (MainPage.Source.IsNext)
                displayLabel.Text = displayLabel.Text.Substring(0, displayLabel.Text.Length - 1);
            // 否则，就将数字传给lastNumber，并将设置bool值为true表示开始输入右侧数字
            else if (displayLabel.Text != "")
            {
                if (displayLabel.Text == "π")
                    MainPage.Source.LastNumber = Math.PI;
                else if (displayLabel.Text == "e")
                    MainPage.Source.LastNumber = Math.E;
                else 
                    MainPage.Source.LastNumber = double.Parse(displayLabel.Text);
                MainPage.Source.IsResult = false;
                MainPage.Source.IsNext = true;
            }
            // 此情况为在按了"="获取结果后再按DEL，显示屏被清空但是结果不被改变的情况
            else
            {
                MainPage.Source.IsResult = false;
                MainPage.Source.IsNext = true;
            }
        }
        // 如果有右侧数，则直接运算后再用结果给
        else
        {
            OnEqualClicked(sender, e);
            MainPage.Source.IsResult = false;
            MainPage.Source.IsNext = true;
        }
        // 更新运算符
        displayLabel.Text += op;
        // 将当前选择的运算符赋值给变量
        MainPage.Source.CurrentOperator = op;
    }
    
    //定义OnSpecialOperatorClicked处理sin、cos等特殊运算符按钮点击事件
    private void OnSpecialOperatorClicked(object sender, EventArgs e)
    {
        //该运算符只能运算一个数
        if(MainPage.Source.IsResult||displayLabel.Text == "")
        {
            var button = sender as Button;
            var op = button.Text;
            displayLabel.Text = op;
            MainPage.Source.CurrentOperator = op;
            MainPage.Source.IsNext = true;
            MainPage.Source.IsResult = false;
        }
    }

    // 定义OnEqualClicked方法来处理等号按钮点击事件
    private void OnEqualClicked(object sender, EventArgs e)
    {
        // 如果当前选择的运算符不为空，就执行上一次选择的运算，并显示结果
        if (MainPage.Source.IsNext && (MainPage.Source.RhsNumberS != "" || MainPage.Source.CurrentOperator == "!"))
        {
            if(MainPage.Source.CurrentOperator != "!")
            {
                if (MainPage.Source.RhsNumberS == "π")
                    MainPage.Source.CurrentNumber = Math.PI;
                else if (MainPage.Source.RhsNumberS == "e")
                    MainPage.Source.CurrentNumber = Math.E;
                else
                    MainPage.Source.CurrentNumber = double.Parse(MainPage.Source.RhsNumberS);
            }
            Calculate();
            displayLabel.Text = MainPage.Source.LastNumber.ToString();
            lastNumberS.Text = MainPage.Source.LastNumber.ToString();
            MainPage.Source.IsResult = true;
            MainPage.Source.IsNext = false;
            MainPage.Source.CurrentOperator = "";
            MainPage.Source.RhsNumberS = "";
            MainPage.Source.CurrentNumber = 0;
        }
    }

    // 定义OnDeleteClicked方法来处理删除按钮点击事件
    private void OnDeleteClicked(object sender, EventArgs e)
    {
        //如果是结果则直接清屏,但是假设已经有了数字
        if (MainPage.Source.IsResult)
            displayLabel.Text = "";
        //如果是在输入左侧数字时回退一格即可
        else if (!MainPage.Source.IsNext)
            displayLabel.Text = displayLabel.Text.Substring(0, displayLabel.Text.Length - 1);
        //如果是在刚输入运算符时回到输入前即可
        else if (MainPage.Source.IsNext && MainPage.Source.RhsNumberS == "")
        {
            displayLabel.Text = displayLabel.Text.Substring(0, displayLabel.Text.Length - 1);
            MainPage.Source.IsNext = false;
            MainPage.Source.CurrentOperator = "";
        }
        //如果输入过右侧字符则回退右侧字符
        else if (MainPage.Source.IsNext && MainPage.Source.RhsNumberS != "")
        {
            displayLabel.Text = displayLabel.Text.Substring(0, displayLabel.Text.Length - 1);
            MainPage.Source.RhsNumberS = MainPage.Source.RhsNumberS.Substring(0, MainPage.Source.RhsNumberS.Length - 1);
        }
    }

    // 定义OnClearClicked方法来处理AC按钮点击事件
    private void OnClearClicked(object sender, EventArgs e)
    {
        MainPage.Source.CurrentNumber = 0;
        MainPage.Source.LastNumber = 0;
        MainPage.Source.CurrentOperator = "";
        MainPage.Source.RhsNumberS = "";
        MainPage.Source.IsNext = false;
        MainPage.Source.IsResult = false;
        displayLabel.Text = MainPage.Source.LastNumber.ToString();
    }

    // 定义Calculate方法来执行运算逻辑
    private void Calculate()
    {
        // 根据当前选择的运算符，对上一次计算的结果和当前输入的数字进行相应的运算，并更新上一次计算的结果
        switch (MainPage.Source.CurrentOperator)
        {
            case "+":
                MainPage.Source.LastNumber += MainPage.Source.CurrentNumber;
                break;
            case "-":
                MainPage.Source.LastNumber -= MainPage.Source.CurrentNumber;
                break;
            case "*":
                MainPage.Source.LastNumber *= MainPage.Source.CurrentNumber;
                break;
            case "/":
                MainPage.Source.LastNumber /= MainPage.Source.CurrentNumber;
                break;
            case "sin":
                MainPage.Source.LastNumber = Math.Sin(MainPage.Source.CurrentNumber);
                break;
            case "cos":
                MainPage.Source.LastNumber = Math.Cos(MainPage.Source.CurrentNumber);
                break;
            case "tan":
                MainPage.Source.LastNumber = Math.Tan(MainPage.Source.CurrentNumber);
                break;
            case "lg":
                MainPage.Source.LastNumber = Math.Log10(MainPage.Source.CurrentNumber);
                break;
            case "ln":
                MainPage.Source.LastNumber = Math.Log(MainPage.Source.CurrentNumber,Math.E);
                break;
            case "√":
                MainPage.Source.LastNumber = Math.Sqrt(MainPage.Source.CurrentNumber);
                break;
            case "^":
                MainPage.Source.LastNumber = Math.Pow(MainPage.Source.LastNumber, MainPage.Source.CurrentNumber);
                break;
            case "!":
                MainPage.Source.LastNumber = Gamma(MainPage.Source.LastNumber + 1);
                break;
            default:
                break;
        }
        MainPage.Source.LastNumber = Math.Round(MainPage.Source.LastNumber, 4);
    }
    //上网搜的Gamma函数写法(代替阶乘)
    static double[] p = {0.99999999999980993, 676.5203681218851, -1259.1392167224028,
        771.32342877765313, -176.61502916214059, 12.507343278686905,
        -0.13857109526572012, 9.9843695780195716e-6, 1.5056327351493116e-7};
    static int g = 7;
    double Gamma(double x)
    {
        if (x < 0.5)
            return Math.PI / (Math.Sin(Math.PI * x)) * Gamma(1 - x);
        x -= 1;
        double y = p[0];
        for (var i = 1; i < g + 2; i++)
            y += p[i] / (x + i);
        double t = x + g + 0.5;
        return Math.Sqrt(2 * Math.PI) * (Math.Pow(t, x + 0.5)) * Math.Exp(-t) * y;
    }
}