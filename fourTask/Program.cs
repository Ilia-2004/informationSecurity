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

    return outputText;
  }

 

  /* Main method */
  public static void Main()
  {
    var inputText = "Input.txt";
    s_encryptionMethod(inputText);
  }
}