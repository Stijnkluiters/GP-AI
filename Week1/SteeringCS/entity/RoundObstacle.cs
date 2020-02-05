using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.entity
{
    class RoundObstacle : ObstacleEntity
    {
        public Color VColor { get; set; }
        public RoundObstacle(Vector2D pos, World w) : base(pos, w)
        {
            this.Scale = 20;
            this.VColor = Color.LightPink;
        }

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;

            Console.WriteLine("I have been rendered");
            Pen p = new Pen(VColor, 4);
            g.DrawEllipse(p, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
        }
    }
}
