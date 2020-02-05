using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class ArriveBehaviour : SteeringBehaviour
    {
        public enum Deceleration
        {
            slow = 3,
            normal = 2,
            fast = 1
        }

        public Deceleration deceleration;

        private Vehicle target;
        public ArriveBehaviour(MovingEntity movingEntity) : base(movingEntity)
        {
            this.target = this.ME.MyWorld.Target;
            this.deceleration = Deceleration.fast;
        }

        public override Vector2D Calculate()
        {
            Vector2D ToTarget = this.target.Pos.Clone().Sub(this.ME.Pos.Clone());
            Console.WriteLine(ToTarget);
            double distanceToTarget = ToTarget.Length();

            if(distanceToTarget > 0)
            {
                // because Deceleration is enumarated as an int, this value is required to provide fine tweaking of the deceleration
                const double DecelerationTweaker = 0.3;

                // calculate the speed required to reach the target given the desired
                double speed = distanceToTarget / ((double)deceleration * DecelerationTweaker);

                // make sure the velocity does not exceed the maxspeed
                speed = Math.Min(speed, this.ME.MaxSpeed);

                // from here proceed just like Seek except we don't need to normalize the ToTarget vector.
                // Because we have already gone to the trouble of calculating its length: distanceToTarget
                ToTarget.Multiply(speed);
                ToTarget.Divide(distanceToTarget);
                
                
                Vector2D DesiredVelocity = ToTarget;

                return DesiredVelocity.Sub(this.ME.Velocity);
            }

            return new Vector2D(0, 0);
        }
    }
}
