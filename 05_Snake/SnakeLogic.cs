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

        private int width;
        private int height;
        private List<Point> snake;
        private SnakeDirection direction;

        private Timer timerSnakeMove = new Timer();

        public int Width { get => width; private set => width = value; }
        public int Height { get => height; private set => height = value; }
        public List<Point> Snake { get => snake; private set => snake = value; }
        internal SnakeDirection Direction
        {
            private get => direction;
            set
            {
                direction = value;
                TimerSnakeMove_Tick(null, null);
            }
        }

        public SnakeLogic(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            timerSnakeMove.Enabled = true;
            timerSnakeMove.Interval = 500;
            timerSnakeMove.Tick += TimerSnakeMove_Tick;


            Snake = new List<Point>();
            Snake.Add(new Point(width / 2, height - 1));
            Snake.Add(new Point(width / 2, height + 0));
            Snake.Add(new Point(width / 2, height + 1));
            Snake.Add(new Point(width / 2, height + 2));

            Direction = SnakeDirection.Up;
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

            Snake.Insert(0, newHead);
            Snake.Remove(Snake.Last());

            if (SnakeMoved != null)
            {
                SnakeMoved();
            }
        }
    }
}
