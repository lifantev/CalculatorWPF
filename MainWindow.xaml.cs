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

				CalculationTree calculationTree = new CalculationTree();

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
								calculationTree.Add(txtValue.Text, NodeType.number, false);
						}
						else
						{
								txtValue.Text += strnum;
								calculationTree.ChangeCurrentValue(txtValue.Text, NodeType.number);
						}
				}

				private void btn_bracket(object sender, RoutedEventArgs e)
				{

				}

				// detects operation button pushes
				private void btn_op(object sender, RoutedEventArgs e)
				{
						if (exeptionHappened)
								return;

						Button button = (Button)sender;
						op = button.Content.ToString();

						if (calculationTree.CurrentType() == NodeType.operation)
								calculationTree.ChangeCurrentValue(op);
						else if (calculationTree.CurrentType() == NodeType.number)
								calculationTree.SwapCurrentTo(op, NodeType.operation);

						txtValue.Text = "0";
				}

				private void DoEquation()
				{
						double result = 0;
						string operation = "";

						try
						{
								numL = Double.Parse(calculationTree.GetLeftChildData());

								if (calculationTree.Current.RightNode == null)
										calculationTree.Add(txtValue.Text, NodeType.number, false);

								numR = Double.Parse(calculationTree.GetRightChildData());

								if (calculationTree.CurrentType() != NodeType.operation)
										operation = "";

								operation = calculationTree.CurrentData();

								switch (operation)
								{
										case "*":
												result = numL * numR;
												break;
										case "/":
												if (numR == 0)
														throw new DivideByZeroException();
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
												if (numL < 0 & numR == 0)
														throw new DivideByZeroException();
												result = Math.Pow(numL, numR);
												break;
										case "":
												result = numL;
												break;
								}

								calculationTree.ReduceCurrentTo(result.ToString(), NodeType.number);
						}

						catch (DivideByZeroException)
						{
								txtValue.Text = "Division by zero";
								exeptionHappened = true;
								op = "";
								numL = 0;
								numR = 0;
								calculationTree.ClearTree();
						}
						catch (Exception)
						{
								txtValue.Text = "Error occurence";
								exeptionHappened = true;
								op = "";
								numL = 0;
								numR = 0;
								calculationTree.ClearTree();
						}
				}

				// calculates result after pushing " = "
				private void btn_eq(object sender, RoutedEventArgs e)
				{
						if (exeptionHappened)
						{
								exeptionHappened = false;
								txtValue.Text = "0";
								op = "";
								numL = 0;
								numR = 0;
								return;
						}

						do
						{
								DoEquation();
						}
						while (calculationTree.Current.Parent != null);

						txtValue.Text = calculationTree.CurrentData();
				}

				// clears everything from input
				private void btn_C(object sender, RoutedEventArgs e)
				{
						exeptionHappened = false;
						txtValue.Text = "0";

						calculationTree.ClearTree();
				}

				private void btn_CE(object sender, RoutedEventArgs e)
				{
						exeptionHappened = false;
						txtValue.Text = "0";

						if (calculationTree.CurrentType() == NodeType.operation)
								calculationTree.ReduceCurrentTo(txtValue.Text, NodeType.number);
						else
								calculationTree.ChangeCurrentValue(txtValue.Text, NodeType.number);
				}

				// deletes last input symbol
				private void btn_delete(object sender, RoutedEventArgs e)
				{
						if (exeptionHappened)
								return;

						txtValue.Text = DeleteLastSym(txtValue.Text);

						calculationTree.ChangeCurrentValue(txtValue.Text);
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
						if (exeptionHappened)
								return;
						double num = 0;

						if (calculationTree.CurrentType() == NodeType.operation)
						{
								num = Double.Parse(calculationTree.GetRightChildData());
								num *= -1;
								txtValue.Text = num.ToString();
								calculationTree.Add(txtValue.Text, NodeType.number);
						}
						else
						{
								num = Double.Parse(calculationTree.CurrentData());
								num *= -1;
								txtValue.Text = num.ToString();
								calculationTree.ChangeCurrentValue(txtValue.Text, NodeType.number);
						}
				}

				private void btn_comma(object sender, RoutedEventArgs e)
				{
						if (exeptionHappened)
								return;

						if (txtValue.Text.Contains(','))
								return;
						else
								txtValue.Text += ',';
				}
		}
}
