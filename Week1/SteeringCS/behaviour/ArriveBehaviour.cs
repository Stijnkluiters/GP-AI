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
            this.deceleration = Deceleration.slow;
        }

        public override Vector2D Calculate()
        {
            Vector2D ToTarget = this.target.Pos.Clone().Sub(this.ME.Pos);

            double distanceToTarget = ToTarget.Length();

            if(distanceToTarget > 0)
            {
                // because Deceleration is enumarated as an int, this value is required to provide fine tweaking of the deceleration
                const double DecelerationTweaker = 0.3;

                // calculate the speed required to reach the target given the desired
                double speed = distanceToTarget / ((double)deceleration * DecelerationTweaker);

                // make sure the velocity does not exceed the maxspeed
                speed = Math.Min(speed, this.ME.MaxSpeed);

                Console.WriteLine("Current speed {0}", speed);
                // from here proceed just like Seek except we don't need to normalize the ToTarget vector.
                // Because we have already gone to the trouble of calculating its length: distanceToTarget
                ToTarget.Divide(distanceToTarget);
                ToTarget.Multiply(speed);
                
                Vector2D DesiredVelocity = ToTarget.Clone();

                return DesiredVelocity.Sub(this.ME.Pos);
            }

            return new Vector2D(0, 0);
        }
    }
}
