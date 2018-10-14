using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace life_game
{

    public class Creature
    {
        private Colony colony;

        public Creature(Colony colony)
        {
            this.colony = colony;
        }

        public Color GetColor()
        {
            return this.colony.GetColor();
        }
    }
}
