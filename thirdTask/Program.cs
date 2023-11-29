using System.IO;
using System;

namespace thirdTask;
internal abstract class Program
{
  // путь к файлам
  private const string Path = @".\Contents\";

  /* Методы */
  // метод шифрования
  private static string s_encryptionMethod()
  {
    /* переменные */
    // вводимый текст
    var inputText = File.ReadAllText($"{Path}Input.txt");
    // создаём массив из текста
    var reversedInputText = inputText.ToCharArray();
    // переварачиваем вводимый текст
    Array.Reverse(reversedInputText);
    // присваеваем изменённый текст
    var reversedInputTextString = new string(reversedInputText);
    // переменная для зашифрованного текста
    var outputText = string.Empty;
    // длина изменённого текста
    var lengthReversedInputTextString = reversedInputTextString.Length;
    // присваиваем файл для зашифрованного текста
    using var sw = new StreamWriter($"{Path}Out.txt");
    
    // разбиваем текст на 6 блоков
    for (var i = 0; i < lengthReversedInputTextString; i += 6)
    {
      // 
      var remainingLength = Math.Min(6, lengthReversedInputTextString - i);
      // 
      var blockElementOutputText = reversedInputTextString.Substring(i, remainingLength);
      // присваиваем зашифрованный текст
      outputText += $"{blockElementOutputText}\n";
    }
    
    // добавляем зашифрованный текст в файл
    sw.WriteLine(outputText);

    // возвращаем зашифрованный текст
    return outputText;
  }
    
  /* Главный метод */
  public static void Main() => Console.WriteLine($"Зашифрованный текст:\n{s_encryptionMethod()}");
}