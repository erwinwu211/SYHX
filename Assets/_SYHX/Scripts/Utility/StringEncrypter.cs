using System;
using System.Text;
using System.Security.Cryptography;


/// <summary>
/// 加密解密用
/// </summary>
public static class StringEncryptor
{
    public static string Encrypt(string source)
    {
        var rijndael = new RijndaelManaged();

        GenerateKey(rijndael);

        var bytesSource = Encoding.UTF8.GetBytes(source);

        var encryptor = rijndael.CreateEncryptor();

        var bytesEncrypt = encryptor.TransformFinalBlock(bytesSource, 0, bytesSource.Length);

        encryptor.Dispose();

        return Convert.ToBase64String(bytesEncrypt);
    }

    public static string Decrypt(string source)
    {
        var rijndael = new RijndaelManaged();

        GenerateKey(rijndael);

        var bytesSource = Convert.FromBase64String(source);

        var decryptor = rijndael.CreateDecryptor();

        var bytesDecrypt = decryptor.TransformFinalBlock(bytesSource, 0, bytesSource.Length);

        decryptor.Dispose();

        return Encoding.UTF8.GetString(bytesDecrypt);
    }

    private static void GenerateKey(RijndaelManaged rijndael)
    {
        var salt = Encoding.UTF8.GetBytes("6AKsxyzuWz^YDc46");

        var deriveBytes = new Rfc2898DeriveBytes("rQX8!sRgNcMtRRf5", salt, 1000);

        rijndael.Key = deriveBytes.GetBytes(rijndael.KeySize / 8);

        rijndael.IV = deriveBytes.GetBytes(rijndael.BlockSize / 8);
    }
}

