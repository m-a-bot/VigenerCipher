using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PVigenerCipher;

namespace HackVigenerCipher;

class HackVigener
{
    private int keyLength = -1;

    private string key="";

    private string _alphabet;

    private TypeAlphabet _typeAlphabet;

    private string _text;
    private string _copyText;

    public string Key { get => key; }
    public int KeyLength { get => keyLength; }

    public HackVigener(string alphabet, TypeAlphabet typeAlphabet, string text)
    {
        _alphabet = alphabet;
        _typeAlphabet = typeAlphabet;
        _copyText = text;
        string Text = RemoveUnwantedSymbols(text.ToLower());
        keyLength = FindKeyLength(Text);

        key = FindKey(Text);
    }

    private int FindKeyLength(string text)
    {
        int newKey = -1;
        IndexCoincidence index = new IndexCoincidence(_alphabet, _typeAlphabet, text);

        newKey = index.FindKeyLength();

        if (newKey == -1)
        {
            throw new Exception("Dont find key length");
        }

        return newKey;
    }

    private string RemoveUnwantedSymbols(string encryptedText)
    {
        string neededSymbols = "";

        for (int i=0; i < encryptedText.Length; i++) 
        {
            if (_alphabet.IndexOf(encryptedText[i]) != -1)
            {
                neededSymbols += encryptedText[i];
            }
        }

        return neededSymbols;
    }

    public string Hack()
    {
        VIgenerCipher.Alphabet = _alphabet;

        return VIgenerCipher.Decrypt(_copyText, key);
    }

    private string FindKey(string text)
    {
        string newKey = "";

        for (int symbolShift=0; symbolShift < keyLength; symbolShift++) 
        {
            string subString = "";

            for (int buka=symbolShift; buka < text.Length; buka+=(int)keyLength)
            {
                subString += text[buka];
            }

            Histogram histogram = new Histogram(_alphabet);
            histogram.Fill(subString);

            int index = histogram.IndexSymbolWithMaxValue();

            if (_typeAlphabet == TypeAlphabet.Rus)
            {
                newKey += _alphabet[Tools.Mod(index - _alphabet.IndexOf('о'), _alphabet.Length)];
            }
            else
            {
                newKey += _alphabet[Tools.Mod(index - _alphabet.IndexOf('e'), _alphabet.Length)];
            }
        }
        if (newKey == "")
        {
            throw new Exception("Ключ не найден");
        }

        return newKey;
    }

}
