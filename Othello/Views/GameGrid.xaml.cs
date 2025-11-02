using Othello.Models;
using Othello.Players;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

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

            for (int r = 0; r < GameBoard.Size; r++)
            {
                for (int c = 0; c < GameBoard.Size; c++)
                {
                    var btn = new Button { Margin = new Thickness(1) };
                    btn.Background = (r + c) % 2 == 0 ? Brushes.DarkGreen : Brushes.ForestGreen;
                    int rr = r, cc = c;
                    btn.Click += (s, e) => TileClicked?.Invoke(rr, cc);

                    var disk = board.GetDisk(r, c);
                    if (disk != null)
                    {
                        var ellipse = new Ellipse { Width = 28, Height = 28 };
                        ellipse.Fill = disk == DiskColor.Black ? Brushes.Black : Brushes.White;
                        ellipse.Stroke = Brushes.Black;
                        ellipse.HorizontalAlignment = HorizontalAlignment.Center;
                        ellipse.VerticalAlignment = VerticalAlignment.Center;
                        btn.Content = ellipse;
                    }

                    GridBoard.Children.Add(btn);
                }
            }
        }
    }
}
