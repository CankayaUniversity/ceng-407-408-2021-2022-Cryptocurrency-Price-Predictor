using System.Globalization;
using System.Text;

namespace Shared.Extentions
{
    /// <summary>
    ///     Value type extension methods
    /// </summary>
    public static class ValueTypeExtensions
    {
        #region Functions

        #region ToBool

        /// <summary>
        ///     Turns a string into a bool
        /// </summary>
        /// <param name="input">Object value</param>
        /// <returns>bool equivalent</returns>
        public static bool ToBool(this string input)
        {
            var booleanValue = false;
            if (input.IsNotNull() && !string.IsNullOrWhiteSpace(input))
                bool.TryParse(input, out booleanValue);
            return booleanValue;
        }

        /// <summary>
        ///     Turns an object into a bool
        /// </summary>
        /// <param name="input">Object value</param>
        /// <returns>bool equivalent</returns>
        public static bool ToBool(this object input)
        {
            var booleanValue = false;
            if (input.IsNotNull() && !string.IsNullOrWhiteSpace(input.ToString()))
                bool.TryParse(input.ToString(), out booleanValue);
            return booleanValue;
        }

        #endregion

        #region ToInt

        /// <summary>
        ///     Converts the bool to an integer
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>The integer equivalent</returns>
        public static int ToInt(this bool value)
        {
            return value ? 1 : 0;
        }


        /// <summary>
        ///     Converts the string to an integer
        /// </summary>
        /// <param name="input">Value to convert</param>
        /// <returns>The integer equivalent</returns>
        public static int ToInt(this string input)
        {
            var integerValue = 0;
            if (input.IsNotNull() && !string.IsNullOrWhiteSpace(input))
                int.TryParse(input, out integerValue);
            return integerValue;
        }

        /// <summary>
        ///     Converts the object to an integer
        /// </summary>
        /// <param name="input">Value to convert</param>
        /// <returns>The integer equivalent</returns>
        public static int ToInt(this object input)
        {
            var integerValue = 0;
            if (input.IsNotNull() && !string.IsNullOrWhiteSpace(input.ToString()))
                int.TryParse(input.ToString(), out integerValue);
            return integerValue;
        }

        #endregion

        #region ToLong

        /// <summary>
        ///     Converts the bool to an integer
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>The integer equivalent</returns>
        public static long ToLong(this bool value)
        {
            return value ? 1 : 0;
        }


        /// <summary>
        ///     Converts the string to an integer
        /// </summary>
        /// <param name="input">Value to convert</param>
        /// <returns>The integer equivalent</returns>
        public static long ToLong(this string input)
        {
            long integerValue = 0;
            if (input.IsNotNull() && !string.IsNullOrWhiteSpace(input))
                long.TryParse(input, out integerValue);
            return integerValue;
        }

        /// <summary>
        ///     Converts the object to an integer
        /// </summary>
        /// <param name="input">Value to convert</param>
        /// <returns>The integer equivalent</returns>
        public static long ToLong(this object input)
        {
            long integerValue = 0;
            if (input.IsNotNull() && !string.IsNullOrWhiteSpace(input.ToString()))
                long.TryParse(input.ToString(), out integerValue);
            return integerValue;
        }

        #endregion

        #region ToStr

        /// <summary>
        ///     Converts the string to a string
        /// </summary>
        /// <param name="input">Value to convert</param>
        /// <returns>The string equivalent</returns>
        public static string ToStr(this string input)
        {
            var stringValue = string.Empty;
            if (!string.IsNullOrWhiteSpace(input))
                stringValue = input;
            return stringValue;
        }

        /// <summary>
        ///     Converts the object to a string
        /// </summary>
        /// <param name="input">Value to convert</param>
        /// <returns>The string equivalent</returns>
        public static string ToStr(this object input)
        {
            var stringValue = string.Empty;
            if (input.IsNotNull() && !string.IsNullOrWhiteSpace(input.ToString()))
                stringValue = input.ToString();
            return stringValue;
        }

