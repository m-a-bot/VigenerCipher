using PVigenerCipher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HackVigenerCipher;

class Histogram
{
    private string _alphabet;

    private long[] counts;

    private long textLength = 0;

    public Histogram(string alphabet)
    {
        _alphabet = alphabet;

        counts = new long[alphabet.Length];

        for (int k=0; k < counts.Length; k++)
            counts[k] = 0;
    }

    public void Fill(string text)
    {
        for (int k = 0; k < counts.Length; k++)
            counts[k] = 0;

        textLength = text.Length;

        foreach (var bukovka in text)
        {
            int index = _alphabet.IndexOf(bukovka);

            counts[index]++;
        }
    }

    public int IndexSymbolWithMaxValue()
    {
        long maxCount = long.MinValue;

        char result = char.MinValue;

        int maxIndex = -1;

        char mBukva = char.MinValue;

        for (int index=0; index < _alphabet.Length; index++)
        {
            if (counts[index] > maxCount)
            {
                maxCount = counts[index];
                result = _alphabet[index];
                maxIndex = index;
            }
        }

        return maxIndex;
    }

    public long GetItem(int index) 
    {
        if (index < 0 || index >= counts.Length)
        {
            throw new ArgumentOutOfRangeException("index");
        }

        return counts[index];
    }

    public double GetItemProbability(int index)
    {
        if (index < 0 || index >= counts.Length)
        {
            throw new ArgumentOutOfRangeException("index");
        }

        double result = counts[index] * 1.0d / textLength;

        return result;
    }
}
