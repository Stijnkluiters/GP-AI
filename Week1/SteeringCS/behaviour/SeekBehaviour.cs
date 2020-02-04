using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class SeekBehaviour : SteeringBehaviour
    {
        private Vehicle target;
        public SeekBehaviour(MovingEntity movingEntity): base(movingEntity) {
            this.target = this.ME.MyWorld.Target;
        }
        
        // to do
        public override Vector2D Calculate()
        {
            Vector2D DesiredVelocity = target.Pos.Clone().Sub(this.ME.Pos).Normalize();
            DesiredVelocity.Multiply(this.ME.MaxSpeed);

            return DesiredVelocity.Sub(this.ME.Velocity);
        }
    }
}
