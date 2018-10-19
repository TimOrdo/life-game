using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace life_game
{
    /// <summary>
    /// Represents a world cell.
    /// </summary>
    public class Cell
    {
        public readonly World world; // Cell's world.

        public readonly int x; // Cell x coordinate.
        public readonly int y; // Cell y coordinate.

        public Creature creature; // Creature in the cell if any.

        /// <summary>
        /// Instantiates world cell.
        /// </summary>
        /// <param name="world">Cell world.</param>
        /// <param name="x">Cell x coordinate.</param>
        /// <param name="y">Cell y cooradinate.</param>
        public Cell(World world, int x, int y)
        {
            if (x < 0)
            {
                throw new Exception("X can not be negative.");
            }
            if (x >= world.width)
            {
                throw new Exception("X can not be bigger then world width.");
            }
            if (y < 0)
            {
                throw new Exception("Y can not be negative.");
            }
            if (y >= world.height)
            {
                throw new Exception("Y can not be bigger then world height.");
            }
            this.x = x;
            this.y = y;
            this.world = world;
        }

        /// <summary>
        /// Renders cell color based on it's contents.
        /// </summary>
        /// <returns>Returns color of the cell.</returns>
        public Color Render()
        {
            if (this.creature != null)
            {
                return this.creature.Render();
            }
            return Color.Black;
        }

    }
}
