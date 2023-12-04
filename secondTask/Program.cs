using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace secondTask;
internal abstract class Program
{
  #region VariablesAndConstants
  // код специальных символов
  private const int CodeSymbol = 32; 
  // путь к файлам 
  private const string Path = @".\Contents\";
  // присваивание русского алфавита
  private static readonly string Alphabet = File.ReadAllText($"{Path}Alphabet.txt");
  #endregion

  #region Methods
  /* Метод для подсчёта частотности символов в тексте */
  private static Dictionary<char, double> s_frequencyMethod(string fileName)
  {
    /* переменные */
    // чтение исходного файла
    var contentInputText = File.ReadAllText($"{Path}{fileName}").ToUpper();
    // список букв и их количество
    var countLetters = new Dictionary<char, int>();
    // список букв и их частотность
    var frequencyLetters = new Dictionary<char, double>();

    // подсчёт букв в тексте
    foreach (var letter in contentInputText.Where(letter => letter >= CodeSymbol).Where(letter => Alphabet.Contains(letter)))
    {
      // если буква есть в словаре
      if (countLetters.ContainsKey(letter))
        // прибавить колличество
        countLetters[letter]++;
      else
        // добавляем букву в список
        countLetters.Add(letter, 1);
    }

    // подсчёт частотности
    var numberLetters = countLetters.Sum(x => x.Value);
    // подсчёт частоты букв
    foreach (var letter in countLetters)
      frequencyLetters[letter.Key] = Math.Round((letter.Value / (double)numberLetters) * 100, 2);

    // сортировка словаря
    var sortedFrequencyLetters = frequencyLetters.OrderByDescending(x => x.Value);
    // перевод списка в словарь
    var sortedFrequencyLettersDictionary = sortedFrequencyLetters.ToDictionary(x => x.Key, x => x.Value);
    
    // возвращение списка чатотности
    return sortedFrequencyLettersDictionary;
  }

  /* Метод расшифровки */
  private static (string, string) s_decryptionMethod(string file, string file1)
  {
    /* переменные */ 
    // первый вводимый текст 
    var inputText = File.ReadAllText($"{Path}{file}").ToUpper();
    // второй вводимый текст 
    var inputText1 = File.ReadAllText($"{Path}{file1}").ToUpper();
    // переменная для первого зашифрованного текста
    var decryptedText = string.Empty;
    // переменная для второго зашифрованного текста
    var decryptedText1 = string.Empty;
      
    // алфавит-ключ к шифрованию
    var alphabetKey = new Dictionary<char, char>
    {
      { 'В', ' ' }, { 'Л', 'о' }, { 'Ц', 'н' },
      { 'Ф', 'а' }, { 'А', 'и' }, { 'С', 'т' },
      { 'И', 'д' }, { 'Ш', 'у' }, { 'Ы', 'к' },
      { 'У', 'в' }, { 'Щ', 'г' }, { 'Е', 'э' },
      { 'Г', 'е' }, { 'Э', 'ж' }, { 'Ъ', 'ы' },
      { 'Ж', 'р' }, { 'Ч', 'з' }, { 'О', 'с' },
      { 'Я', 'п' }, { 'Б', 'х' }, { 'Д', 'й' },
      { 'Ь', 'л' }, { 'З', 'м' }, { 'Ю', 'ь' },
      { 'Н', 'б' }, { ' ', 'ю' }, { 'К', 'я' },
      { 'Т', 'ш' }, { 'Х', 'щ' }, { 'П', 'ч' },
      { 'Й', 'ц' }
    };
      
    // алфавит-ключ для второго текста 
    var alphabetKey1 = new Dictionary<char, char>
    {
      {'П', 'ч'}, {'Й', 'с'}, {'С', 'щ'},
      {'Ж', 'о'}, {'Х', 'э'}, {'З', 'п'},
      {'Б', 'й'}, {'Ш', 'а'}, {'Д', 'м'},
      {'Г', 'л'}, {'Р', 'е'}, {'Н', 'в'},
      {'Е', 'н'}, {'И', 'р'}, {'Ф', 'ь'},
      {'Ь', 'д'}, {'В', 'к'}, {'А', 'и'},
      {'О', 'ц'}, {'У', 'ы'}, {'К', 'т'},
      {'Э', 'е'}, {'Ч', 'я'}, {'М', 'ф'},
      {'Ы', 'г'}, {'Ц', 'ю'}, {'Ъ', 'в'},
      {'Щ', 'б'}, {'Л', 'у'}, {'Ю', 'ж'},
      {'Я', 'з'}, {'Т', 'ъ'},
    };

    // разшифровка первого текста
    foreach (var sym in inputText)
    {
      if (alphabetKey.TryGetValue(sym, out var value))
        decryptedText += value;
      else
        decryptedText += sym;
    }
      
    // расшифровка второго текста
    foreach (var sym in inputText1)
    {
      if (alphabetKey1.TryGetValue(sym, out var value))
        decryptedText1 += value;
      else
        decryptedText1 += sym;
    }

    File.WriteAllText($"{Path}Result1.txt", decryptedText);
    File.WriteAllText($"{Path}Result2.txt", decryptedText1);
    
    // возвращение расшифрованных текстов
    return (decryptedText, decryptedText1);
  }
  #endregion

  /* Главный метод */
  public static void Main()
  {
    /* константы */
    // первый текст
    const string fileName = "input1.txt";
    // второй текст
    const string fileName1 = "input2.txt";

    // вывод частотности первого текста
    Console.WriteLine("\nПодсчёт частотности букв в первом тексте:");
    foreach(var sym in s_frequencyMethod(fileName))
      Console.WriteLine($"{sym.Key} {sym.Value}");
    // вывод частотности второго текста
    Console.WriteLine("\nПодсчёт частотности букв во втором тексте:");
    foreach(var sym in s_frequencyMethod(fileName1))
      Console.WriteLine($"{sym.Key} {sym.Value}");
      
    // вывод расшифровки первого и второго текста
    Console.WriteLine("\nРасшифровка текстов:");
    Console.WriteLine($"{s_decryptionMethod(fileName, fileName1).Item1}\n");
    Console.WriteLine(s_decryptionMethod(fileName, fileName1).Item2);
  }
}