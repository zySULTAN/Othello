using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Othello.Models;

namespace Othello.Views
{
    public partial class GameGrid : UserControl
    {
        public event Action<int, int> TileClicked;

        public GameGrid()
        {
            InitializeComponent();
        }

        public void Render(GameBoard board)
        {
            GridBoard.Children.Clear();
            GridBoard.Rows = GameBoard.Size;
            GridBoard.Columns = GameBoard.Size;

            for (int i = 0; i < GameBoard.Size; i++)
            {
                for (int j = 0; j < GameBoard.Size; j++)
                {
                    Button btn = new Button();
                    btn.Margin = new Thickness(1);

                    if ((i + j) % 2 == 0)
                        btn.Background = Brushes.DarkGreen;
                    else
                        btn.Background = Brushes.ForestGreen;

                    btn.Tag = new Tuple<int, int>(i, j);
                    btn.Click += Button_Click;

                    string disk = board.GetDisk(i, j);

                    if (disk != null)
                    {
                        Ellipse el = new Ellipse();
                        el.Width = 28;
                        el.Height = 28;
                        el.Stroke = Brushes.Black;
                        el.HorizontalAlignment = HorizontalAlignment.Center;
                        el.VerticalAlignment = VerticalAlignment.Center;

                        if (disk == "Black") el.Fill = Brushes.Black;
                        else el.Fill = Brushes.White;

                        btn.Content = el;
                    }

                    GridBoard.Children.Add(btn);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            Tuple<int, int> tag = btn.Tag as Tuple<int, int>;
            if (tag == null) return;

            if (TileClicked != null)
            {
                TileClicked(tag.Item1, tag.Item2);
            }
        }
    }
}
