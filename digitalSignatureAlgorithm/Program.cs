using System;
using System.IO;
using System.Security.Cryptography;
class DSAEncryptionExample
{
    static byte[] EncryptData(byte[] data, DSAParameters publicKey)
    {
        using (DSACryptoServiceProvider dsa = new DSACryptoServiceProvider())
        {
            dsa.ImportParameters(publicKey);
            byte[] encryptedData = dsa.Encrypt(data, false);
            return encryptedData;
        }
    }
    static byte[] DecryptData(byte[] encryptedData, DSAParameters privateKey)
    {
        using (DSACryptoServiceProvider dsa = new DSACryptoServiceProvider())
        {
            dsa.ImportParameters(privateKey);
            byte[] decryptedData = dsa.Decrypt(encryptedData, false);
            return decryptedData;
        }
    }
    static void Main()
    {
        string filePath = "example.txt";
        string publicKeyFile = "publicKey.xml";
        string privateKeyFile = "privateKey.xml";
        // Генерация ключевой пары
        using (DSACryptoServiceProvider dsa = new DSACryptoServiceProvider())
        {
            File.WriteAllText(publicKeyFile, dsa.ToXmlString(false));
            File.WriteAllText(privateKeyFile, dsa.ToXmlString(true));
        }
        // Чтение файлов ключей
        string publicKeyXml = File.ReadAllText(publicKeyFile);
        string privateKeyXml = File.ReadAllText(privateKeyFile);
        DSACryptoServiceProvider dsaPublic = new DSACryptoServiceProvider();
        dsaPublic.FromXmlString(publicKeyXml);
        DSACryptoServiceProvider dsaPrivate = new DSACryptoServiceProvider();
        dsaPrivate.FromXmlString(privateKeyXml);
        // Чтение данных из файла
        byte[] data = File.ReadAllBytes(filePath);
        // Шифрование данных с помощью открытого ключа
        byte[] encryptedData = EncryptData(data, dsaPublic.ExportParameters(false));
        // Запись зашифрованных данных в файл
        File.WriteAllBytes("encryptedFile.txt", encryptedData);
        // Расшифрование данных с помощью закрытого ключа
        byte[] decryptedData = DecryptData(encryptedData, dsaPrivate.ExportParameters(true));
        // Запись расшифрованных данных в файл
        File.WriteAllBytes("decryptedFile.txt", decryptedData);
    }
}
