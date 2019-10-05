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
using System.Diagnostics;
using System.Management.Automation;



namespace Calculator
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        bool press_eq = false;
        string prev_ans = ""; // 前回の値を記録する

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Content.ToString() == "DEL") // deleteキーが押されたとき
            {
                delete(sender, e);
            }
            else if (((Button)sender).Content.ToString() == "x") // かけるが押されたとき*で表示
            {
                textBox.Text += "*";
            }
            else if (((Button)sender).Content.ToString() == "AC") // all-clear
            {
                textBox.Text = "";
            }
            else if (((Button)sender).Content.ToString() == "=") {
                equal(sender, e);
            }
            else
            {
                if (press_eq == true)
                {
                    textBox.Text = string.Empty;
                    press_eq = false;
                }
                textBox.Text += (sender as Button).Content;

            }
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            string text_delete = textBox.Text;
            if (text_delete.Length > 1)
            {
                text_delete = text_delete.Substring(0, text_delete.Length - 1); // 最後の一文字を消す
            }
            else
            {
                text_delete = "";
            }

            textBox.Text = text_delete;
        }


        //  現時点でtextBoxに入っている計算をPowerShellに投げる
        private void equal(object sender, RoutedEventArgs e)
        {
            string input = @textBox.Text;
            string output;

            using (var invoker = new RunspaceInvoke())
            {
                var result = invoker.Invoke(input);

                //output = result.ToString();
                press_eq = true;

                /*You can't pass collection of PSObject to datasource directly. 
                  You need to retrieve relating data and regroup it as right format that datagridview recognized
                */            
                output = result[0].ToString();

                textBox.Text += "\r\n = " + output;
            }
            //object result = invoker.Invoke(input)[0]

            //input.Replace('1', '2');

            //textBox.Text += input.ToString();

            //Process cmd = new Process();
            //cmd.StartInfo.FileName = "PowerShell.exe";
            //// PowerShellのWindowを立ち上げずに実行
            //cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            // 

        }

    }
}
