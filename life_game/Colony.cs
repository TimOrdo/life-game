using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace life_game
{
    /// <summary>
    /// Represents a single Colony in the world.
    /// </summary>
    public class Colony
    {
        public readonly Color color; // Colony color.
        public readonly World world; // Reference to the world.

        public List<Creature> creatures = new List<Creature>(); // A list of creatues in the colony.

        private Random rnd = new Random(); // Internal randomizer.

        /// <summary>
        /// Creates new colony.
        /// </summary>
        /// <param name="world">World for the colony to exist within.</param>
        /// <param name="color">Colony color.</param>
        public Colony(World world, Color color, Cell center, int radius, int count)
        {
            this.color = color; // Colony color.
            this.world = world; // Reference to the Colony world.
            this.world.colonies.Add(this); // Add colony to the world's colonies list.

            Spawn(center, radius, count); // Spawn colony in the world.
        }


        /// <summary>
        /// Spawns a colony with the given amount of creatures in the world in the circle with the given radius and center. 
        /// Creatures that have not found the place to live in for will not be spawned.
        /// </summary>
        /// <param name="center">Spawn circle center.</param>
        /// <param name="radius">Spawn circle radius.</param>
        /// <param name="count">Creatures count.</param>
        private void Spawn(Cell center, int radius, int count)
        {
            Cell potentialCell;

            // Spawn count colony creatures.
            for (int i = 0; i < count; i++)
            {
                // Pick a random angle from the point of interest.
                double angle = 2.8 * Math.PI * rnd.NextDouble();

                // Pick random radius.
                int adjustedRadius = rnd.Next(radius);

                // Calculate x and y coordinates.
                int adjustedX = center.x + Convert.ToInt32(adjustedRadius * Math.Cos(angle));
                int adjustedY = center.y + Convert.ToInt32(adjustedRadius * Math.Sin(angle));

                potentialCell = this.world.GetCell(adjustedX, adjustedY);

                // Make sure coordinates are within a grid and the cell is empty.
                if (adjustedX >= 0 && adjustedY >= 0 && adjustedX < this.world.width && adjustedY < this.world.height && potentialCell != null && potentialCell.creature == null)
                {
                    // If grid cell is empty - create a new creature and put it there.
                    new Creature(this, this.world.GetCell(adjustedX, adjustedY));
                }
            }
        }

        /// <summary>
        /// Performs one action for the colony.
        /// </summary>
        public void Act()
        {
            // Make a copy of list of creatures since colony creatures list can potentially change during the iteration.
            List<Creature> actors = new List<Creature>(creatures);

            // For each creature:
            foreach (Creature creature in actors)
            {
                // Let it act.
                creature.Act();
            }

        }
    }
}
