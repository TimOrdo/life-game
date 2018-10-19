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
        public readonly int width; // Width of the world.
        public readonly int height; // Height of the world.

        public List<Colony> colonies = new List<Colony>(); // A full list of existing colonies.
        private Cell[][] grid; // World grid.
        private PictureBox pb; // PictureBox used for world rendering.

        public readonly Random random = new Random(); // Internal randomzier.

        /// <summary>
        /// Creates a world.
        /// </summary>
        /// <param name="pb">PictureBox for the world to render on to.</param>
        public World(System.Windows.Forms.PictureBox pb)
        {
            width = pb.Width; // Setting world width based on the PictureBox current width.
            height = pb.Height; // Setting world height based on the PictureBox current height.

            // Build grid.
            this.grid = new Cell[width][];
            for (int x = 0; x < width; x++)
            {
                this.grid[x] = new Cell[height];
            }

            // Initialize grid. For each place in the grid:
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Create a cell and put it there.
                    grid[x][y] = new Cell(this, x, y);
                }
            }

            pb.Image = new Bitmap(width, height); // Initializing PictureBox image to work with.
            this.pb = pb; // Saving reference to PictureBox for later usage, rendering and upates.
        }

        /// <summary>
        /// The world BigBang, performs necessary initializations and spawns a few colonies.
        /// </summary>
        public void BigBang()
        {
            // Spawn a few colonies.
            int population = 20;
            int spawnRadius = 5;

            new Colony(this, Color.Cyan, this.GetCell(random.Next(width), random.Next(height)), spawnRadius, population);
            new Colony(this, Color.Magenta, this.GetCell(random.Next(width), random.Next(height)), spawnRadius, population);
            new Colony(this, Color.Yellow, this.GetCell(random.Next(width), random.Next(height)), spawnRadius, population);
            new Colony(this, Color.Red, this.GetCell(random.Next(width), random.Next(height)), spawnRadius, population);
            new Colony(this, Color.Green, this.GetCell(random.Next(width), random.Next(height)), spawnRadius, population);
            new Colony(this, Color.Blue, this.GetCell(random.Next(width), random.Next(height)), spawnRadius, population);

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
            // Make a copy of list of colonies since colonies list can potentially change during the iteration.
            List<Colony> actors = new List<Colony>(colonies);

            // For each colony:
            foreach (Colony colony in actors)
            {
                // Let it act.
                colony.Act();
            }

            // Render tick results.
            this.RenderFrame();
        }

        /// <summary>
        /// Renders frame into the PictureBox according to the current state of the world.
        /// </summary>
        private void RenderFrame()
        {
            // Get picture bitmap.
            Bitmap bmp = (pb.Image as Bitmap);

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            // For each col in the world:
            for (int y = 0; y < height; y++)
            {
                // For each cell of the col:
                for (int x = 0; x < width; x++)
                {
                    // Get cell color.
                    var color = grid[x][y].Render();
                    rgbValues[(y * width + x) * 4 + 0] = color.B;
                    rgbValues[(y * width + x) * 4 + 1] = color.G;
                    rgbValues[(y * width + x) * 4 + 2] = color.R;
                    rgbValues[(y * width + x) * 4 + 3] = color.A;
                }
            }

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);

            // Refresh the PictureBox to show the changes.
            pb.Refresh();
        }

        /// <summary>
        /// Returns the cell in question.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public Cell GetCell(int x, int y)
        {
            if (x > 0 && y > 0 && x < width && y < height)
            {
                return grid[x][y];
            }
            return null;
        }
    }
}
