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
        public delegate void SnakeEvent();
        public event SnakeEvent SnakeMoved;

        private int width;
        private int height;
        private Point snake;
        private Timer timerSnakeMove = new Timer();

        public int Width { get => width; private set => width = value; }
        public int Height { get => height; private set => height = value; }
        public Point Snake { get => snake; private set => snake = value; }

        public SnakeLogic(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            timerSnakeMove.Enabled = true;
            timerSnakeMove.Interval = 500;
            timerSnakeMove.Tick += TimerSnakeMove_Tick;

            snake = new Point(width / 2, height - 1);
        }

        private void TimerSnakeMove_Tick(object sender, EventArgs e)
        {
            snake = new Point(snake.X, snake.Y - 1);
            if (SnakeMoved != null)
            {
                SnakeMoved();
            }
        }
    }
}
