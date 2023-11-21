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
      var alphabet = _encryptionMethod().Item2;
      var stringResult = string.Empty;

      foreach (var word in stringOut)
      {
        Console.WriteLine(word);
      }
      
      // for (var i = 0; i < strOut.Length; i++)
      // {
      //   foreach (var sym in alphabet)
      //   {
      //     if (sym.Value == Convert.ToString(strOut[i])) strResult += sym.Key;
      //     else if (strOut.Contains(strOut[i + 1]) && sym.Value == $"{strOut[i]}{strOut[i + 1]}") strResult += sym.Key;
      //     else strResult += " ";
      //   }
      // }
      
      Console.WriteLine(stringResult);
      
      return stringResult.ToLower();
    }

    // main method 
    public static void Main(string[] args)
    {
       Console.WriteLine(string.Format($"Encryption text is: {_encryptionMethod()}"));
       _decryptionMethod();
    }
  }
}


