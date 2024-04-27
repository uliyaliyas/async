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

namespace SnakeWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Ellipse figure = new Ellipse();
            figure.Width = 100;
            figure.Height = 100;
            figure.Fill = Brushes.Black;
            figure.Stroke = Brushes.Blue;
            figure.StrokeThickness = 3;
            OurCanvas.Children.Add(figure);
            OurCanvas.Children[0].
        }
    }
}