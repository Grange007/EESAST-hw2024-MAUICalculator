namespace MAUICalculator
using System;
using Microsoft.Maui.Controls;

namespace YourNamespace
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SubPage), typeof(SubPage));
        }
    }
}
