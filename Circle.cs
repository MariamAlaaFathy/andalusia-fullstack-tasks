using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTwo
{
    internal class Circle : Shape, IDrawable, IResizable
    {
        private double _radius;
        public double Radius { get { return _radius; } }

        public Circle(double radius)
        {
            _radius = radius;
        }

        public override double Area()
        {
            return Math.PI * Radius * Radius;
        }

        public void Draw()
        {
            Console.WriteLine($"Drawing a {GetType().Name} with a radius of {Radius}:");
            int r = (int)Radius;
            for (int y = -r; y <= r; y++)
            {
                for (int x = -r; x <= r; x++)
                {
                    double distance = Math.Sqrt(x * x + y * y);

                    if (Math.Abs(distance - r) < 0.5)
                        Console.Write("*");
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        public void Scale(double factor)
        {
            Console.WriteLine($"Scaling the {GetType().Name} from radius {Radius} to radius {Radius * factor}");
            _radius *= factor;
        }
    }
}
