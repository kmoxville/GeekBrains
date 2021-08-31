using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paycheck
{
    class Paycheck
    {
        private int width;
        private List<PaycheckRow> rowList = new List<PaycheckRow>();

        private const char UL_CORNER          = '╔';
        private const char BL_CORNER          = '╚';
        private const char UR_CORNER          = '╗';
        private const char BR_CORNER          = '╝';
        private const char VERTICAL_LINE      = '║';
        private const char HORIZONTAL_LINE    = '═';

        public Paycheck(int width = 50)
        {
            this.width = width;
        }

        public string Header
        {
            get => UL_CORNER.ToString().PadRight(width - 2, HORIZONTAL_LINE) + UR_CORNER;
        }

        public string Footer
        {
            get => BL_CORNER.ToString().PadRight(width - 2, HORIZONTAL_LINE) + BR_CORNER;
        }

        public Paycheck AddRow(PaycheckRow newRow)
        {
            rowList.Add(newRow);
            return this;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(Header);
            rowList.ForEach(a => result.AppendLine(a.ToString()));
            result.AppendLine(Footer);

            return result.ToString();
        }

        static void Main(string[] args)
        {
            Paycheck paycheck = new Paycheck();

            //шапка
            paycheck.AddRow(new PaycheckRow(50, ' ') { LeftValue = "Welcome to Lenta" });
            paycheck.AddRow(new PaycheckRow(50, ' '));
            paycheck.AddRow(new PaycheckRow(50, ' ') { LeftValue = "Today is", RightValue = DateTime.Now.ToString("f") });
            paycheck.AddRow(new PaycheckRow(50, ' '));

            //список товаров
            paycheck.AddRow(new PaycheckRow(50, ' ') { LeftValue = "Goods", RightValue = "Price,USD" });
            paycheck.AddRow(new PaycheckRow(50, '-'));
            paycheck.AddRow(new PaycheckRow(50, '.') { LeftValue = "Product 1", RightValue = (100.00).ToString() });
            paycheck.AddRow(new PaycheckRow(50, '.') { LeftValue = "Product 2", RightValue = (56.50).ToString() });
            paycheck.AddRow(new PaycheckRow(50, '.') { LeftValue = "Product 3", RightValue = (1000.00).ToString() });
            paycheck.AddRow(new PaycheckRow(50, '-'));

            //итого
            paycheck.AddRow(new PaycheckRow(50, ' ') { LeftValue = "Total", RightValue = "1156.50" });

            Console.WriteLine(paycheck);
        }
    }

    class PaycheckRow
    {
        private int width;
        private char gap;

        private string leftValue    = string.Empty;
        private string rightValue   = string.Empty;

        private const char VERTICAL_LINE = '║';

        public PaycheckRow(int width = 50, char gap = ' ')
        {
            this.width = width;
            this.gap = gap;
        }

        public string LeftValue
        {
            get => leftValue;
            set 
            {
                leftValue = value;
                CheckBounds();
            }
        }

        public string RightValue
        {
            get => rightValue;
            set
            {
                rightValue = value;
                CheckBounds();
            }
        }

        public override string ToString()
        {
            int gapBetweenLeftAndRightValue = width - rightValue.Length - 3;

            return VERTICAL_LINE + LeftValue.PadRight(gapBetweenLeftAndRightValue, gap) + RightValue + VERTICAL_LINE;
        }

        private void CheckBounds()
        {
            if (leftValue.Length + rightValue.Length > width - 2)
                throw new Exception("Row overflow exception");
        }
    }
}
