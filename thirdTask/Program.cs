using System.IO;
using System;

namespace thirdTask
{
  internal abstract class Program
  {
    // files path
    private const string Path = @".\Contents\";

    // encryption method
    private static void _encryptionMethod()
    {
      string input;
      using (var stream = new StreamReader($"{Path}Input.txt"))
      {
        input = stream.ReadToEnd();
      }
      
      // reverses string
      var reversed = input.ToCharArray();
      Array.Reverse(reversed);
      var reversedString = new string(reversed);
      
      // on 6 blocks
      var length = reversedString.Length;
      using (var sw = new StreamWriter($"{Path}Out.txt"))
      {
        for (var i = 0; i < length; i += 6)
        {
          var remaining = Math.Min(6, length - i);
          var block = reversedString.Substring(i, remaining);
          sw.WriteLine(block);
          Console.WriteLine(block);
        }
      }
    }
    
    // main method
    public static void Main(string[] args) => _encryptionMethod();
  }
}
