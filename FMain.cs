using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace life_game
{
    public partial class FMain : Form
    {
        public FMain()
        {
            InitializeComponent();
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            var world = new World(PBWorld);

            var rnd = new Random();

            var x = 0;
            var y = 0;

            x = rnd.Next(640);
            y = rnd.Next(480);
            world.SpawnColony(Color.Aqua, x, y, 1000);

            x = rnd.Next(640);
            y = rnd.Next(480);
            world.SpawnColony(Color.Orange, x, y, 1000);

            x = rnd.Next(640);
            y = rnd.Next(480);
            world.SpawnColony(Color.Red, x, y, 1000);

            x = rnd.Next(640);
            y = rnd.Next(480);
            world.SpawnColony(Color.Green, x, y, 1000);

            world.RenderFrame();

            Console.WriteLine("Done!");
        }
    }
}
