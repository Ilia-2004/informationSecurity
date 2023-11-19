using System;

namespace secondTask
{
  internal abstract class Program
  {
    public static void Main(string[] args)
    {
      Console.Write("Введите строку: ");
      var input = Console.ReadLine();

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