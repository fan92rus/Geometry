using FsCheck;
using GeometryLibrary;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        private static readonly Gen<decimal> DecimalGen = Arb.Default.Decimal().Generator;
        private static readonly Gen<decimal> PositiveDecimalGen = DecimalGen.Where(x => x > 0);
        private static readonly Gen<decimal> NegativeDecimalGen = PositiveDecimalGen.Select(x => -x);

        [Test]
        public void TestCircleProperties()
        {
            var positiveRadiusCircleGenerator = from radius in PositiveDecimalGen
                                                select new Circle(radius);

            var negativeRadiusCircleGenerator = from radius in NegativeDecimalGen
                                                select new Circle(radius);

            Prop.ForAll(Arb.From(positiveRadiusCircleGenerator),
                        circle => circle.CalculateArea() > 0)
                .Label("Круг с положительным радиусом должен иметь положительную площадь")
                .VerboseCheckThrowOnFailure();

            Prop.ForAll(Arb.From(negativeRadiusCircleGenerator),
                circle => double.IsNaN(circle.CalculateArea()))
                .Label("Круг с отрицательным радиусом не должен иметь площадь")
                .VerboseCheckThrowOnFailure();

            Assert.That(new Circle(0).CalculateArea(), Is.EqualTo(0), () => "Круг с 0 радиусом должен иметь 0 площадь");
        }

        [Test]
        public void TestTriangleProperties()
        {
            var positiveAreaTriangleGenerator =
                from a in DecimalGen
                from b in DecimalGen
                from c in DecimalGen
                where Validate(a, b, c)
                select new Triangle(a, b, c);


            bool Validate(decimal sideA, decimal sideB, decimal sideC)
            {
                var isValid = sideA < (sideB + sideC) && sideB < (sideC + sideA) && sideC < (sideA + sideB);
                return isValid;
            }

            // Тругольник с 0 строной не может существовать

            Assert.Throws<ArgumentException>(() => new Triangle(0, 2, 2));
            Assert.Throws<ArgumentException>(() => new Triangle(3, 1, 0));
            Assert.Throws<ArgumentException>(() => new Triangle(3, 0, 1));

            Prop.ForAll(Arb.From(positiveAreaTriangleGenerator),
                    circle => circle.CalculateArea() > 0)
                    .Label("Валидный теугольник должен иметь положительную плозадь")
                .VerboseCheckThrowOnFailure();
        }
    }
}