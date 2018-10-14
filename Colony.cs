using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace life_game
{
    public class Colony
    {
        private Color color;
        private Random rnd = new Random();
        private List<Creature> creatures = new List<Creature>();
        private World world;

        public Colony(World world)
        {
            this.color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            this.world = world;
        }

        public Colony(World world, Color color)
        {
            this.color = color;
            this.world = world;
        }

        public Color GetColor()
        {
            return this.color;
        }

        public Creature NewCreature()
        {
            var creature = new Creature(this);
            this.creatures.Add(creature);
            return creature;
        }

        public void DelCreature(Creature creature)
        {
            creatures.Remove(creature);
        }

        public int GetCount()
        {
            return creatures.Count();
        }
    }
}
