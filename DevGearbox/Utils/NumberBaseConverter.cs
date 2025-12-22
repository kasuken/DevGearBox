using System;

namespace DevGearbox.Utils;

public static class NumberBaseConverter
{
    public static class BinaryConverter
    {
        public static string ToOctal(string binary)
        {
            if (string.IsNullOrWhiteSpace(binary)) return "";
            try
            {
                var decimal64 = Convert.ToInt64(binary, 2);
                return Convert.ToString(decimal64, 8);
            }
            catch
            {
                throw new ArgumentException("Invalid binary number");
            }
        }

        public static string ToDecimal(string binary)
        {
            if (string.IsNullOrWhiteSpace(binary)) return "";
            try
            {
                var decimal64 = Convert.ToInt64(binary, 2);
                return decimal64.ToString();
            }
            catch
            {
                throw new ArgumentException("Invalid binary number");
            }
        }

        public static string ToHex(string binary)
        {
            if (string.IsNullOrWhiteSpace(binary)) return "";
            try
            {
                var decimal64 = Convert.ToInt64(binary, 2);
                return Convert.ToString(decimal64, 16).ToUpper();
            }
            catch
            {
                throw new ArgumentException("Invalid binary number");
            }
        }
    }

    public static class OctalConverter
    {
        public static string ToBinary(string octal)
        {
            if (string.IsNullOrWhiteSpace(octal)) return "";
            try
            {
                var decimal64 = Convert.ToInt64(octal, 8);
                return Convert.ToString(decimal64, 2);
            }
            catch
            {
                throw new ArgumentException("Invalid octal number");
            }
        }

        public static string ToDecimal(string octal)
        {
            if (string.IsNullOrWhiteSpace(octal)) return "";
            try
            {
                var decimal64 = Convert.ToInt64(octal, 8);
                return decimal64.ToString();
            }
            catch
            {
                throw new ArgumentException("Invalid octal number");
            }
        }

        public static string ToHex(string octal)
        {
            if (string.IsNullOrWhiteSpace(octal)) return "";
            try
            {
                var decimal64 = Convert.ToInt64(octal, 8);
                return Convert.ToString(decimal64, 16).ToUpper();
            }
            catch
            {
                throw new ArgumentException("Invalid octal number");
            }
        }
    }

    public static class DecimalConverter
    {
        public static string ToBinary(string decimalValue)
        {
            if (string.IsNullOrWhiteSpace(decimalValue)) return "";
            try
            {
                var decimal64 = Convert.ToInt64(decimalValue, 10);
                return Convert.ToString(decimal64, 2);
            }
            catch
            {
                throw new ArgumentException("Invalid decimal number");
            }
        }

        public static string ToOctal(string decimalValue)
        {
            if (string.IsNullOrWhiteSpace(decimalValue)) return "";
            try
            {
                var decimal64 = Convert.ToInt64(decimalValue, 10);
                return Convert.ToString(decimal64, 8);
            }
            catch
            {
                throw new ArgumentException("Invalid decimal number");
            }
        }

        public static string ToHex(string decimalValue)
        {
            if (string.IsNullOrWhiteSpace(decimalValue)) return "";
            try
            {
                var decimal64 = Convert.ToInt64(decimalValue, 10);
                return Convert.ToString(decimal64, 16).ToUpper();
            }
            catch
            {
                throw new ArgumentException("Invalid decimal number");
            }
        }
    }

    public static class HexConverter
    {
        public static string ToBinary(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex)) return "";
            try
            {
                hex = hex.Replace("0x", "").Replace("0X", "");
                var decimal64 = Convert.ToInt64(hex, 16);
                return Convert.ToString(decimal64, 2);
            }
            catch
            {
                throw new ArgumentException("Invalid hexadecimal number");
            }
        }

        public static string ToOctal(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex)) return "";
            try
            {
                hex = hex.Replace("0x", "").Replace("0X", "");
                var decimal64 = Convert.ToInt64(hex, 16);
                return Convert.ToString(decimal64, 8);
            }
            catch
            {
                throw new ArgumentException("Invalid hexadecimal number");
            }
        }

        public static string ToDecimal(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex)) return "";
            try
            {
                hex = hex.Replace("0x", "").Replace("0X", "");
                var decimal64 = Convert.ToInt64(hex, 16);
                return decimal64.ToString();
            }
            catch
            {
                throw new ArgumentException("Invalid hexadecimal number");
            }
        }
    }
}

