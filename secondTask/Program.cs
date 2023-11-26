using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace secondTask
{
  internal abstract class Program
  {
    // files path
    private const string Path = @".\Contents\";

    // the method of calculating the frequency of letters in the text
    private static Dictionary<string, double> s_frequencyMethod(string fileName) 
    {
      /* переменные */ 
      // чтение исходного файла
      var contentInput = File.ReadAllText($"{Path}{fileName}");
      // длина исходного файла
      var lengthContentInput = contentInput.Length;
      // список букв и их количество
      var countingContentInput = new Dictionary<string, double>();
      // список букв и их частотность
      var countingFrequencyContentInput = new Dictionary<string, double>();
      // присваивание русского алфавита
      var listLetter = File.ReadAllText($"{Path}Alphabet.txt");
      // список удаляемых символов
      var deletedSymbols = new List<string>();                               

      // посчёт символов
      foreach (var sym in contentInput) 
      {
        if ((int)sym <= 32) continue;
        if (countingContentInput.ContainsKey(Convert.ToString(sym).ToUpper()))
          countingContentInput[Convert.ToString(sym).ToUpper()] += 1; 
        else
          countingContentInput.Add(Convert.ToString(sym).ToUpper(), 1);
      }
      
      // вывод количества символов
      Console.WriteLine(lengthContentInput + " колличество символов");
      Console.WriteLine();
      
      // вывод символов и их количества в тексте
      Console.WriteLine("Вывод символов и их количества в тексте: ");
      foreach (var sym in countingContentInput)
        Console.WriteLine(sym.Key + " " + sym.Value);
      Console.WriteLine();
      
      // подсчёт частотности
      foreach (var sym in countingContentInput)
      {
        var symFrequency = Math.Round(sym.Value / lengthContentInput, 7);
        countingFrequencyContentInput.Add(sym.Key, symFrequency);
      }

      // вывод частотности символов
      Console.WriteLine("Вывод частотности символов: ");
      foreach (var sym in countingFrequencyContentInput)
        Console.WriteLine(sym.Key + " " + sym.Value);
      Console.WriteLine();

      // сортировка символов
      var listFrequencyContent = countingFrequencyContentInput
                                                  .OrderBy(e => e.Value).Reverse();
      // добавление в словарь
      var sortingCountingFrequencyContentInput = listFrequencyContent
                                                    .ToDictionary(x => x.Key, x => x.Value);

      // вывод отсортированного списка частотности
      Console.WriteLine("Вывод отсортированного списка частотности: ");
      foreach (var sym in sortingCountingFrequencyContentInput)
        Console.WriteLine(sym.Key + " " + sym.Value);
      
      
      // добавление удаляемых символов в список
      foreach (var sym in sortingCountingFrequencyContentInput)
        if (!listLetter.Contains(sym.Key) || sym.Key == "") 
          deletedSymbols.Add(sym.Key);
      
      // вывод удаляемых символов
      Console.WriteLine("Вывод удаляемых символов: ");
      foreach (var sym in deletedSymbols)
        Console.WriteLine(sym);
      Console.WriteLine();
            
      // удаление символов, не являющимися буквами
      foreach (var sym in deletedSymbols)
        sortingCountingFrequencyContentInput.Remove(sym);
            
      // вывод частотности символов после удаления символов
      Console.WriteLine("Вывод частотности символов после удаления символов: ");
      foreach (var sym in sortingCountingFrequencyContentInput)
        Console.WriteLine(sym.Key + " " + sym.Value);
      Console.WriteLine();
      
      Console.WriteLine("---------------");
      
      return sortingCountingFrequencyContentInput;
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
      var requenceiesLettersToInput = new Dictionary<string, string>();
      // строка результата
      var resultDecryptionInputText = string.Empty;
      
      // добавление символов в список сапоставления
      foreach (var sym in resultFrequencyMethod)
      {
        foreach (var symbol in listLetterFrequencies)
        {
          if (!requenceiesLettersToInput.ContainsKey(sym.Key) && 
            !requenceiesLettersToInput.ContainsValue(Convert.ToString(symbol)))
          {
             requenceiesLettersToInput.Add(sym.Key, Convert.ToString(symbol));
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
      foreach (var sym in inputText)
      {
        //Console.WriteLine(sym);
        if (requenceiesLettersToInput.ContainsKey(Convert.ToString(sym).ToUpper()))
        {
          resultDecryptionInputText += requenceiesLettersToInput[Convert.ToString(sym).ToUpper()];
          //Console.WriteLine(resultDecryptionInputText + " second");
        }
        else if (Convert.ToString(sym).ToUpper() == "Ё")
          resultDecryptionInputText += "Ё";
        else resultDecryptionInputText += sym; 
      }
        
      // вывод результата
      Console.WriteLine(resultDecryptionInputText);

      return resultDecryptionInputText;
    }

    /* Main method */
    public static void Main(string[] args)
    {
      var fileName = "input1.txt";
      Console.WriteLine("Counting letters in the first text:");
        //foreach (var sym in s_frequencyMethod(fileName))
        //Console.WriteLine(sym.Key + " " + sym.Value);
      Console.WriteLine("Method second");
      s_decryptionMethod();
    }
  }
}