using System;
using Microsoft.Maui.Controls;

namespace MAUICalculator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
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
                    displayLabel.Text = displayLabel.Text.Remove(displayLabel.Text.Length - 1);
                    if (displayLabel.Text == "") displayLabel.Text += "0";
                    state.CurrentNumber = double.Parse(displayLabel.Text);
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
            }
            state.LastNumber = Math.Round(state.LastNumber, 4);
            state.CurrentNumber = state.LastNumber;
        }

        private async void OnNavigateToComplexModeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SubPage());
        }
    }
}
