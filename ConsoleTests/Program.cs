using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ExpressionTree.Part1;
using ExpressionTree.Part2;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Expression<Func<int, int, int>> root = (x, y) => (x - (x + 1)) / (y - 1) - 1;

            int xConst = 1;
            int yConst = 2;

            var whoala = root.Convert<Func<int, int, int>, Func<int>, int>(new Dictionary<string, int> { { "x", xConst }, { "y", yConst } });

            Console.WriteLine(root.ToString());
            var old = root.Compile();

            Console.WriteLine($"x = {xConst}, y = {yConst}");
            Console.WriteLine($"result = {old(xConst, yConst)}");

            Console.WriteLine();

            Console.WriteLine(whoala.ToString());
            var @new = whoala.Compile();

            Console.WriteLine($"x = {xConst}, y = {yConst}");
            Console.WriteLine($"result = {@new()}");

            Console.WriteLine();

            var f3 = MapFactory.From<A>().To<B>();

            var b3 = f3.Map(new A { Test = 1, Bla = "sfd" });

            Console.ReadKey();
        }
    }

    class A
    {
        public int Test { get; set; }

        public string Bla { get; set; }
    }

    class B
    {
        public int Test { get; set; }
    }
}
