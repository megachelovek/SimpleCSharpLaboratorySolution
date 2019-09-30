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

            //Менять тестовый текст здесь
            var testText =
                "Текст (от лат. textus — ткань; сплетение, сочетание) — зафиксированная на каком-либо материальном носителе человеческая мысль; в общем плане связная и полная последовательность символов.\r\n\r\nСуществуют две основные трактовки понятия «текст»: имманентная (расширенная, философски нагруженная) и репрезентативная (более частная). Имманентный подход подразумевает отношение к тексту как к автономной реальности, нацеленность на выявление его внутренней структуры. Репрезентативный — рассмотрение текста как особой формы представления знаний о внешней тексту действительности.\r\n\r\nВ лингвистике термин «текст» используется в широком значении, включая и образцы устной речи. Восприятие текста изучается в рамках лингвистики текста и психолингвистики. Так, например, И. Р. Гальперин определяет текст следующим образом: «Это письменное сообщение, объективированное в виде письменного документа, состоящее из ряда высказываний, объединённых разными типами лексической, грамматической и логической связи, имеющее определённый моральный характер, прагматическую установку и соответственно литературно обработанное»";

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