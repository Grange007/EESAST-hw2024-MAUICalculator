using System;
using Microsoft.Maui.Controls;

namespace Calculator
{
    public partial class AdvancedCalculatorPage : ContentPage
    {
        private string _currentInput = "";
        private string _lastOperator = "";
        private double _lastNumber = 0;
        private bool _isLastInputOperator = false;

        public AdvancedCalculatorPage()
        {
            InitializeComponent();
        }

        private void OnNumberClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                if (_isLastInputOperator)
                {
                    _currentInput = "";
                    _isLastInputOperator = false;
                }
                _currentInput += button.Text;
                UpdateDisplay();
            }
        }

        private void OnOperatorClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                if (_currentInput != "")
                {
                    if (_lastNumber != 0 && !_isLastInputOperator)
                    {
                        CalculateResult();
                    }
                    else
                    {
                        _lastNumber = double.Parse(_currentInput);
                    }
                    _lastOperator = button.Text;
                    _currentInput = "";
                    _isLastInputOperator = true;
                }
            }
        }

        private void OnAdvancedOperatorClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null && _currentInput != "")
            {
                double result = 0;
                try
                {
                    if (button.Text == "lg")
                    {
                        result = Math.Log10(double.Parse(_currentInput));
                    }
                    else if (button.Text == "sqrt")
                    {
                        result = Math.Sqrt(double.Parse(_currentInput));
                    }
                    else if (button.Text == "!")
                    {
                        result = Factorial(double.Parse(_currentInput));
                    }
                    else if (button.Text == "π")
                    {
                        result = Math.PI;
                    }
                    _currentInput = result.ToString();
                    UpdateDisplay();
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

        private void OnACClicked(object sender, EventArgs e)
        {
            _currentInput = "";
            _lastNumber = 0;
            _lastOperator = "";
            _isLastInputOperator = false;
            UpdateDisplay();
        }

        private void OnDELClicked(object sender, EventArgs e)
        {
            if (_currentInput.Length > 0)
            {
                _currentInput = _currentInput.Substring(0, _currentInput.Length - 1);
                UpdateDisplay();
            }
        }

        private void OnEqualsClicked(object sender, EventArgs e)
        {
            if (_currentInput != "")
            {
                CalculateResult();
                _lastOperator = "";
                _currentInput = _lastNumber.ToString();
                _lastNumber = 0;
                UpdateDisplay();
            }
        }

        private void OnDotClicked(object sender, EventArgs e)
        {
            if (!_currentInput.Contains("."))
            {
                _currentInput += ".";
                UpdateDisplay();
            }
        }

        private void UpdateDisplay()
        {
            displayLabel.Text = _currentInput;
        }

        private void CalculateResult()
        {
            double result = 0;
            try
            {
                double currentNumber = double.Parse(_currentInput);
                switch (_lastOperator)
                {
                    case "+":
                        result = _lastNumber + currentNumber;
                        break;
                    case "-":
                        result = _lastNumber - currentNumber;
                        break;
                    case "*":
                        result = _lastNumber * currentNumber;
                        break;
                    case "/":
                        result = _lastNumber / currentNumber;
                        break;
                }
                _lastNumber = result;
                _currentInput = result.ToString();
                _isLastInputOperator = false;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private double Factorial(double number)
        {
            if (number < 0) throw new ArgumentException("Factorial is not defined for negative numbers.");
            if (number == 0 || number == 1) return 1;
            double result = 1;
            for (int i = 2; i <= number; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}
