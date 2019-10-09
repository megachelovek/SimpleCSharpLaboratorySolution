using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MySpecialList;

namespace FirstWork
{
    public static class MyExtensions
    {
        /// <summary>
        /// Суммирует все элементы массива
        /// </summary>
        public static double SumDynamicArray(this DynamicArray dynamicArray)
        {
            double result = 0;
            if (dynamicArray == null) return result;
            dynamicArray.GetEnumerator().Reset();
            do
            {
                var o = dynamicArray.GetEnumerator().Current;
                if (o != null)
                {
                    var current = double.Parse(o.ToString());
                    result += current;
                }
            } while (dynamicArray.MoveNext());

            return result;
        }

        /// <summary>
        /// Поиск объекта по массиву
        /// </summary>
        public static object FindObjectInArray(this DynamicArray dynamicArray, string word)
        {
            if (dynamicArray == null) return null;
            dynamicArray.GetEnumerator().Reset();
            do
            {
                var o = dynamicArray.GetEnumerator().Current;
                if (o != null)
                {
                    if (o.ToString() == word)
                    {
                        return o;
                    }
                }
            } while (dynamicArray.MoveNext());

            return null;
        }


        public static object FindObjectInArrayDelegate(this DynamicArray dynamicArray,Func<string, string> del, string text)
        {
            if (dynamicArray == null) return null;
            string stringAction = del.Invoke(text);
            dynamicArray.GetEnumerator().Reset();
            do
            {
                var o = dynamicArray.GetEnumerator().Current;
                if (o != null)
                {
                    if (o.ToString() == stringAction)
                    {
                        return o;
                    }
                }
            } while (dynamicArray.MoveNext());

            return null;
        }

        public static string[] FindInArrayLinqExpression(Expression expressionIn, ParameterExpression valueExpression, List<string> listString)
        {
            var result = Expression.Lambda<Func<string, string[]>>(expressionIn, valueExpression).Compile()("test123456");
            return result;
        }
        
        public static string ToLowerCase123(string stringToLowerCase)
        {
            StringBuilder sb=new StringBuilder(stringToLowerCase.ToLower());
            sb.Append("123");
            return sb.ToString();
        }
    }
}
