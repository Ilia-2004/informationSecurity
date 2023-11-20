using System;
using System.IO;

namespace secondTask
{
  internal abstract class Program
  {
    // files path
    private const string Path = @".\Contents\";
    public static void Main(string[] args)
    {
            //Console.Write("Введите строку: ");
            //var input = Console.ReadLine();
            var input = string.Empty;
            var strOut = string.Empty;
            using (var stream = new StreamReader($"{Path}Input.txt"))
            {
                input = stream.ReadToEnd();
                //arrKey = strKey.Split(',');
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