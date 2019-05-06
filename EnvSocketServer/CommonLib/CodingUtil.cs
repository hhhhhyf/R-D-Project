

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonLib
{
    public class CodingUtil
    {
        public static List<string> GetPackagesFromData(string data)
        {
            List<string> listPkg = new List<string>();
            if (!data.Contains("{") || !data.Contains("}"))
                return null;
            string pattern = @"\{.*?\}";//匹配模式
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(data);
            
            string value = "";
            foreach (Match match in matches)
            {
                value = match.Value.Trim('{', '}');
                listPkg.Add(value);
            }
            return listPkg;
        }

        /// <summary>
        /// 字符串转Unicode码
        /// </summary>
        /// <returns>The to unicode.</returns>
        /// <param name="value">Value.</param>
        public static string StringToUnicode(string value)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i += 2)
            {
                // 取两个字符，每个字符都是右对齐。
                stringBuilder.AppendFormat("u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Unicode转字符串
        /// </summary>
        /// <returns>The to string.</returns>
        /// <param name="unicode">Unicode.</param>
        public static string UnicodeToString(string unicode)
        {
            string resultStr = "";
            string[] strList = unicode.Split('u');
            for (int i = 1; i < strList.Length; i++)
            {
                resultStr += (char)int.Parse(strList[i], System.Globalization.NumberStyles.HexNumber);
            }
            return resultStr;
        }


        public static string Decode(string str)
        {
            string unicodeStr = "";
            byte[] b = System.Text.Encoding.Default.GetBytes(str);

            for (int i = 0; i < b.Length; i++)
            {
                if (i % 2 == 0) unicodeStr += "u";
                string str16 = Convert.ToString(b[i], 16);
                if (str16.Length == 1) str16 = "0" + str16;
                unicodeStr += str16;
            }
            //Console.WriteLine(unicodeStr);
            return UnicodeToString(unicodeStr);
        }

        public string Encode(string str)
        {
            string unicode = StringToUnicode(str);
            string[] unicodes = unicode.Split('u');
            string[] temp = new string[unicodes.Length - 1];
            for (int i = 0; i < temp.Length; i++) temp[i] = unicodes[i + 1];
            string[] str16 = new string[temp.Length * 2];
            int j = 0;
            foreach (string t in temp)
            {
                str16[j++] += "" + t[0] + t[1];
                str16[j++] += "" + t[2] + t[3];
            }

            int[] ASCII = new int[str16.Length * 2];
            for (int i = 0; i < str16.Length; i++)
            {
                ASCII[i] = Convert.ToInt32(str16[i], 16);

            }
            string result = "";
            foreach (int t in ASCII)
            {
                result += (char)t;
            }
            return result;
        }



       

        private static byte[] ConvertHexToBytes(string input)
        {
            var result = new byte[(input.Length + 1) / 2];
            var offset = 0;
            if (input.Length % 2 == 1)
            {
                // If length of input is odd, the first character has an implicit 0 prepended.
                result[0] = (byte)Convert.ToUInt32(input[0] + "", 16);
                offset = 1;
            }
            for (int i = 0; i < input.Length / 2; i++)
            {
                result[i + offset] = (byte)Convert.ToUInt32(input.Substring(i * 2 + offset, 2), 16);
            }
            return result;
        }

        public static string UnicodeToCharacter(string text)
        {
            //byte[] arr = HexStringToByteArray(text);
            byte[] arr = ConvertHexToBytes(text);

            System.Text.UnicodeEncoding converter = new System.Text.UnicodeEncoding();

            string str = converter.GetString(arr);


            return str;
        }

        /// <summary>
        /// Bytes转结构体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public T BytesToStruct<T>(byte[] bytes, Type type)
        {
            T obj = default(T);

            int size = Marshal.SizeOf(type);

            if (size > bytes.Length)
            {
                return obj;
            }

            IntPtr structPtr = Marshal.AllocHGlobal(size);

            Marshal.Copy(bytes, 0, structPtr, size);

            obj = (T)Marshal.PtrToStructure(structPtr, type);

            Marshal.FreeHGlobal(structPtr);

            return obj;
        }

        public static string StringToHexString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组
            string result = string.Empty;
            for (int i = 0; i < b.Length; i++)//逐字节变为16进制字符
            {
                result += Convert.ToString(b[i], 16);
            }
            return result;
        }

        public static string HexStringToString(string hs, Encoding encode)
        {
            string strTemp = "";
            byte[] b = new byte[hs.Length / 2];
            for (int i = 0; i < hs.Length / 2; i++)
            {
                strTemp = hs.Substring(i * 2, 2);
                b[i] = Convert.ToByte(strTemp, 16);
            }
            //按照指定编码将字节数组变为字符串
            return encode.GetString(b);
        }

        public static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }
    }
}