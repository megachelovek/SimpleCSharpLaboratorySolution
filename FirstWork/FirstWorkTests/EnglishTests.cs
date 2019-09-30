using System;
using System.Collections.Generic;
using FirstExam;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FirstExamTests
{
    [TestClass]
    public class EnglishTests
    {
        [TestMethod]
        public void RunEnglish()
        {
            Console.WriteLine("Start");

            //������ �������� ����� �����
            var testText =
                "����� (�� ���. textus � �����; ���������, ���������) � ��������������� �� �����-���� ������������ �������� ������������ �����; � ����� ����� ������� � ������ ������������������ ��������.\r\n\r\n���������� ��� �������� ��������� ������� ������: ����������� (�����������, ���������� �����������) � ���������������� (����� �������). ����������� ������ ������������� ��������� � ������ ��� � ���������� ����������, ������������ �� ��������� ��� ���������� ���������. ���������������� � ������������ ������ ��� ������ ����� ������������� ������ � ������� ������ ����������������.\r\n\r\n� ����������� ������ ������ ������������ � ������� ��������, ������� � ������� ������ ����. ���������� ������ ��������� � ������ ����������� ������ � ����������������. ���, ��������, �. �. ��������� ���������� ����� ��������� �������: ���� ���������� ���������, ����������������� � ���� ����������� ���������, ��������� �� ���� ������������, ����������� ������� ������ �����������, �������������� � ���������� �����, ������� ����������� ��������� ��������, �������������� ��������� � �������������� ����������� ������������";

            Console.WriteLine("===");
            var dictionary = (Dictionary<string, int>) EnglishText.GetCountWords(testText);
            WriteDictionary(dictionary);
            Console.WriteLine("END");
        }

        private static void WriteDictionary(IDictionary<string, int> dictionary)
        {
            foreach (var item in dictionary) Console.WriteLine($"{item.Key}={item.Value}");
        }
    }
}