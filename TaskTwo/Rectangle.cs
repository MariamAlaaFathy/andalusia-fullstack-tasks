using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTwo
{
    internal class Rectangle : Shape, IDrawable, IResizable
    {
        private double _width;
        private double _height;
        public double Width { get { return _width; } }
        public double Height { get { return _height; } }

        public Rectangle(double width, double height)
        {
            _width = width;
            _height = height;
        }

        public override double Area()
        {
            return Width * Height;
        }

        public void Draw()
        {
            Console.WriteLine($"Drawing a {GetType().Name} {Width}x{Height}:");
            for (int i = 0; i < Width; i++) {
                Console.Write("-");
            }
            Console.WriteLine();

            for (int i = 0; i < Height; i++)
            {
                Console.Write("|");
                for(int j = 0; j < Width - 2; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("|");
                Console.WriteLine("");
            }

            for (int i = 0; i < Width; i++)
            {
                Console.Write("-");
            }
        }

        public void Scale(double factor)
        {
            Console.WriteLine($"Scaling the {GetType().Name} from {Width}x{Height} to {Width * factor}x{Height * factor}");
            _width *= factor;
            _height *= factor;
        }
    }
}
