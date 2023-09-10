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
using System.Windows.Shapes;

namespace MineSweeperWPF.Views
{
    public partial class CustomMessageBox : Window
    {
        public bool IsOk { get; set; } = true;

        public CustomMessageBox()
        {
            InitializeComponent();

            rowCount.GotFocus += new RoutedEventHandler(RemoveText);
            rowCount.LostFocus += new RoutedEventHandler(AddTextRowCount);

            columnCount.GotFocus += new RoutedEventHandler(RemoveText);
            columnCount.LostFocus += new RoutedEventHandler(AddTextColumnCount);

            bombCount.GotFocus += new RoutedEventHandler(RemoveText);
            bombCount.LostFocus += new RoutedEventHandler(AddTextBombCount);
        }

        public void RemoveText(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox.Text == "Number of rows" || textBox.Text == "Number of columns" || textBox.Text == "Number of bombs")
            {
                textBox.FontStyle = FontStyles.Normal;
                textBox.FontWeight = FontWeights.SemiBold;
                textBox.Text = String.Empty;
            }
        }

        public void ClearText(TextBox textBox)
        {
            textBox.Text = String.Empty;
        }

        public void AddTextRowCount(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rowCount.Text))
            {
                rowCount.FontStyle = FontStyles.Italic;
                rowCount.FontWeight = FontWeights.Light;
                rowCount.Text = "Number of rows";
            }
        }

        public void AddTextColumnCount(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(columnCount.Text))
            {
                columnCount.FontStyle = FontStyles.Italic;
                columnCount.FontWeight = FontWeights.Light;
                columnCount.Text = "Number of columns";
            }
        }

        public void AddTextBombCount(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(bombCount.Text))
            {
                bombCount.FontStyle = FontStyles.Italic;
                bombCount.FontWeight = FontWeights.Light;
                bombCount.Text = "Number of bombs";
            }
        }

        private void Ok_btn_Click(object sender, RoutedEventArgs e)
        {
            IsOk = true;

            if (string.IsNullOrWhiteSpace(rowCount.Text))
            {
                AddTextRowCount(sender, e);
                IsOk = false;
            }

            if (string.IsNullOrWhiteSpace(columnCount.Text))
            {
                AddTextColumnCount(sender, e);
                IsOk = false;
            }

            if (string.IsNullOrWhiteSpace(bombCount.Text))
            {
                AddTextBombCount(sender, e);
                IsOk = false;
            }

            if (!int.TryParse(rowCount.Text.ToString(), out int rCount))
            {
                ClearText(rowCount);
                AddTextRowCount(sender, e);
                IsOk = false;
            }

            if (!int.TryParse(columnCount.Text.ToString(), out int cCount))
            {
                ClearText(columnCount);
                AddTextColumnCount(sender, e);
                IsOk = false;
            }

            if (!int.TryParse(bombCount.Text.ToString(), out int bCount))
            {
                ClearText(bombCount);
                AddTextBombCount(sender, e);
                IsOk = false;
            }

            if (IsOk)
            {
                var count = int.Parse(rowCount.Text.ToString());
                var num = count % 2;

                if (int.Parse(rowCount.Text.ToString()) < 8 || int.Parse(rowCount.Text.ToString()) > 24 || num == 1) IsOk = false;
            }

            if (IsOk)
            {
                var rowDivideColumn = double.Parse(columnCount.Text.ToString()) / double.Parse(rowCount.Text.ToString());
                
                var count = int.Parse(columnCount.Text.ToString());
                var num = count % 2;

                if (int.Parse(columnCount.Text.ToString()) < 10 || int.Parse(columnCount.Text.ToString()) > 30 || num == 1 || rowDivideColumn < 1.2) IsOk = false;
            }

            if (IsOk)
            {
                var maxBombCount = (int.Parse(columnCount.Text.ToString()) - 1) * (int.Parse(rowCount.Text.ToString()) - 1);

                if (int.Parse(bombCount.Text.ToString()) < 10 || int.Parse(bombCount.Text.ToString()) > maxBombCount) IsOk = false;
            }

            if (IsOk) Hide();
            else
            {
                ClearText(rowCount);
                AddTextRowCount(sender, e);

                ClearText(columnCount);
                AddTextColumnCount(sender, e);

                ClearText(bombCount);
                AddTextBombCount(sender, e);
            }
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            IsOk = false;
            Hide();
        } 
    }
}
