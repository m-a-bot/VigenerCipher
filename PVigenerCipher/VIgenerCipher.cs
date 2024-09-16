using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PVigenerCipher;

enum TypeAction
{
    Encrypt = 1,
    Decrypt = -1
}

public enum TypeAlphabet
{
    Rus,
    Eng
}

public class VIgenerCipher
{
    public static string Alphabet { get; set; } = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя0123456789";

    public static string Encrypt(string source, string key)
    {
        
        string encryptedText = Semetric(source, key);

        return encryptedText;
    }

    public static string Decrypt(string encryptedText, string key)
    {
        string decryptedText = Semetric(encryptedText, key, TypeAction.Decrypt);

        return decryptedText;
    }

    static string Semetric(string source, string key, TypeAction action = TypeAction.Encrypt)
    {
        int lengthSource = source.Length;

        int currentIndex = 0;
        int lengthKey = key.Length;

        string result = "";

        for (int i = 0, alphabetLength = Alphabet.Length; i < lengthSource; i++)
        {

            bool bukovkaIsUpper = char.IsUpper(source[i]);
            char bukovka = char.ToLower(source[i]);

            int index = Alphabet.IndexOf(bukovka);
            int indexKey = Alphabet.IndexOf(char.ToLower(key[currentIndex]));

            if (index == -1)
            {
                result += bukovka;
            }
            else
            {
                int newIndex = Tools.Mod(index + (int) action * indexKey, alphabetLength);

                result += bukovkaIsUpper ? char.ToUpper(Alphabet[newIndex]) : Alphabet[newIndex];

                currentIndex = (currentIndex + 1) % lengthKey;
            }

        }

        return result;
    }

}
