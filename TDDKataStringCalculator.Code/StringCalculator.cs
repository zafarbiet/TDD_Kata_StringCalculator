﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDKataStringCalculator.Code
{
    public class StringCalculator
    {
        public int Add(string number)
        {
            if (string.IsNullOrEmpty(number))
                return 0;
            var numberForSum = number.Replace('\n', ',').Split(',');
            ReturnDelimitedNumbers(ref numberForSum);
            var numberForAdd = numberForSum.Where(c=>!string.IsNullOrEmpty(c)).Select(int.Parse).ToArray();
            ValidateNegetiveNumbers(numberForAdd);
            return numberForAdd.Where(c => c < 1001).Sum();
        }

        private static void ValidateNegetiveNumbers(int[] numberForSum)
        {
            if (numberForSum.Any(c => c < 0))
                throw new Exception($"negatives not allowed {string.Join(" ", numberForSum.Where(c => c < 0))}");
        }

        private void ReturnDelimitedNumbers(ref string[] numberForSum)
        {
            if (numberForSum[0].StartsWith("//"))
            {
                var customDelimiter = numberForSum[0].Remove(0, 2).Distinct();
                foreach (var _delimiter in customDelimiter)
                {
                    numberForSum[1] = numberForSum[1].Replace(_delimiter, ',');
                }
                numberForSum = numberForSum[1].Split(',');
            }

        }
    }
}
