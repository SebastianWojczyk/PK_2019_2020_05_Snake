using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _05_Snake
{

    class SnakeLogic
    {
        public enum SnakeDirection { Left, Right, Up, Down }

        public delegate void SnakeEvent();
        public event SnakeEvent SnakeMoved;
        public event SnakeEvent GameEnded;

        private Random generator = new Random();

        private int width;
        private int height;
        private List<Point> snake;
        private SnakeDirection direction;
        private Point apple;
        private int appleCount;

        private Timer timerSnakeMove = new Timer();

        public int Width { get => width; private set => width = value; }
        public int Height { get => height; private set => height = value; }
        public List<Point> Snake { get => snake; private set => snake = value; }
        internal SnakeDirection Direction
        {
            private get => direction;
            set
            {
                if ((direction == SnakeDirection.Up && value != SnakeDirection.Down) ||
                    (direction == SnakeDirection.Down && value != SnakeDirection.Up) ||
                    (direction == SnakeDirection.Left && value != SnakeDirection.Right) ||
                    (direction == SnakeDirection.Right && value != SnakeDirection.Left))
                {
                    direction = value;
                    TimerSnakeMove_Tick(null, null);
                }
            }
        }

        public Point Apple { get => apple; private set => apple = value; }
        public int AppleCount { get => appleCount; private set => appleCount = value; }

        public SnakeLogic(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            Snake = new List<Point>();
            Snake.Add(new Point(width / 2, height - 1));
            Snake.Add(new Point(width / 2, height + 0));
            Snake.Add(new Point(width / 2, height + 1));
            Snake.Add(new Point(width / 2, height + 2));

            Direction = SnakeDirection.Up;

            generateApple();
            AppleCount = 0;

            timerSnakeMove.Tick += TimerSnakeMove_Tick;
            timerSnakeMove.Interval = 500;
            timerSnakeMove.Enabled = true;
        }

        private void generateApple()
        {
            Point tmpApple;
            do
            {
                tmpApple = new Point(generator.Next(Width), generator.Next(Height));
            } while (Snake.Contains(tmpApple));

            Apple = tmpApple;
        }

        private void TimerSnakeMove_Tick(object sender, EventArgs e)
        {
            Point newHead = Point.Empty;
            switch (Direction)
            {
                case SnakeDirection.Left:
                    newHead = new Point(Snake.First().X - 1, Snake.First().Y);
                    break;
                case SnakeDirection.Right:
                    newHead = new Point(Snake.First().X + 1, Snake.First().Y);
                    break;
                case SnakeDirection.Up:
                    newHead = new Point(Snake.First().X, Snake.First().Y - 1);
                    break;
                case SnakeDirection.Down:
                    newHead = new Point(Snake.First().X, Snake.First().Y + 1);
                    break;
            }
            newHead = new Point((newHead.X + Width) % Width, (newHead.Y + Height) % Height);

            if(Snake.Contains(newHead))
            {
                timerSnakeMove.Stop();
                if (GameEnded != null)
                {
                    GameEnded();
                }
                return;
            }

            Snake.Insert(0, newHead);
            if (newHead == Apple)
            {
                AppleCount++;
                generateApple();
                timerSnakeMove.Interval = (int)(timerSnakeMove.Interval * 0.8);
            }
            else
            {
                Snake.Remove(Snake.Last());
            }

            if (SnakeMoved != null)
            {
                SnakeMoved();
            }
        }
    }
}
