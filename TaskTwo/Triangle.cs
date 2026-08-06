using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTwo
{
    internal class Triangle : Shape, IDrawable, IResizable
    {
        private double _width;
        private double _height;
        public double Width { get { return _width; } }
        public double Height { get { return _height; } }

        public Triangle(double width, double height)
        {
            _width = width;
            _height = height;
        }

        public override double Area()
        {
            return 0.5 * Width * Height;
        }

        public void Draw()
        {
            Console.WriteLine($"Drawing a {GetType().Name} with a width of {Width} and a height of {Height}:");
            int w = (int)Width;
            int h = (int)Height;
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w - i - 1; j++)
                {
                    Console.Write(" ");
                }
                for (int j = 0; j < 2 * i + 1; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }

        public void Scale(double factor)
        {
            Console.WriteLine($"Scaling the {GetType().Name} from a width of {Width} and a height of {Height} to a width of {Width * factor} and a height of {Height * factor}");
            _width *= factor;
            _height *= factor;
        }
    }
}
