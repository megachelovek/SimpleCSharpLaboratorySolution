using System;
using System.Text;
using MySpecialList;

namespace FirstWork
{
    public static class MyExtensions
    {
        /// <summary>
        ///     Суммирует все элементы массива
        /// </summary>
        public static float SumDynamicArray<T>(this DynamicArray<T> dynamicArray)
        {
            float result = 0;
            if (dynamicArray == null) return result;
            dynamicArray.GetEnumerator().Reset();
            do
            {
                var o = dynamicArray.GetEnumerator().Current;
                if (o != null)
                {
                    var current = float.Parse(o.ToString());
                    result += current;
                }
            } while (dynamicArray.MoveNext());

            return result;
        }

        /// <summary>
        ///     Является ли строка положительным целым числом
        /// </summary>
        public static bool PositiveString(this string currentString)
        {
            if (currentString == null) return false;
            foreach (var charItem in currentString)
                if (charItem == '-' || charItem == '.' || charItem == ',')
                    return false;
            return true;
        }

        /// <summary>
        ///     Поиск объекта по массиву
        /// </summary>
        public static object FindObjectInArray<T>(this DynamicArray<T> dynamicArray, string word)
        {
            if (dynamicArray == null) return null;
            dynamicArray.GetEnumerator().Reset();
            do
            {
                var o = dynamicArray.GetEnumerator().Current;
                if (o != null)
                    if (o.ToString() == word)
                        return o;
            } while (dynamicArray.MoveNext());

            return null;
        }

        /// <summary>
        ///     Поиск, модифицированной с помощью делегата, строчки в массиве
        /// </summary>
        public static object FindObjectInArrayDelegate<T>(this DynamicArray<T> dynamicArray, Func<string, string> del,
            string text)
        {
            if (dynamicArray == null) return null;
            var stringAction = del.Invoke(text);
            dynamicArray.GetEnumerator().Reset();
            do
            {
                var o = dynamicArray.GetEnumerator().Current;
                if (o != null)
                    if (o.ToString() == stringAction)
                        return o;
            } while (dynamicArray.MoveNext());

            return null;
        }


        public static DynamicArray<int> FindInArrayNegativeDigitDelegate<T>(this DynamicArray<int> dynamicArray,
            Func<int, bool> deleg)
        {
            var resultList = new DynamicArray<int>();
            dynamicArray.GetEnumerator().Reset();
            do
            {
                if (dynamicArray.Current != null && deleg.Invoke((int) dynamicArray.Current))
                    resultList.Add((int) dynamicArray.Current);
            } while (dynamicArray.MoveNext());

            return resultList;
        }

        public static DynamicArray<int> SimpleSearchNegativeNumbers<T>(this DynamicArray<int> dynamicArray)
        {
            var resultList = new DynamicArray<int>();
            dynamicArray.GetEnumerator().Reset();
            do
            {
                if (dynamicArray.Current != null && (int) dynamicArray.Current < 0)
                    resultList.Add((int) dynamicArray.Current);
            } while (dynamicArray.MoveNext());

            return resultList;
        }

        public static string ToLowerCase123(string stringToLowerCase)
        {
            var sb = new StringBuilder(stringToLowerCase.ToLower());
            sb.Append("123");
            return sb.ToString();
        }

        public static bool IsNegativeDigit(int digit)
        {
            return digit < 0;
        }
    }
}