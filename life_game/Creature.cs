using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace life_game
{

    // Represents singular Creature.
    public class Creature
    {
        private readonly Colony colony; // Creature's Colony.

        /// <summary>
        /// Creates new Creature for the Colony specified.
        /// </summary>
        /// <param name="colony"></param>
        public Creature(Colony colony)
        {
            // Save reference to the colony.
            this.colony = colony;
        }

        /// <summary>
        /// Returns current creature's color.
        /// </summary>
        /// <returns></returns>
        public Color GetColor()
        {
            return this.colony.GetColor();
        }
    }
}
