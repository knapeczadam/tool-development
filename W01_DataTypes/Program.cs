namespace W01_DataTypes
{
    internal class Program
    {
        private struct ValuePoint(int x, int y)
        {
            public int X { get; set; }
            public int Y { get; set; }

            public override string ToString() => $"({X}, {Y})";
        }

        private class ReferencePoint
        {
            public int X { get; set; }
            public int Y { get; set; }

            public override string ToString() => $"({X},{Y})";
        }

        private struct Point<T>(T x, T y)
        {
            public T X { get; set; } = x;
            public T Y { get; set; } = y;

            public override string ToString() => $"({X}, {Y})";
        }

        private static void Main(string[] args)
        {
            ValuePoint p1 = new ValuePoint { X = 1, Y = 1};
            ValuePoint p2 = new ValuePoint { X = 2, Y = 2 };
            Console.WriteLine("\nValue Type Test:");
            Console.WriteLine($"Before: point1 = {p1}, point2 = {p2}");
            TrySwap(p1, p2);
            // RefSwap(ref p1, ref p2);
            Console.WriteLine($"After TrySwap: point1 = {p1}, point2 = {p2}");
            
            ReferencePoint p3 = new ReferencePoint { X = 3, Y = 3 };
            ReferencePoint p4 = new ReferencePoint { X = 4, Y = 4 };
            Console.WriteLine("\nReference Type Test:");
            Console.WriteLine($"Before: point3 = {p3}, point4 = {p4}");
            Swap(p3, p4);
            Console.WriteLine($"After Swap: point3 = {p3}, point4 = {p4}");
            
            Console.WriteLine("\nRef Parameter Test");
            int x = 5;
            int y = 10;
            Console.WriteLine($"Before: x = {x}, y = {y}");
            RefSwap(ref x, ref y);
            Console.WriteLine($"After RefSwap: x = {x}, y = {y}");
            
            Console.WriteLine("\nCalculate Test:");
            int a = 5;
            int b = 3;
            int sum = 0 ;
            Calculate(a, b, ref sum, out int product);
            Console.WriteLine($"Numbers: {a} and {b}");
            Console.WriteLine($"Sum: {sum}");
            Console.WriteLine($"Product: {product}");
            
            var points = new List<Point<int>>
            {
                new Point<int>(1, 5),
                new Point<int>(-3, 0),
                new Point<int>(5, 1),
                new Point<int>(2, 9),
            };
            
            GetMinMax(points, out Point<int> min, out Point<int> max);
            Console.WriteLine($"Min is: {min}, max is: {max}");
            
            Benchmark();
        }

        private static void TrySwap(ValuePoint p1, ValuePoint p2)
        {
            (p1, p2) = (p2, p1);
        }

        private static void Swap(ReferencePoint p1, ReferencePoint p2)
        {
            int tempX = p1.X;
            int tempY = p1.Y;
            p1.X = p2.X;
            p1.Y = p2.Y;
            p2.X = tempX;
            p2.Y = tempY;
        }

        private static void RefSwap(ref int x, ref int y)
        {
            (x, y) = (y, x);
        }

        private static void RefSwap(ref ValuePoint p1, ref ValuePoint p2)
        {
            (p1, p2) = (p2, p1);
        }

        private static void Calculate(int a, int b, ref int sum, out int product)
        {
            sum = a + b;
            product = a * b;
        }

        private static void GetMinMax<T>(IEnumerable<Point<T>> points, out Point<T> min, out Point<T> max) where T : IComparable<T>
        {
            if (!points.Any())
            {
                throw new ArgumentException("Point collection is empty");
            }

            var first = points.First();
            T minX = first.X, maxX = first.X;
            T minY = first.Y, maxY = first.Y;

            foreach (var p in points)
            {
                if (p.X.CompareTo(minX) < 0) minX = p.X;
                if (p.X.CompareTo(maxX) > 0) maxX = p.X;
                if (p.Y.CompareTo(minY) < 0) minY = p.Y;
                if (p.Y.CompareTo(maxY) > 0) maxY = p.Y;
            }

            min = new Point<T>(minX, minY);
            max = new Point<T>(maxX, maxY);
        }

        private static void Benchmark()
        {
            const int count = 1_000_000;
            // Warm-up
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            GC.Collect();
            long beforeStruct = GC.GetTotalMemory(true);
            var structList = new ValuePoint[count];
            for (int i = 0; i < count; ++i)
                structList[i] = new ValuePoint { X = i, Y = i };
            long afterStruct = GC.GetTotalMemory(true);
            Console.WriteLine($"ValuePoint memory: {afterStruct - beforeStruct:N0} bytes");
            
            GC.Collect();
            long beforeClass = GC.GetTotalMemory(true);
            var classList = new ReferencePoint[count];
            for (int i = 0; i < count; ++i)
                classList[i] = new ReferencePoint { X = i, Y = i };
            long afterClass = GC.GetTotalMemory(true);
            Console.WriteLine($"ReferencePoint memory: {afterClass - beforeClass:N0} bytes");
        }
    }
}
