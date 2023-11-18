using System;
using System.IO;
using System.Linq;

namespace informationSecurity
{
  internal abstract class Program
  {
    private const string Path = @".\Contents\";

    // encryption method
    private static string _encryptionMethod()
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
      
      return strOut.ToUpper();
    }

    // decryption method
    private static string _decryptionMehtod() 
    {
        
      return "str";
    }

    // main method 
    public static void Main(string[] args)
    {
       Console.WriteLine($"Encryption text is: {_encryptionMethod()}");
    }
  }
}