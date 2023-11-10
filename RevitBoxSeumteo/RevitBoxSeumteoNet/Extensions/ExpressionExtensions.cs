using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RevitBoxSeumteoNet.Extensions
{
    public static class ExpressionExtensions
    {
        public static MemberInfo GetMemberInfo(this Expression expression)
        {
            LambdaExpression lambdaExpression = (LambdaExpression)expression;
            return lambdaExpression.Body is UnaryExpression body ? (body.Operand as MemberExpression).Member : (lambdaExpression.Body as MemberExpression).Member;
        }
    }
}
