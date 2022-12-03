using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Day09Vis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string[] _input;
        public byte[,] _map;
        private Dictionary<int, Brush> _brushes;
        private Rectangle[,] _mapRef;

        public MainWindow()
        {
            InitializeComponent();
            _mapRef = new Rectangle[100, 100];
            _brushes = new Dictionary<int, Brush>();
            for (int i = 0; i < 10; i++)
            {
                _brushes.Add(i, new SolidColorBrush(Color.FromRgb((byte)(i * 10), (byte)(i * 10), (byte)(i * 10))));
            }
            _input = File.ReadAllLines("09.txt");
            _map = new byte[102, 102];
            for (int i = 0; i < 102; i++)
            {
                for (int j = 0; j < 102; j++)
                {
                    if (i == 0 || i == 101 || j == 0 || j == 101) _map[i, j] = (byte)9;
                    else _map[i, j] = Byte.Parse(_input[i - 1][j - 1].ToString());
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            createMap();
        }        

        private void createMap()
        {
            for (int i=0; i<100; i++)
            {
                for (int j=0; j<100; j++)
                {
                    Rectangle r = new Rectangle
                    {
                        Width = 10,
                        Height = 10,
                        Stroke = Brushes.Black,
                        StrokeThickness = 0.5,
                        Fill = _brushes[_map[i+1,j+1]]
                    };
                    Canvas.SetLeft(r, j * 10);
                    Canvas.SetTop(r, i * 10);
                    _mapRef[i, j] = r;
                    map.Children.Add(r);
                }
            }
        }

        public void run()
        {                         
            List<Pos> lowPoints = new List<Pos>();
            for (byte i = 1; i < 101; i++)
            {
                for (byte j = 1; j < 101; j++)
                {
                    byte cur = _map[i, j];
                    byte up = _map[i, j - 1];
                    byte right = _map[i + 1, j];
                    byte down = _map[i, j + 1];
                    byte left = _map[i - 1, j];

                    if (cur < up && cur < right && cur < down && cur < left)
                    {
                        lowPoints.Add(new Pos() { x = i, y = j });
                        _mapRef[i-1, j-1].Fill = Brushes.DarkRed;                        
                    }
                }
            }

            List<int> sizes = new List<int>();
            List<Basin> basins = new List<Basin>();
            foreach (Pos p in lowPoints)
            {
                Basin b = new Basin(p.x, p.y, _map[p.x, p.y]);
                basins.Add(b);

                int pointsAdded = 0;
                int pointsAddedLast;

                do
                {
                    pointsAddedLast = pointsAdded;
                    foreach (KeyValuePair<Tuple<byte, byte>, byte> kvp in b.Points.ToList())
                    {
                        byte upH = _map[kvp.Key.Item1, kvp.Key.Item2 - 1];
                        if (upH != 9 && b.Points.TryAdd(new Tuple<byte, byte>(kvp.Key.Item1, (byte)(kvp.Key.Item2 - 1)), upH))
                        {
                            pointsAdded++;
                            _mapRef[kvp.Key.Item1 - 1, kvp.Key.Item2 - 2].Fill = Brushes.DarkSalmon;
                        }
                        byte rightH = _map[kvp.Key.Item1 + 1, kvp.Key.Item2];
                        if (rightH != 9 && b.Points.TryAdd(new Tuple<byte, byte>((byte)(kvp.Key.Item1 + 1), kvp.Key.Item2), rightH))
                        {
                            pointsAdded++;
                            _mapRef[kvp.Key.Item1, kvp.Key.Item2 - 1].Fill = Brushes.DarkSalmon;
                        }
                        byte downH = _map[kvp.Key.Item1, kvp.Key.Item2 + 1];
                        if (downH != 9 && b.Points.TryAdd(new Tuple<byte, byte>(kvp.Key.Item1, (byte)(kvp.Key.Item2 + 1)), downH))
                        {
                            pointsAdded++;
                            _mapRef[kvp.Key.Item1 - 1, kvp.Key.Item2].Fill = Brushes.DarkSalmon;
                        }
                        byte leftH = _map[kvp.Key.Item1 - 1, kvp.Key.Item2];
                        if (leftH != 9 && b.Points.TryAdd(new Tuple<byte, byte>((byte)(kvp.Key.Item1 - 1), kvp.Key.Item2), leftH))
                        {
                            pointsAdded++;
                            _mapRef[kvp.Key.Item1 - 2, kvp.Key.Item2 - 1].Fill = Brushes.DarkSalmon;
                        }
                    }

                } while (pointsAdded > pointsAddedLast);
            }
        }

        public struct Pos
        {
            public byte x;
            public byte y;
        }

        public class Basin
        {
            public Dictionary<Tuple<byte, byte>, byte> Points;
            public Basin(byte lowx, byte lowy, byte lowHeight)
            {
                Points = new Dictionary<Tuple<byte, byte>, byte>
                {
                    { new Tuple<byte, byte>(lowx, lowy), lowHeight }
                };
            }

            public bool getBasinContainsPos(byte x, byte y)
            {
                return Points.ContainsKey(new Tuple<byte, byte>(x, y));
            }

            public int getSize()
            {
                return Points.Count();
            }
        }

        private void runButton_Click(object sender, RoutedEventArgs e)
        {
            run();
        }
    }

    

}
