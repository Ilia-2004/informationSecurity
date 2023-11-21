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

      foreach (var element in reverseAlphabetKey)
        Console.WriteLine(element.Key);

      Console.WriteLine(); 

      for (var i = 0; i < stringOut.Length; i++)
      {
        Console.WriteLine(stringOut[i] + " 1");
        const string AloneSymbols = "�������";
        if (AloneSymbols.Contains(stringOut[i]))
        {
          Console.WriteLine(stringOut[i] + " + " + stringOut[++i]); 
          if (stringOut[i] == 'а' && stringOut[++i] == '�') 
          {
            Console.WriteLine(stringOut[i] + " proshol");
            var key = $"{stringOut[i]}{stringOut[++i]}";
            stringResult += $"{reverseAlphabetKey[key]}";
            i++;
          }
          else 
          { 
            Console.WriteLine(stringOut[i] + " lsjkdf");
            stringResult += reverseAlphabetKey[Convert.ToString(stringOut[i])]; 
          }
        }
        else if (stringOut[i] == '-') stringResult += " ";
        else
        {
          Console.WriteLine(stringResult);
          var key = $"{stringOut[i]}{stringOut[++i]}";
          stringResult += $"{reverseAlphabetKey[key]}";
          Console.WriteLine(stringResult);

        }
      }


      Console.WriteLine(" ");      
      Console.WriteLine(stringResult);
      
      return stringResult.ToLower();
    }

    // main method 
    public static void Main()
    {
      Console.WriteLine(string.Format($"Encryption text is: {s_encryptionMethod().Item1}"));
      s_decryptionMethod();
    }
  }
}