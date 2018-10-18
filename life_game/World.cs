using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace life_game
{
    public class World
    {
        private int maxX;
        private int maxY;

        private List<Colony> colonies = new List<Colony>();
        private Creature[,] creatures;
        private PictureBox pb;

        public World(System.Windows.Forms.PictureBox pb)
        {
            maxX = pb.Width;
            maxY = pb.Height;
            creatures = new Creature[maxX, maxY];
            pb.Image = new Bitmap(maxX, maxY);
            this.pb = pb;
        }

        public void Tick(object sender, EventArgs e)
        {
            (pb.Image as Bitmap).SetPixel(5, 5, Color.Black);


            var rnd = new Random();

            var x = 0;
            var y = 0;

            x = rnd.Next(640);
            y = rnd.Next(480);
            SpawnColony(Color.Aqua, x, y, 100);

            x = rnd.Next(640);
            y = rnd.Next(480);
            SpawnColony(Color.Orange, x, y, 100);

            x = rnd.Next(640);
            y = rnd.Next(480);
            SpawnColony(Color.Red, x, y, 100);

            x = rnd.Next(640);
            y = rnd.Next(480);
            SpawnColony(Color.Green, x, y, 100);

            RenderFrame();

            Console.WriteLine("Done!");
        }

        public void RenderFrame()
        {
            var color = Color.Black;
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    if (creatures[x, y] != null)
                    {
                        color = creatures[x, y].GetColor();
                    }
                    else
                    {
                        color = Color.Black;
                    }
                    (pb.Image as Bitmap).SetPixel(x, y, color);
                }
            }
            pb.Refresh();
        }

        public Colony SpawnColony(Color color, int x, int y, int creaturesCount = 10)
        {
            var colony = NewColony(color);

            for (int i = 0; i < creaturesCount; i++)
            {
                var creature = colony.NewCreature();
                if (!PutCreature(x, y, creature))
                {
                    colony.DelCreature(creature);
                }
            }


            Console.WriteLine(colony.GetCount());

            return colony;
        }

        public Colony NewColony(Color color)
        {
            var colony = new Colony(this, color);
            colonies.Add(colony);
            return colony;
        }

        public bool PutCreature(int x, int y, Creature creature)
        {
            var rnd = new Random();
            var newX = 0;
            var newY = 0;
            var retries = 0;

            for (int radius = 0; radius <= 100; radius++)
            {
                retries = radius * 8;
                if (retries == 0)
                {
                    retries = 1;
                }

                for (int i = 0; i < retries; i++)
                {

                    double angle = 2.8 * Math.PI * rnd.NextDouble(); 

                    newX = x + Convert.ToInt32(radius * Math.Cos(angle));
                    newY = y + Convert.ToInt32(radius * Math.Sin(angle));
                    
                    if (newX >= 0 && newY >= 0 && newX < maxX && newY < maxY && creatures[newX, newY] == null)
                    {
                        creatures[newX, newY] = creature;
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
