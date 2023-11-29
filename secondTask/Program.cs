using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace secondTask;
internal abstract class Program
{
  // путь к файлам 
  private const string Path = @".\Contents\";

  // метод для подсчёта частотности символов в тексте
  private static Dictionary<char, double> s_frequencyMethod(string fileName) 
  {
    /* переменные */ 
    // присваивание русского алфавита
    var alphabet = File.ReadAllText($"{Path}Alphabet.txt");                              
    // чтение исходного файла
    var contentInputText = File.ReadAllText($"{Path}{fileName}").ToUpper();
    // общее колличество букв
    // список букв и их количество
    var countLetters = new Dictionary<char, int>();
    // список букв и их частотность
    var frequencyLetters = new Dictionary<char, double>();
    // список отсортерованных букв по их частотности 

    // подсчёт букв в тексте
    foreach (var letter in contentInputText.Where(letter => 
               letter >= 32).Where(letter => alphabet.Contains(letter)))
    {
      if (countLetters.ContainsKey(letter))
        countLetters[letter]++;
      else 
        countLetters.Add(letter, 1);
    }

    // подсчёт частотности
    var numberLetters = countLetters.Sum(x => x.Value);
    foreach (var letter in countLetters)
      frequencyLetters[letter.Key] = Math.Round((letter.Value / (double)numberLetters) * 100, 2);

    // сортировка словаря
    var sortedFrequencyLetters = frequencyLetters.OrderByDescending(x => x.Value);
    var sortedFrequencyLettersDictionary = sortedFrequencyLetters.ToDictionary(x => x.Key, x => x.Value);

    return sortedFrequencyLettersDictionary;
  }

  // метод расшифровки 
  private static (string, string) s_decryptionMethod(string file, string file1)
  {
    var fileContent = File.ReadAllText($"{Path}{file}").ToUpper();
    var k = new Dictionary<char, char>
    {
      { 'В', ' ' },
      { 'Л', 'о' },
      { 'Ц', 'н' },
      { 'Ф', 'а' },
      { 'А', 'и' },
      { 'С', 'т' },
      { 'И', 'д' },
      { 'Ш', 'у' },
      { 'Ы', 'к' },
      { 'У', 'в' },
      { 'Щ', 'г' },
      { 'Е', 'э' },
      { 'Г', 'у' },
      { 'Э', 'ж' },
      { 'Ъ', 'ы' },
      { 'Ж', 'р' },
      { 'Ч', 'з' },
      { 'О', 'с' },
      { 'Я', 'п' },
      { 'Б', 'х' },
      { 'Д', 'й' },
      { 'Ь', 'л' },
      { 'З', 'м' },
      { 'Ю', 'ь' },
      { 'Н', 'б' },
      { ' ', 'ю' },
      { 'К', 'я' },
      { 'Т', 'ш' },
      { 'Х', 'щ' },
      { 'П', 'ч' },
      { 'Й', 'ц' }
    };
        
    var decryptedText = new StringBuilder();
    foreach (var c in fileContent)
      decryptedText.Append(k.TryGetValue(c, out var value) ? value : c);

    var fileContent1 = File.ReadAllText($"{Path}{file1}").ToUpper();
    var k1 = new Dictionary<char, char>
    {
      {'П', 'в'},
      {'Й', 'е'},
      {'С', 'д'},
      {'Ж', 'н'},
      {'Х', 'и'},
      {'З', 'о'},
      {'Б', 'с'},
      {'Ш', 'ч'},
      {'Д', 'л'},
      {'Г', 'к'},
      {'Р', 'г'},
      {'Н', 'а'},
      {'Е', 'м'},
      {'И', 'п'},
      {'Ф', 'ь'},
      {'Ь', 'ю'},
      {'В', 'т'},
      {'А', 'р'},
      {'О', 'б'},
      {'У', 'ы'},
      {'К', 'ж'},
      {'Э', 'я'},
      {'Ч', 'ц'},
      {'М', 'у'},
      {'Ы', 'э'},
      {'Ц', 'й'},
      {'Ъ', 'щ'},
      {'Щ', 'ш'},
      {'Л', 'з'},
      {'Ю', 'ф'},
      {'Я', 'х'},
      {'Т', 'ъ'},
    };

    var decryptedText1 = string.Empty;
    foreach (var c in fileContent1)
    {
      if (k1.TryGetValue(c, out var value))
        decryptedText1 += value;
      else
        decryptedText1 += c;
    }

    return (decryptedText.ToString(), decryptedText1);
  }

  /* Главный метод */
  public static void Main()
  {
    const string fileName = "input1.txt";
    const string fileName1 = "input2.txt";
    Console.WriteLine("Counting letters in the first text:");
    foreach (var sym in s_frequencyMethod(fileName1))
      Console.WriteLine(sym.Key + " " + sym.Value);

    Console.WriteLine("Method second");
    Console.WriteLine(s_decryptionMethod(fileName, fileName1).Item1);
    //Console.WriteLine(s_decryptionMethod(fileName, fileName1).Item2);
  }
}