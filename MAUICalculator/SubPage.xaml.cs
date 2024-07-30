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

    // ����OnNumberClicked�������������ְ�ť����¼�
    private void OnNumberClicked(object sender, EventArgs e)
    {
        // ��ȡ��ť���ı�ֵ
        var button = sender as Button;
        var number = button.Text;
        Channel.type = 1;

        // �����ǰ��ʾ���ǽ����������0���������ʾ��
        if (Channel.isResult || displayLabel.Text == "0")
        {
            displayLabel.Text = "";
            if (number == ".")
                displayLabel.Text = "0";
            Channel.isResult = false;
        }

        // ������׷�ӵ���ʾ���������µ�ǰ���������
        displayLabel.Text += number;
        if (number == "e")
            Channel.currentNumber = Math.E;
        else if (number == "pi")
            Channel.currentNumber = Math.PI;
        else
            Channel.currentNumber = double.Parse(displayLabel.Text);
        Channel.flag = 1;
    }

    // ����OnOperatorClicked�����������������ť����¼�
    private void OnOperatorClicked(object sender, EventArgs e)
    {
        // ��ȡ��ť���ı�ֵ
        var button = sender as Button;
        var op = button.Text;
        Channel.type = 2;

        // �����ǰ���������Ϊ�գ���ִ����һ��ѡ������㣬����ʾ���
        if (Channel.currentOperator != "" && Channel.flag != 0)
        {
            Calculate();
            displayLabel.Text = Channel.lastNumber.ToString();
            Channel.isResult = true;
        }
        else
        {
            // ���򣬾ͽ���ǰ��������ָ�ֵ����һ�μ���Ľ��
            Channel.flag = 0;
            Channel.lastNumber = Channel.currentNumber;
            displayLabel.Text = "0";
            Channel.isResult = false;
        }

        // ����ǰѡ����������ֵ������������յ�ǰ���������
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

    // ����OnEqualClicked����������ȺŰ�ť����¼�
    private void OnEqualClicked(object sender, EventArgs e)
    {
        // �����ǰѡ����������Ϊ�գ���ִ����һ��ѡ������㣬����ʾ���
        Channel.type = 3;
        if (Channel.currentOperator != "")
        {
            Calculate();
            displayLabel.Text = Channel.lastNumber.ToString();
            Channel.isResult = true;
            Channel.currentOperator = "";
        }
    }

    // ����OnClearClicked����������AC��ť����¼�
    private void OnClearClicked(object sender, EventArgs e)
    {
        Channel.currentNumber = 0;
        Channel.lastNumber = 0;
        Channel.currentOperator = "";
        Channel.isResult = false;
        displayLabel.Text = Channel.lastNumber.ToString();
    }

    // ����Calculate������ִ�������߼�
    private void Calculate()
    {
        // ���ݵ�ǰѡ��������������һ�μ���Ľ���͵�ǰ��������ֽ�����Ӧ�����㣬��������һ�μ���Ľ��
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

    // ����OnDELClicked����������DEL��ť����¼�
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