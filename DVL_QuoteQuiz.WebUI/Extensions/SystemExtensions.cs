﻿using System;
using System.Collections.Generic;

namespace DVL_QuoteQuiz.WebUI.Extensions
{
    public static class SystemExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rn = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rn.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
