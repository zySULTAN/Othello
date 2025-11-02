using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Othello
{
    public class TileClickedEventArgs : EventArgs
    {
        public int Row { get; }
        public int Col { get; }
        public TileClickedEventArgs(int r, int c) { Row = r; Col = c; }
    }

    public partial class GameGrid : UserControl
    {
        public event EventHandler<TileClickedEventArgs>? TileClicked;
        private const int Size = 8;
        private Rectangle[,] _cells = new Rectangle[Size, Size];

        public GameGrid()
        {
            InitializeComponent();
            BuildGrid();
        }

        private void BuildGrid()
        {
            RootGrid.Children.Clear();
            RootGrid.RowDefinitions.Clear();
            RootGrid.ColumnDefinitions.Clear();
            for (int i = 0; i < Size; i++)
            {
                RootGrid.RowDefinitions.Add(new RowDefinition());
                RootGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    var rect = new Rectangle
                    {
                        Stroke = Brushes.Black,
                        Fill = Brushes.Transparent,
                        Margin = new Thickness(2)
                    };
                    rect.MouseLeftButtonUp += (s, e) => TileClicked?.Invoke(this, new TileClickedEventArgs(r, c));
                    Grid.SetRow(rect, r);
                    Grid.SetColumn(rect, c);
                    RootGrid.Children.Add(rect);
                    _cells[r, c] = rect;
                }
            }
        }

        public void UpdateBoard(Disk[,] board)
        {
            int n = board.GetLength(0);
            // Clear existing disc visuals
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    // Remove any child ellipse at cell (we'll add at same grid cell)
                    // Simpler approach: set Fill based on state
                    switch (board[r, c])
                    {
                        case Disk.Black:
                            _cells[r, c].Fill = Brushes.Black;
                            break;
                        case Disk.White:
                            _cells[r, c].Fill = Brushes.White;
                            break;
                        default:
                            _cells[r, c].Fill = Brushes.Transparent;
                            break;
                    }
                }
            }
        }
    }
}
