using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class FleeBehaviour : SteeringBehaviour
    {
        private Vehicle target;
        public FleeBehaviour(MovingEntity movingEntity): base(movingEntity) {
            this.target = this.ME.MyWorld.Target;
        }
        
        // Flee Behaviour, opposite of seek
        // Page: 92
        public override Vector2D Calculate()
        {
            Vector2D DesiredVelocity = this.ME.Pos.Clone().Sub(this.target.Pos).Normalize();
            DesiredVelocity.Multiply(this.ME.MaxSpeed);

            return DesiredVelocity.Sub(this.ME.Velocity);
        }
    }
}
