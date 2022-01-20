using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace HomeTask_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            foreach (UIElement el in MainGrid.Children)
            {
                if (el is Button)
                {
                    ((Button)el).Click += ButtonClick;
                }
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string str = (string)((Button)e.OriginalSource).Content;

                if (str == "CE") // удаляет результат
                {
                    TextView2.Text = "";
                }
                else if (str == "C") // удаляет всё
                {
                    TextView1.Text = "";
                    TextView2.Text = "";
                }
                else if (str == "<") // удаляет последний введенный символ
                {
                    TextView1.Text = TextView1.Text.Remove(TextView1.Text.Length - 1, 1);
                }
                else if (str == "=")
                {
                    string result = new DataTable().Compute(TextView1.Text, null).ToString();
                    {
                        TextView2.Text = result;
                    }
                }
                // создаем исключение на ввод нулевых значений и точки в начале выражения:
                else if (TextView1.Text.StartsWith(".") ||
                         TextView1.Text.StartsWith("00") || TextView1.Text.StartsWith("01") ||
                         TextView1.Text.StartsWith("02") || TextView1.Text.StartsWith("03") ||
                         TextView1.Text.StartsWith("04") || TextView1.Text.StartsWith("05") ||
                         TextView1.Text.StartsWith("06") || TextView1.Text.StartsWith("07") ||
                         TextView1.Text.StartsWith("08") || TextView1.Text.StartsWith("09") )
                {
                    throw new Exception("Make sure you are typing double properly!");
                }

                else
                { TextView1.Text += str; }
            }
            catch (Exception ex) // ловим остальные виды исключений
            {
                //можно использовать варианты:
                MessageBox.Show(ex.Message);
                //TextView2.Text = ex.Message;
            }
        }
    }
}