        /// <summary>
        ///     Converts the double to a string
        /// </summary>
        /// <param name="input">Value to convert</param>
        /// <param name="decimalPlacesNumber">Number of decimal places</param>
        /// <returns>The string equivalent</returns>
        public static string ToStringWithoutZero(this double input, int decimalPlacesNumber)
        {
            if (input == 0)
                return string.Empty;
            var decimalMultiplier = 10;
            for (var i = 1; i <= decimalPlacesNumber; i++)
                decimalMultiplier = decimalMultiplier * 10;
            var decimalPlaces = (input % 1) * decimalMultiplier;
            return decimalPlaces == 0 ? input.ToString("F0") : input.ToString("F" + decimalPlacesNumber);
        }

        /// <summary>
        ///     Converts the double to a string
        /// </summary>
        /// <param name="input">Value to convert</param>
        /// <param name="decimalPlacesNumber">Number of decimal places</param>
        /// <returns>The string equivalent</returns>
        public static string ToStringWithoutZero(this double? input, int decimalPlacesNumber)
        {
            return input.IsNull() ? string.Empty : (!input.HasValue ? string.Empty : ToStringWithoutZero(input.Value, decimalPlacesNumber));
        }

        #endregion

        #region ToGuid

        /// <summary>
        ///     Converts the string to a guid
        /// </summary>
        /// <param name="input">Value to convert</param>
        /// <returns>The guid equivalent</returns>
        public static Guid ToGuid(this string input)
        {
            var guidValue = Guid.Empty;
            if (input.IsNotNull() && !string.IsNullOrWhiteSpace(input))
                Guid.TryParse(input, out guidValue);
            return guidValue;
        }

        /// <summary>
        ///     Converts the object to a guid
        /// </summary>
        /// <param name="input">Value to convert</param>
        /// <returns>The guid equivalent</returns>
        public static Guid ToGuid(this object input)
        {
            var guidValue = Guid.Empty;
            if (input.IsNotNull() && !string.IsNullOrWhiteSpace(input.ToString()))
                Guid.TryParse(input.ToString(), out guidValue);
            return guidValue;
        }

        #endregion

        #region ToDouble

