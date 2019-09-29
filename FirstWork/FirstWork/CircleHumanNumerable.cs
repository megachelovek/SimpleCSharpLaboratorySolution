using System.Collections.Generic;

namespace FirstExam
{
    /// <summary>
    /// Задание не готово к проверке.
    /// Нет точки входа, непонятно, как вообще оно проверялось. В тестах дохлая зависимость FirstExam.
    /// </summary>
    public static class CircleHumanNumerable
    {
        public static IList<int> StartCounting(IList<int> listOfHuman)
        {
            var iterator = -1;
            var countOfElements = listOfHuman.Count;
            while (countOfElements != 1)
                if (iterator + 2 <= countOfElements - 1)
                {
                    iterator += 2;
                    listOfHuman.RemoveAt(iterator);
                    countOfElements--;
                }
                else
                {
                    if (iterator == countOfElements && countOfElements == 2 || iterator + 2 == countOfElements)
                        iterator = -2;
                    if (iterator + 1 == countOfElements) iterator = -1;
                    if (iterator == countOfElements) iterator = 0;
                }

            return listOfHuman;
        }
    }
}