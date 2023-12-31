﻿// /**
//  * This file is part of: ClickyCursors-GMTK2023
//  * Created: 09.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

namespace F4B1.UI.Shop
{
    public static class NumberFormatter
    {
        public static string FormatNumberWithLetters(double number)
        {
            string[] suffixes = { "", "K", "M", "B", "T", "Q", "S" };
            var suffixIndex = 0;

            while (number >= 1000 && suffixIndex < suffixes.Length - 1)
            {
                number /= 1000;
                suffixIndex++;
            }

            return number.ToString("0.#") + suffixes[suffixIndex];
        }
    }
}