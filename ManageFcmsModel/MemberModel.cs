using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    //会员表
    public class MemberModel
    {

        /// <summary>
        /// 会员ID
        /// </summary>		
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 用户名
        /// </summary>		
        public string MemberName
        {
            get;
            set;
        }
        /// <summary>
        /// 登陆密码
        /// </summary>		
        public string PassWord
        {
            get;
            set;
        }
        /// <summary>
        /// 交易密码
        /// </summary>		
        public string TranPassWord
        {
            get;
            set;
        }
        /// <summary>
        /// 注册手机号码
        /// </summary>		
        public string Mobile
        {
            get;
            set;
        }
        /// <summary>
        /// 电子邮箱

        /// </summary>		
        public string Email
        {
            get;
            set;
        }
        /// <summary>
        /// 最后一次登陆IP地址
        /// </summary>		
        public string LastIP
        {
            get;
            set;
        }
        /// <summary>
        /// 是否禁用 : 0.启用 1.禁用
        /// </summary>		
        public bool IsDisable
        {
            get;
            set;
        }
        /// <summary>
        /// 注册时间
        /// </summary>		
        public DateTime RegTime
        {
            get;
            set;
        }
        /// <summary>
        /// 修改时间
        /// </summary>		
        public DateTime UpdateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 是否锁定
        /// </summary>		
        public bool IsLocked
        {
            get;
            set;
        }
        /// <summary>
        /// 最后一次登陆时间
        /// </summary>		
        public DateTime LastLoginTime
        {
            get;
            set;
        }
        /// <summary>
        /// 信用积分
        /// </summary>		
        public int CreditPoint
        {
            get;
            set;
        }
        /// <summary>
        /// 会员积分
        /// </summary>		
        public int MemberPoint
        {
            get;
            set;
        }
        /// <summary>
        /// 登陆次数
        /// </summary>		
        public int Times
        {
            get;
            set;
        }
        /// <summary>
        /// 推荐码
        /// </summary>		
        public string RecoCode
        {
            get;
            set;
        }
        /// <summary>
        /// 可用余额
        /// </summary>		
        public decimal Balance
        {
            get;
            set;
        }
        /// <summary>
        /// 会员类型（0：个人会员 1：企业会员）
        /// </summary>		
        public int Type
        {
            get;
            set;
        }
        /// <summary>
        /// 注册完场状态
        /// </summary>		
        public int CompleStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 会员等级
        /// </summary>		
        public int MemberLevel
        {
            get;
            set;
        }
        /// <summary>
        /// VIP开始时间
        /// </summary>		
        public DateTime VIPStartTime
        {
            get;
            set;
        }
        /// <summary>
        /// VIP结束时间
        /// </summary>		
        public DateTime VIPEndTime
        {
            get;
            set;
        }
        /// <summary>
        /// Icon
        /// </summary>		
        public string Icon
        {
            get;
            set;
        }
        /// <summary>
        /// TimeStamp
        /// </summary>		
        public long TimeStamp
        {
            get;
            set;
        }
        /// <summary>
        /// 是否允许提现
        /// </summary>
        public bool AllowWithdraw
        {
            get;
            set;
        }
        /// <summary>
        /// 是否为营销人员
        /// </summary>
        public bool IsMarket { get; set; }

    }
}
