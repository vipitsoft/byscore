using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace BYSCORE.Common
{
    public static class Tools
    {

        public static string Week(DateTime Day)
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(Day.DayOfWeek)];
            return week;
        }

        public static string RemoveHTML(string Htmlstring)
        {
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            //Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            //Htmlstring = HttpContext.Current.Server.HtmlDecode(Htmlstring).Trim();
            Htmlstring = HttpUtility.HtmlDecode(Htmlstring).Trim();
            //HtmlEncode： 将 Html 源文件中不允许出现的字符进行编码，通常是编码以下字符"<"、">"、"&" 等。HtmlDecode： 刚好跟 HtmlEncode 相关，解码出来原本的字符。

            return Htmlstring;
        }

        /// <summary>
        /// 过滤危险字符，防URL注入
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterStr(string str)
        {
            string[] Badword = { "'", " or ", " and ", "select", "update", "delete", "insert", "master.", "set ", "exec ", "--", "script", "truncate", "xp_cmdshell", ";", "%20", "--", "==", "=", "<", ">", "%", "alter", "<a></a>" };

            if (str == null || str == "") return "";

            str = str.ToLower();

            foreach (string s in Badword)
            {

                str.Replace(s, "");

            }

            return str;

        }



        /// <summary> 根据字符数量截取字符串
        /// 根据字符数量截取字符串
        /// </summary>
        /// <param name="Str">传入的字符串</param>
        /// <param name="Length">要截取的长度</param>
        /// <returns></returns>
        public static string StrLen(string sInput, int Length)
        {
            if (sInput == null || sInput.Length <= 0)
            {
                return "";
            }

            int i = 0, j = 0;
            foreach (char Char in sInput)
            {
                if ((int)Char > 127) //判断是汉字,还是单个字符
                    i += 2;
                else
                    i++;
                if (i > Length)
                {
                    sInput = sInput.Substring(0, j) + "...";
                    break;
                }
                j++;
            }
            return sInput;
        }


        /// 获取文章中第一张图片的地址方法
        public static string getImgUrl(string html)
        {
            if (html == null || html == "")
            {
                return "";
            }

            string regstr = @"<IMG[^>]+src=\s*(?:'(?<src>[^']+)'|""(?<src>[^""]+)""|(?<src>[^>\s]+))\s*[^>]*>";
            string keyname = "src";
            ArrayList resultStr = new ArrayList();
            Regex r = new Regex(regstr, RegexOptions.IgnoreCase);
            MatchCollection mc = r.Matches(html);

            foreach (Match m in mc)
            {
                resultStr.Add(m.Groups[keyname].Value.ToLower());
            }
            if (resultStr.Count > 0)
            {
                return resultStr[0].ToString();
            }
            else
            {
                //没有地址的时候返回空字符
                return "";
            }
        }


        ///移除所有图片的方法
        public static string RemoveImgUrl(string html)
        {
            if (html == null || html == "")
            {
                return "";
            }

            string regstr = @"<img\s*.*src=['""]{1}.*['""]{1} .+\s*>";
            html = Regex.Replace(html, regstr, " ");

            return html;
        }




        /// <summary>
        /// 获得内容中匹配正则表达式分组的 字符串的集合
        /// </summary>
        /// <param name="html">正文</param>
        /// <param name="regstr">正则表达式</param>
        /// <param name="keyname">正则表达式中的分组名称</param>
        /// <returns>匹配的 字符串的 集合</returns>
        public static List<string> getMatchGroups(string html, string regstr, string keyname)
        {
            List<string> resultStr = new List<string>();
            Regex r = new Regex(regstr, RegexOptions.IgnoreCase);
            MatchCollection mc = r.Matches(html);

            foreach (Match m in mc)
            {
                resultStr.Add(m.Groups[keyname].Value.ToLower());
            }
            if (resultStr.Count > 0)
            {
                return resultStr;
            }
            else
            {
                //没有地址的时候返回空字符
                resultStr.Add("");
                return resultStr;
            }
        }

        /// <summary>
        /// 生成随机函数
        /// </summary>
        /// <param name="len">返回的长度</param>
        /// <param name="counts">数值范围</param>
        /// <returns></returns>
        public static int[] MakeRand(int len, int counts)
        {
            if (len > counts) len = counts;

            int[] tmp = new int[counts];
            int[] output = new int[len];

            if (len == 0 || counts == 0) return output;

            Random ra = new Random(unchecked((int)DateTime.Now.Ticks));

            for (int i = 0; i < counts; i++)
            {
                tmp[i] = i;
            }
            int end = counts - 1;

            for (int j = 0; j < len; j++)
            {
                int n = ra.Next(0, end);
                output[j] = tmp[n];
                tmp[n] = tmp[end];
                end--;
            }
            return output;
        }


        /// <summary>  格式化时间函数
        /// 格式化时间函数
        /// case 1:2012-03-18 18:18:18
        /// case 2:2012-03-18 18:18
        /// case 3:2012-03-18
        /// case 4:201203181818
        /// case 5:2012031818
        /// case 6:2012年03月18日 18:18:18
        /// case 7:2012年03月18日 18:18
        /// case 8:2012年03月18日
        /// case 9:2012.03.18 18:18:18
        /// case 10:2012.03.18 18:18
        /// case 11:2012.03.18
        /// case 12:2012/03/18 18:18:18
        /// case 13:2012/03/18 18:18
        /// case 14:2012/03/18
        /// case 15:03/18
        /// case 16:03-18
        /// case 17:03.18
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string FormatDateTime(DateTime dt, int type)
        {

            string y, m, d, h, mi, s, ss, strDateTime;
            strDateTime = "";

            y = dt.Year.ToString();
            m = dt.Month < 10 ? "0" + dt.Month.ToString() : dt.Month.ToString();
            d = dt.Day < 10 ? "0" + dt.Day.ToString() : dt.Day.ToString();
            h = dt.Hour < 10 ? "0" + dt.Hour.ToString() : dt.Hour.ToString();
            mi = dt.Minute < 10 ? "0" + dt.Minute.ToString() : dt.Minute.ToString();
            s = dt.Second < 10 ? "0" + dt.Second.ToString() : dt.Second.ToString();

            if (dt.Millisecond < 100)
            {
                if (dt.Millisecond < 10)
                {
                    ss = "00" + dt.Millisecond.ToString();
                }
                else
                {
                    ss = "0" + dt.Millisecond.ToString();
                }
            }
            else
            {
                ss = dt.Millisecond.ToString();
            }

            switch (type)
            {
                case 1:
                    //2012-03-18 18:18:18
                    strDateTime = y + "-" + m + "-" + d + " " + h + ":" + mi + ":" + s;
                    break;
                case 2:
                    //2012-03-18 18:18
                    strDateTime = y + "-" + m + "-" + d + " " + h + ":" + mi;
                    break;
                case 3:
                    //2012-03-18
                    strDateTime = y + "-" + m + "-" + d;
                    break;
                case 4:
                    //201203181818
                    strDateTime = y + m + d + h + mi + s;
                    break;
                case 5:
                    //2012031818
                    strDateTime = y + m + d + h + mi;
                    break;
                case 6:
                    //2012年03月18日 18:18:18
                    strDateTime = y + "年" + m + "月" + d + "日 " + h + ":" + mi + ":" + s;
                    break;
                case 7:
                    //2012年03月18日 18:18
                    strDateTime = y + "年" + m + "月" + d + "日 " + h + ":" + mi;
                    break;
                case 8:
                    //2012年03月18日
                    strDateTime = y + "年" + m + "月" + d + "日";
                    break;
                case 9:
                    //2012.03.18 18:18:18
                    strDateTime = y + "." + m + "." + d + " " + h + ":" + mi + ":" + s;
                    break;
                case 10:
                    //2012.03.18 18:18
                    strDateTime = y + "." + m + "." + d + " " + h + ":" + mi;
                    break;
                case 11:
                    //2012.03.18
                    strDateTime = y + "." + m + "." + d;
                    break;
                case 12:
                    //2012/03/18 18:18:18
                    strDateTime = y + "/" + m + "/" + d + " " + h + ":" + mi + ":" + s;
                    break;
                case 13:
                    //2012/03/18 18:18
                    strDateTime = y + "/" + m + "/" + d + " " + h + ":" + mi;
                    break;
                case 14:
                    //2012/03/18
                    strDateTime = y + "/" + m + "/" + d;
                    break;
                case 15:
                    //03/18
                    strDateTime = m + "/" + d;
                    break;
                case 16:
                    //03-18
                    strDateTime = m + "-" + d;
                    break;
                case 17:
                    //03.18
                    strDateTime = m + "." + d;
                    break;

            }
            return strDateTime;

        }

        /// <summary>
        /// 获得分页的字符串
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static string ShowPageNavigate(int pageSize, int currentPage, int totalCount, string pms)
        {
            string redirectTo = "";
            pageSize = pageSize == 0 ? 3 : pageSize;
            var totalPages = Math.Max((totalCount + pageSize - 1) / pageSize, 1); //总页数
            var output = new StringBuilder();
            output.Append("<div class=\"oagrid_pagenav\">");
            output.Append("<span class=\"oagrid_pagenav_recordcnt\">共<strong>" + totalCount + "</strong>行</span>");
            if (totalPages > 1)
            {
                if (currentPage != 1)
                {
                    //处理首页连接
                    output.AppendFormat("<a href='{0}?page={1}{2}' class=\"oagrid_pagenav_pagelink\">首页</a>", redirectTo, 1, pms);
                }
                if (currentPage > 1)
                {
                    //处理上一页的连接
                    output.AppendFormat("<a  href='{0}?page={1}{2}' class=\"oagrid_pagenav_pagelink\">← <span>上一页</span></a> ", redirectTo, currentPage - 1, pms);
                }
                else
                {
                    output.AppendFormat("<a href='javascript:;' class=\"oagrid_pagenav_pagelink\">← <span>上一页</span></a> ", redirectTo, currentPage - 1, pms);
                }

                output.Append(" ");
                int currint = 5;
                for (int i = 0; i <= 10; i++)
                {
                    //一共最多显示10个页码，前面5个，后面5个
                    if ((currentPage + i - currint) >= 1 && (currentPage + i - currint) <= totalPages)
                    {
                        if (currint == i)
                        {
                            //当前页处理
                            output.AppendFormat("<a  href='{0}?page={1}{2}' class=\"oagrid_pagenav_pagelink\">{3}</a>", redirectTo, currentPage, pms, currentPage);
                        }
                        else
                        {
                            //一般页处理
                            output.AppendFormat("<a  href='{0}?page={1}{2}' class=\"oagrid_pagenav_pagelink\">{3}</a>", redirectTo, currentPage + i - currint, pms, currentPage + i - currint);
                        }
                    }
                    output.Append(" ");
                }

                if (currentPage < totalPages)
                {//处理下一页的链接
                    output.AppendFormat("<a  href='{0}?page={1}{2}' class=\"oagrid_pagenav_pagelink\"><span class='hidden-480'>下一页</span> → </a> ", redirectTo, currentPage + 1, pms);
                }
                else
                {
                    output.AppendFormat("<a  href='javascript:;' class=\"oagrid_pagenav_pagelink\"><span>下一页</span> → </a> ", redirectTo, currentPage + 1, pms);
                }

                output.Append(" ");

                if (currentPage != totalPages)
                {
                    output.AppendFormat("<a href='{0}?page={1}{2}' class=\"oagrid_pagenav_pagelink\">末页</a>", redirectTo, totalPages, pms);
                }
                output.Append(" ");
            }
            output.Append("</div>");
            return output.ToString();
        }




        /// <summary>
        /// 根据 , 分割的字符串获得整型数组
        /// </summary>
        /// <param name="ids">,号分割的字符串</param>
        /// <returns></returns>
        public static int[] GetIntIds(string ids)
        {
            ids = ids.TrimEnd(',').TrimStart(',');
            if (ids == "")
            {
                return null;
            }
            string[] strIds = ids.Split(',');
            int[] intIds = new int[strIds.Length];
            for (int i = 0; i < strIds.Length; i++)
            {
                intIds[i] = int.Parse(strIds[i]);
            }
            return intIds;
        }

        /// <summary>
        /// 根据 , 分割的字符串获得string数组
        /// </summary>
        /// <param name="ids">,号分割的字符串</param>
        /// <returns></returns>
        public static string[] GetStrCodes(string codes)
        {
            codes = codes.TrimEnd(',').TrimStart(',');
            if (codes == "")
            {
                return null;
            }
            string[] strIds = codes.Split(',');

            return strIds;
        }



        /// <summary>
        /// 根据整型数组 获得 , 分割的字符串获得
        /// </summary>
        public static string GetStrIds(int[] ids)
        {
            string str = "";
            if (ids.Length <= 0)
            {
                return "";
            }

            foreach (var item in ids)
            {
                str = str + "," + item;
            }

            return str.TrimStart(',');
        }

        /// <summary>
        /// 获得距离现在的 大概的时间 的中文描述
        /// </summary>
        /// <param name="time">要计算的时间</param>
        /// <returns></returns>
        public static string GetApproximatelyPastTimeStr(DateTime time)
        {
            DateTime curTime = DateTime.Now;
            TimeSpan span = curTime - time;//获得时间差
            if (span.TotalMinutes < 1)
            {
                return span.Seconds.ToString() + "秒前";
            }

            if (span.TotalHours < 1)
            {
                return span.Minutes.ToString() + "分钟前";
            }


            if (span.TotalDays < 1)
            {
                return span.Hours.ToString() + "小时前";
            }


            if (span.TotalDays / 30 < 1)
            {
                return span.Days.ToString() + "天前";
            }



            if (span.TotalDays / 365 < 1)
            {
                return Math.Floor(span.TotalDays / 30) + "个月前";
            }

            return Math.Floor(span.TotalDays / 365) + "年前";
        }

        public static string GetEncryptParam(string phoneParam)
        {
            //对参数做base64编码加密
            byte[] bytes = Encoding.Default.GetBytes(phoneParam);
            phoneParam = Convert.ToBase64String(bytes);

            phoneParam = phoneParam.Replace("=", "");//把base64中的=号去掉

            string beginChar = phoneParam.Substring(0, 1);//第一个字符
            string centerStr = phoneParam.Substring(1, phoneParam.Length - 1);//中间的字符
            phoneParam = centerStr + beginChar; //第一个字符放到最后,这个是以phone版的编码方式编码的。
            return phoneParam;
        }



        public static string ConvertChinese(int str)
        {
            var cstr = "";
            switch (str)
            {
                case 1: cstr = "一"; break;
                case 2: cstr = "二"; break;
                case 3: cstr = "三"; break;
                case 4: cstr = "四"; break;
                case 5: cstr = "五"; break;
                case 6: cstr = "六"; break;
                case 7: cstr = "七"; break;
                case 8: cstr = "八"; break;
                case 9: cstr = "九"; break;
                case 10: cstr = "十"; break;
                case 11: cstr = "十 一"; break;
                case 12: cstr = "十 二"; break;
            }
            return (cstr);
        }

        /// <summary>  
        /// 获取程序集中的实现类
        /// </summary>  
        /// <param name="assemblyName">程序集</param>
        public static List<Type> GetClass(string assemblyName)
        {
            if (!String.IsNullOrEmpty(assemblyName))
            {
                Assembly assembly = Assembly.Load(assemblyName);
                List<Type> ts = assembly.GetTypes().ToList();

                var result = new List<Type>();
                foreach (var item in ts.Where(t => t.IsClass))
                {

                    result.Add(item);
                }
                return result;
            }
            return new List<Type>();
        }

        /// <summary> 
        /// 获取两个时间段之间的月份 
        /// </summary> 
        /// <param name="startTime">开始月份</param> 
        /// <param name="endTime">结束月份</param> 
        /// <returns>月份字符串</returns> 
        public static List<string> DateFiledMonths(DateTime startTime, DateTime endTime)
        {
            int index = 0;
            string filed = string.Empty;
            DateTime c1 = Convert.ToDateTime(startTime.ToString("yyyy-MM"));
            DateTime c2 = Convert.ToDateTime(endTime.ToString("yyyy-MM"));
            if (c1 > c2)
            {
                DateTime tmp = c1;
                c1 = c2;
                c2 = tmp;
            }
            List<string> months = new List<string>();
            while (c2 >= c1)
            {
                index++;
                if (index > 12)  // 判断是否大于12个月，如果大于，跳出 www.2cto.com
                    break;
                months.Add(c1.ToString("yyyy-MM"));
                c1 = c1.AddMonths(1);
            }
            return months;

        }

        public static string[] ShortUrl(string url)
        {
            //可以自定义生成MD5加密字符传前的混合KEY
            string key = "Freemud";
            //要使用生成URL的字符
            string[] chars = new string[]
            {
                "a", "b", "c", "d", "e", "f", "g", "h",
                "i", "j", "k", "l", "m", "n", "o", "p",
                "q", "r", "s", "t", "u", "v", "w", "x",
                "y", "z", "0", "1", "2", "3", "4", "5",
                "6", "7", "8", "9", "A", "B", "C", "D",
                "E", "F", "G", "H", "I", "J", "K", "L",
                "M", "N", "O", "P", "Q", "R", "S", "T",
                "U", "V", "W", "X", "Y", "Z"
            };
            //对传入网址进行MD5加密
            string hex = Encryption.MD5Str(key + url);
            string[] resUrl = new string[4];
            for (int i = 0; i < 4; i++)
            {
                //把加密字符按照8位一组16进制与0x3FFFFFFF进行位与运算
                int hexint = 0x3FFFFFFF & Convert.ToInt32("0x" + hex.Substring(i * 8, 8), 16);
                string outChars = string.Empty;
                for (int j = 0; j < 6; j++)
                {
                    //把得到的值与0x0000003D进行位与运算，取得字符数组chars索引
                    int index = 0x0000003D & hexint;
                    //把取得的字符相加
                    outChars += chars[index];
                    //每次循环按位右移5位
                    hexint = hexint >> 5;
                }
                //把字符串存入对应索引的输出数组
                resUrl[i] = outChars;
            }
            return resUrl;
        }
    }
}
