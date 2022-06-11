using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Shared.Extentions
{
    public static class StringExtentions
    {
        #region ToTitleCaseGlobalization

        public static string ToTitleCaseGlobalization(this string input, string culture)
        {
            input = !string.IsNullOrWhiteSpace(input) ? input.ToLower() : "";
            var ti = new CultureInfo(culture, false).TextInfo;
            return ti.ToTitleCase(input);
        }

        #endregion

        #region IsNullOrWhiteSpace

        /// <summary>
        ///     Checks if it is null or whitespace
        /// </summary>
        /// <param name="str1"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str1)
        {
            var temp1 = !string.IsNullOrWhiteSpace(str1) ? str1.Trim() : null;
            return string.IsNullOrWhiteSpace(temp1);
        }
        
        #endregion

        #region IsNotNullOrNotWhiteSpace

        /// <summary>
        ///     Checks if it is not null or not whitespace
        /// </summary>
        /// <param name="str1"></param>
        /// <returns></returns>
        public static bool IsNotNullOrNotWhiteSpace(this string str1)
        {
            var temp1 = !string.IsNullOrWhiteSpace(str1) ? str1.Trim() : null;
            return !string.IsNullOrWhiteSpace(temp1);
        }

        #endregion

        #region Reverse

        /// <summary>
        ///     Reverses a string
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>The reverse of the input string</returns>
        public static string Reverse(this string input)
        {
            return new string(input.Reverse<char>().ToArray());
        }

        #endregion

        #region ToByteArray

        /// <summary>
        ///     Converts a string to a byte array
        /// </summary>
        /// <param name="input">input string</param>
        /// <param name="encodingUsing">The type of encoding the string is using (defaults to UTF8)</param>
        /// <returns>the byte array representing the string</returns>
        public static byte[] ToByteArray(this string input, Encoding encodingUsing = null)
        {
            return string.IsNullOrEmpty(input) ? null : encodingUsing.NullCheck(new UTF8Encoding()).GetBytes(input);
        }

        #endregion

        #region NullCheck

        /// <summary>
        ///     Does a null check and either returns the default value (if it is null) or the object
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="objectToCall">Object to check</param>
        /// <param name="defaultValue">The default value in case it is null</param>
        /// <returns>The default value if it is null, the object otherwise</returns>
        public static T NullCheck<T>(this T objectToCall, T defaultValue = default(T))
        {
            return objectToCall.IsNull() ? defaultValue : objectToCall;
        }

        /// <summary>
        ///     Does a null check and either returns the default value (if it is null) or the object
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="objectToCall">Object to check</param>
        /// <param name="defaultValue">Function that returns the default value in case it is null</param>
        /// <returns>The default value if it is null, the object otherwise</returns>
        public static T NullCheck<T>(this T objectToCall, Func<T> defaultValue)
        {
            return objectToCall.IsNull() ? defaultValue() : objectToCall;
        }

        #endregion
        
        #region IsNotNull

        /// <summary>
        ///     Determines if the object is not null
        /// </summary>
        /// <param name="objectToCall">The object to check</param>
        /// <returns>False if it is null, true otherwise</returns>
        public static bool IsNotNull(this object objectToCall)
        {
            return !objectToCall.IsNull();
        }

        #endregion

        #region IsNull

        /// <summary>
        ///     Determines if the object is null
        /// </summary>
        /// <param name="objectToCall">The object to check</param>
        /// <returns>True if it is null, false otherwise</returns>
        public static bool IsNull(this object objectToCall)
        {
            return objectToCall == null || Convert.IsDBNull(objectToCall);
        }

        #endregion

        #region IsNotDefault

        public static bool IsNotNullAndNotDefault(this object objectToCall)
        {
            return !objectToCall.IsNull() && !EqualityComparer<object>.Default.Equals(objectToCall.ToLong(), default(long));
        }

        public static bool IsNotNullAndNotDoubleDefault(this object objectToCall)
        {
            return !objectToCall.IsNull() && !EqualityComparer<object>.Default.Equals(objectToCall.ToDouble(), default(double));
        }

        #endregion

        #region IsNotNullOrEmpty

        /// <summary>
        ///     Determines if a list is not null or empty
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="value">List to check</param>
        /// <returns>True if it is not null or empty, false otherwise</returns>
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> value)
        {
            return !value.IsNullOrEmpty();
        }

        #endregion

        #region IsNullOrEmpty

        /// <summary>
        ///     Determines if a list is null or empty
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="value">List to check</param>
        /// <returns>True if it is null or empty, false otherwise</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> value)
        {
            return value.IsNull() || !value.Any();
        }

        #endregion

        #region AppendLineFormat

        /// <summary>
        ///     Does an AppendFormat and then an AppendLine on the StringBuilder
        /// </summary>
        /// <param name="builder">Builder object</param>
        /// <param name="format">Format string</param>
        /// <param name="objects">Objects to format</param>
        /// <returns>The StringBuilder passed in</returns>
        public static StringBuilder AppendLineFormat(this StringBuilder builder, string format, params object[] objects)
        {
            return builder.AppendFormat(format, objects).AppendLine();
        }

        #endregion

        #region XmlSerialize

        public static string XmlSerialize<T>(this T objectToSerialize)
        {
            if (objectToSerialize.IsNull())
                return string.Empty;

            if (typeof(T) != typeof(Exception))
            {

                var ms = new MemoryStream();
                var serializer = new XmlSerializer(typeof(T));
                var xmlWriter = new XmlTextWriter(ms, Encoding.UTF8) { Formatting = Formatting.Indented };
                serializer.Serialize(xmlWriter, objectToSerialize);
                var encoding = new UTF8Encoding();
                return encoding.GetString(((MemoryStream)xmlWriter.BaseStream).ToArray());

            }
            else
            {
                return objectToSerialize.ToString();
            }
        }

        #endregion

        #region CreateRandomToken
        public static string CreateRandomToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }


        #endregion

    }
}
