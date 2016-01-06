using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace ManageFcmsCommon
{
    public class ControlHelper
    {
       /// <summary>
        /// 获取CheckBoxList选中的值
       /// </summary>
       /// <param name="ckb"></param>
       /// <param name="parator">分隔符</param>
       /// <returns></returns>
        public static string GetCheckBoxList(CheckBoxList ckb, string parator)
        {
            string ckbVal = string.Empty;
            for (int i = 0; i < ckb.Items.Count; i++)
            {
                if (ckb.Items[i].Selected)
                {
                    if (string.IsNullOrEmpty(ckbVal))
                    {
                        ckbVal = ckb.Items[i].Value.Trim();
                    }
                    else
                    {
                        ckbVal += parator + ckb.Items[i].Value.Trim();
                    }
                }
            }
            return ckbVal;
        }
        /// <summary>
        /// 获取CheckBoxList选中的值
        /// </summary>
        /// <param name="ckb"></param>
        /// <returns></returns>
        public static string GetCheckBoxList(CheckBoxList ckb)
        {
            string ckbVal = string.Empty;
            for (int i = 0; i < ckb.Items.Count; i++)
            {
                if (ckb.Items[i].Selected)
                {
                    if (string.IsNullOrEmpty(ckbVal))
                    {
                        ckbVal = "1";
                    }
                    else
                    {
                        ckbVal += ",1";
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(ckbVal))
                    {
                        ckbVal = "0";
                    }
                    else
                    {
                        ckbVal += ",0";
                    }
                }
            }
            return ckbVal;
        }

        /// <summary>
        /// 初始化CheckBoxList的值
        /// </summary>
        /// <param name="checkList"></param>
        /// <param name="strVal">初始化字符串数据</param>
        /// <param name="parator">分隔符</param>
        /// <returns></returns>
        public static string SetChecked(CheckBoxList checkList, string strVal, string parator)
        {
            strVal = parator + strVal + parator; 
            for (int i = 0; i < checkList.Items.Count; i++)
            {
                checkList.Items[i].Selected = false;
                var val = parator + checkList.Items[i].Value + parator;
                if (strVal.IndexOf(val) != -1)
                {
                    checkList.Items[i].Selected = true;
                    strVal = strVal.Replace(val, parator);        //然后从原来的值串中删除已经选中了的
                    if (strVal == parator)                        //strVal的最后一项也被选中的话，此时经过Replace后，只会剩下一个分隔符
                    {
                        strVal += parator;        //添加一个分隔符
                    }
                }
            }
            strVal = strVal.Substring(1, strVal.Length - 2);        //除去前后加的分割符号
            return strVal;
        }
        /// <summary>
        /// 初始化CheckBoxList的值
        /// </summary>
        /// <param name="checkList"></param>
        /// <param name="strVal">初始化字符串数据</param>
        /// <returns></returns>
        public static void SetChecked(CheckBoxList checkList, string strVal)
        {
            var chars = strVal.Replace(",","").ToCharArray();
            for (int i = 0; i < checkList.Items.Count; i++)
            {
                checkList.Items[i].Selected = false;
                if (chars[i].ToString().Equals("1"))
                    checkList.Items[i].Selected = true;
            }
        }
    }
}
