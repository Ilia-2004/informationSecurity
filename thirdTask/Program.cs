using System.IO;
using System;

namespace thirdTask;
internal abstract class Program
{
  /* Methods */
  // files path
  private const string Path = @".\Contents\";

  // encryption method
  private static string s_encryptionMethod()
  {
    /* Variables */
    // the input text
    var inputText = File.ReadAllText($"{Path}Input.txt");
    // reversing input text
    var reversedInputText = inputText.ToCharArray();
    Array.Reverse(reversedInputText);
    // the reverse input text
    var reversedInputTextString = new string(reversedInputText);
      
    // the output text
    var outputText = string.Empty;
    // the length of the reverse input text
    var lengthReversedInputTextString = reversedInputTextString.Length;
    // the variables of the output file
    using var sw = new StreamWriter($"{Path}Out.txt");
    
    // division into 6 blocks
    for (var i = 0; i < lengthReversedInputTextString; i += 6)
    {
      var remainingLength = Math.Min(6, lengthReversedInputTextString - i);
      var blockElementOutputText = reversedInputTextString.Substring(i, remainingLength);
      outputText += $"{blockElementOutputText}\n";
    }
    
    // writing output text to a file 
    sw.WriteLine(outputText);

    return outputText;
  }
    
  /* Main method */
  public static void Main() => Console.WriteLine($"Encryption text is:\n{s_encryptionMethod()}");
}