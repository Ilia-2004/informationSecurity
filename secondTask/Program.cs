using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace secondTask;
internal abstract class Program
{
  // files path
  private const string Path = @".\Contents\";

  // the method of calculating the frequency of letters in the text
  private static Dictionary<char, double> s_frequencyMethod(string fileName) 
  {
    /* переменные */ 
    // присваивание русского алфавита
    var alphabet = File.ReadAllText($"{Path}Alphabet.txt");                              
    // чтение исходного файла
    var contentInputText = File.ReadAllText($"{Path}{fileName}").ToUpper();
    // общее колличество букв
    var numberLetters = 0;
    // список букв и их количество
    var countLetters = new Dictionary<char, int>();
    // список букв и их частотность
    var frequencyLetters = new Dictionary<char, double>();
    // список отсортерованных букв по их частотности 
    var sortedFrequencyLettersDictionary = new Dictionary<char, double>();

    // подсчёт букв в тексте
    foreach (var sym in contentInputText)
    {
      var letter = sym;
      if ((int)letter < 32) continue;
      if (alphabet.Contains(letter))
      {
        if (countLetters.ContainsKey(letter))
          countLetters[letter]++;
        else 
          countLetters.Add(letter, 1);
      }
    }

    // вывод списка букв
    Console.WriteLine("Вывод списка букв:"); 
    foreach (var letter in countLetters)
      Console.WriteLine(letter.Key + " " + letter.Value);

    // вывод количества букв
    numberLetters = countLetters.Sum(x => x.Value);
    Console.WriteLine("Колличество букв");
    Console.WriteLine(numberLetters);
    Console.WriteLine();

    // подсчёт частотности
    foreach (var letter in countLetters)
      frequencyLetters[letter.Key] = Math.Round((letter.Value / (double)numberLetters) * 100, 2);

    // сортировка словаря
    var sortedFrequencyLetters = frequencyLetters.OrderByDescending(x => x.Value);
    sortedFrequencyLettersDictionary = sortedFrequencyLetters.ToDictionary(x => x.Key, x => x.Value);

    // вывод отсортированного словаря
    Console.WriteLine("Вывод отсортерованного списка:");
    foreach (var item in sortedFrequencyLettersDictionary)
      Console.WriteLine($"{item.Key}: {item.Value}%");

    Console.WriteLine("---------------");

    return sortedFrequencyLettersDictionary;
  }

  // decryption method
  private static string s_decryptionMethod()
  {
    /* переменные */
    // чтение изходного текста
    var inputText = File.ReadAllText($"{Path}input2.txt");
    // подсчёт частотности исходного текста
    var resultFrequencyMethod = s_frequencyMethod("input2.txt");
    // присваивания алфавита по частотности
    var listLetterFrequencies = File.ReadAllText($"{Path}AlphabetFrequencies.txt").ToList();
    // список сапоставления символов
    var requenceiesLettersToInput = new Dictionary<char, char>();
    // строка результата
    var resultDecryptionInputText = string.Empty;
      
    // добавление символов в список сапоставления
    foreach (var sym in resultFrequencyMethod)
    {
      foreach (var symbol in listLetterFrequencies)
      {
        if (!requenceiesLettersToInput.ContainsKey(sym.Key) && 
            !requenceiesLettersToInput.ContainsValue(symbol))
        {
          requenceiesLettersToInput.Add(sym.Key, symbol);
          continue;
        }
      }
    }

    // вывод списка сапоставления
    Console.WriteLine("Вывод списка сапоставления: ");
    foreach (var sym in requenceiesLettersToInput)
      Console.WriteLine(sym.Key + " " + sym.Value);
    Console.WriteLine();
      
    // добавление в строку результата
    //foreach (var sym in inputText)
    //{
    //  //Console.WriteLine(sym);
    //  if (requenceiesLettersToInput.ContainsKey(sym))
    //  {
    //    resultDecryptionInputText += requenceiesLettersToInput[sym];
    //    //Console.WriteLine(resultDecryptionInputText + " second");
    //  }
    //  else if (Convert.ToString(sym).ToUpper() == "Ё")
    //    resultDecryptionInputText += "Ё";
    //  else resultDecryptionInputText += sym; 
    //}
        
    // вывод результата
    //Console.WriteLine(resultDecryptionInputText);

    return resultDecryptionInputText;
  }

  /* Main method */
  public static void Main(string[] args)
  {
    var fileName = "input1.txt";
    Console.WriteLine("Counting letters in the first text:");
    foreach (var sym in s_frequencyMethod(fileName))
      Console.WriteLine(sym.Key + " " + sym.Value);
    //Console.WriteLine("Method second");
    //s_decryptionMethod();
  }
}