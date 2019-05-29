using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class ByteArrayExtension
    {
        /// <summary>
        /// to hex string, 0xABCD
        /// </summary>
        public static string ToHexString(this byte[] values)
        {
            string result = "";
            for (int i = 0; i < values.Length; i++) {
                result = (i == 0 ? "" : "") + values[i].ToString("X2") + result;
            }
            return result;
        }

        /// <summary>
        /// to hex string, 可设置分隔符
        /// </summary>
        public static string ToHexString(this byte[] values, string separator)
        {
            string result = "";
            for (int i = 0; i < values.Length; i++) {
                result += (i == 0 ? "" : separator) + values[i].ToString("X2");
            }
            return result;
        }
    }
}
