using UnityEngine;
using System;
using System.Text.RegularExpressions;

namespace Game
{
    [Serializable]
    public struct ScientificNotation
    {
        #region prefix
        //K M B T - далее алфавит перебираем. Подумать, как автоматизировать, может есть вариант (считать по количеству букавок в алфавите - первая цифра. Вторая цифра - остаток от деления от этого кол-ва.
        private static readonly string[] _prefix = { "K", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av"
                , "aw", "ax", "ay", "az", "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq" };

        private const string _regexPattern = @"(?<=\d)(?=[A-Za-z])";

        private const int _epsilon = 9;

        #endregion prefix

        [SerializeField] private double _mantissa;
        [SerializeField] private int _order;

        public double Mantissa
        {
            get => _mantissa;
            private set => _mantissa = value;
        }
        public int Order
        {
            get => _order;
            private set => _order = value;
        }

        public ScientificNotation(double mantissa, int order)
        {
            this = ConvertMantissa(mantissa);
            _order += order;
        }

        public static ScientificNotation FromString(string value)
        {
            if (string.IsNullOrEmpty(value))
                return new ScientificNotation();

            var partsValue = Regex.Split(value, _regexPattern);

            if (partsValue.Length == 0 || partsValue.Length > 2)
                throw new Exception("The value is not in the correct format");

            double mantissa = double.Parse(partsValue[0]);
            int order = 0;

            if (partsValue.Length != 1)
                order = GetOrderByPrefix(partsValue[1]);

            return new ScientificNotation(mantissa, order);
        }

        public static implicit operator ScientificNotation(double value)
        {
            return ConvertMantissa(value);
        }

        public static ScientificNotation operator +(ScientificNotation a, ScientificNotation b)
        {
            if (a.IsZero())
                return b;

            if (b.IsZero())
                return a;

            if (a.Order == b.Order)
                a.Mantissa += b.Mantissa;
            else if (a.Order > b.Order)
                a = Additional(a, b);
            else
                a = Additional(b, a);

            ScientificNotation newScn = a.Mantissa;

            if (newScn.Order != 0)
            {
                a.Mantissa = newScn.Mantissa;
                a.Order += newScn.Order;
            }

            return a;
        }

        private static ScientificNotation Additional(ScientificNotation scn1, ScientificNotation scn2)
        {
            int differenceExponent = scn1.Order - scn2.Order;

            if (differenceExponent > _epsilon)
            {
                return scn1;
            }
            else
            {
                scn1._mantissa += scn2.Mantissa * Math.Pow(0.1, differenceExponent);
                return scn1;
            }
        }

        public static ScientificNotation operator -(ScientificNotation a, ScientificNotation b)
        {
            if (b.IsZero())
                return a;

            int differenceExponent = a.Order - b.Order;

            if (differenceExponent < 0)
                return new ScientificNotation();

            if (differenceExponent > _epsilon)
                return a;

            a.Mantissa -= b.Mantissa * Math.Pow(0.1, differenceExponent);

            if (a.Mantissa < 0)
                return new ScientificNotation();

            ScientificNotation v = a.Mantissa;

            if (v.Order != 0)
            {
                a.Order += v.Order;
                a.Mantissa = v.Mantissa;
            }

            return a;
        }

        public static ScientificNotation operator *(ScientificNotation a, ScientificNotation b)
        {
            a.Mantissa *= b.Mantissa;
            a.Order += b.Order;

            ScientificNotation v = a.Mantissa;

            if (v.Order != 0)
            {
                a.Mantissa = v.Mantissa;
                a.Order += v.Order;
            }

            return a;
        }

        public static ScientificNotation operator *(ScientificNotation a, double b)
        {
            if (a.IsZero())
                return 0;

            a.Mantissa *= b;

            ScientificNotation v = a.Mantissa;

            if (v.Order != 0)
            {
                a.Order += v.Order;
                a.Mantissa = v.Mantissa;
            }

            return a;
        }

        public static ScientificNotation operator /(ScientificNotation a, ScientificNotation b)
        {
            if (a.IsZero())
                return 0;

            a.Mantissa /= b.Mantissa;
            a.Order -= b.Order;

            ScientificNotation v = a.Mantissa;

            if (v.Order != 0)
            {
                a.Mantissa = v.Mantissa;
                a.Order += v.Order;
            }

            return a;
        }

        public static bool operator >(ScientificNotation a, ScientificNotation b)
        {
            if (b.IsZero())
                return a.Mantissa > 0;

            if (a.Order == b.Order)
                return Math.Round(a.Mantissa, _epsilon) > Math.Round(b.Mantissa, _epsilon);

            return a.Order > b.Order;
        }

