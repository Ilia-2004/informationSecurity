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
      // переменные
      var contentInput = File.ReadAllText($"{Path}{fileName}");     // чтение исходного файла
      var lengthContentInput = contentInput.Length;                       // длина исходного файла
      var countingContentInput = new Dictionary<string, double>();           // список букв и их количество
      var countingFrequencyContentInput = new Dictionary<string, double>();  // список букв и их частотность

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
      foreach (var sym in countingContentInput)
        Console.WriteLine(sym.Key + " " + sym.Value);
      
      // подсчёт частотности
      foreach (var sym in countingContentInput)
      {
        var symFrequency = Math.Round(sym.Value / lengthContentInput, 7);
        countingFrequencyContentInput.Add(sym.Key, symFrequency);
      }

      // вывод частотности символов
      Console.WriteLine();
      foreach (var sym in countingFrequencyContentInput)
        Console.WriteLine(sym.Key + " " + sym.Value);

      // сортировка символов
      var listFrequencyContent = countingFrequencyContentInput
                                                  .OrderBy(e => e.Value).Reverse();
      
      // добавление в словарь
      var sortingCountingFrequencyContentInput = listFrequencyContent
                                                    .ToDictionary(x => x.Key, x => x.Value);

      // вывод отсортированного списка частотности
      Console.WriteLine();
      foreach (var sym in sortingCountingFrequencyContentInput)
        Console.WriteLine(sym.Key + " " + sym.Value);
      
      return sortingCountingFrequencyContentInput;
    }

    // decryption method
    private static string s_decryptionMethod()
    {
      var inputText = File.ReadAllText($"{Path}input2.txt");
      var resultFrequencyMethod = s_frequencyMethod("input2.txt");
      var listLetter = File.ReadAllText($"{Path}Alphabet.txt");
      var listLetterFrequencies = File.ReadAllText($"{Path}AlphabetFrequencies.txt").ToList(); 
      var deletedSymbols = new List<string>();
      var requenceiesLettersToInput = new Dictionary<string, string>();
      var resultDecryptionInputText = string.Empty;

      foreach (var sym in resultFrequencyMethod)
        if (!listLetter.Contains(sym.Key) || sym.Key == "") 
           deletedSymbols.Add(sym.Key);

      foreach (var sym in deletedSymbols)
        resultFrequencyMethod.Remove(sym);
      
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

      foreach (var sym in requenceiesLettersToInput)
        Console.WriteLine(sym.Key + " " + sym.Value);
      
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
        
      Console.WriteLine(resultDecryptionInputText);

      return "str";
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