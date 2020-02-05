using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.entity
{
    class ObstacleEntity : BaseGameEntity
    {
        public ObstacleEntity(Vector2D pos, World w) : base(pos, w)
        {
            this.Scale = 3;
        }

        public override void Update(float delta)
        {
            throw new NotImplementedException();
        }
    }
}
