using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUICalculator
{
    public static class CalculatorData
    {
        // 定义一些变量来存储当前输入的数字，当前选择的运算符，以及上一次计算的结果
        public static double currentNumber = 0;
        public static bool currentNumberDied = false;
        public static double lastNumber = 0;
        public static string currentOperator = "";
        public static bool isResult = false;
        public static string displayText = "0";
        public static string historyText = "";

    }
}
