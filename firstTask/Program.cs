using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace informationSecurity;
internal abstract class Program
{
  // путь к файлам 
  private const string Path = @".\Contents\";

  /* Методы */
  // метод шифрования
  private static (string, Dictionary<string, string>) s_encryptionMethod()
  {
    /* переменные */
    // вводимый текст 
    var inputText = File.ReadAllText($"{Path}Input.txt");
    // содержание ключа
    var stringKey = File.ReadAllText($"{Path}Key.txt");
    // разделённый ключ
    var arrayKey = stringKey.Split(',');
    // алфавит ключа
    var alphabetKey = arrayKey.ToDictionary(t => t.Split('-')[0].Trim(),
      t => t.Split('-')[1]);
    // переменная для зашифрованного текста
    var outputText = string.Empty; 
      
    // шифрование текста
    foreach (var symbol in inputText)
    {
      // проверяем, есть символ содержится в алфавите ключа
      if (alphabetKey.ContainsKey(Convert.ToString(symbol))) 
        // добавляем зашифрованный символ в переменную
        outputText += alphabetKey[Convert.ToString(symbol)]; 
      else
        // иначе добавляем "-" в переменную
        outputText += '-';
    }

    // создание переменной файла для зашифрованного текста
    using var sw = new StreamWriter($"{Path}Out.txt");
    // добавление текста в файл
    sw.Write(outputText);
    
    // возвращаем зашифрованный текст и алфавит ключа
    return (outputText.ToUpper(), alphabetKey);
  }

  // метод для расшифровки
  private static string s_decryptionMethod()
  {
    /* переменные */
    // единичные буквы шифрования
    const string aloneLetters = "уъьяфаю";
    // зашифрованный текст
    var stringOut = s_encryptionMethod().Item1.ToLower();
    // алфавит шифрования
    var alphabetKey = s_encryptionMethod().Item2;
    // словарь для ключа дешифровки
    var reverseAlphabetKey = new Dictionary<string, string>();
    // переменная для расшифрованного текста
    var stringResult = string.Empty;

    // меняем алфавит шифрования
    foreach (var element in alphabetKey)
      // присваиваем содержание к ключу, а сам ключ к содержанию
      reverseAlphabetKey[element.Value] = element.Key;
      
    // дешифруем текст
    for (var i = 0; i < stringOut.Length; i++)
    {
      // првоеряем, есть ли буква есть в списке единичных букв 
      if (aloneLetters.Contains(stringOut[i]))
      { 
        // проверяем, является ли символ буквой "а"
        if (stringOut[i] == 'а')
        {
          // проверяем, является ли последующий символ буквой "м"
          if (stringOut[i + 1] == 'м') 
          {
            // составляем последовательность символов для дешифровки
            var key = $"{stringOut[i]}{stringOut[++i]}";
            // присваиваем результат последовательностей символов
            stringResult += $"{reverseAlphabetKey[key]}";
            // прибавляем индекс
            i++;
          }
          else 
            // иначе присваевам символ дешифровки под "а"
            stringResult += reverseAlphabetKey[Convert.ToString(stringOut[i])]; 
        }
        else 
          // если символ не является "а", то присваеваем символ дешифровки 
          stringResult += reverseAlphabetKey[Convert.ToString(stringOut[i])];
      }
      // проверяем, является ли символ "-"
      else if (stringOut[i] == '-') 
        // присваеваем пробел
        stringResult += " ";
      else
      {
        // составляем ключ дешифровки из символов
        var key = $"{stringOut[i]}{stringOut[++i]}";
        // присваемваем элемент дешифровки 
        stringResult += $"{reverseAlphabetKey[key]}";
      }
    }

    // создаём переменную файла для расшифрованного текста
    using var sw = new StreamWriter($"{Path}Result.txt");
    // заполняем файл
    sw.Write(stringResult.ToLower());

    // возвращаем расшифрованный текст
    return stringResult.ToLower(); 
  }

    /* Главный метод */
  public static void Main()
  {
    // вывод зашифрованного текста
    Console.WriteLine($"Зашифрованный текст:\n{s_encryptionMethod().Item1}\n");
    // вывод расшифрованного текста
    Console.WriteLine($"Расшифрованный текст:\n{s_decryptionMethod()}");
  }
}
