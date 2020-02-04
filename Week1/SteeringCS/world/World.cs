using SteeringCS.behaviour;
using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS
{
    class World
    {
        private List<MovingEntity> entities = new List<MovingEntity>();
        public Vehicle Target { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int UpdateBehaviourCounter = 0;

        public World(int w, int h)
        {
            Width = w;
            Height = h;
            populate();
        }

        private void populate()
        {
            Vehicle v = new Vehicle(new Vector2D(10,10), this);
            v.VColor = Color.Blue;
            entities.Add(v);

            Target = new Vehicle(new Vector2D(100, 60), this);
            Target.VColor = Color.DarkRed;
            Target.Pos = new Vector2D(100, 40);
        }

        public void Update(float timeElapsed)
        {
            if (UpdateBehaviourCounter > 200)
            {
                UpdateBehaviourCounter = 0;
            }
            Console.WriteLine(UpdateBehaviourCounter);
            foreach (MovingEntity me in entities)
            {
                if(UpdateBehaviourCounter > 180) { 
                    me.SB = new FleeBehaviour(me); // restore later
                } else
                {
                    me.SB = new SeekBehaviour(me);
                }
                me.Update(timeElapsed);
            }
            UpdateBehaviourCounter++;
        }

        public void Render(Graphics g)
        {
            entities.ForEach(e => e.Render(g));
            Target.Render(g);
        }
    }
}
