using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionTree.Part2.Interfaces;

namespace ExpressionTree.Part2
{
    public class MapBuilder<T1> where T1 : class, new()
    {
        public IMapper<T1, T2> To<T2>() where T2 : class, new()
        {
            return new Mapper<T1, T2>();
        }
    }
}
