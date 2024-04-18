using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace SnakeGame
{
    public partial class MainWindow : Window
    {
        private SnakeGameLogic gameLogic;

        public MainWindow()
        {
            InitializeComponent();
            gameLogic = new SnakeGameLogic(canvas);
            gameLogic.InitializeGame();
            gameLogic.SetupUDP("192.168.113.42", 12345); 
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            gameLogic.HandleKeyPress(e.Key);
        }
    }
}
