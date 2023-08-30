namespace GeometryLibrary;

public class Circle : IShape
{
    private readonly decimal Radius;

    public Circle(decimal radius)
    {
        Radius = radius;
    }

    public double CalculateArea()
    {
        if (Radius < 0)
            return Double.NaN;

        return (double)((decimal)Math.PI * (Radius * Radius));
    }
}