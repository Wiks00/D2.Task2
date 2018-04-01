using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace ExpressionTree.Part1
{
    public class MagicExpressionVisitor<TParam> : ExpressionVisitor
    {
        private readonly Dictionary<string, TParam> parameters;

        public MagicExpressionVisitor(Dictionary<string, TParam> parameters)
        {
            this.parameters = parameters;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (parameters.TryGetValue(node.Name, out TParam constValue) && node.Type == typeof(TParam))
            {
                return Expression.Constant(constValue);
            }

            return base.VisitParameter(node);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            return Expression.Lambda(Visit(node.Body), node.Parameters.Where(param => !parameters.ContainsKey(param.Name)));
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.Right.NodeType == ExpressionType.Constant)
            {
                if (node.NodeType == ExpressionType.Add)
                {
                    return VisitUnary(Expression.Increment(node.Left));
                }

                if (node.NodeType == ExpressionType.Subtract)
                {
                    return VisitUnary(Expression.Decrement(node.Left));
                }
            }

            return base.VisitBinary(node);
        }
    }
}
