using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using FirstWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySpecialList;

namespace FirstExamTests
{
    [TestClass]
    public class MyExtensionsTests
    {
        [TestMethod]
        public void ExtensionSumTest()
        {
            Console.WriteLine("Start");

            DynamicArray dynamicArray = new DynamicArray(5);
            dynamicArray.Add(12.333);
            dynamicArray.Add(4);
            dynamicArray.Add(34.44);
            dynamicArray.Add(55.2);
            dynamicArray.Add(111.2);
            ShowList(dynamicArray);
            double result = dynamicArray.SumDynamicArray();
            Console.WriteLine($"Result= {result}");
            Console.WriteLine("END");
        }

        [TestMethod]
        public void ExtensionFindSimpleTest()
        {
            Console.WriteLine("Start");

            DynamicArray dynamicArray = new DynamicArray(5);
            dynamicArray.Add("test1");
            dynamicArray.Add("test2");
            dynamicArray.Add("test3");
            dynamicArray.Add("test4");
            dynamicArray.Add("test5");
            ShowList(dynamicArray);
            var result = dynamicArray.FindObjectInArray("test4");
            Console.WriteLine($"Result= {result}");
            Console.WriteLine("END");
        }

        [TestMethod]
        public void ExtensionFindDelegateSimpleTest()
        {
            Console.WriteLine("Start");

            DynamicArray dynamicArray = new DynamicArray(5);
            dynamicArray.Add("test1");
            dynamicArray.Add("test2");
            dynamicArray.Add("test3");
            dynamicArray.Add("test4");
            dynamicArray.Add("test123");
            var action = new Func<string,string>(MyExtensions.ToLowerCase123);
            ShowList(dynamicArray);
            var result = dynamicArray.FindObjectInArrayDelegate(action,"test");
            Console.WriteLine($"Result= {result}");
            Console.WriteLine("END");
        }

        [TestMethod]
        public void ExtensionFindAnonymousDelegateTest()
        {
            Console.WriteLine("Start");

            DynamicArray dynamicArray = new DynamicArray(5);
            dynamicArray.Add("test1");
            dynamicArray.Add("test2");
            dynamicArray.Add("test3");
            dynamicArray.Add("test4");
            dynamicArray.Add("test456");

            Func<string, string> action = delegate(string input)
            {
                StringBuilder sb = new StringBuilder(input.ToLower());
                sb.Append("456");
                return sb.ToString();
            }; 
            ShowList(dynamicArray);
            var result = dynamicArray.FindObjectInArrayDelegate(action, "test");
            Console.WriteLine($"Result= {result}");
            Console.WriteLine("END");
        }

        [TestMethod]
        public void ExtensionFindLambdaDelegateTest()
        {
            Console.WriteLine("Start");

            DynamicArray dynamicArray = new DynamicArray(5);
            dynamicArray.Add("test1");
            dynamicArray.Add("test2");
            dynamicArray.Add("test3");
            dynamicArray.Add("test4");
            dynamicArray.Add("test789");

            Func<string, string> action = (string input) =>
            {
                StringBuilder sb = new StringBuilder(input.ToLower());
                sb.Append("789");
                return sb.ToString();
            };
            ShowList(dynamicArray);
            var result = dynamicArray.FindObjectInArrayDelegate(action, "test");
            Console.WriteLine($"Result= {result}");
            Console.WriteLine("END");
        }

        [TestMethod]
        public void ExtensionLinqExpressionsDelegateTest()
        {
            Console.WriteLine("Start");

            List<string> stringList = new List<string>(5);
            stringList.Add("test1");
            stringList.Add("test123456");
            stringList.Add("test3");
            stringList.Add("test123456");
            stringList.Add("test789");
            
            ParameterExpression valueExpression = Expression.Parameter(typeof(string), "value");
            ParameterExpression resultExpression = Expression.Parameter(typeof(string), "result");
            LabelTarget label = Expression.Label(typeof(string));

            var action = Expression.Block(
                new[] { resultExpression },
                Expression.Assign(resultExpression, Expression.Constant("")),
                Expression.Loop(
                Expression.IfThenElse(
                    Expression.Equal(valueExpression, Expression.Constant("test123456")),
                    Expression.Assign(resultExpression, Expression.Constant("test123456")),
                    Expression.Break(label, resultExpression)
                    ),
                label
                )
            );
            
            var result = MyExtensions.FindInArrayLinqExpression(action, valueExpression, stringList);
            Console.WriteLine($"Result= {result}");
            Console.WriteLine("END");
        }

        private static void ShowList(DynamicArray dynamicArray)
        {
            if (dynamicArray != null)
            {
                Console.WriteLine("==Вывод списка==");
                dynamicArray.GetEnumerator().Reset();
                do
                {
                    Console.WriteLine(dynamicArray.Current.ToString());
                } while (dynamicArray.MoveNext());
            }
        }
    }
}