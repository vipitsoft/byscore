using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BYSCORE.UI
{
    public static class StringExtension
    {
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="normalString">普通的字符串</param>
        /// <returns>加密后的二进制字符串</returns>
        /// <remarks>不可逆加密</remarks>
        public static string Encrypt(this string normalString)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            HashAlgorithm algroithm = new MD5CryptoServiceProvider();
            byte[] buffer = algroithm.ComputeHash(encoding.GetBytes(normalString));
            return BitConverter.ToString(buffer);
        }

        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string UrlEncode(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            return HttpUtility.UrlEncode(input);
        }
        /// <summary>
        /// URL解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string UrlDecode(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            return HttpUtility.UrlDecode(input);
        }
        /// <summary>
        /// 拼接URL
        /// </summary>
        /// <param name="baseUrlPath"></param>
        /// <param name="additionalNode"></param>
        /// <returns></returns>
        public static string Link(this string baseUrlPath, object additionalNode)
        {
            if (additionalNode == null)
            {
                return baseUrlPath;
            }
            if (baseUrlPath == null)
            {
                return additionalNode.ToString();
            }
            return Link(baseUrlPath, additionalNode.ToString());
        }
        /// <summary>
        /// 拼接URL
        /// </summary>
        /// <param name="baseUrlPath"></param>
        /// <param name="additionalNode"></param>
        /// <returns></returns>
        public static string Link(this string baseUrlPath, string additionalNode)
        {
            if (baseUrlPath == null)
            {
                return additionalNode;
            }
            if (additionalNode == null)
            {
                return baseUrlPath;
            }
            if (baseUrlPath.EndsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                if (additionalNode.StartsWith("/", StringComparison.OrdinalIgnoreCase))
                {
                    baseUrlPath = baseUrlPath.TrimEnd(new char[] { '/' });
                }
                return baseUrlPath + additionalNode;
            }
            if (additionalNode.StartsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                return baseUrlPath + additionalNode;
            }
            return baseUrlPath + "/" + additionalNode;
        }
        /// <summary>
        /// 拼接URL
        /// </summary>
        /// <param name="baseUrlPath"></param>
        /// <param name="additionalNodes"></param>
        /// <returns></returns>
        public static string Link(this string baseUrlPath, params object[] additionalNodes)
        {
            var temp = baseUrlPath;
            foreach (var item in additionalNodes)
            {
                temp = temp.Link(item);
            }
            return temp;
        }
        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        /// <summary>
        /// 格式化输出
        /// </summary>
        /// <param name="format"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string Format(this string format, params object[] values)
        {
            return string.Format(format, values);
        }
    }
}
