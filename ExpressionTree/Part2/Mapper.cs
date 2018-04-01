using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ExpressionTree.Part2.Interfaces;

namespace ExpressionTree.Part2
{
    internal class Mapper<T1, T2> : IMapper<T1, T2>
        where T1 : class, new()
        where T2 : class, new()
    {
        private readonly List<string> memberNames;

        private static Func<T1, T2> backFiled;

        public IReadOnlyList<string> mappedMembers { get; private set; }

        public Func<T1, T2> mapDelegate
        {
            get
            {
                if (backFiled is null)
                {
                    backFiled = Build(typeof(T1), typeof(T2)).Compile();
                }

                return backFiled;
            }
        }

        public Mapper(params string[] memberNames)
        {
            this.memberNames = memberNames.Length == 0 ? new List<string>(typeof(T1).GetMembers().Where(item => item.MemberType == MemberTypes.Field || item.MemberType == MemberTypes.Property).Select(item => item.Name)) : new List<string>(memberNames);

            mappedMembers = new ReadOnlyCollection<string>(this.memberNames);
        }

        public T2 Map(T1 fromObject)
        {
            return mapDelegate(fromObject);
        }

        private Expression<Func<T1, T2>> Build(Type fromObject, Type toObject)
        {
            var parameter = Expression.Parameter(fromObject);
            var list = mappedMembers.Select(name =>
            {
                var member = (MemberInfo) toObject.GetField(name) ?? toObject.GetProperty(name);

                if (!(member is null))
                {
                    return Expression.Bind(member,
                            Expression.PropertyOrField(parameter, name));
                }

                return null;
            }).Where(item => !(item is null));

            var init = Expression.MemberInit(Expression.New(toObject.GetConstructor(Type.EmptyTypes)), list);

            return (Expression<Func<T1, T2>>)Expression.Lambda(init, parameter);
        }
    }
}
