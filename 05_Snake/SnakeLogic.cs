using System;
using System.Collections.Generic;
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

        Timer timer = new Timer();

        public SnakeLogic()
        {
            timer.Enabled = true;
            timer.Interval = 200;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (SnakeMoved != null)
            {
                SnakeMoved();
            }
        }
    }
}
