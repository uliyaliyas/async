using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SnakeWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double X { get; set; }
        private double Y { get; set; }
        private Ellipse figure;
        private int to = 1;
        DispatcherTimer gameTickTimer;
        public MainWindow()
        {
            InitializeComponent();
            figure = new Ellipse();
            figure.Width = 50;
            figure.Height = 50;
            figure.Fill = Brushes.DeepPink;
            figure.Stroke = Brushes.White;
            figure.StrokeThickness = 3;
            OurCanvas.Children.Add(figure);
            X = 120.0;
            Y = 150.0;
            figure.SetValue(Canvas.TopProperty, Y);
            figure.SetValue(Canvas.LeftProperty, X);
            gameTickTimer = new DispatcherTimer();
            gameTickTimer.Tick += Move;
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(300);
            gameTickTimer.Start();
        }
        private void Move(object sender, EventArgs e)
        {

            switch (to)
            {
                case 1:
                    {
                        X += 10;
                        figure.SetValue(Canvas.LeftProperty, X);
                    }
                    break;
                case 2:
                    {
                        X -= 10;
                        figure.SetValue(Canvas.LeftProperty, X);
                    }
                    break;
                case 3:
                    {
                        Y -= 10;
                        figure.SetValue(Canvas.TopProperty, Y);
                    }
                    break;
                case 4:
                    {
                        Y += 10;
                        figure.SetValue(Canvas.TopProperty, Y);
                    }
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            to = 1;
            //X += 10;
            //figure.SetValue(Canvas.LeftProperty, X);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            to = 3;
            //Y -= 10;
            //figure.SetValue(Canvas.TopProperty, Y);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            to = 2;
            //X -= 10;
            //figure.SetValue(Canvas.LeftProperty, X);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            to = 4;
            //Y += 10;
            //figure.SetValue(Canvas.TopProperty, Y);
        }
        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            switch (e.Key.ToString())
            {
                case "Down": to = 4; break;
                case "Up": to = 3; break;
                case "Left": to = 2; break;
                case "Right": to = 1; break;
            }
        }
    }
}