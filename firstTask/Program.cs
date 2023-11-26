using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace informationSecurity;
internal abstract class Program
{
  /* Methods */
  // files path
  private const string Path = @".\Contents\";

  // encryption method
  private static (string, Dictionary<string, string>) s_encryptionMethod()
  {
    /* Variables */
    // the input text 
    var inputText = File.ReadAllText($"{Path}Input.txt");
    // the encryption key
    var stringKey = File.ReadAllText($"{Path}Key.txt");
    // the divided encryption key
    var arrayKey = stringKey.Split(',');
    // the alphabet of the encryption key
    var alphabetKey = arrayKey.ToDictionary(t => t.Split('-')[0].Trim(),
      t => t.Split('-')[1]);
    // the variable for output text
    var outputText = string.Empty; 
      
    // encryption of the input text
    foreach (var symbol in inputText)
    {
      if (alphabetKey.ContainsKey(Convert.ToString(symbol))) 
        outputText += alphabetKey[Convert.ToString(symbol)]; 
      else outputText += '-';
    }
      
    // writing output text to a file 
    using (var sw = new StreamWriter($"{Path}Out.txt")) 
    { sw.Write(outputText); }
    
    return (outputText.ToUpper(), alphabetKey);
  }

  // decryption method
  private static string s_decryptionMethod()
  {
    /* Variables */
    // the repeated letters
    const string aloneSymbols = "уъьяфаю";
    // the encrypted text
    var stringOut = s_encryptionMethod().Item1.ToLower();
    // the alphabet of the encryption key
    var alphabetKey = s_encryptionMethod().Item2;
    // the reverse alphabet of the encryption key
    var reverseAlphabetKey = new Dictionary<string, string>();
    // the result text
    var stringResult = string.Empty;

    // extended reverse alphabet
    foreach (var element in alphabetKey)
      reverseAlphabetKey[element.Value] = element.Key;
      
    // decryption of the text
    for (var i = 0; i < stringOut.Length; i++)
    {
      if (aloneSymbols.Contains(stringOut[i]))
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
      
    // writing the result text to a file
    using (var sw = new StreamWriter($"{Path}Result.txt")) { sw.Write(stringResult.ToLower()); }

    return stringResult.ToLower(); 
  }

    /* Main method */
  public static void Main()
  {
    // output encryption text
    Console.WriteLine($"Encryption text is:\n{s_encryptionMethod().Item1}\n");
    // output decryption text
    Console.WriteLine($"Decryption text is:\n{s_decryptionMethod()}");
  }
}
