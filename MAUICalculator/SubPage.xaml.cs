using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace MAUICalculator;


public partial class SubPage : ContentPage, INotifyPropertyChanged
{
    public SubPage()
    {
        InitializeComponent();
        RestoreState();
    }

    // ����һЩ�������洢��ǰ��������֣���ǰѡ�����������Լ���һ�μ���Ľ��
    private double currentNumber = 0;
    private double lastNumber = 0;
    private string currentOperator = "";
    private bool isResult = false;


    private void RestoreState()
    {
        displayLabel.Text = AppShell.CalculatorState.displaytext;
        lastNumber = AppShell.CalculatorState.lastnumber;
    }

    private void SaveState()
    {
        AppShell.CalculatorState.displaytext = displayLabel.Text;
        AppShell.CalculatorState.lastnumber = lastNumber;
    }
    private ulong Factorial(uint num)
    {
        if (num == 1)
            return 1;
        else
            return Factorial(num - 1) * num;
    }

    // ����OnNumberClicked�������������ְ�ť����¼�
    private void OnNumberClicked(object sender, EventArgs e)
    {
        // ��ȡ��ť���ı�ֵ
        var button = sender as Button;
        var number = button.Text;

        // �����ǰ��ʾ���ǽ����������0���������ʾ��
        if (isResult || displayLabel.Text == "0")
        {
            displayLabel.Text = "";
            if (number == ".")
                displayLabel.Text = "0";
            isResult = false;
        }

        // ������׷�ӵ���ʾ���������µ�ǰ���������
        displayLabel.Text += number;
        currentNumber = double.Parse(displayLabel.Text);
        SaveState();
    }

    private void OnDelClicked(object sender, EventArgs e)
    {
        if (isResult)
        {// ��һ���� =
            displayLabel.Text = "";
            isResult = false;
            currentNumber = 0;
        }
        if (currentOperator != "")
        { // ���������
            currentOperator = "";
            isResult = false;
        }
        else
        { // ���������ַ�
            currentNumber = 0;
            isResult = false;
        }
        SaveState();
    }

    // ����OnOperatorClicked�����������������ť����¼�
    private void OnOperatorClicked(object sender, EventArgs e)
    {
        RestoreState();
        // ��ȡ��ť���ı�ֵ
        var button = sender as Button;
        var op = button.Text;

        // �����ǰ���������Ϊ�գ��͸ı��������
        if (currentOperator != "")
        {
            currentOperator = op;
            displayLabel.Text = lastNumber.ToString();
            isResult = false;
        }
        else
        {
            // ���򣬾ͽ���ǰ��������ָ�ֵ����һ�μ���Ľ��
            lastNumber = currentNumber;
            currentNumber = 0;
            displayLabel.Text = "0";
            isResult = false;
            currentOperator = op;
        }
        SaveState();
    }

    // ����OnEqualClicked����������ȺŰ�ť����¼�
    private void OnEqualClicked(object sender, EventArgs e)
    {
        // �����ǰѡ����������Ϊ�գ���ִ����һ��ѡ������㣬����ʾ���
        if (currentOperator != "")
        {
            Calculate();
            displayLabel.Text = lastNumber.ToString();
            isResult = true;
            currentOperator = "";
        }
        SaveState();
    }

    // ����OnEqualClicked����������ȺŰ�ť����¼�
    private void OnClearClicked(object sender, EventArgs e)
    {
        currentNumber = 0;
        lastNumber = 0;
        currentOperator = "";
        isResult = false;
        displayLabel.Text = lastNumber.ToString();
        SaveState();
    }

    // ����OnFunctionClicked������������ť���ʱ��
    private void OnFunctionClicked(object sender, EventArgs e)
    {
        RestoreState();
        var button = sender as Button;
        var func = button.Text;


        if(currentOperator != "")
        { // �����������Ϊ�����Ա��� 
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
                        displayLabel.Text = "-��";
                    else
                        lastNumber = currentNumber;
                        currentNumber = Math.Log10(currentNumber);
                        displayLabel.Text = currentNumber.ToString();
                    break;

                case "ln x":
                    if (currentNumber < 0)
                        displayLabel.Text = "Negative numbers can't have logarithm!";
                    else if (currentNumber == 0)
                        displayLabel.Text = "-��";
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

                case "��x":
                    if(currentNumber < 0)
                    { 
                        displayLabel.Text = "Negative numbers can't have square root!";
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
                        if(result<0)
                        {
                            displayLabel.Text = "Negative numbers don't have factorial (Elementary)";
                        }
                        displayLabel.Text = Factorial((uint)Math.Abs(result)).ToString();
                        lastNumber = currentNumber;
                        currentNumber = result;
                    }
                    else
                    {
                        displayLabel.Text = "Only Integers have Factorial (Elementary)";
                    }
                    break;

                case "arcsin x":
                    if (Math.Abs(currentNumber) > 1)
                    {
                        displayLabel.Text = "NaN";
                        break;
                    }
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
        SaveState();
    }

    // ����Calculate������ִ�������߼�
    private void Calculate()
    {
        // ���ݵ�ǰѡ��������������һ�μ���Ľ���͵�ǰ��������ֽ�����Ӧ�����㣬��������һ�μ���Ľ��
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
        SaveState();
    }
}