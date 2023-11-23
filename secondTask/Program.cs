﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace secondTask
{
  internal abstract class Program
  {
    // files path
    private const string Path = @".\Contents\";

    // the method of calculating the frequency of letters in the text
    private static Dictionary<string, double> s_frequencyMethod(string fileName) 
    {
      var contentInput = File.ReadAllText($"{Path}{fileName}");
      var lengthContentInput = contentInput.Length;
      var countingContentInput = new Dictionary<string, double>();
      var countingFrequencyContentInput = new Dictionary<string, double>();

      foreach (var symb in contentInput) 
      {
        if (countingContentInput.ContainsKey(Convert.ToString(symb)))
          countingContentInput[Convert.ToString(symb)] += 1; 
        else
          countingContentInput.Add(Convert.ToString(symb), 1);
      }

            Console.WriteLine(lengthContentInput);

      foreach (var symb in countingContentInput)
      {
        var symbFrequency = symb.Value / lengthContentInput;
        countingFrequencyContentInput.Add(symb.Key, symbFrequency);
      }

      var listFrequencyContent = countingFrequencyContentInput
                                                  .OrderBy(e => e.Value).Reverse();
      var sortingCountingFrequencyContentInput = listFrequencyContent
                                                    .ToDictionary(x => x.Key, x => x.Value);

      return sortingCountingFrequencyContentInput;
    }

    // decryption method
    private static string s_decoryptionMethod()
    {
      var resultFrequencyMethod = s_frequencyMethod("input2.txt");
      var listLetters = new List<char>() {'о', 'а', 'е', 'н', 'и', 'л', 'т', 'с', 'к', 'р', 'в', 'м', 'д', 
                                          'у', 'п', 'ь', 'ы', 'з', 'я', 'г', 'ч', 'б', 'й', 'ш', 'ж', 
                                          'х', 'ю', 'ц', 'щ', 'э', 'ф', 'ъ'};
      
       Console.WriteLine();
      foreach (var symb in resultFrequencyMethod) {
            Console.WriteLine(symb.Key + " " + symb.Value);
            }

            if (!resultFrequencyMethod.ContainsKey("а")) Console.WriteLine("Pridor");
      return "str";
    }

    /* Main method */
    public static void Main(string[] args)
    {
      var fileName = "input1.txt";
      Console.WriteLine("Counting letters in the first text:");
      foreach (var symb in s_frequencyMethod(fileName))
        Console.WriteLine(symb.Key + " " + symb.Value);
      Console.WriteLine("Method second");
      s_decoryptionMethod();
    }
  }
}