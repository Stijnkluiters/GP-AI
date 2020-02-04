using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.entity
{

    abstract class MovingEntity : BaseGameEntity
    {
        public Vector2D Velocity { get; set; }
        public float Mass { get; set; }
        public float MaxSpeed { get; set; }

        public SteeringBehaviour SB { get; set; }

        public MovingEntity(Vector2D pos, World w) : base(pos, w)
        {
            Mass = 30;
            MaxSpeed = 150;
            Velocity = new Vector2D();
        }

        public override void Update(float timeElapsed)
        {
            // to do
            // First step calculate steering force 
            Vector2D steeringForce = this.SB.Calculate();
            // Second step divide by the mass of the moving entity
            Vector2D Acceleration = steeringForce.Divide(this.Mass);
            // add the acceleration to the velocity (multiply by timeElapsed)
            this.Velocity.Add(Acceleration.Multiply(timeElapsed));
            // truncate the velocity back to regular speeds
            this.Velocity.truncate(this.MaxSpeed);
            // update the current position;
            this.Pos.Add(this.Velocity);

            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            return String.Format("{0}", Velocity);
        }
    }
}
