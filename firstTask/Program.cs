using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace informationSecurity
{
  internal abstract class Program
  {
    // files path
    private const string Path = @".\Contents\";

    // encryption method
    private static (string, Dictionary<string, string>) _encryptionMethod()
    {
      string[] arrayKey;
      string inputText;
      var outputText = string.Empty; 
      using (var sr = new StreamReader($"{Path}Key.txt"))
      {
        var stringKey = sr.ReadToEnd();
        arrayKey = stringKey.Split(',');
      }
      var alphabet = arrayKey.ToDictionary(t => t.Split('-')[0].Trim(), 
                                                              t => t.Split('-')[1]);

      using (var sr = new StreamReader($"{Path}Input.txt")) { inputText = sr.ReadToEnd().ToUpper(); }

      foreach (var symbol in inputText)
      {
        if (alphabet.ContainsKey(Convert.ToString(symbol))) outputText += alphabet[Convert.ToString(symbol)]; 
        else outputText += '-';
      }
      
      using (var sw = new StreamWriter($"{Path}Out.txt")) { sw.Write(outputText); }
      
      return (outputText.ToUpper(), alphabet);
    }

    // decryption method
    private static string _decryptionMethod()
    {
      var stringOut = _encryptionMethod().Item1.ToLower().Split('-');
      var stringResult = string.Empty;
      
      foreach (var t in stringOut)
      {
        for (var j = 0; j < t.Length; j++)
        { 
          Console.WriteLine(t[j]);
        }
      }
      
      Console.WriteLine(stringResult);
      
      return stringResult.ToLower();
    }

    // main method 
    public static void Main()
    {
       Console.WriteLine(string.Format($"Encryption text is: {_encryptionMethod().Item1}"));
       _decryptionMethod();
    }
  }
}


