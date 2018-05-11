using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TOEC_SASTAD_Data.Model
{
    static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }
        //false
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.Or(expr1.Body, invokedExpr), expr1.Parameters);
        }
        //true
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
        }


        /// <summary>
        /// 排序扩展
        /// DistinctBy不是.net framework提供的扩展方法，是第三方的扩展方法
        /// </summary>
        /// <typeparam name="t"></typeparam>
        /// <param name="list"></param>
        /// <param name="propertySelector"></param>
        /// <returns></returns>
        public static IEnumerable<t> DistinctBy<t>(this IEnumerable<t> list, Func<t, object> propertySelector)
        {
            return list.GroupBy(propertySelector).Select(x => x.First());
        }
         
    }
}
