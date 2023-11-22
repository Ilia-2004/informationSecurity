using System.IO;
using System;

namespace thirdTask
{
  internal abstract class Program
  {
    // files path
    private const string Path = @".\Contents\";

    // encryption method
    private static string s_encryptionMethod()
    {
      string inputText;
      using (var sr = new StreamReader($"{Path}Input.txt")) { inputText = sr.ReadToEnd(); }
      
      // reverses string
      var reversedInputText = inputText.ToCharArray();
      Array.Reverse(reversedInputText);
      var reversedInputTextString = new string(reversedInputText);
      
      // on 6 blocks
      var lengthReversedInputTextString = reversedInputTextString.Length;
      var outputText = string.Empty;
      using (var sw = new StreamWriter($"{Path}Out.txt"))
      {
        for (var i = 0; i < lengthReversedInputTextString; i += 6)
        {
          var remainingLength = Math.Min(6, lengthReversedInputTextString - i);
          var blockElementOutputText = reversedInputTextString.Substring(i, remainingLength);
          outputText += $"{blockElementOutputText}\n";
        }
        sw.WriteLine(outputText);
      }

      return outputText;
    }
    
    /* Main method */
    public static void Main() => Console.WriteLine($"Encryption text is:\n{s_encryptionMethod()}");
  }
}
