using System;
using Microsoft.Maui.Controls;

namespace MAUICalculator
{
    public partial class SubPage : ContentPage
    {
        public SubPage()
        {
            InitializeComponent();
            LoadState();
        }

        private void LoadState()
        {
            var state = CalculatorState.Instance;
            displayLabel.Text = state.IsResult ? state.LastNumber.ToString() : state.CurrentNumber.ToString();
        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            var state = CalculatorState.Instance;
            if (state.IsResult) displayLabel.Text = "";
            else
            {
                if (state.CurrentOperator != "")
                {
                    state.CurrentOperator = "";
                    displayLabel.Text = state.LastNumber.ToString();
                }
                else
                {
                    if (displayLabel.Text.Length > 0)
                    {
                        displayLabel.Text = displayLabel.Text.Remove(displayLabel.Text.Length - 1);
                        if (displayLabel.Text == "") displayLabel.Text = "0";
                        state.CurrentNumber = double.Parse(displayLabel.Text);
                    }
                }
            }
        }

        private void OnNumberClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var number = button.Text;
            var state = CalculatorState.Instance;

            if (state.IsResult || displayLabel.Text == "0")
            {
                displayLabel.Text = "";
                if (number == ".")
                    displayLabel.Text = "0";
                state.IsResult = false;
            }

            displayLabel.Text += number;
            state.CurrentNumber = double.Parse(displayLabel.Text);
        }

        private void OnOperatorClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var op = button.Text;
            var state = CalculatorState.Instance;

            if (state.CurrentOperator != "")
            {
                if (!state.IsResult)
                {
                    Calculate();
                    displayLabel.Text = state.LastNumber.ToString();
                    state.IsResult = true;
                }
            }
            else
            {
                state.LastNumber = state.CurrentNumber;
                displayLabel.Text = "0";
                state.IsResult = false;
            }

            state.CurrentOperator = op;
        }

        private void OnEqualClicked(object sender, EventArgs e)
        {
            var state = CalculatorState.Instance;
            if (state.CurrentOperator != "")
            {
                Calculate();
                displayLabel.Text = state.LastNumber.ToString();
                state.CurrentOperator = "";
                state.IsResult = true;
            }
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            var state = CalculatorState.Instance;
            state.CurrentNumber = 0;
            state.LastNumber = 0;
            state.CurrentOperator = "";
            state.IsResult = false;
            displayLabel.Text = state.LastNumber.ToString();
        }

        private void Calculate()
        {
            var state = CalculatorState.Instance;
            switch (state.CurrentOperator)
            {
                case "+":
                    state.LastNumber += state.CurrentNumber;
                    break;
                case "-":
                    state.LastNumber -= state.CurrentNumber;
                    break;
                case "*":
                    state.LastNumber *= state.CurrentNumber;
                    break;
                case "/":
                    state.LastNumber /= state.CurrentNumber;
                    break;
                case "x^y":
                    state.LastNumber = Math.Pow(state.LastNumber, state.CurrentNumber);
                    break;
                case "sqrt":
                    state.LastNumber = Math.Sqrt(state.CurrentNumber);
                    break;
                case "lg":
                    state.LastNumber = Math.Log10(state.CurrentNumber);
                    break;
                case "ln":
                    state.LastNumber = Math.Log(state.CurrentNumber);
                    break;
                case "!":
                    state.LastNumber = Factorial((int)state.CurrentNumber);
                    break;
                case "sin":
                    state.LastNumber = Math.Sin(state.CurrentNumber);
                    break;
                case "cos":
                    state.LastNumber = Math.Cos(state.CurrentNumber);
                    break;
                case "tan":
                    state.LastNumber = Math.Tan(state.CurrentNumber);
                    break;
            }
            state.LastNumber = Math.Round(state.LastNumber, 4);
            state.CurrentNumber = state.LastNumber;
        }

        private void OnComplexOperatorClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var op = button.Text;
            var state = CalculatorState.Instance;

            if (state.CurrentOperator != "")
            {
                Calculate();
                displayLabel.Text = state.LastNumber.ToString();
            }
            else
            {
                state.LastNumber = state.CurrentNumber;
                displayLabel.Text = "0";
            }

            state.CurrentOperator = op;
        }

        private void OnComplexNumberClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var number = button.Text;
            var state = CalculatorState.Instance;

            if (number == "pi")
            {
                state.CurrentNumber = Math.PI;
            }
            else if (number == "e")
            {
                state.CurrentNumber = Math.E;
            }

            displayLabel.Text = state.CurrentNumber.ToString();
            state.IsResult = true;
        }

        private double Factorial(int number)
        {
            if (number < 0) throw new ArgumentException("Number must be non-negative.");
            double result = 1;
            for (int i = 2; i <= number; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}
