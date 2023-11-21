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
       Console.WriteLine(string.Format($"Encryption text is: {_encryptionMethod()}"));
       _decryptionMethod();
    }
  }
}


