using System;
using System.IO;
using System.Collections.Generic; 

namespace informationSecurity;
internal abstract class Program 
{ 
  // путь к файлам
  private const string Path = @".\Contents\";
  
  private static readonly HashSet<int> Prime = new HashSet<int>();
  private static int? _publicKey = null;
  private static int? _privateKey = null;
  private static int? _n = null;
  private static readonly Random Random = new Random();

  private static void s_primeFiller()
  {
    var sieve = new bool[250];
    for (var i = 0; i < 250; i++)
      sieve[i] = true;
 
    sieve[0] = false;
    sieve[1] = false;
 
    for (var i = 2; i < 250; i++)
      for (var j = i * 2; j < 250; j += i)
        sieve[j] = false;
 
    for (var i = 0; i < sieve.Length; i++)
      if (sieve[i])
        Prime.Add(i);
  }

  private static void s_setKeys()
  {
    const int p = 2039;
    const int q = 4001;
    const int fi = (p - 1) * (q - 1);
    _n = p * q;
 
    var e = 2;
    while (true)
    {
      if (s_gcd(e, fi) == 1)
        break;
      e += 1;
    }
 
    _publicKey = e;
 
    var d = 2;
    while (true)
    {
      if ((d * e) % fi == 1)
        break;
      d += 1;
    }
 
    _privateKey = d;
  }

  private static int s_encrypt(int message)
  {
    var e = _publicKey.Value;
    var encrypted_text = 1;
    while (e > 0)
    {
      encrypted_text *= message;
      encrypted_text %= _n.Value;
      e -= 1;
    }
    return encrypted_text;
  }

  private static int s_decrypt(int encrypted_text)
  {
    var d = _privateKey.Value;
    var decrypted = 1;
    while (d > 0)
    {
      decrypted *= encrypted_text;
      decrypted %= _n.Value;
      d -= 1;
    }
    return decrypted;
  }

  private static int s_gcd(int a, int b)
  {
    if (b == 0) return a;
    return s_gcd(b, a % b);
  }

  private static List<int> s_encoder(string message)
  {
    var encoded = new List<int>();
    foreach (var letter in message)
      encoded.Add(s_encrypt((int)letter));
    return encoded;
  }

  private static string s_decoder(List<int> encoded)
  {
    var s = "";
    foreach (var num in encoded)
      s += (char)s_decrypt(num);

    using var sw = new StreamWriter($"{Path}Result.txt");
    // добавление текста в файл
    sw.Write(s);
    
    return s;
  }
  
  /* Главный метод */
  private static void Main()
  {
    s_primeFiller();
    s_setKeys();
    var inputText = File.ReadAllText($"{Path}Input.txt");
 
    var encryptedText = s_encoder(inputText);
 
    Console.WriteLine("Вводимый текст:");
    Console.WriteLine(inputText);
    Console.WriteLine("\n\nЗакодированное сообщение:\n");
    Console.WriteLine(string.Join("", encryptedText));
    Console.WriteLine("\n\nРасшифрованное сообщение:\n");
    Console.WriteLine(s_decoder(encryptedText));
  }
}