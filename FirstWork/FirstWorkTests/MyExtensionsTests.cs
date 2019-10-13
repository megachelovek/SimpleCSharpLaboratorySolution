using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;
using FirstWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySpecialList;

namespace FirstExamTests
{
    [TestClass]
    public class MyExtensionsTests
    {
        #region Задания по Delegate и Expression

        /// <summary>
        ///     Задание 1
        ///     Напишите расширяющий метод, который определяет сумму элементов массива.
        /// </summary>
        [TestMethod]
        public void Extension1SumTest()
        {
            Console.WriteLine("Start");

            var dynamicArray = new DynamicArray<float>(5);
            dynamicArray.Add(12.333F);
            dynamicArray.Add(4F);
            dynamicArray.Add(34.44F);
            dynamicArray.Add(55.2F);
            dynamicArray.Add(111.2F);
            ShowList(dynamicArray);
            var result = dynamicArray.SumDynamicArray();
            Console.WriteLine($"Result= {result}");
            Console.WriteLine("END");
        }

        /// <summary>
        ///     Задание 2
        ///     Напишите расширяющий метод, который определяет, является ли строка положительным целым числом. Методы Parse и
        ///     TryParse не использовать.
        /// </summary>
        [TestMethod]
        public void Extension2PositiveStringTest()
        {
            Console.WriteLine("Start");
            var string1 = "-23.23";
            var string2 = "23.23";
            var string3 = "23";
            Console.WriteLine(
                $"Result= {string1}={string1.PositiveString()} | {string2}={string2.PositiveString()} | {string3}={string3.PositiveString()}");
            Console.WriteLine("END");
        }

        /// <summary>
        ///     1. метода, реализующего поиск напрямую;
        /// </summary>
        [TestMethod]
        public void Extension31SimpleNegativeSearchTest()
        {
            var stopwatch1 = Stopwatch.StartNew();
            Console.WriteLine("Start");

            var arrayOfNumbers = GenerateBigArray();
            var resultArray = arrayOfNumbers.SimpleSearchNegativeNumbers<int>();

            Console.WriteLine($"Time = {stopwatch1.ElapsedTicks}");
            ShowList(resultArray);
            Console.WriteLine("END");
        }

        /// <summary>
        ///     2.	метода, которому условие поиска передается через делегат;
        /// </summary>
        [TestMethod]
        public void Extension32DelegateTest()
        {
            var stopwatch2 = Stopwatch.StartNew();
            Console.WriteLine("Start");

            var arrayOfNumbers = GenerateBigArray();

            var action = new Func<int, bool>(MyExtensions.IsNegativeDigit);
            var resultArray = arrayOfNumbers.FindInArrayNegativeDigitDelegate<int>(action);

            Console.WriteLine($"Time = {stopwatch2.ElapsedTicks}");
            ShowList(resultArray);
            Console.WriteLine("END");
        }

        /// <summary>
        ///     3.	метода, которому условие поиска передается через делегат в виде анонимного метода;
        /// </summary>
        [TestMethod]
        public void Extension33AnonymousDelegateTest()
        {
            var stopwatch3 = Stopwatch.StartNew();
            Console.WriteLine("Start");

            var arrayOfNumbers = GenerateBigArray();

            Func<int, bool> action = delegate(int input) { return input < 0; };
            var resultArray = arrayOfNumbers.FindInArrayNegativeDigitDelegate<int>(action);

            Console.WriteLine($"Time = {stopwatch3.ElapsedTicks}");
            ShowList(resultArray);
            Console.WriteLine("END");
        }

        /// <summary>
        ///     4.	метода, которому условие поиска передается через делегат в виде лямбда-выражения;
        /// </summary>
        [TestMethod]
        public void Extension34LamdaDelegateTest()
        {
            var stopwatch4 = Stopwatch.StartNew();
            Console.WriteLine("Start");

            var arrayOfNumbers = GenerateBigArray();

            Func<int, bool> action = input => { return input < 0; };
            var resultArray = arrayOfNumbers.FindInArrayNegativeDigitDelegate<int>(action);

            Console.WriteLine($"Time = {stopwatch4.ElapsedTicks}");
            ShowList(resultArray);
            Console.WriteLine("END");
        }

        /// <summary>
        ///     5.	LINQ-выражения
        /// </summary>
        [TestMethod]
        public void Extension35ExpressionsDelegateTest()
        {
            var stopwatch5 = Stopwatch.StartNew();
            Console.WriteLine("Start");

            var arrayOfNumbers = GenerateBigArray();

            Expression<Func<int, bool>> action = num => num < 0; //Expession via lambda
            var expessionFunc = action.Compile();
            var resultArray = arrayOfNumbers.FindInArrayNegativeDigitDelegate<int>(expessionFunc);

            Console.WriteLine($"Time = {stopwatch5.ElapsedTicks}");
            ShowList(resultArray);
            Console.WriteLine("END");
        }

        #endregion

        #region Дополнительные тесты для пробы Delegate и Expressions

        [TestMethod]
        public void ExtensionFindSimpleTest()
        {
            Console.WriteLine("Start");

            var dynamicArray = new DynamicArray<string>(5);
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

            var dynamicArray = new DynamicArray<string>(5);
            dynamicArray.Add("test1");
            dynamicArray.Add("test2");
            dynamicArray.Add("test3");
            dynamicArray.Add("test4");
            dynamicArray.Add("test123");

            var action = new Func<string, string>(MyExtensions.ToLowerCase123);

            ShowList(dynamicArray);
            var result = dynamicArray.FindObjectInArrayDelegate(action, "test");
            Console.WriteLine($"Result= {result}");
            Console.WriteLine("END");
        }


        [TestMethod]
        public void ExtensionFindAnonymousDelegateTest()
        {
            Console.WriteLine("Start");

            var dynamicArray = new DynamicArray<string>(5);
            dynamicArray.Add("test1");
            dynamicArray.Add("test2");
            dynamicArray.Add("test3");
            dynamicArray.Add("test4");
            dynamicArray.Add("test456");

            Func<string, string> action = delegate(string input)
            {
                var sb = new StringBuilder(input.ToLower());
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

            var dynamicArray = new DynamicArray<string>(5);
            dynamicArray.Add("test1");
            dynamicArray.Add("test2");
            dynamicArray.Add("test3");
            dynamicArray.Add("test4");
            dynamicArray.Add("test789");

            Func<string, string> action = input =>
            {
                var sb = new StringBuilder(input.ToLower());
                sb.Append("789");
                return sb.ToString();
            };

            ShowList(dynamicArray);
            var result = dynamicArray.FindObjectInArrayDelegate(action, "test");
            Console.WriteLine($"Result= {result}");
            Console.WriteLine("END");
        }

        #endregion

        #region Функции для удобной работы с тестами

        private static void ShowList<T>(DynamicArray<T> dynamicArray)
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

        private static DynamicArray<int> GenerateBigArray()
        {
            var count = 150;
            var random = new Random();
            var dynamicArray = new DynamicArray<int>();
            for (var i = 0; i < count; i++) dynamicArray.Add(random.Next(200) - 100);

            return dynamicArray;
        }

        #endregion
    }
}