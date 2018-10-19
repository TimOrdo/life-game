using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace life_game
{
    public partial class FMain : Form
    {

        public FMain()
        {
            InitializeComponent();

            PictureBox pb = new PictureBox
            {
                Name = "PBWorld",
                Location = new Point(0, 0),
                Size = new Size(640, 480),
            };

            this.Controls.Add(pb);

            var world = new World(pb);

            world.BigBang();

            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(world.Tick);
            timer.Start();
        }
    }
}
