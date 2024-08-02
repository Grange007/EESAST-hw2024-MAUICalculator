namespace MAUICalculator;
using System;
using Microsoft.Maui.Controls;

namespace YourNamespace
{
    public partial class SubPage : ContentPage
    {
        public SubPage()
        {
            InitializeComponent();
        }

        // Method to handle DEL button click
        void OnDelButtonClicked(object sender, EventArgs e)
        {
            if (displayLabel.Text.Length > 0)
            {
                if (displayLabel.Text.EndsWith("="))
                {
                    displayLabel.Text = "";
                }
                else
                {
                    displayLabel.Text = displayLabel.Text.Remove(displayLabel.Text.Length - 1);
                }
            }
        }

        // Method to handle AC button click
        void OnAcButtonClicked(object sender, EventArgs e)
        {
            displayLabel.Text = "";
            lastNumber = 0;
        }

        // Other methods for button clicks...
    }
}
