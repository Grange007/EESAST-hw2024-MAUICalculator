namespace MAUICalculator;

public partial class SubPage : ContentPage
{
	public SubPage()
	{
		InitializeComponent();
	}

    // ����һЩ�������洢��ǰ��������֣���ǰѡ�����������Լ���һ�μ���Ľ��
    //private double currentNumber = 0;
    //private double lastNumber = 0;
    //private string currentOperator = "";
    //private bool isResult = false;

    // ����OnNumberClicked�������������ְ�ť����¼�
    private void OnNumberClicked(object sender, EventArgs e)
    {
        // ��ȡ��ť���ı�ֵ
        var button = sender as Button;
        var number = button.Text;

        // �����ǰ��ʾ���ǽ����������0���������ʾ��
        if (CalculatorState.isResult || displayLabel.Text == "0")
        {
            displayLabel.Text = "";
            if (number == ".")
                displayLabel.Text = "0";
            CalculatorState.isResult = false;
        }

        // �� pi �� e �����⴦��
        if (number == "pi")
        {
            CalculatorState.currentNumber = Math.PI;
            displayLabel.Text = CalculatorState.currentNumber.ToString();
            return;
        }
        if (number == "e")
        {
            CalculatorState.currentNumber = Math.E;
            displayLabel.Text = CalculatorState.currentNumber.ToString();
            return;
        }

        // ������׷�ӵ���ʾ���������µ�ǰ���������
        displayLabel.Text += number;
        CalculatorState.currentNumber = double.Parse(displayLabel.Text);
    }

    // ����OnOperatorClicked�����������������ť����¼�
    private void OnOperatorClicked(object sender, EventArgs e)
    {
        // ��ȡ��ť���ı�ֵ
        var button = sender as Button;
        var op = button.Text;

        // �����ǰ���������Ϊ�գ���������������������и��£�����������
        if (CalculatorState.currentOperator != "")
        {
            //Calculate();
            //displayLabel.Text = lastNumber.ToString();
            //isResult = true;
            CalculatorState.currentOperator = op;
        }
        else
        {
            // ���򣬾ͽ���ǰ��������ָ�ֵ����һ�μ���Ľ��
            CalculatorState.lastNumber = CalculatorState.currentNumber;
            displayLabel.Text = "0";
            CalculatorState.isResult = false;
        }

        // ����ǰѡ����������ֵ������������յ�ǰ���������
        CalculatorState.currentOperator = op;
    }

    // ����OnEqualClicked����������ȺŰ�ť����¼�
    private void OnEqualClicked(object sender, EventArgs e)
    {
        // �����ǰѡ����������Ϊ�գ���ִ����һ��ѡ������㣬����ʾ���
        if (CalculatorState.currentOperator != "")
        {
            Calculate();
            displayLabel.Text = CalculatorState.lastNumber.ToString();
            CalculatorState.isResult = true;
            CalculatorState.currentOperator = "";
        }
    }

    // ����OnClearClicked����������AC��ť����¼�
    private void OnClearClicked(object sender, EventArgs e)
    {
        CalculatorState.currentNumber = 0;
        CalculatorState.lastNumber = 0;
        CalculatorState.currentOperator = "";
        CalculatorState.isResult = false;
        displayLabel.Text = CalculatorState.lastNumber.ToString();
    }

    // ����Calculate������ִ�������߼�
    private void Calculate()
    {
        // ���ݵ�ǰѡ��������������һ�μ���Ľ���͵�ǰ��������ֽ�����Ӧ�����㣬��������һ�μ���Ľ��
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
            CalculatorState.currentNumber = 0 ;
        }
    }

    // ����DEL������ִ�е�����
    private void OnDeleteClicked(object sender, EventArgs e)
    {
        //            displayLabel.Text = $@"
        //currentN = {currentNumber},lastN = {lastNumber},currentOp = {currentOperator},isResult = {isResult}
        //";
        // ����һ���ַ�Ϊ`=`ʱ�������ʾ�������ı�`lastNumber`��ֵ
        if (CalculatorState.isResult)
        {
            displayLabel.Text = "";
        }
        else
        {
            // �򻯿��ǣ�
            // (1) ��һ���ַ�Ϊ����� <=> ��Ļ��ʾΪ"0"
            // (2) ��һ���ַ�Ϊ���� <=> ��Ļ��ʾΪ��������
            if (displayLabel.Text == "0")
            {
                CalculatorState.currentOperator = "";
            }
            else if (displayLabel.Text != "")
            {
                // ����һ���ַ�Ϊ���� ����Ļ��ʾ�ַ�����ɾ�����һ���ַ�
                displayLabel.Text = displayLabel.Text.Remove(
                        displayLabel.Text.Length - 1, 1
                    );
                //  Ȼ��ת��Ϊ������ ����currentNumber 
                if (displayLabel.Text != "")
                    CalculatorState.currentNumber = double.Parse(displayLabel.Text);
                else
                    CalculatorState.currentNumber = 0;
            }

        }
    }

    // �л�ʱ��֤��ʾ�����ݵ�һ��
    protected override void OnAppearing()
    {
        base.OnAppearing();
        displayLabel.Text = CalculatorState.currentNumber.ToString(); // �������Ҫ��ʾ�����ݸ���
    }
}