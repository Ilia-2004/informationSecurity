using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace informationSecurity;
internal abstract class Program
{
  #region VariablesAndConstants
  // путь к файлам 
  private const string Path = @".\Contents\";
  // единичные буквы шифрования
  private const string AloneLetters = "уъьяфаю";
  // вводимый текст 
  private static readonly string InputText = File.ReadAllText($"{Path}Input.txt");
  // содержание ключа
  private static readonly string StringKey = File.ReadAllText($"{Path}Key.txt");
  #endregion
  
  #region Methods
  /* Метод шифрования */
  private static (string, Dictionary<string, string>) s_encryptionMethod()
  {
    /* переменные */
    // разделённый ключ
    var arrayKey = StringKey.Split(',');
    // алфавит ключа
    var alphabetKey = arrayKey.ToDictionary(t => t.Split('-')[0].Trim(),
      t => t.Split('-')[1]);
    // переменная для зашифрованного текста
    var outputText = string.Empty; 
      
    // шифрование текста
    foreach (var symbol in InputText)
    {
      // проверяем, есть символ содержится в алфавите ключа
      if (alphabetKey.ContainsKey(Convert.ToString(symbol).ToUpper()))
        // добавляем зашифрованный символ в переменную
        outputText += alphabetKey[Convert.ToString(symbol).ToUpper()]; 
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

  /* Метод для расшифровки */
  private static string s_decryptionMethod()
  {
    /* переменные */
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
      if (AloneLetters.Contains(stringOut[i]))
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
  #endregion
  
  /* Главный метод */
  private static void Main()
  {
    // вывод зашифрованного текста
    Console.WriteLine($"Зашифрованный текст:\n{s_encryptionMethod().Item1}\n");
    // вывод расшифрованного текста
    Console.WriteLine($"Расшифрованный текст:\n{s_decryptionMethod()}");
  }
}