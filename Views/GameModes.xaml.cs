using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Media;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MineSweeperWPF.Views
{
    public partial class GameModes : Window, INotifyPropertyChanged
    {
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public int BombCount { get; set; }
        public int MainColumn { get; set; }
        public int MainRow { get; set; }
        public int ButtonFontSize { get; set; }
        public int BestResult { get; set; }
        public int PreviousSelectedIndex { get; set; }
        public bool IsFirstChoice { get; set; } = true;
        public bool IsWin { get; set; } = false;
        public bool IsSoundOn { get; set; } = true;
        public bool IsShowEntrance { get; set; } = true;
        public string[] GameLevels { get; set; }

        public SoundPlayer SoundPlayer { get; set; }
        public DispatcherTimer GameDispatcher { get; set; }

        public List<string> NegativeButtons { get; set; } = new List<string>();
        public List<string> ButtonSurrounding { get; set; } = new List<string>();

        private int _flagCount = 0;
        public int FlagCount
        {
            get => _flagCount;
            set
            {
                _flagCount = value;
                OnPropertyChanged();
            }
        }

        private int _timerSeconds = 0;
        public int TimerSeconds
        {
            get => _timerSeconds;
            set
            {
                _timerSeconds = value;
                OnPropertyChanged();
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GameModes()
        {
            InitializeComponent();

            SoundPlayer = new SoundPlayer();

            GameLevels = new string[] { "Easy", "Medium", "Hard", "Custom" };

            GameDispatcher = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1)
            };
            GameDispatcher.Tick += new EventHandler(DispatcherTickEvent);

            DataContext = this;
        }

        private void Main_Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (IsShowEntrance)
            {
                Entrance entrance = new Entrance();
                entrance.ShowDialog();

                IsShowEntrance = false;
            }
        }

        public void DispatcherTickEvent(object sender, EventArgs e)
        {
            if (TimerSeconds < 999) TimerSeconds++;
            else GameDispatcher.Stop();
        }

        public void MakeGrid()
        {
            Buttons.Children.Clear();
            Buttons.RowDefinitions.Clear();
            Buttons.ColumnDefinitions.Clear();

            for (int i = 0; i < RowCount; i++)
            {
                Buttons.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < ColumnCount; i++)
            {
                Buttons.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        public void FillGrid()
        {
            int count = 1;

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    Button Grid_Button = new Button
                    {
                        Name = "Btn_" + count.ToString(),
                        Content = "0",
                        FontSize = ButtonFontSize,
                    };

                    Grid_Button.Click += new RoutedEventHandler(LeftClick);
                    Grid_Button.MouseRightButtonDown += new MouseButtonEventHandler(RightClick);

                    var num = int.Parse((Grid_Button.Name as string).Split('_')[1]) + i;

                    if (num % 2 == 1) Grid_Button.Style = FindResource("LightGreenButton") as Style;
                    else Grid_Button.Style = FindResource("DarkGreenButton") as Style;

                    Grid.SetRow(Grid_Button, i);
                    Grid.SetColumn(Grid_Button, j);

                    Buttons.Children.Add(Grid_Button);

                    count++;
                }
            }
        }

        private void Levels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((sender as ComboBox).SelectedIndex)
            {
                case 0:
                    CustomizeMainComponents(8, 10, 24, 10, 420, 450);
                    PreviousSelectedIndex = 0;

                    break;
                case 1:
                    CustomizeMainComponents(14, 18, 20, 40, 480, 550);
                    PreviousSelectedIndex = 1;

                    break;
                case 2:
                    CustomizeMainComponents(20, 24, 16, 99, 540, 600);
                    PreviousSelectedIndex = 2;

                    break;
                case 3:
                    {
                        CustomMessageBox messageBox = new CustomMessageBox();

                        messageBox.ShowDialog();

                        if (!messageBox.IsOk)
                        {
                            Modes.SelectedIndex = PreviousSelectedIndex;
                            break;
                        }

                        var rowCount = int.Parse(messageBox.rowCount.Text);
                        var columnCount = int.Parse(messageBox.columnCount.Text);
                        var bombCount = int.Parse(messageBox.bombCount.Text);

                        double fontSize = ((double)rowCount - 8) / 6;
                        if (fontSize - (int)fontSize > 0) fontSize++;

                        var btnFontSize = 24 - ((int)fontSize * 4);

                        var height = rowCount - 8;
                        var windowHeight = (height * 10) + 360;

                        var width = columnCount - 10;
                        var windowWidth = (width * 12) + 450;

                        CustomizeMainComponents(rowCount, columnCount, btnFontSize, bombCount, windowHeight, windowWidth);

                        break;
                    }
            }
        }

        public void CustomizeMainComponents(int rowCount, int columnCount, int buttonFontSize, int bombCount, int windowHeight, int windowWidth)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;

            ButtonFontSize = buttonFontSize;

            MakeGrid();
            FillGrid();

            Height = windowHeight;
            Width = windowWidth;

            GridRow1.Height = new GridLength(60);
            GridRow2.Height = new GridLength(windowHeight - 60);

            if (!Modes.IsEnabled) Modes.IsEnabled = true;
            if (!Buttons.IsHitTestVisible) Buttons.IsHitTestVisible = true;

            BombCount = bombCount;
            FlagCount = bombCount;

            GameDispatcher.Stop();

            if (TimerSeconds > 0) TimerSeconds = 0;

            if (!IsFirstChoice) IsFirstChoice = true;

            if (ButtonSurrounding.Count > 0) ButtonSurrounding.Clear();

            if (NegativeButtons.Count > 0) NegativeButtons.Clear();

            if (IsWin) IsWin = false;
        }

        private void Close_btn_Click(object sender, RoutedEventArgs e) => Close();

        private void Speaker_btn_Click(object sender, RoutedEventArgs e)
        {
            if (IsSoundOn)
            {
                SoundPlayer.Stop();

                IsSoundOn = false;

                speaker_btn.Content = new Image
                {
                    Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Images/mute.png", UriKind.RelativeOrAbsolute))
                };
            }
            else
            {
                IsSoundOn = true;

                speaker_btn.Content = new Image
                {
                    Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Images/sound.png", UriKind.RelativeOrAbsolute))
                };
            }
        }

        public bool CheckButtonContentIfNeg(string rowAndColumn) => NegativeButtons.Contains(rowAndColumn);

        public void FindButtonSurrounding(Button button)
        {
            var row = Grid.GetRow(button);
            var column = Grid.GetColumn(button);

            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = column - 1; j <= column + 1; j++)
                {
                    if (!IsInGrid(i, j)) continue;

                    var rowAndColumn = i.ToString() + "," + j.ToString();

                    ButtonSurrounding.Add(rowAndColumn);
                }
            }
        }

        public void ChangeButtonForeground(Button button, int num)
        {
            if (num == 1) button.Foreground = (Brush)new BrushConverter().ConvertFrom("#1976D2");
            else if (num == 2) button.Foreground = (Brush)new BrushConverter().ConvertFrom("#388E3C");
            else if (num == 3) button.Foreground = (Brush)new BrushConverter().ConvertFrom("#D33030");
            else if (num == 4) button.Foreground = (Brush)new BrushConverter().ConvertFrom("#7B1FA2");
            else button.Foreground = (Brush)new BrushConverter().ConvertFrom("#F4840D");
        }

        public int CheckButtonEnabled()
        {
            int count = 0;

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    var button = GetChild(Buttons, i, j) as Button;

                    if (!button.IsEnabled) count++;
                }
            }

            return count;
        }

        public int CheckButtonContent()
        {
            int count = 0;

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    var button = GetChild(Buttons, i, j) as Button;

                    if (button.Content is Image || button.Content.ToString() == "-1") count++;
                }
            }

            return count;
        }

        public void CheckWin()
        {
            int count = CheckButtonEnabled() + CheckButtonContent();

            if (count == (RowCount * ColumnCount))
            {
                IsWin = true;
                EndGame();
            }
        }

        public async void EndGame()
        {
            Modes.IsEnabled = false;
            
            Buttons.IsHitTestVisible = false;

            GameDispatcher.Stop();

            if (!IsWin) TimerSeconds = 0;

            if (IsWin) WinSound();
            else MineSound();

            if (IsWin) ShowBombsIfWin();
            else ShowBombsIfLose();

            await Task.Delay(5000);

            Hide();

            GameEndSound();

            End endWindow = new End();

            if (IsWin)
            {
                endWindow.timer_result.Content = TimerSeconds;

                if (BestResult == 0) BestResult = TimerSeconds;
                else if (TimerSeconds < BestResult) BestResult = TimerSeconds;
            }

            if (BestResult > 0) endWindow.trophy_result.Content = BestResult;

            endWindow.ShowDialog();

            SoundPlayer.Stop();

            if (Modes.SelectedIndex == 0)
            {
                CustomizeMainComponents(8, 10, 24, 10, 420, 450);
                PreviousSelectedIndex = 0;
            }
            else Modes.SelectedIndex = 0;

            Show();
        }

        public void RightClick(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Button;

            if (!button.IsEnabled) return;

            if (button.Content is Image)
            {
                var row = Grid.GetRow(button);
                var column = Grid.GetColumn(button);
                var rowAndColumn = row.ToString() + "," + column.ToString();

                if (CheckButtonContentIfNeg(rowAndColumn)) button.Content = -1;
                else button.Content = 0;

                TakeOutFlagSound();

                FlagCount++;
            }
            else
            {
                button.Content = new Image
                {
                    Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Images/red_flag.png", UriKind.RelativeOrAbsolute))
                };

                FlagSound();

                FlagCount--;
            }
        }

        public void LeftClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (TimerSeconds == 0) GameDispatcher.Start();

            if (IsFirstChoice)
            {
                EmptyEreaSound();

                FindButtonSurrounding(button);

                AddBombs();

                OpenCells(button);

                IsFirstChoice = false;
            }
            else
            {
                if (button.Content is Image) return;

                if (button.Content.ToString() == "-1") EndGame();
                else
                {
                    EmptyEreaSound();

                    OpenCells(button);

                    CheckWin();
                }
            }
        }

        public static UIElement GetChild(Grid grid, int row, int column)
        {
            foreach (UIElement child in grid.Children)
            {
                if (Grid.GetRow(child) == row && Grid.GetColumn(child) == column) return child;
            }
            return null;
        }

        public void ShowBombsIfWin()
        {
            foreach (var item in NegativeButtons)
            {
                var splitItem = item.Split(',');

                var row = int.Parse(splitItem[0]);
                var column = int.Parse(splitItem[1]);

                var button = GetChild(Buttons, row, column) as Button;

                button.Content = new Image
                {
                    Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Images/black_bomb.png", UriKind.RelativeOrAbsolute))
                };
            }
        }

        public void ShowBombsIfLose()
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    var button = GetChild(Buttons, i, j) as Button;
                    var rowColumn = i.ToString() + "," + j.ToString();

                    if (button.Content is Image)
                    {
                        if (!NegativeButtons.Contains(rowColumn))
                        {
                            button.Content = new Image
                            {
                                Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Images/cancel.png", UriKind.RelativeOrAbsolute))
                            };
                        }
                    }
                    else
                    {
                        if (NegativeButtons.Contains(rowColumn))
                        {
                            button.Background = (Brush)new BrushConverter().ConvertFrom("#FF8080");

                            button.Content = new Image
                            {
                                Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Images/black_bomb.png", UriKind.RelativeOrAbsolute))
                            };
                        }
                    }
                }
            }
        }

        public void EmptyEreaSound()
        {
            SoundPlayer = new SoundPlayer(Properties.Resources.btn_click);
            if (IsSoundOn) SoundPlayer.Play();
        }

        public void MineSound()
        {
            SoundPlayer = new SoundPlayer(Properties.Resources.lose_minesweeper);
            if (IsSoundOn) SoundPlayer.Play();
        }

        public void FlagSound()
        {
            SoundPlayer = new SoundPlayer(Properties.Resources.flag);
            if (IsSoundOn) SoundPlayer.Play();
        }

        public void TakeOutFlagSound()
        {
            SoundPlayer = new SoundPlayer(Properties.Resources.take_out_flag);
            if (IsSoundOn) SoundPlayer.Play();
        }

        public void WinSound()
        {
            SoundPlayer = new SoundPlayer(Properties.Resources.win);
            if (IsSoundOn) SoundPlayer.Play();
        }

        public void GameEndSound()
        {
            SoundPlayer = new SoundPlayer(Properties.Resources.game_end);
            if (IsSoundOn) SoundPlayer.Play();
        }

        public bool IsInGrid(int row, int column)
        {
            if (row < 0 || column < 0 || row > RowCount - 1 || column > ColumnCount - 1) return false;
            return true;
        }

        public void AddBombs()
        {
            Random random = new Random();

            for (int i = 0; i < BombCount; i++)
            {
                while (true)
                {
                    var randomRow = random.Next(0, RowCount);
                    var randomColumn = random.Next(0, ColumnCount);

                    var rowAndColumn = randomRow.ToString() + "," + randomColumn.ToString();

                    if (ButtonSurrounding.Contains(rowAndColumn)) continue;

                    var button = GetChild(Buttons, randomRow, randomColumn) as Button;

                    if (!button.IsEnabled || button.Content.ToString() == "-1") continue;

                    button.Content = -1;

                    NegativeButtons.Add(rowAndColumn);

                    break;
                }
            }
        }

        public int CheckButtonSurrounding(Button button)
        {
            int bombCount = 0;

            var row = Grid.GetRow(button);
            var column = Grid.GetColumn(button);

            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = column - 1; j <= column + 1; j++)
                {
                    if (!IsInGrid(i, j)) continue;

                    var relatedButton = GetChild(Buttons, i, j) as Button;

                    if (!relatedButton.IsEnabled) continue;

                    if (relatedButton.Content is Image || relatedButton.Content.ToString() == "-1") bombCount++;
                }
            }

            return bombCount;
        }

        public void OpenCell(Button button)
        {
            var row = Grid.GetRow(button);
            var num = int.Parse((button.Name as string).Split('_')[1]) + row;

            if (button.Content is Image) return;

            if (num % 2 == 1)
            {
                if (button.IsEnabled)
                {
                    button.Background = (Brush)new BrushConverter().ConvertFrom("#DBBFA3");

                    var bombCount = CheckButtonSurrounding(button);

                    if (bombCount > 0) ChangeButtonForeground(button, bombCount);
                    else button.Foreground = (Brush)new BrushConverter().ConvertFrom("#DBBFA3");

                    button.IsEnabled = false;
                }

                else return;
            }

            else if (num % 2 == 0)
            {
                if (button.IsEnabled)
                {
                    button.Background = (Brush)new BrushConverter().ConvertFrom("#E5C29F");

                    var bombCount = CheckButtonSurrounding(button);

                    if (bombCount > 0) ChangeButtonForeground(button, bombCount);
                    else button.Foreground = (Brush)new BrushConverter().ConvertFrom("#E5C29F");

                    button.IsEnabled = false;
                }
                else return;
            }
        }

        private void OpenCells(Button button)
        {
            var row = Grid.GetRow(button);
            var column = Grid.GetColumn(button);
            var bombCount = CheckButtonSurrounding(button);

            OpenCell(button);

            if (bombCount > 0)
            {
                button.Content = bombCount;
                return;
            }

            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = column - 1; j <= column + 1; j++)
                {
                    if (!IsInGrid(i, j)) continue;

                    if (!(GetChild(Buttons, i, j) as Button).IsEnabled) continue;

                    var relatedButton = GetChild(Buttons, i, j) as Button;
                    var relatedBombCount = CheckButtonSurrounding(relatedButton);

                    if (relatedBombCount == 0)
                    {
                        OpenCells(relatedButton);
                    }
                    else if (relatedBombCount > 0)
                    {
                        relatedButton.Content = CheckButtonSurrounding(relatedButton);
                        OpenCell(relatedButton);
                    }
                }
            }
        }
    }
}
