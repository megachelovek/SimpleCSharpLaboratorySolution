using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySpecialList;

namespace FirstExamTests
{
    [TestClass]
    public class DynamicArrayTests
    {
        [TestMethod]
        public void AddItemList()
        {
            Console.WriteLine("Start");

            var dynamicArray = new DynamicArray<string>(5);
            dynamicArray.Add("elem1");
            dynamicArray.Add("elem2");
            dynamicArray.Add("elem3");
            dynamicArray.Add("elem4");
            dynamicArray.Add("elem5");
            ShowList(dynamicArray);
            Console.WriteLine("END");
        }

        [TestMethod]
        public void AddRangeList()
        {
            Console.WriteLine("Start");

            var dynamicArray = new DynamicArray<string>(5);
            dynamicArray.Add("elem1");
            dynamicArray.Add("elem2");
            dynamicArray.Add("elem3");
            dynamicArray.Add("elem4");
            dynamicArray.Add("elem5");
            var dynamicArray2 = new DynamicArray<string>(5);
            dynamicArray2.Add("elem6");
            dynamicArray2.Add("elem7");
            dynamicArray2.Add("elem8");
            dynamicArray2.Add("elem9");
            dynamicArray2.Add("elem10");

            dynamicArray.AddRange(dynamicArray2);
            ShowList(dynamicArray);
            Console.WriteLine("END");
            Assert.AreEqual(dynamicArray.Capacity, 10);
        }

        [TestMethod]
        public void RemoveItemList()
        {
            Console.WriteLine("Start");

            var dynamicArray = new DynamicArray<string>(5);
            dynamicArray.Add("elem1");
            dynamicArray.Add("elem2");
            dynamicArray.Add("elem3");
            dynamicArray.Add("elem4");
            dynamicArray.Add("elem5");

            dynamicArray.Remove(2);

            ShowList(dynamicArray);
            Console.WriteLine("END");
        }

        [TestMethod]
        public void InsertItemList()
        {
            Console.WriteLine("Start");

            var dynamicArray = new DynamicArray<string>(5);
            dynamicArray.Add("elem1");
            dynamicArray.Add("elem2");
            dynamicArray.Add("elem3");
            dynamicArray.Add("elem4");
            dynamicArray.Add("elem5");

            dynamicArray.Insert("newSuperElement", 3);

            ShowList(dynamicArray);
            Console.WriteLine("END");
        }

        [TestMethod]
        public void CreateFromCollectionList()
        {
            Console.WriteLine("Start");
            var list = new List<string>();
            list.Add("elem1");
            list.Add("elem2");
            list.Add("elem3");
            list.Add("elem4");
            list.Add("elem5");

            var dynamicArray = new DynamicArray<string>(list);

            ShowList(dynamicArray);
            Console.WriteLine("END");
        }

        private static void ShowList(DynamicArray<string> dynamicArray)
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