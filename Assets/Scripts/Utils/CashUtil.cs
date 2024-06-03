using System;
using UnityEngine;

namespace Utils
{
    public static class CashUtil
    { 
        public static string NiceCash(int cash)
        {
            string[] suffixes = { "", "k", "m", "b" };
            int suffixIndex;
            int digits;
            if (cash == 0)
            {
                suffixIndex = 0;
                digits = cash.ToString().Length;
            }
            else if (cash > 0)
            {
                suffixIndex = (int)(Mathf.Log10(cash) / 3);
                digits = cash.ToString().Length;
            }
            else
            {
                suffixIndex = (int)(Mathf.Log10(Math.Abs(cash)) / 3);
                digits = Math.Abs(cash).ToString().Length;
            }

            var dividor = Mathf.Pow(10, suffixIndex * 3);
            var text = "";

            if (digits < 4)
                text = (cash / dividor).ToString() + suffixes[suffixIndex];
            else if (digits >= 4 && digits < 7)
                text = (cash / dividor).ToString("F1") + suffixes[suffixIndex];
            else
                text = (cash / dividor).ToString("F2") + suffixes[suffixIndex];
            return text;
        }
        
    }
}