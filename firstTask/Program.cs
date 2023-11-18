using System;
using System.IO;
using System.Collections.Generic;

namespace informationSecurity
{
  internal abstract class Program
  {
    private static string _path = @".\Contents\Key.txt";
    // encryption method
    private static string _encryptionMethod()
    {
      var alphabet = new Dictionary<char, string>();

      using (FileStream fstream = File.OpenRead(_path))
      {
        var strKey = fstream;
        Console.WriteLine(strKey);
      }
      return "str";
    }

    // decryption method
    private static string _decryptionMehtod() 
    {
        
      return "str";
    }

    // main method 
    public static void Main(string[] args)
    {
       _encryptionMethod();

       Console.WriteLine("Hello");
    }
  }
}