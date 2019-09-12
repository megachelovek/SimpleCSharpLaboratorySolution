using System;
using System.Collections.Generic;
using FirstExam;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FirstExamTests
{
    [TestClass]
    public class CircleHumanNumerableTests
    {
        [TestMethod]
        public void RunCircleHumanNumerable()
        {
            Console.WriteLine("Start");
            var count = 10; // Изменять здесь
            var testArray = new List<int>(count);
            Console.WriteLine("===");
            for (var i = 0; i < count; i++)
            {
                testArray.Add(i + 1);
                Console.WriteLine(testArray[i]);
            }

            Console.WriteLine("===");
            testArray = (List<int>) CircleHumanNumerable.StartCounting(testArray);
            WriteList(testArray);
            Console.WriteLine("END");
        }

        private static void WriteList(IList<int> listToShow)
        {
            for (var i = 0; i < listToShow.Count; i++) Console.WriteLine(listToShow[i]);
        }
    }
}