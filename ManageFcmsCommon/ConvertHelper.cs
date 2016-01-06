using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ManageFcmsCommon
{
    public class ConvertHelper
    {
        /// <summary>
        /// 转换成整数
        /// </summary>
        public static int ToInt(string str)
        {
            int i = 0;
            return int.TryParse(str, out i) ? i : 0;
        }

        /// <summary>
        /// 转换成Bool值
        /// </summary>
        public static bool ToBool(string str)
        {
            bool i = false;
            Boolean.TryParse(str, out i);
            return i;
        }

        /// <summary>
        /// 转换成时间
        /// </summary>
        public static DateTime ToDateTime(string str)
        {
            DateTime dt = DateTime.Now;
            if (string.IsNullOrEmpty(str))
                return dt;
            DateTime.TryParse(str, out dt);
            return dt;
        }

        /// <summary>
        /// 转换成数字
        /// </summary>
        public static Decimal ToDecimal(string str)
        {
            Decimal d = 0;
            if (string.IsNullOrEmpty(str))
                return d;
            Decimal.TryParse(str, out d);
            return d;
        }

        /// <summary>
        /// 接收查询参数，返回字符串值
        /// </summary>
        public static string QueryString(HttpRequest request, string param, string defaultValue)
        {
            if (request.Params[param] != null && !string.IsNullOrEmpty(request.Params[param]))
            {
                return request.Params[param].Trim();
            }
            return defaultValue;
        }

        /// <summary>
        /// 接收查询参数，返回整型值
        /// </summary>
        public static int QueryString(HttpRequest request, string param, int defValue)
        {
            if (request.Params[param] != null && !string.IsNullOrEmpty(request.Params[param]))
            {
                return ToInt(request.Params[param].Trim());
            }
            return defValue;
        }

        #region 小写金额转换成中文大写

        /// <summary>
        /// 小写金额转换成中文大写
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string LowAmountConvertChCap(decimal num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字 
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字 
            string str3 = "";    //从原num值中取出的值 
            string str4 = "";    //数字的字符串形式 
            string str5 = "";  //人民币大写金额形式 
            int i;    //循环变量 
            int j;    //num的值乘以100的字符串长度 
            string ch1 = "";    //数字的汉语读法 
            string ch2 = "";    //数字位的汉字读法 
            int nzero = 0;  //用来计算连续的零值是几个 
            int temp;            //从原num值中取出的值 

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数 
            str4 = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式 
            j = str4.Length;      //找出最高位 
            if (j > 15) { return "溢出"; }
            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分 

            //循环取出每一位需要转换的值 
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //取出需转换的某一位的值 
                temp = Convert.ToInt32(str3);      //转换为数字 
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时 
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位 
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    str5 = str5 + '整';
                }
            }
            if (num == 0)
            {
                str5 = "零元整";
            }
            return str5;
        }

        public static string LowAmountConvertChCap(string numstr)
        {
            try
            {
                decimal num = ToDecimal(numstr);
                return LowAmountConvertChCap(num);
            }
            catch
            {
                return "非数字形式！";
            }
        }
        #endregion
        /// <summary>
        /// 根据身份证号码获取性别
        /// </summary>
        /// <param name="IDNum"></param>
        /// <returns></returns>
        public static char getSexByIDCard(string IDNum)
        {
            if (IDNum.Length == 15 && (Int32.Parse(IDNum.Substring(14, 1)) / 2) * 2 == Int32.Parse(IDNum.Substring(14, 1)))
            {
                return '女';
            }
            if (IDNum.Length == 18 && (Int32.Parse(IDNum.Substring(16, 1)) / 2) * 2 == Int32.Parse(IDNum.Substring(16, 1)))
            {
                return '女';
            }
            if (IDNum.Length < 15)
            {
                return '无';
            }
            return '男';
        }

        /// <summary>
        /// 根据身份证号码获取生日
        /// </summary>
        /// <param name="IDNum"></param>
        /// <returns></returns>
        public static DateTime getBirthdayByIDCard(string IDNum)
        {
            if (string.IsNullOrEmpty(IDNum) || IDNum.Length < 15)
            {
                return ToDateTime("1970-01-01");
            }
            string tmpStr = "";
            if (IDNum.Length == 15)
            {
                tmpStr = IDNum.Substring(6, 6);
                tmpStr = "19" + tmpStr;
            }
            else
            {
                tmpStr = IDNum.Substring(6, 8);
            }
            return new DateTime(Int32.Parse(tmpStr.Substring(0, 4)), Int32.Parse(tmpStr.Substring(4, 2)), Int32.Parse(tmpStr.Substring(6)));
        }

        /// <summary>
        /// 数据导入
        /// </summary>
        /// <param name="filepath">绝对路径</param>
        /// <param name="sheetname">表名</param>
        /// <param name="fields">查询字段</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-22</remarks>
        public static DataSet ExcelDataSource(string filepath, string sheetname, string fields)
        {
            string strConn;
            //strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbDataAdapter oada = new OleDbDataAdapter("select " + fields + " from [" + sheetname + "$]", strConn);
            DataSet ds = new DataSet();
            oada.Fill(ds);
            return ds;
        }
    }

    public static class CommonHelper
    {
        /// <summary>
        /// 进行ID加密
        /// </summary>
        /// <param name="dt">数据结果集</param>
        /// <param name="name">加密列名</param>
        /// <returns>加密过ID后的数据</returns>
        /// <remarks>add 卢侠勇,2015-08-06</remarks>
        public static DataTable EncryptID(this DataTable dt, string name)
        {
            dt.Columns.Add("EncryptID", typeof(string));
            foreach (var items in dt.AsEnumerable())
            {
                var id = items[name].ToString();
                items.SetField<string>("EncryptID", DESStringHelper.EncryptString(id));
            }
            return dt;
        }

        /// <summary>
        /// 进行ID加密
        /// </summary>
        /// <param name="dt">数据结果集</param>
        /// <param name="name">加密列名</param>
        /// <returns>加密过后的数据</returns>
        /// <remarks>add 卢侠勇,2015-08-28</remarks>
        public static DataTable EncryptID(this DataTable dt, string[] name)
        {
            for (var i = 0; i < name.Length; i++)
            {
                dt.Columns.Add("Encrypt_" + name[i], typeof(string));
            }
            foreach (var items in dt.AsEnumerable())
            {
                for (var i = 0; i < name.Length; i++)
                {
                    var id = items[name[i]].ToString();
                    items.SetField<string>("Encrypt_" + name[i], DESStringHelper.EncryptString(id));
                }
            }
            return dt;
        }

        /// <summary>
        /// 进行ID加密
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>加密过后的id</returns>
        /// <remarks>add 卢侠勇,2015-08-06</remarks>
        public static string EncryptID(this int id)
        {
            return DESStringHelper.EncryptString(id.ToString());
        }

        /// <summary>
        /// 将加密过后的ID进行解密
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>解密过后的id</returns>
        /// <remarks>add 卢侠勇,2015-08-06</remarks>
        public static int DecryptID(this string id)
        {
            return int.Parse(DESStringHelper.DecryptString(id));
        }
    }
}
