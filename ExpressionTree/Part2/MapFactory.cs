using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTree.Part2
{
    public static class MapFactory
    {
        public static MapBuilder<T1> From<T1>() where T1 : class, new()
        {
            return new MapBuilder<T1>();
        }
    }
}
