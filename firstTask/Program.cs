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
      string[] arrKey;
      string strInput;
      var strOut = string.Empty; 
      using (var stream = new StreamReader($"{Path}Key.txt"))
      {
        var strKey = stream.ReadToEnd();
        arrKey = strKey.Split(',');
      }
      var alphabet = arrKey.ToDictionary(t => t.Split('-')[0].Trim(), 
                                                              t => t.Split('-')[1]);

      using (var stream = new StreamReader($"{Path}Input.txt"))
      {
        strInput = stream.ReadToEnd().ToUpper();
      }

      foreach (var sym in strInput)
      {
        if (alphabet.ContainsKey(Convert.ToString(sym))) strOut += alphabet[Convert.ToString(sym)]; 
        else strOut += '-';
      }

      using (var sw = new StreamWriter($"{Path}Out.txt"))
      {
        sw.Write(strOut);
      }
      
      return (strOut.ToUpper(), alphabet);
    }

    // decryption method
    private static string _decryptionMethod()
    {
      var strOut = _encryptionMethod().Item1.ToLower().Split('-');
      var alphabet = _encryptionMethod().Item2;
      var strResult = string.Empty;

      foreach (var VARIABLE in strOut)
      {
        Console.WriteLine(VARIABLE);
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
      
      Console.WriteLine(strResult);
      
      return strResult.ToLower();
    }

    // main method 
    public static void Main(string[] args)
    {
       //Console.WriteLine($"Encryption text is: {_encryptionMethod()}");
       _decryptionMethod();
    }
  }
}


using System;
using System.IO;

class Program
{
  static void Main(string[] args)
  {
    string inputPath = "Input.txt";
    string outputPath = "Out.txt";
    string keyPath = "Key.txt";

    // Read the input text from the file
    string inputText = File.ReadAllText(inputPath);

    // Read the key from the file
    string key = File.ReadAllText(keyPath);

    // Convert the input text to uppercase
    inputText = inputText.ToUpper();

    // Replace the letters in the input text with the corresponding letters from the key
    string outputText = "";
    foreach (char c in inputText)
    {
      if (c >= 'А' && c <= 'Я')
      {
        outputText += key[c - 'А'];
      }
      else if (c == 'Ё')
      {
        outputText += key['Е' - 'А'];
      }
      else
      {
        outputText += c;
      }
    }

    // Write the output text to the file
    File.WriteAllText(outputPath, outputText);
  }
}

using System;
using System.IO;

class Program
{
  static void Main(string[] args)
  {
    string inputPath = "Out.txt";
    string outputPath = "Result.txt";
    string keyPath = "Key.txt";

    // Read the encoded text from the file
    string encodedText = File.ReadAllText(inputPath);

    // Read the key from the file
    string key = File.ReadAllText(keyPath);

    // Replace the letters in the encoded text with the corresponding letters from the key
    string decodedText = "";
    foreach (char c in encodedText)
    {
      if (c >= 'А' && c <= 'Я')
      {
        decodedText += (char)('А' + key.IndexOf(c));
      }
      else if (c == 'Ё')
      {
        decodedText += 'Е';
      }
      else
      {
        decodedText += c;
      }
    }

    // Convert the decoded text to lowercase
    decodedText = decodedText.ToLower();

    // Write the decoded text to the file
    File.WriteAllText(outputPath, decodedText);
  }
}
