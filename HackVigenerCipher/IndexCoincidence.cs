using PVigenerCipher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace HackVigenerCipher;

class IndexCoincidence
{
    private int alphabetLength;

    private string _text;

    private TypeAlphabet _typeAlphabet;

    private Histogram histogram;

    public IndexCoincidence(string alphabet, TypeAlphabet typeAlphabet, string text)
    {
        alphabetLength = alphabet.Length;
        _typeAlphabet = typeAlphabet;

        _text = text;
        histogram = new Histogram(alphabet);
    }
    public const double RusCoincidence = 0.0553;

    public const double EngCoincidence = 0.0667;

    public const double polyRus = 0.03846;

    public const double polyEng = 0.03125;

    public int FindKeyLength()
    {
        int keyLength = -1;

        double eps = 0.005;

        for (int key = 1; key < _text.Length / 2; key++)
        {

            string subText = "";

            for (int i = 0; i < _text.Length; i += key)
            {
                subText += _text[i];
            }

            histogram.Fill(subText);
            
            double index = FindGeneralIndexCoincidence(subText.Length, subText);


            if (_typeAlphabet == TypeAlphabet.Rus)
            {
                if (Math.Abs(index - RusCoincidence) <= eps)// || Math.Abs(index - polyRus) <= eps)
                {
                    keyLength = key;
                    break;
                }
            }
            else
            {
                if (Math.Abs(index - EngCoincidence) <= eps)// || Math.Abs(index - polyEng) <= eps)
                {
                    keyLength = key;
                    break;
                }
            }
        }

        return keyLength;
    }

    private double FindGeneralIndexCoincidence(long textLength, string text)
    {
        double S = 0;

        for (int i=0; i < alphabetLength; i++) 
        {
            long fi = histogram.GetItem(i);

            double res = fi * (fi - 1) * 1.0d / ( textLength * (textLength - 1) ) ;

            S += res;
        }

        return S;
    }

    private double FindOpenTextIndexCoincidence(long textLength, string text)
    {
        double S = 0;

        for (int i = 0; i < alphabetLength; i++)
        {
            double pi = histogram.GetItemProbability(i);

            S += pi * pi;
        }

        return S;
    }

    private double FindRandomTextIndexCoincidence(long alphabetLength)
    {
        double S = 1.0d / alphabetLength;

        return S;
    }

    //private void decrypt(object sender, EventArgs e)
    //{
        
    //    int len;
    //    for (len = 1; len < text.Length / 2; len++)
    //    {
    //        string str = "";
    //        for (int i = 0; i < text.Length; i += len)
    //        {
    //            str += text[i];
    //        }

    //        double index = 0;
    //        if (lang == "ru")
    //            foreach (char c in ru)
    //            {
    //                double f = str.Count(g => g == c);
    //                index += f * (f - 1) / str.Length / (str.Length - 1);
    //            }
    //        else
    //            foreach (char c in en)
    //            {
    //                double f = str.Count(g => g == c);
    //                index += f * (f - 1) / str.Length / (str.Length - 1);
    //            }

    //        // Console.WriteLine("{0}: {1}, {2}", len, indexMatches, index);

    //        if (Math.Abs(index - indexMatches) < 0.005)
    //            break;
    //    }

    //    for (int i = 0; i < len; i++)
    //    {
    //        string str = "";
    //        for (int j = i; j < text.Length; j += len)
    //            str += text[j];

    //        if (lang == "en")
    //        {
    //            int[] mas = new int[en.Length];
    //            foreach (char c in str)
    //                mas[en.IndexOf(c)] += 1;
    //            key += en[((Array.IndexOf(mas, mas.Max()) - 4) + en.Length) % en.Length];
    //        }
    //        else
    //        {
    //            int[] mas = new int[ru.Length];
    //            foreach (char c in str)
    //                mas[ru.IndexOf(c)] += 1;
    //            key += ru[((Array.IndexOf(mas, mas.Max()) - 15) + ru.Length) % ru.Length];
    //        }

    //    }

    //    decryptedBox.Text = vigenere(text, key, -1);
    //    decryptKeyBox.Text = key;
    //}
}
