﻿using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS
{
   
    public class Vector2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2D() : this(0,0)
        {
        }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        /**
         * Just for clarivication
         */
        public double Speed()
        {
            return this.Length();
        }

        public double Length()
        {
            return Math.Sqrt(this.LengthSquared());
        }

        public double LengthSquared()
        {
            return (Math.Pow(this.X, 2) + Math.Pow(this.Y, 2));
        }   

        public Vector2D Add(Vector2D v)
        {
            this.X += v.X;
            this.Y += v.Y;
            return this;
        }

        public Vector2D Sub(Vector2D v)
        {
            this.X -= v.X;
            this.Y -= v.Y;
            return this;
        }

        public Vector2D Multiply(double value)
        {
            this.X *= value;
            this.Y *= value;
            return this;
        }

        public Vector2D Divide(double value)
        {
            this.X /= value;
            this.Y /= value;
            return this;
        }

        public Vector2D Normalize()
        {
            this.X = this.X / this.Length();
            this.Y = this.Y / this.Length();
            return this;
        }

        public Vector2D truncate(double maX)
        {
            if (Length() > maX)
            {
                Normalize();
                Multiply(maX);
            }
            return this;
        }
        /**
         * Returns the distance from current position to the given target
         */
        public double Distance(Vector2D target)
        {
            Vector2D ToTarget = target.Clone().Sub(this);

            return ToTarget.Length();
        }
        
        public Vector2D Clone()
        {
            return new Vector2D(this.X, this.Y);
        }
        
        public override string ToString()
        {
            return String.Format("({0},{1})", X, Y);
        }
    }


}
