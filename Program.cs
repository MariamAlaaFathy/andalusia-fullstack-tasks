using TaskTwo;

void DescribeAndDrawShapes(Shape[] shapes)
{
    foreach (var shape in shapes)
    {
        shape.Describe();
        (shape as IDrawable)?.Draw();
        Console.WriteLine();
    }
}

void ScaleShapes(IEnumerable<IResizable> shapes, double factor)
{
    foreach (var shape in shapes)
    {
        shape.Scale(factor);
    }
}

Shape[] shapes = new Shape[]
{
    new Circle(5),
    new Rectangle(10, 5),
    new Circle(3),
    new Rectangle(4, 2),
    new Triangle(7, 4)
};

DescribeAndDrawShapes(shapes);

Console.WriteLine("Scaling all shapes by a factor of 2:");
ScaleShapes(shapes.OfType<IResizable>(), 2);

DescribeAndDrawShapes(shapes);