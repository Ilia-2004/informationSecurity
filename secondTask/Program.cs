using System;
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
    private static IEnumerable<KeyValuePair<string, double>> s_frequencyMethod(string fileName) 
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

      foreach (var symb in countingContentInput)
      {
        var symbFrequency = symb.Value / lengthContentInput;
        countingFrequencyContentInput.Add(symb.Key, symbFrequency);
      }

      var sortingCountingFrequencyContentInput = countingFrequencyContentInput
                                                  .OrderBy(e => e.Value).Reverse();

      return sortingCountingFrequencyContentInput;
    }

    // decryption method
    private static string s_decoryptionMethod()
    {
      var resutlFrequencyMethod = s_frequencyMethod("input2.txt");
      

      return "str";
    }

    /* Main method */
    public static void Main(string[] args)
    {
      var fileName = "input1.txt";
      Console.WriteLine("Counting letters in the first text:");
      foreach (var symb in s_frequencyMethod(fileName))
        Console.WriteLine(symb.Key + " " + symb.Value);
      s_decoryptionMethod();
    }
  }
}