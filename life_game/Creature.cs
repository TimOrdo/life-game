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
        private Cell cell; // Reference to the cell creature is currently in.

        /// <summary>
        /// Creates new Creature for the Colony specified.
        /// </summary>
        /// <param name="colony"></param>
        public Creature(Colony colony, Cell cell)
        {
            // Save colony reference.
            this.colony = colony;

            // Also sets cell for the creature.
            this.cell = cell;

            // Save creature to the internal list of colony creatures.
            this.colony.creatures.Add(this);

            // And put creature into the cell as well.
            cell.creature = this;
        }

        /// <summary>
        /// Returns current creature's color.
        /// </summary>
        /// <returns></returns>
        public Color Render()
        {
            return this.colony.color;
        }

        public void Act()
        {
            // Initialize point of interest we will be looking at.
            Cell poi = this.colony.world.GetCell(0, 0);

            switch (this.cell.world.random.Next(8))
            {
                case 0: // N.
                    poi = this.cell.world.GetCell(this.cell.x, this.cell.y + 1);
                    break;
                case 1: // NE.
                    poi = this.cell.world.GetCell(this.cell.x + 1, this.cell.y - 1);
                    break;
                case 2: // E.
                    poi = this.cell.world.GetCell(this.cell.x + 1, this.cell.y);
                    break;
                case 3: // SE.
                    poi = this.cell.world.GetCell(this.cell.x + 1, this.cell.y + 1);
                    break;
                case 4: // S.
                    poi = this.cell.world.GetCell(this.cell.x, this.cell.y + 1);
                    break;
                case 5: // SW.
                    poi = this.cell.world.GetCell(this.cell.x - 1, this.cell.y + 1);
                    break;
                case 6: // W.
                    poi = this.cell.world.GetCell(this.cell.x - 1, this.cell.y);
                    break;
                case 7: // NW.
                    poi = this.cell.world.GetCell(this.cell.x - 1, this.cell.y - 1);
                    break;
            }

            if (poi != null)
            {
                Creature result = poi.creature;

                // If creature sees nothing in the cell it looked at - it creates a creature of the same colony.
                if (result == null)
                {
                    //this.Move(poi);
                    new Creature(this.colony, poi);
                    //poi.SetCreature(new Creature(this.colony));
                    return;
                }
            }


            // Creature was not able to do anything. It dies.
            //this.Kill();
        }

        /// <summary>
        /// Moves creature to another cell.
        /// </summary>
        /// <param name="newCell"></param>
        public void Move(Cell newCell)
        {
            // Remove creature reference from previous cell.
            this.cell.creature = null;

            // Put creature into the new cell.
            newCell.creature = this;

            // Update creature's cell reference.
            this.cell = newCell;
        }

        public void Kill()
        {
            // Remove itself from colony.
            this.colony.creatures.Remove(this);

            // Remove itself from cell
            this.cell.creature = null;
        }
    }
}
