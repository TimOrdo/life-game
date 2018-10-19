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
    /// <summary>
    /// Represents world, it's current state and interface to interact with it.
    /// </summary>
    public class World
    {
        private readonly int maxX; // Width of the world.
        private readonly int maxY; // Height of the world.

        private List<Colony> colonies = new List<Colony>(); // A full list of existing colonies.
        private Creature[,] grid; // World grid.
        private PictureBox pb; // PictureBox used for world rendering.

        private Random rnd = new Random(); // Internal randomzier.

        /// <summary>
        /// Creates a world.
        /// </summary>
        /// <param name="pb">PictureBox for the world to render on to.</param>
        public World(System.Windows.Forms.PictureBox pb)
        {
            maxX = pb.Width; // Setting world width based on the PictureBox current width.
            maxY = pb.Height; // Setting world height based on the PictureBox current height.
            grid = new Creature[maxX, maxY]; // Initializing creatures list.
            pb.Image = new Bitmap(maxX, maxY); // Initializing PictureBox image to work with.
            this.pb = pb; // Saving reference to PictureBox for later usage, rendering and upates.
        }

        /// <summary>
        /// The world BigBang, performs necessary initializations and spawns a few colonies.
        /// </summary>
        public void BigBang()
        {
            // Spawn a few colonies.
            SpawnColony(Color.Cyan, rnd.Next(maxX), rnd.Next(maxY), 100);
            SpawnColony(Color.Magenta, rnd.Next(maxX), rnd.Next(maxY), 100);
            SpawnColony(Color.Yellow, rnd.Next(maxX), rnd.Next(maxY), 100);
            SpawnColony(Color.Red, rnd.Next(maxX), rnd.Next(maxY), 100);
            SpawnColony(Color.Green, rnd.Next(maxX), rnd.Next(maxY), 100);
            SpawnColony(Color.Blue, rnd.Next(maxX), rnd.Next(maxY), 100);

            // Render resulting frame.
            RenderFrame();
        }

        /// <summary>
        /// Performs one world tick and updates the PictureBox with the new frame.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Tick(object sender, EventArgs e)
        {
            this.RenderFrame();
        }

        /// <summary>
        /// Renders frame into the PictureBox according to the current state of the world.
        /// </summary>
        private void RenderFrame()
        {
            // Initialize color variable for later usage.
            Color color;

            // Now for each col of the world:
            for (int x = 0; x < maxX; x++)
            {
                // For each cel of the col:
                for (int y = 0; y < maxY; y++)
                {
                    // If there is a creature in the cell:
                    if (grid[x, y] != null)
                    {
                        // Render creature.
                        color = grid[x, y].GetColor();
                    }
                    else
                    {
                        // Otherwise render the cell black.
                        color = Color.Black;
                    }

                    // Set the pixel color.
                    (pb.Image as Bitmap).SetPixel(x, y, color);
                }
            }

            // Refresh the PictureBox to show the changes.
            pb.Refresh();
        }

        /// <summary>
        /// Spawns a colony in the world. Depending on free space available around coordinates specified as well as Creatures count - spawn location may be slightly adjusted.
        /// </summary>
        /// <param name="color">Colony color.</param>
        /// <param name="x">Desired x coordinate.</param>
        /// <param name="y">Desired y coordinate.</param>
        /// <param name="creaturesCount">Amount of creatures in the Colony to spawn.</param>
        /// <returns>Newly spawned Colony.</returns>
        private Colony SpawnColony(Color color, int x, int y, int creaturesCount = 10)
        {
            // Create new colony.
            var colony = NewColony(color);

            // Spawn creatures:
            for (int i = 0; i < creaturesCount; i++)
            {
                // Create new creature.
                var creature = colony.NewCreature();
                // Try to put creature into the world:
                if (!PutCreature(x, y, creature))
                {
                    // If we were not able to find a place for creature in the world - delete it.
                    colony.DelCreature(creature);
                }
            }

            // Return newly spawned colony.
            return colony;
        }

        /// <summary>
        /// Creates new Colony and saves it into the world Colonies list.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private Colony NewColony(Color color)
        {
            // Create new Colony.
            var colony = new Colony(this, color);

            // Add Colony to the list of world Colonies.
            colonies.Add(colony);
            
            // Return newly created Colony.
            return colony;
        }

        /// <summary>
        /// Tries to put Creature to the world coordinates specified.
        /// </summary>
        /// <param name="x">Desired x coordinate.</param>
        /// <param name="y">Desired y coordinate.</param>
        /// <param name="creature">A creature to put into the world.</param>
        /// <returns>True if placement was successfull, false otherwise.</returns>
        private bool PutCreature(int x, int y, Creature creature)
        {
            // Initialize vars to contain adjusted placement coordinates.
            int adjustedX;
            int adjustedY;

            // Retries nubmer tracker.
            int tries;

            // Maximum placement radius to search.
            int maxRadius = 100;

            // For each possible radius from 0 to maxRadius:
            for (int radius = 0; radius <= maxRadius; radius++)
            {
                // Specify number of retries.
                tries = radius * 8;

                // Special case, if radius is 0 - we make 1 try.
                if (tries == 0)
                {
                    tries = 1;
                }

                // Now perform amount of tries:
                for (int i = 0; i < tries; i++)
                {
                    // Pick a random angle from the point of interest.
                    double angle = 2.8 * Math.PI * rnd.NextDouble();

                    // Calculate x and y coordinates.
                    adjustedX = x + Convert.ToInt32(radius * Math.Cos(angle));
                    adjustedY = y + Convert.ToInt32(radius * Math.Sin(angle));

                    // Make sure coordinates are within a grid and grid cell is empty.
                    if (adjustedX >= 0 && adjustedY >= 0 && adjustedX < maxX && adjustedY < maxY && grid[adjustedX, adjustedY] == null)
                    {
                        // If grid cell is empty - place a creature there.
                        grid[adjustedX, adjustedY] = creature;

                        // Return success.
                        return true;
                    }
                }
            }

            // We were not able to find a place for a creature. Return failure.
            return false;
        }
    }
}
