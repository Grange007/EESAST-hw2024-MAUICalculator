namespace MAUICalculator;

public partial class SubPage : ContentPage
{
	public SubPage()
	{
		InitializeComponent();
      displayLabel.Text = MainPage.Source.Display;
      lastNumberS.Text = MainPage.Source.LastNumS;
  }
  //����ʵ���������湲���ĺ���
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
  // ����OnNumberClicked�������������ְ�ť����¼�
  private void OnNumberClicked(object sender, EventArgs e)
  {
      //�׳�ֻ���������
      if(MainPage.Source.CurrentOperator != "!")
      {
          // ��ȡ��ť���ı�ֵ
          var button = sender as Button;
          var number = button.Text;
          // �����ǰ��ʾ���ǽ����������0�����൱�ڰ�����AC��������
          if (MainPage.Source.IsResult || displayLabel.Text == "0")
          {
              OnClearClicked(sender, e);
              displayLabel.Text = "";
              if (number == ".")
                  displayLabel.Text = "0";
          }
          // �����ַ��󲻿��Ը�����
          if (displayLabel.Text != "��" && displayLabel.Text != "e")
          {
              if (!MainPage.Source.IsNext)
              {
                  displayLabel.Text += number;
                  //�����ַ�ǰ�����Ը�����
                  if (number == "��" || number == "e")
                      displayLabel.Text = number;
              }
              //�����������ұߣ��Ͷ��⸳ֵ
              else
              {
                  if (MainPage.Source.RhsNumberS != "��" && MainPage.Source.RhsNumberS != "e")
                  {
                      MainPage.Source.RhsNumberS += number;
                      displayLabel.Text += number;
                      if (number == "��" || number == "e")
                      {
                          displayLabel.Text = displayLabel.Text.Replace(MainPage.Source.RhsNumberS, ""); displayLabel.Text += number;
                          MainPage.Source.RhsNumberS = number;

                      }
                  }
              }
          }
      }
  }

  // ����OnOperatorClicked�����������������ť����¼�
  private void OnOperatorClicked(object sender, EventArgs e)
  {
      // ��ȡ��ť���ı�ֵ
      var button = sender as Button;
      var op = button.Text;
      // ���û���Ҳ������Ͱ�˫Ŀ������
      if (MainPage.Source.RhsNumberS == "")
      {
          // �����ǰ���������Ϊ�գ���ɾȥ֮ǰ���ַ�
          if (MainPage.Source.IsNext)
              displayLabel.Text = displayLabel.Text.Substring(0, displayLabel.Text.Length - 1);
          // ���򣬾ͽ����ִ���lastNumber����������boolֵΪtrue��ʾ��ʼ�����Ҳ�����
          else if (displayLabel.Text != "")
          {
              if (displayLabel.Text == "��")
                  MainPage.Source.LastNumber = Math.PI;
              else if (displayLabel.Text == "e")
                  MainPage.Source.LastNumber = Math.E;
              else 
                  MainPage.Source.LastNumber = double.Parse(displayLabel.Text);
              MainPage.Source.IsResult = false;
              MainPage.Source.IsNext = true;
          }
          // �����Ϊ�ڰ���"="��ȡ������ٰ�DEL����ʾ������յ��ǽ�������ı�����
          else
          {
              MainPage.Source.IsResult = false;
              MainPage.Source.IsNext = true;
          }
      }
      // ������Ҳ�������ֱ����������ý����
      else
      {
          OnEqualClicked(sender, e);
          MainPage.Source.IsResult = false;
          MainPage.Source.IsNext = true;
      }
      // ���������
      displayLabel.Text += op;
      // ����ǰѡ����������ֵ������
      MainPage.Source.CurrentOperator = op;
  }

  //����OnSpecialOperatorClicked����sin��cos�������������ť����¼�
  private void OnSpecialOperatorClicked(object sender, EventArgs e)
  {
      //�������ֻ������һ����
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

  // ����OnEqualClicked�����������ȺŰ�ť����¼�
  private void OnEqualClicked(object sender, EventArgs e)
  {
      // �����ǰѡ����������Ϊ�գ���ִ����һ��ѡ������㣬����ʾ���
      if (MainPage.Source.IsNext && (MainPage.Source.RhsNumberS != "" || MainPage.Source.CurrentOperator == "!"))
      {
          if(MainPage.Source.CurrentOperator != "!")
          {
              if (MainPage.Source.RhsNumberS == "��")
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

  // ����OnDeleteClicked����������ɾ����ť����¼�
  private void OnDeleteClicked(object sender, EventArgs e)
  {
      //����ǽ����ֱ������,���Ǽ����Ѿ���������
      if (MainPage.Source.IsResult)
          displayLabel.Text = "";
      //������������������ʱ����һ�񼴿�
      else if (!MainPage.Source.IsNext)
          displayLabel.Text = displayLabel.Text.Substring(0, displayLabel.Text.Length - 1);
      //������ڸ����������ʱ�ص�����ǰ����
      else if (MainPage.Source.IsNext && MainPage.Source.RhsNumberS == "")
      {
          displayLabel.Text = displayLabel.Text.Substring(0, displayLabel.Text.Length - 1);
          MainPage.Source.IsNext = false;
          MainPage.Source.CurrentOperator = "";
      }
      //���������Ҳ��ַ�������Ҳ��ַ�
      else if (MainPage.Source.IsNext && MainPage.Source.RhsNumberS != "")
      {
          displayLabel.Text = displayLabel.Text.Substring(0, displayLabel.Text.Length - 1);
          MainPage.Source.RhsNumberS = MainPage.Source.RhsNumberS.Substring(0, MainPage.Source.RhsNumberS.Length - 1);
      }
  }

  // ����OnClearClicked����������AC��ť����¼�
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

  // ����Calculate������ִ�������߼�
  private void Calculate()
  {
      // ���ݵ�ǰѡ��������������һ�μ���Ľ���͵�ǰ��������ֽ�����Ӧ�����㣬��������һ�μ���Ľ��
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
          case "��":
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
  //�����ѵ�Gamma����д��(����׳�)
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