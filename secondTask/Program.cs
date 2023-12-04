using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace secondTask;
internal abstract class Program
{
    #region VariablesAndConstants
    // путь к файлам 
    private const string Path = @".\Contents\";
    // присваивание русского алфавита
    private static readonly string Alphabet = File.ReadAllText($"{Path}Alphabet.txt");
    #endregion

    #region Methods
    // метод для подсчёта частотности символов в тексте
    private static Dictionary<char, double> s_frequencyMethod(string fileName)
    {
        /* переменные */
        // чтение исходного файла
        var contentInputText = File.ReadAllText($"{Path}{fileName}").ToUpper();
        // список букв и их количество
        var countLetters = new Dictionary<char, int>();
        // список букв и их частотность
        var frequencyLetters = new Dictionary<char, double>();
        // список отсортерованных букв по их частотности 

        // подсчёт букв в тексте
        foreach (var letter in contentInputText.Where(letter =>
                   letter >= 32).Where(letter => Alphabet.Contains(letter))) // const
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

    var decryptedText = new StringBuilder();
        foreach (var c in fileContent)
            decryptedText.Append(k.TryGetValue(c, out var value) ? value : c);

        var fileContent1 = File.ReadAllText($"{Path}{file1}").ToUpper();
        var k1 = new Dictionary<char, char>
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

        var decryptedText1 = string.Empty;
        foreach (var c in fileContent1)
        {
            if (k1.TryGetValue(c, out var value))
                decryptedText1 += value;
            else
                decryptedText1 += c;
        }

        return (decryptedText.ToString(), decryptedText1.ToString());
    }
    #endregion

    /* Главный метод */
    public static void Main()
    {
        const string fileName = "input1.txt";
        const string fileName1 = "input2.txt";
        Console.WriteLine("Подсчёт частотности букв:");
        foreach (var sym in s_frequencyMethod(fileName1))
            Console.WriteLine(sym.Key + " " + sym.Value);

        Console.WriteLine("\nРасшифровка текстов:");
        Console.WriteLine($"{s_decryptionMethod(fileName, fileName1).Item1}\n");
        Console.WriteLine(s_decryptionMethod(fileName, fileName1).Item2);
    }
}