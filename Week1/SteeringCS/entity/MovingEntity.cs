using SteeringCS.behaviour;
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
            MaxSpeed = 50;
            Velocity = new Vector2D();
        }

        public override void Update(float timeElapsed)
        {
            // First step calculate steering force 
            Vector2D steeringForce = this.SB.Calculate();

            // If the ObstacleAvoidance returns something that is not null. overrule the steeringforce.
            this.SB = new ObstacleAvoidance(this);
            Vector2D _avoidance = this.SB.Calculate();
            if(_avoidance != null)
            {
                steeringForce = _avoidance;
            }

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
