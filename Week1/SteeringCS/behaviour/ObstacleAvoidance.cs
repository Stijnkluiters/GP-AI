using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class ObstacleAvoidance : SteeringBehaviour
    {
        private List<ObstacleEntity> Obstacles;

        private double minDectionBoxLength = 10;

        public ObstacleAvoidance(MovingEntity movingEntity) : base(movingEntity)
        {
            this.Obstacles = this.ME.MyWorld.obstacles;
        }
        public override Vector2D Calculate()
        {
            double dBoxLength = this.minDectionBoxLength + (this.ME.Pos.Speed() / this.ME.MaxSpeed) * this.minDectionBoxLength;

            List<ObstacleEntity> inRangeObstacles = this.getObstaclesInRange();

            ObstacleEntity ClosestIntersectionObstacle = null;
            Vector2D LocalPosOfClosestObstacle = null;
            Vector2D localPos = new Vector2D();

            double DistToClosestIP = Double.MaxValue;

            foreach (ObstacleEntity Obstacle in this.Obstacles)
            {

                // Get the distance, and angle between your own vehicle and obstacle;
                localPos = this.ME.Pos.Clone().Sub(Obstacle.Pos);

                // if the position is behind, we can ignore the value.
                if (localPos.X >= 0)
                {
                    // if the distance from the x axis to the object's position is less
                    // than its radius + half of the width of the detection box.
                    // then there is a potential intersection
                    double ExpendedRadius = (Obstacle.Scale * 2) + this.ME.Scale * 2;

                    if (Math.Abs(localPos.Y) < ExpendedRadius)
                    {
                        // Now to do a line/circle intersection test. the center of the circle is represented by (cX, cY).
                        // The intersection points are given by the formula x = cX +/-sqrt(r^2-cY^2) for y=0.
                        // We only need to look at the smallest positive value of x because
                        // That will be the closest point of intersection.
                        double cX = localPos.X;
                        double cY = localPos.Y;

                        // We only need to calculate the sqrt part of the above equation once.
                        double SqrtPart = Math.Sqrt(ExpendedRadius * ExpendedRadius - cY * cY);

                        double ip = cX - SqrtPart;

                        if (ip <= 0)
                        {
                            ip = cX + SqrtPart;
                        }

                        // test to see if this is the closest so far. if it is, keep a
                        // record of the obstacle and it local coordinates.
                        if (ip < DistToClosestIP)
                        {
                            DistToClosestIP = ip;

                            ClosestIntersectionObstacle = Obstacle;

                            LocalPosOfClosestObstacle = localPos;
                        }
                    }
                }
            }

            // if we have found an intersecting obstacle, calculate a steering force away from it
            Vector2D SteeringForce = null;

            if (ClosestIntersectionObstacle != null)
            {
                SteeringForce = new Vector2D();
                // The closer the agent is to an object, the stronger the steering force should be.
                double Multiplier = 1.0 + (dBoxLength - LocalPosOfClosestObstacle.X) / dBoxLength;

                // calculate the lateral force
                SteeringForce.Y = (ClosestIntersectionObstacle.Scale * 2 - LocalPosOfClosestObstacle.Y) * Multiplier;

                // apply a braking force proportional to the obstacle's distance from the vehicle.
                const double BrakingWeight = 0.2;

                SteeringForce.X = (ClosestIntersectionObstacle.Scale * 2) - LocalPosOfClosestObstacle.X * BrakingWeight;
            }

            // Finally, convert the steering vector from local to world space.
            return SteeringForce;
        }

        public List<ObstacleEntity> getObstaclesInRange()
        {
            List<ObstacleEntity> inRangeObstacles = new List<ObstacleEntity>();

            return inRangeObstacles;
        }

    }
}
