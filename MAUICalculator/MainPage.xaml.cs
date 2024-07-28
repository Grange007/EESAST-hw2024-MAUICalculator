namespace MAUICalculator
{
    public partial class MainPage : ContentPage
    {
        // 定义一些变量来共享两个计算机之间的计算情况
        static public class Source
        {
            public static string Display { get; set; }
            public static string LastNumS {  get; set; }
            public static double CurrentNumber {  get; set; }
            public static double LastNumber {  get; set; }
            public static string CurrentOperator { get; set; }
            public static string RhsNumberS {  get; set; }
            public static bool IsNext { get; set; }
            public static bool IsResult {  get; set; }
        }
        public MainPage()
        {
            //初始化
            InitializeComponent();
            if (Source.Display == "")
            {
                Source.Display = "";
                Source.LastNumS = "0";
                Source.CurrentNumber = 0;
                Source.LastNumber = 0;
                Source.CurrentOperator = "";
                Source.RhsNumberS = "";
                Source.IsNext = false;
                Source.IsResult = false;
            }
            displayLabel.Text = Source.Display;
            lastNumberS.Text = Source.LastNumS;
        }
        //实现两个界面的显示
        protected override void OnAppearing()
        {
            base.OnAppearing();
            displayLabel.Text = Source.Display;
            lastNumberS.Text = Source.LastNumS;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Source.Display = displayLabel.Text;
            Source.LastNumS = lastNumberS.Text;
        }
        // 定义OnNumberClicked方法来处理数字按钮点击事件
        private void OnNumberClicked(object sender, EventArgs e)
        {
            // 获取按钮的文本值
            var button = sender as Button;
            var number = button.Text;
            // 如果当前显示的是结果，或者是0，就相当于按下了AC后再输入
            if (Source.IsResult||displayLabel.Text == "0")
            {
                OnClearClicked(sender, e);
                displayLabel.Text = "";
                if (number == ".")
                    displayLabel.Text = "0";
            }
            // 将数字追加到显示屏，并更新当前输入的数字
            if (!Source.IsNext)
                displayLabel.Text += number;
            //如果在运算符右边，就额外赋值
            else
            {
                Source.RhsNumberS += number;
                displayLabel.Text += number;
            }
        }

        // 定义OnOperatorClicked方法来处理运算符按钮点击事件
        private void OnOperatorClicked(object sender, EventArgs e)
        {
            // 获取按钮的文本值
            var button = sender as Button;
            var op = button.Text;
            // 如果没有右侧数，就按双目运算来
            if (Source.RhsNumberS == "")
            {
                // 如果当前的运算符不为空，就删去之前的字符
                if (Source.IsNext)
                    displayLabel.Text = displayLabel.Text.Substring(0, displayLabel.Text.Length - 1);
                // 否则，就将数字传给lastNumber，并将设置bool值为true表示开始输入右侧数字
                else if(displayLabel.Text != "")
                {
                    Source.LastNumber = double.Parse(displayLabel.Text);
                    Source.IsResult = false;
                    Source.IsNext = true;
                }
                // 此情况为在按了"="获取结果后再按DEL，显示屏被清空但是结果不被改变的情况
                else
                {
                    Source.IsResult = false;
                    Source.IsNext = true;
                }
            }
            // 如果有右侧数，则直接运算后再用结果给
            else
            {
                OnEqualClicked(sender, e);
                Source.IsResult = false;
                Source.IsNext = true;
            }
            // 更新运算符
            displayLabel.Text += op;
            // 将当前选择的运算符赋值给变量
            Source.CurrentOperator = op;
            //传递与高级计算器的共享值
        }

        // 定义OnEqualClicked方法来处理等号按钮点击事件
        private void OnEqualClicked(object sender, EventArgs e)
        {
            // 如果当前选择的运算符不为空，就执行上一次选择的运算，并显示结果
            if (Source.IsNext &&Source.RhsNumberS !="")
            {
                Source.CurrentNumber = double.Parse(Source.RhsNumberS);
                Calculate();
                displayLabel.Text = Source.LastNumber.ToString();
                lastNumberS.Text = Source.LastNumber.ToString();
                Source.IsResult = true;
                Source.IsNext = false;
                Source.CurrentOperator = "";
                Source.RhsNumberS = "";
                Source.CurrentNumber = 0;
            }
        }

        // 定义OnDeleteClicked方法来处理删除按钮点击事件
        private void OnDeleteClicked(object sender, EventArgs e)
        {
            //如果是结果则直接清屏,但是假设已经有了数字
            if (Source.IsResult)
                displayLabel.Text = "";
            //如果是在输入左侧数字时回退一格即可
            else if(!Source.IsNext)
                displayLabel.Text = displayLabel.Text.Substring(0, displayLabel.Text.Length - 1);
            //如果是在刚输入运算符时回到输入前即可
            else if(Source.IsNext && Source.RhsNumberS == "")
            {
                displayLabel.Text = displayLabel.Text.Substring(0, displayLabel.Text.Length - 1);
                Source.IsNext = false;
                Source.CurrentOperator = "";
            }
            //如果输入过右侧字符则回退右侧字符
            else if(Source.IsNext && Source.RhsNumberS != "")
            {
                displayLabel.Text = displayLabel.Text.Substring(0, displayLabel.Text.Length - 1);
                Source.RhsNumberS = Source.RhsNumberS.Substring(0, Source.RhsNumberS.Length - 1);
            }
            //传递与高级计算器的共享值
        }

        // 定义OnClearClicked方法来处理AC按钮点击事件
        private void OnClearClicked(object sender, EventArgs e)
        {
            Source.CurrentNumber = 0;
            Source.LastNumber = 0;
            Source.CurrentOperator = "";
            Source.RhsNumberS = "";
            Source.IsNext = false;
            Source.IsResult = false;
            displayLabel.Text = Source.LastNumber.ToString();
        }

        // 定义Calculate方法来执行运算逻辑
        private void Calculate()
        {
            // 根据当前选择的运算符，对上一次计算的结果和当前输入的数字进行相应的运算，并更新上一次计算的结果
            switch (Source.CurrentOperator)
            {
                case "+":
                    Source.LastNumber += Source.CurrentNumber;
                    break;
                case "-":
                    Source.LastNumber -= Source.CurrentNumber;
                    break;
                case "*":
                    Source.LastNumber *= Source.CurrentNumber;
                    break;
                case "/":
                    Source.LastNumber /= Source.CurrentNumber;
                    break;
                default:
                    break;
            }
            Source.LastNumber = Math.Round(Source.LastNumber, 4);
        }
    }

}
