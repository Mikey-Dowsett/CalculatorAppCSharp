using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    private readonly DataTable dataTable = new();
    private string? answer;
    private string? calculation = "";

    public MainWindow() {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e) {
        FrameworkElement? sourceFrameworkElement = e.Source as FrameworkElement;
        switch (sourceFrameworkElement?.Name) {
            case "Zero":
                calculation += calculation == "" ? "" : "0";
                break;
            case "One":
                calculation += "1";
                break;
            case "Two":
                calculation += "2";
                break;
            case "Three":
                calculation += "3";
                break;
            case "Four":
                calculation += "4";
                break;
            case "Five":
                calculation += "5";
                break;
            case "Six":
                calculation += "6";
                break;
            case "Seven":
                calculation += "7";
                break;
            case "Eight":
                calculation += "8";
                break;
            case "Nine":
                calculation += "9";
                break;
            case "Plus":
                AddOperator('+');
                break;
            case "Minus":
                AddOperator('-');
                break;
            case "Multiply":
                AddOperator('*');
                break;
            case "Divide":
                AddOperator('/');
                break;
            case "Remainder":
                AddOperator('%');
                break;
            case "Decimal":
                AddOperator('.');
                break;
            case "Equals":
                try {
                    answer = Convert.ToString(dataTable.Compute(calculation, null));
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text += $"{calculation} = {answer}";
                    HistoryText.Children.Insert(0, textBlock);
                    calculation = answer;
                    break;
                }
                catch (ArithmeticException ae) {
                    Console.WriteLine(ae.Message);
                    calculation = "Error";
                    break;
                }
            case "Clear":
                calculation = "";
                break;
        }


        CalcDisplay.Text = calculation == "" ? "0" : calculation;
    }

    private void AddOperator(char op) {
        if (calculation != null) {
            char lastChar = calculation.Last();
            Console.WriteLine(lastChar);
            Console.WriteLine(lastChar);
            if (lastChar != '+' && lastChar != '-' && lastChar != '*' && lastChar != '/' && lastChar != '%')
                calculation += op;
        }
    }

    private void ShowHistory(object sender, RoutedEventArgs routedEventArgs) {
        HistoryPanel.Visibility =
            HistoryPanel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        CalculatorBody.Visibility =
            CalculatorBody.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
    }

    private void CloseApp(object sender, RoutedEventArgs e) {
        Application.Current.Shutdown();
    }

    private void MinimizeApp(object sender, RoutedEventArgs e) {
        WindowState = WindowState.Minimized;
    }

    private void MaximizeApp(object sender, RoutedEventArgs e) {
        WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    }

    private void DragWindow(object sender, MouseButtonEventArgs e) {
        if (e.LeftButton == MouseButtonState.Pressed) DragMove();
    }

    private void ListViewScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e) {
        ScrollViewer scv = (ScrollViewer)sender;
        scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
        e.Handled = true;
    }
}