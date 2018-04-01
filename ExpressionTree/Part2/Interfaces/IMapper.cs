using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTree.Part2.Interfaces
{
    public interface IMapper<in T1, out T2>
    {
        T2 Map(T1 fromObject);
    }
}
