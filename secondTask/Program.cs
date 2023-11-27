using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace secondTask;
internal abstract class Program
{
  // files path
  private const string Path = @".\Contents\";

  // the method of calculating the frequency of letters in a text
  private static Dictionary<char, double> s_frequencyMethod(string fileName) 
  {
    /* переменные */ 
    // присваивание русского алфавита
    var alphabet = File.ReadAllText($"{Path}Alphabet.txt");                              
    // чтение исходного файла
    var contentInputText = File.ReadAllText($"{Path}{fileName}").ToUpper();
    // общее колличество букв
    var numberLetters = 0;
    // список букв и их количество
    var countLetters = new Dictionary<char, int>();
    // список букв и их частотность
    var frequencyLetters = new Dictionary<char, double>();
    // список отсортерованных букв по их частотности 
    var sortedFrequencyLettersDictionary = new Dictionary<char, double>();

    // подсчёт букв в тексте
    foreach (var sym in contentInputText)
    {
      var letter = sym;
      if ((int)letter < 32) continue;
      if (alphabet.Contains(letter))
      {
        if (countLetters.ContainsKey(letter))
          countLetters[letter]++;
        else 
          countLetters.Add(letter, 1);
      }
    }

    // подсчёт частотности
    numberLetters = countLetters.Sum(x => x.Value);
    foreach (var letter in countLetters)
      frequencyLetters[letter.Key] = Math.Round((letter.Value / (double)numberLetters) * 100, 2); ;

    // сортировка словаря
    var sortedFrequencyLetters = frequencyLetters.OrderByDescending(x => x.Value);
    sortedFrequencyLettersDictionary = sortedFrequencyLetters.ToDictionary(x => x.Key, x => x.Value);

    return sortedFrequencyLettersDictionary;
  }

  // decryption method
  private static (string, string) s_decryptionMethod(string file, string file1)
  {
        var FileContent = File.ReadAllText($"{Path}{file}").ToUpper();
        var K = new Dictionary<char, char>
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
        StringBuilder decryptedText = new StringBuilder();
        foreach (var c in FileContent)
        {
            if (K.ContainsKey(c))
            {
                decryptedText.Append(K[c]);
            }
            else
            {
                decryptedText.Append(c);
            }
        }


        var FileContent1 = File.ReadAllText($"{Path}{file1}").ToUpper();
        var K1 = new Dictionary<char, char>
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

        string decryptedText1 = string.Empty;
        foreach (var c in FileContent1)
        {
            if (K1.ContainsKey(c))
            {
                decryptedText1 += K1[c];
            }
            else
            {
                decryptedText1 += c;
            }
        }


        return (decryptedText.ToString(), decryptedText1);
    }

  /* Main method */
  public static void Main()
  {
    var fileName = "input1.txt";
    var fileName1 = "input2.txt";
    Console.WriteLine("Counting letters in the first text:");
    foreach (var sym in s_frequencyMethod(fileName1))
      Console.WriteLine(sym.Key + " " + sym.Value);

    Console.WriteLine("Method second");
    //Console.WriteLine(s_decryptionMethod(fileName, fileName1).Item1);
    Console.WriteLine(s_decryptionMethod(fileName, fileName1).Item2);
  }
}