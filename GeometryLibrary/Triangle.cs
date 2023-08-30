namespace GeometryLibrary;

public class Triangle : IShape
{
    private decimal SideA { get; }
    private decimal SideB { get; }
    private decimal SideC { get; }

    public Triangle(decimal sideA, decimal sideB, decimal sideC)
    {
        SideA = sideA;
        SideB = sideB;
        SideC = sideC;
        
        if (!Validate())
        {
            throw new ArgumentException("Треугольник не существует");
        }
    }

    private bool Validate()
    {
        return SideA < (SideB + SideC) && SideB < (SideC + SideA) && SideC < (SideA + SideB);
    }

    public double CalculateArea()
    {
        var s = (SideA + SideB + SideC) / 2;
        var sqr = (double)(s * (s - SideA) * (s - SideB) * (s - SideC));
        var area = Math.Sqrt(sqr);
        if (double.IsNaN(area))
        {

        }
        return area;
    }

    public bool IsRightAngle()
    {
        decimal[] sides = { SideA, SideB, SideC };
        Array.Sort(sides);

        return Math.Abs((double)((sides[0] * sides[0]) + (sides[1] * sides[1]) - (sides[2] * sides[2]))) < double.Epsilon;
    }
}