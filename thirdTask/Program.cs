using System.IO;
using System;

namespace thirdTask
{
  internal abstract class Program
  {
    // files path
    private const string Path = @".\Contents\";
    public static void Main(string[] args)
    {
      var input = string.Empty;
      using (var stream = new StreamReader($"{Path}Input.txt"))
      {
        input = stream.ReadToEnd();
      }

      // Переворачиваем строку
      var reversed = input.ToCharArray();
      Array.Reverse(reversed);
      var reversedString = new string(reversed);

      // Разбиваем строку на блоки по 6 символов
      var length = reversedString.Length;
      for (var i = 0; i < length; i += 6)
      {
        var remaining = Math.Min(6, length - i);
        var block = reversedString.Substring(i, remaining);
        Console.WriteLine(block);
      }
    }
  }
}
