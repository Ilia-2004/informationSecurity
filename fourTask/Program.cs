using System;
using System.IO;

namespace fourTask;
internal abstract class Program
{
  // путь к файлу 
  private const string Path = @".\Contents\";

  /* Методы */
  // метод шифрования
  private static string s_encryptionMethod(string nameFile)
  {
    /* переменные */
    // содержание ключа
    var contentKey = File.ReadAllText($"{Path}Key.txt");
    // содержание текста ввода
    var contentInputText = File.ReadAllText($"{Path}{nameFile}");
    // обозначение переменной выводимого текста
    var outputText = string.Empty;
    // индекс текста ключа
    var keyIndex = 0; 
    
    /* решение */
    // цикл шифрования
    for (var i = 0; i < contentInputText.Length; i++)
    {
      // конвертируем нужные нам символы в число
      var a = (int)contentInputText[i];
      var b = (int)contentKey[keyIndex];

      // выполняем побитовую операцию сравнения и расчёта
      var c = ((a & b) | (~a & ~b)) % 32 + 'а';
      
      // присваеваем выводимому тексту символ,
      // конвертируя полученный код в "char"
      outputText += (char)c;   
      // увеличиваем индекс
      keyIndex++;

      // проверяем, не выходит ли инцекс за границы текста ключа
      if (keyIndex >= contentKey.Length) 
        // если да, то приравниваем индекс к 0 
        keyIndex = 0;
    }

    // создаём переменную файла
    using var sw = new StreamWriter($"{Path}Out.txt");
    // заполняем файл зашифрованным текстом
    sw.Write(outputText.ToLower());

    // возвращаем зашифрованный текст для вывода
    return outputText;
  }

  // метод расшифрования
  private static string s_decryptionMethod(string nameFile)
  {
    /* переменные */
    // содержание ключа
    var contentKey = File.ReadAllText($"{Path}Key.txt");
    // передаём зашифрованный текст
    var contentInputText = s_encryptionMethod(nameFile);
    // создаём переменную для расшифрованного текста
    var resultText = string.Empty;
    // индекс текста ключа
    var keyIndex = 0; 
    
    /* решение */
    for (var i = 0; i < contentInputText.Length; i++)
    {
      var a = (int)contentInputText[i];
      var b = (int)contentKey[keyIndex];
      var c = ((a & b) | (~a & ~b)) % 32 + 'а';
      
      resultText += (char)c;   
      keyIndex++;

      if (keyIndex >= contentKey.Length) keyIndex = 0;
    }

    using var sw = new StreamWriter($"{Path}Result.txt");
    sw.Write(resultText.ToLower());

    return resultText;
  }

  /* Главный метод */
  public static void Main()
  {
    // обозначаем константу названия файла,
    // в котором хранится исходный текст
    const string inputText = "Input.txt";

    // выводим зашифрованный текст и расшифрованный текст
    Console.WriteLine(s_encryptionMethod(inputText));
    Console.WriteLine();
    Console.WriteLine(s_decryptionMethod(inputText));
  }
}