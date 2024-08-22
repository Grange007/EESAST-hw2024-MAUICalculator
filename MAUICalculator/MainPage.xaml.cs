using System;
using Microsoft.Maui.Controls;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        private string _currentInput = string.Empty;
        private string _lastNumber = string.Empty;
        private string _lastOperator = string.Empty;
        private bool _isLastInputOperator = false;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnACClicked(object sender, EventArgs e)
        {
            _currentInput = string.Empty;
            _lastNumber = string.Empty;
            _lastOperator = string.Empty;
            _isLastInputOperator = false;
            displayLabel.Text = "0";
        }

        private void OnDELClicked(object sender, EventArgs e)
        {
            if (_currentInput.Length > 0)
            {
                _currentInput = _currentInput.Substring(0, _currentInput.Length - 1);
                if (_currentInput.Length == 0)
                {
                    displayLabel.Text = "0";
                }
                else
                {
                    displayLabel.Text = _currentInput;
                }
            }
            else if (_lastNumber.Length > 0)
            {
                _lastNumber = string.Empty;
                displayLabel.Text = "0";
            }
        }

        private void OnDotClicked(object sender, EventArgs e)
        {
            if (!_currentInput.Contains("."))
            {
                _currentInput += ".";
                displayLabel.Text = _currentInput;
            }
        }

        private void OnNumberClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                _currentInput += button.Text;
                displayLabel.Text = _currentInput;
                _isLastInputOperator = false;
            }
        }

        private void OnOperatorClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                if (_isLastInputOperator)
                {
                    _lastOperator = button.Text;
                }
                else
                {
                    if (_lastNumber != string.Empty && _currentInput != string.Empty)
                    {
                        CalculateResult();
                    }

                    _lastNumber = _currentInput;
                    _currentInput = string.Empty;
                    _lastOperator = button.Text;
                    _isLastInputOperator = true;
                }
            }
        }

        private void OnEqualsClicked(object sender, EventArgs e)
        {
            if (_lastNumber != string.Empty && _currentInput != string.Empty && _lastOperator != string.Empty)
            {
                CalculateResult();
                _lastNumber = string.Empty;
                _lastOperator = string.Empty;
                _isLastInputOperator = false;
            }
        }

        private void CalculateResult()
        {
            double num1 = double.Parse(_lastNumber);
            double num2 = double.Parse(_currentInput);
            double result = 0;

            switch (_lastOperator)
            {
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "*":
                    result = num1 * num2;
                    break;
                case "/":
                    result = num1 / num2;
                    break;
            }

            _currentInput = result.ToString();
            displayLabel.Text = _currentInput;
        }
    }
}
