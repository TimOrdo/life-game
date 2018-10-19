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
        /// <summary>
        /// Main form of the program.
        /// </summary>
        public FMain()
        {
            InitializeComponent();

            // Create new picture box that will be used for world rendering.
            PictureBox pb = new PictureBox
            {
                Name = "PBWorld",
                Location = new Point(0, 0),
                Size = new Size(320, 240),
            };

            // Add picture box to the form.
            this.Controls.Add(pb);

            // Create new world.
            var world = new World(pb);

            // Initialize world.
            world.BigBang();

            // Create new timer.
            var timer = new System.Windows.Forms.Timer{Interval = 100};
            
            // Add world ticks handler.
            timer.Tick += new EventHandler(world.Tick);

            // Run the world.
            timer.Start();

            // End.
            Console.WriteLine("Done!");
        }
    }
}
