using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double numL = 0;
        double numR = 0;
        string op = "";
        bool exeptionHappened = false;

        public CalculationTree calculationTree = new CalculationTree();

        public MainWindow()
        {
            InitializeComponent();
        }

        // detects number from typing
        private void btn_num(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            String strnum = button.Content.ToString();

            if (txtValue.Text == "0" | exeptionHappened)
            { 
                txtValue.Text = strnum;
                exeptionHappened = false;
            }
            else
                txtValue.Text += strnum;

            if (op == "")
                numL = Double.Parse(txtValue.Text);
            else
                numR = Double.Parse(txtValue.Text);
        }

        // detects operation button pushes
        private void btn_op(object sender, RoutedEventArgs e)
        {
            if (exeptionHappened) return;

            Button button = (Button)sender;
            op = button.Content.ToString();
            txtValue.Text = "0";
        }

        // calculates result after pushing " = "
        private void btn_eq(object sender, RoutedEventArgs e)
        {
            double result = 0;

            if (exeptionHappened)
            {
                exeptionHappened = false;
                txtValue.Text = "0";
                op = "";
                numL = 0;
                numR = 0;
                return;
            }

            try
            {
                switch (op)
                {
                    case "*":
                        result = numL * numR;
                        break;
                    case "/":
                        if (numR == 0) throw new DivideByZeroException();
                        result = numL / numR;
                        break;
                    case "+":
                        result = numL + numR;
                        break;
                    case "-":
                        result = numL - numR;
                        break;
                    case "sqrt()":
                        result = Math.Sqrt(numL);
                        break;
                    case "x^y":
                        if (numL < 0 & numR == 0) throw new DivideByZeroException();
                        result = Math.Pow(numL, numR);
                        break;
                    case "1/x":
                        if (numL == 0) throw new DivideByZeroException();
                        result = 1 / numL;
                        break;
                    case "":
                        result = numL;
                        break;
                }

                txtValue.Text = result.ToString();
                op = "";
                numL = result;
                numR = 0;
            }

            catch (DivideByZeroException)
            {
                exeptionHappened = true;
                txtValue.Text = "Division by zero";
                op = "";
                numL = 0;
                numR = 0;
            }
            catch (Exception)
            {
                exeptionHappened = true;
                btn_clear(sender, e);
                txtValue.Text = "Error occurence";
            }
        }

        // clears everything from input
        private void btn_clear(object sender, RoutedEventArgs e)
        {
            exeptionHappened = false;

            if (op == "")
                numL = 0;
            else
                numR = 0;
            txtValue.Text = "0";
        }

        // deletes last input symbol
        private void btn_delete(object sender, RoutedEventArgs e)
        {
            if (exeptionHappened) return;

            txtValue.Text = DeleteLastSym(txtValue.Text);

            if (op == "")
                numL = Double.Parse(txtValue.Text);
            else
                numR = Double.Parse(txtValue.Text);
        }

        private string DeleteLastSym(string str)
        {
            if (str.Length < 2)
                str = "0";
            else
            {
                str = str.Remove(str.Length - 1, 1);
                if (str[str.Length - 1] == ',')
                    str = str.Remove(str.Length - 1, 1);
            }

            return str;
        }

        private void btn_plusminus(object sender, RoutedEventArgs e)
        {
            if (exeptionHappened) return;

            if (op == "")
            {
                numL *= -1;
                txtValue.Text = numL.ToString();
            }
            else
            {
                numR *= -1;
                txtValue.Text = numR.ToString();
            }
        }

        private void btn_comma(object sender, RoutedEventArgs e)
        {
            if (exeptionHappened) return;

            if (txtValue.Text.Contains(',')) return;
            else
                txtValue.Text += ',';
        }
    }
}
