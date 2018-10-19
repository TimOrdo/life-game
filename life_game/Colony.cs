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
        private readonly Color color; // Colony color.
        private Random rnd = new Random(); // Internal randomizer.
        private List<Creature> creatures = new List<Creature>(); // A list of creatues in the colony.
        private readonly World world; // Reference to the world.

        /// <summary>
        /// Creates new Colony with random color.
        /// </summary>
        /// <param name="world">World for the Colony to exist within.</param>
        public Colony(World world)
        {
            this.color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)); // Pick a random color if color not specified.
            this.world = world; // Reference to the Colony world.
        }

        /// <summary>
        /// Creates new Colony with the color specified.
        /// </summary>
        /// <param name="world">World for the colony to exist within.</param>
        /// <param name="color">Colony color.</param>
        public Colony(World world, Color color)
        {
            this.color = color; // Colony color.
            this.world = world; // Reference to the Colony world.
        }

        /// <summary>
        /// Returns current Colony color.
        /// </summary>
        /// <returns>Colony color.</returns>
        public Color GetColor()
        {
            return this.color;
        }

        /// <summary>
        /// Creates new Creature that belongs to the colony.
        /// </summary>
        /// <returns>Newly created Creature.</returns>
        public Creature NewCreature()
        {
            // Create new Creature.
            var creature = new Creature(this);

            // Save Creature to the internal list of creatures.
            this.creatures.Add(creature);

            // Return newly created Creature.
            return creature;
        }

        /// <summary>
        /// Ceases creature's existance.
        /// </summary>
        /// <param name="creature"></param>
        public void DelCreature(Creature creature)
        {
            creatures.Remove(creature);
        }

        // Returns the count of the Colony Creatures.
        public int GetCount()
        {
            return creatures.Count();
        }
    }
}
