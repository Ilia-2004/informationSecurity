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
    private static string s_frequencyMethod(string fileName) 
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
        Console.WriteLine(symb.Key + " " + symb.Value);

      Console.WriteLine();
      Console.WriteLine(lengthContentInput);

      foreach (var symb in countingContentInput)
      {
        var symbFrequency = symb.Value / lengthContentInput;
        countingFrequencyContentInput.Add(symb.Key, symbFrequency);
      }

      foreach(var symb in countingFrequencyContentInput)
      {
         Console.WriteLine(symb.Key + " " + symb.Value);
      }



      return "str";
    }



    /* Main method */
    public static void Main(string[] args)
    {
      var fileName = "input1.txt";
      s_frequencyMethod(fileName);    
    }
  }
}