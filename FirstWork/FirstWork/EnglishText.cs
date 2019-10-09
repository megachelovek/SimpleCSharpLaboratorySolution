using System.Collections.Generic;

namespace FirstWork
{
    public static class EnglishText
    {
        public static IDictionary<string, int> GetCountWords(string plainText)
        {
            var resultDictionary = new Dictionary<string, int>();
            var wordsInPlainText = plainText.Split(' ', '.');
            foreach (var currentString in wordsInPlainText)
                if (resultDictionary.TryGetValue(currentString.ToLower(), out var value))
                    resultDictionary[currentString] = value + 1;
                else
                    resultDictionary.Add(currentString, 1);
            return resultDictionary;
        }
    }
}