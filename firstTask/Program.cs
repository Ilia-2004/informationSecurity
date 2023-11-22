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
    private static (string, Dictionary<string, string>) s_encryptionMethod()
    {
      string[] arrayKey;
      string inputText;
      var outputText = string.Empty; 
      using (var sr = new StreamReader($"{Path}Key.txt"))
      {
        var stringKey = sr.ReadToEnd();
        arrayKey = stringKey.Split(',');
      }
      var alphabetKey = arrayKey.ToDictionary(t => t.Split('-')[0].Trim(), 
                                                              t => t.Split('-')[1]);

      using (var sr = new StreamReader($"{Path}Input.txt")) { inputText = sr.ReadToEnd().ToUpper(); }

      foreach (var symbol in inputText)
      {
        if (alphabetKey.ContainsKey(Convert.ToString(symbol))) outputText += alphabetKey[Convert.ToString(symbol)]; 
        else outputText += '-';
      }
      
      using (var sw = new StreamWriter($"{Path}Out.txt")) { sw.Write(outputText); }
      
      return (outputText.ToUpper(), alphabetKey);
    }

    // decryption method
    private static string s_decryptionMethod()
    {
      var stringOut = s_encryptionMethod().Item1.ToLower();
      var alphabetKey = s_encryptionMethod().Item2;
      var reverseAlphabetKey = new Dictionary<string, string>();
      var stringResult = string.Empty;

      foreach (var element in alphabetKey)
        reverseAlphabetKey[element.Value] = element.Key;

      for (var i = 0; i < stringOut.Length; i++)
      {
        const string AloneSymbols = "уъьяфаю";
        if (AloneSymbols.Contains(stringOut[i]))
        { 
          if (stringOut[i] == 'а')
          {
            if (stringOut[i + 1] == 'м') 
            {
              var key = $"{stringOut[i]}{stringOut[++i]}";
              stringResult += $"{reverseAlphabetKey[key]}";
              i += 1;
            }
            else 
              stringResult += reverseAlphabetKey[Convert.ToString(stringOut[i])]; 
          }
          else 
            stringResult += reverseAlphabetKey[Convert.ToString(stringOut[i])];
        }
        else if (stringOut[i] == '-') stringResult += " ";
        else
        {
          var key = $"{stringOut[i]}{stringOut[++i]}";
          stringResult += $"{reverseAlphabetKey[key]}";
        }
      }

      using (var sw = new StreamWriter($"{Path}Result.txt")) { sw.Write(stringResult.ToLower()); }

      return stringResult.ToLower();
    }

    /* Main method */
    public static void Main()
    {
      Console.WriteLine($"Encryption text is:\n{s_encryptionMethod().Item1}\n");
      Console.WriteLine($"Decryption text is:\n{s_decryptionMethod()}");
    }
  }
}