        public static bool operator <(ScientificNotation a, ScientificNotation b)
        {
            if (b.IsZero())
                return a.Mantissa < 0;

            if (a.Order == b.Order)
                return Math.Round(a.Mantissa, _epsilon) < Math.Round(b.Mantissa, _epsilon);

            return a.Order < b.Order;
        }

        public static bool operator >=(ScientificNotation a, ScientificNotation b)
        {
            if (b.IsZero())
                return a.Mantissa >= 0;

            if (a.Order == b.Order)
                return Math.Round(a.Mantissa, _epsilon) >= Math.Round(b.Mantissa, _epsilon);

            return a.Order > b.Order;
        }

        public static bool operator <=(ScientificNotation a, ScientificNotation b)
        {
            if (b.IsZero())
                return a.Mantissa <= 0;

            if (a.Order == b.Order)
                return Math.Round(a.Mantissa, _epsilon) <= Math.Round(b.Mantissa, _epsilon);

            return a.Order < b.Order;
        }

        public static ScientificNotation Pow(ScientificNotation value, int power)
        {
            if (power == 0)
                return 1;

            if (power == 1)
                return value;

            ScientificNotation powerScn = new ScientificNotation(value.Mantissa, value.Order);

            for (int i = 1; i < power; i++)
            {
                powerScn.Mantissa *= value.Mantissa;
                powerScn.Order += value.Order;

                ScientificNotation newScn = powerScn.Mantissa;

                if (newScn.Order != 0)
                {
                    powerScn.Mantissa = newScn.Mantissa;
                    powerScn.Order += newScn.Order;
                }
            }

            return powerScn;
        }

        public static ScientificNotation Pow(double value, int power)
        {
            ScientificNotation scn = value;

            return Pow(scn, power);
        }

        public ScientificNotation ToInt()
        {
            if (GetDecimalDigitsCount(Mantissa) > Order)
            {
                return new ScientificNotation()
                {
                    Mantissa = Math.Round(Mantissa, Math.Abs(Order)),
                    Order = Order
                };
            }

            return this;
        }

        public override string ToString()
        {
            if (_order < 5)
                return (Mantissa * Math.Pow(10, _order)).ToString("N0");

            int resudieExponent = _order % 3;
            int indexPrefix = ((_order / 3) - 1);

            if (resudieExponent != 2)
                indexPrefix--;

            if (indexPrefix >= _prefix.Length)
            {
                double value = _mantissa * Math.Pow(10, 3);
                return $"{value.ToString("N0")} e{_order - 3}";
            }

            double adjustedSignificand = _mantissa * Math.Pow(10, resudieExponent);
            string strValue = adjustedSignificand.ToString(resudieExponent == 2 ? "N0" : "N3");
            return $"{strValue}{_prefix[indexPrefix]}";
        }

        public bool IsZero()
        {
            if (Mantissa == 0)
                return true;

            return false;
        }

        public static float Procent(ScientificNotation a, ScientificNotation b)
        {
            if (a.IsZero() || b.IsZero())
                return 0;

            if (b >= a)
                return 1;

            var result = b / a;

            return ((float)result.Mantissa * Mathf.Pow(0.1f, Math.Abs(result.Order)));
        }

        private static int GetOrderByPrefix(string prefix)
        {
            for (int i = 0; i < _prefix.Length; i++)
            {
                if (_prefix[i].Equals(prefix))
                    return (i + 1) * 3;
            }

            if (prefix.Length > 1 && prefix.Contains("E+"))
                return int.Parse(prefix.Remove(0, 2));
            else if (prefix.Length > 1 && prefix[0].Equals("e"))
                return int.Parse(prefix.Remove(0, 1));

            throw new Exception("The prefix is not in the correct format");
        }

        private static ScientificNotation ConvertMantissa(double mantissa)
        {
            if (mantissa == 0)
                return new ScientificNotation();

            int order = (int)Math.Log10(mantissa);

            mantissa = mantissa * Math.Pow(0.1, order);

            if (mantissa < 1)
            {
                mantissa *= 10;
                order--;
            }

            return new ScientificNotation()
            {
                _mantissa = mantissa,
                _order = order
            };
        }

        private static int GetDecimalDigitsCount(double value)
        {
            var result = value.ToString().Split(',');

            if (result.Length == 1)
                return 0;
            else
                return result[1].Length;
        }
    }
}
