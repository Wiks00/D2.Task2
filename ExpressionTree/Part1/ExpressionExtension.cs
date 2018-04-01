using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTree.Part1
{
    public static class ExpressionExtension
    {
        public static Expression<Tout> Convert<Tin, Tout, TParam>(this Expression<Tin> root, Dictionary<string, TParam> replacmentDict)
        {
            var visitor = new MagicExpressionVisitor<TParam>(replacmentDict);

            return (Expression<Tout>)visitor.Visit(root);
        }
    }
}
