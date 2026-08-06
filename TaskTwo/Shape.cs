using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTwo
{
    internal abstract class Shape
    {
        abstract public double Area();

        public void Describe()
        {
            Console.WriteLine($"This is a {GetType().Name} with an area of {Area()}.");
        }
    }
}
