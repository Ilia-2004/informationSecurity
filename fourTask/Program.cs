using System;
using System.IO;

namespace fourTask;
internal abstract class Program
{
  // files path
  private const string Path = @".\Contents\";

  /* Methods */
  private static string s_encryptionMethod(string nameFile)
  {
    var contentKey = File.ReadAllText($"{Path}Key.txt");
    var contentInputText = File.ReadAllText($"{Path}{nameFile}");
    var outputText = string.Empty;
    var keyIndex = 0; 
    
    for (int i = 0; i < contentInputText.Length; i++)
    {
      var a = (int)contentInputText[i];
      var b = (int)contentKey[keyIndex];
      int c = ((a & b) | (~a & ~b)) % 32 + 'а';
      
      outputText += (char)c;   
      keyIndex++;

      if (keyIndex >= contentKey.Length) keyIndex = 0;
    }

    using (var sw = new StreamWriter($"{Path}Out.txt")) { sw.Write(outputText.ToLower()); }

    return outputText;
  }

  // decryption method
  private static string s_decryptionMethod(string nameFile)
  {
    var contentKey = File.ReadAllText($"{Path}Key.txt");
    var contentInputText = s_encryptionMethod(nameFile);
    var resultText = string.Empty;
    var keyIndex = 0; 
    
    for (int i = 0; i < contentInputText.Length; i++)
    {
      var a = (int)contentInputText[i];
      var b = (int)contentKey[keyIndex];
      int c = ((a & b) | (~a & ~b)) % 32 + 'а';
      
      resultText += (char)c;   
      keyIndex++;

      if (keyIndex >= contentKey.Length) keyIndex = 0;
    }

    using (var sw = new StreamWriter($"{Path}Result.txt")) { sw.Write(resultText.ToLower()); }

    return resultText;
  }

  /* Main method */
  public static void Main()
  {
    var inputText = "Input.txt";
    Console.WriteLine(s_encryptionMethod(inputText));
    Console.WriteLine();
    Console.WriteLine(s_decryptionMethod(inputText));
  }
}