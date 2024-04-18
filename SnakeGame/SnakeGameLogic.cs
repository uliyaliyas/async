using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.ComponentModel;
using System.Net.Sockets;
using System.Net;
using System.Windows.Threading;
using System.Text;
using System.Windows.Input;

namespace SnakeGame
{
    public class SnakeGameLogic
    {
        private const int TileSize = 20;
        private const int BoardWidth = 20;
        private const int BoardHeight = 20;
        private List<Point> snake;
        private Point food;
        private Direction direction;
        private DispatcherTimer timer;
        private UdpClient client;
        private IPEndPoint remoteEndPoint;
        private Canvas canvas;

        public SnakeGameLogic(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public void InitializeGame()
        {
            snake = new List<Point>();
            snake.Add(new Point(5, 5)); // Initial position
            direction = Direction.Right; // Initial direction
            food = GenerateFood();
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(100); // Update interval
            timer.Start();
        }

        public void SetupUDP(string remoteIPAddress, int port)
        {
            client = new UdpClient();
            remoteEndPoint = new IPEndPoint(IPAddress.Parse(remoteIPAddress), port);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            MoveSnake();
            CheckCollision();
            UpdateUI();
            SendData();
        }

        private void MoveSnake()
        {
            Point newHead = snake[0];
            switch (direction)
            {
                case Direction.Up:
                    newHead.Y--;
                    break;
                case Direction.Down:
                    newHead.Y++;
                    break;
                case Direction.Left:
                    newHead.X--;
                    break;
                case Direction.Right:
                    newHead.X++;
                    break;
            }
            snake.Insert(0, newHead);
            if (newHead != food)
            {
                snake.RemoveAt(snake.Count - 1); // Remove tail
            }
            else
            {
                food = GenerateFood(); // Spawn new food
            }
        }

        private void CheckCollision()
        {
            // Check collision with walls
            if (snake[0].X < 0 || snake[0].X >= BoardWidth || snake[0].Y < 0 || snake[0].Y >= BoardHeight)
            {
                MessageBox.Show("Game over!");
                InitializeGame();
            }

            // Check collision with self
            for (int i = 1; i < snake.Count; i++)
            {
                if (snake[0] == snake[i])
                {
                    MessageBox.Show("Game over!");
                    InitializeGame();
                }
            }
        }

        private void UpdateUI()
        {
            canvas.Children.Clear();
            DrawSnake();
            DrawFood();
        }

        private void DrawSnake()
        {
            foreach (Point segment in snake)
            {
                Rectangle rect = new Rectangle();
                rect.Width = TileSize;
                rect.Height = TileSize;
                rect.Fill = Brushes.Green;
                Canvas.SetLeft(rect, segment.X * TileSize);
                Canvas.SetTop(rect, segment.Y * TileSize);
                canvas.Children.Add(rect);
            }
        }

        private void DrawFood()
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = TileSize;
            ellipse.Height = TileSize;
            ellipse.Fill = Brushes.Red;
            Canvas.SetLeft(ellipse, food.X * TileSize);
            Canvas.SetTop(ellipse, food.Y * TileSize);
            canvas.Children.Add(ellipse);
        }

        private Point GenerateFood()
        {
            Random rand = new Random();
            int x = rand.Next(BoardWidth);
            int y = rand.Next(BoardHeight);
            return new Point(x, y);
        }

        private void SendData()
        {
            try
            {
                string data = $"{snake[0].X},{snake[0].Y}";
                byte[] bytes = Encoding.ASCII.GetBytes(data);
                client.Send(bytes, bytes.Length, remoteEndPoint);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending data: " + ex.Message);
            }
        }

        public void HandleKeyPress(Key key)
        {
            switch (key)
            {
                case Key.W:
                    direction = Direction.Up;
                    break;
                case Key.S:
                    direction = Direction.Down;
                    break;
                case Key.A:
                    direction = Direction.Left;
                    break;
                case Key.D:
                    direction = Direction.Right;
                    break;
            }
        }

        private enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
    }
}
