using System.Windows;

namespace MineSweeperWPF.Views
{
    public partial class Entrance : Window
    {
        public string Info { get; set; } = "Click left mouse button to select an area, \nright mouse button to mark a bomb";
        
        public Entrance()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void StartButtonClick(object sender, RoutedEventArgs e) => Close();
    }
}