        /// <summary>
        ///     Converts the object to a double
        /// </summary>
        /// <param name="input">Value to convert</param>
        /// <returns>The double equivalent</returns>
        public static double ToDouble(this object input)
        {
            double doubleValue = 0;
            if (input.IsNotNull() && !string.IsNullOrWhiteSpace(input.ToString()))
                try
                {
                    doubleValue = double.Parse(input.ToString(), CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    doubleValue = 0;
                }
            return doubleValue;
        }

        public static string TrimmedDouble(this object input, int digitValue)
        {
            try
            {
                var doubleValue = input.ToDouble();
                if (digitValue == 0)
                    digitValue = 2;
                var pattern = "{0:0." + "".PadRight(digitValue, '0') + "}";
                return string.Format(((System.Math.Round(doubleValue) == doubleValue) ? "{0:0}" : pattern), doubleValue);
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region ToDecimal

        /// <summary>
        ///     Converts the object to a decimal
        /// </summary>
        /// <param name="input">Value to convert</param>
        /// <returns>The decimal equivalent</returns>
        public static decimal ToDecimal(this object input)
        {
            decimal decimalValue = 0;
            if (input.IsNotNull() && !string.IsNullOrWhiteSpace(input.ToString()))
                try
                {
                    decimalValue = decimal.Parse(input.ToString(), CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    decimalValue = 0;
                }
            return decimalValue;
        }

        #endregion

        #region ToBase64String

        /// <summary>
        ///     Converts a byte array into a base 64 string
        /// </summary>
        /// <param name="input">Input array</param>
        /// <returns>The equivalent byte array in a base 64 string</returns>
        public static string ToBase64String(this byte[] input)
        {
            return input.IsNull() ? string.Empty : Convert.ToBase64String(input);
        }

        #endregion

        #region ToEncodedString

        /// <summary>
        ///     Converts a byte array to a string
        /// </summary>
        /// <param name="input">input array</param>
        /// <param name="encodingUsing">The type of encoding the string is using (defaults to UTF8)</param>
        /// <param name="count">
        ///     Number of bytes starting at the index to convert (use -1 for the entire array starting at the
        ///     index)
        /// </param>
        /// <param name="index">Index to start at</param>
        /// <returns>string of the byte array</returns>
        public static string ToEncodedString(this byte[] input, Encoding encodingUsing = null, int index = 0, int count = -1)
        {
            if (input.IsNull())
                return string.Empty;
            if (count == -1)
                count = input.Length - index;
            return encodingUsing.NullCheck(new UTF8Encoding()).GetString(input, index, count);
        }

        #endregion

        #region IsControl

        /// <summary>
        ///     Is the character a control character
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True if it is, false otherwise</returns>
        public static bool IsControl(this char value)
        {
            return char.IsControl(value);
        }

        #endregion

        #region IsDigit

        /// <summary>
        ///     Is the character a digit character
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True if it is, false otherwise</returns>
        public static bool IsDigit(this char value)
        {
            return char.IsDigit(value);
        }

        #endregion

        #region IsHighSurrogate

        /// <summary>
        ///     Is the character a high surrogate character
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True if it is, false otherwise</returns>
        public static bool IsHighSurrogate(this char value)
        {
            return char.IsHighSurrogate(value);
        }

        #endregion

        #region IsLetter

        /// <summary>
        ///     Is the character a letter character
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True if it is, false otherwise</returns>
        public static bool IsLetter(this char value)
        {
            return char.IsLetter(value);
        }

        #endregion

        #region IsLetterOrDigit

        /// <summary>
        ///     Is the character a letter or digit character
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True if it is, false otherwise</returns>
        public static bool IsLetterOrDigit(this char value)
        {
            return char.IsLetterOrDigit(value);
        }

        #endregion

        #region IsLower

        /// <summary>
        ///     Is the character a lower case character
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True if it is, false otherwise</returns>
        public static bool IsLower(this char value)
        {
            return char.IsLower(value);
        }

        #endregion

        #region IsLowSurrogate

        /// <summary>
        ///     Is the character a low surrogate character
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True if it is, false otherwise</returns>
        public static bool IsLowSurrogate(this char value)
        {
            return char.IsLowSurrogate(value);
        }

        #endregion

        #region IsNumber

        /// <summary>
        ///     Is the character a number character
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True if it is, false otherwise</returns>
        public static bool IsNumber(this char value)
        {
            return char.IsNumber(value);
        }

        #endregion

        #region IsPunctuation

        /// <summary>
        ///     Is the character a punctuation character
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True if it is, false otherwise</returns>
        public static bool IsPunctuation(this char value)
        {
            return char.IsPunctuation(value);
        }

        #endregion

        #region IsSurrogate

        /// <summary>
        ///     Is the character a surrogate character
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True if it is, false otherwise</returns>
        public static bool IsSurrogate(this char value)
        {
            return char.IsSurrogate(value);
        }

        #endregion

        #region IsSymbol

        /// <summary>
        ///     Is the character a symbol character
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True if it is, false otherwise</returns>
        public static bool IsSymbol(this char value)
        {
            return char.IsSymbol(value);
        }

        #endregion

        #region IsUpper

        /// <summary>
        ///     Is the character an upper case character
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True if it is, false otherwise</returns>
        public static bool IsUpper(this char value)
        {
            return char.IsUpper(value);
        }

        #endregion

        #region IsWhiteSpace

        /// <summary>
        ///     Is the character a whitespace character
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True if it is, false otherwise</returns>
        public static bool IsWhiteSpace(this char value)
        {
            return char.IsWhiteSpace(value);
        }

        #endregion
        
        #endregion
    }
}