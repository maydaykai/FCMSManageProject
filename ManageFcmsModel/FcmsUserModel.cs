using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{


    //FcmsUser
    [Serializable]
    public class FcmsUserModel
    {
        /// <summary>
        /// ID
        /// </summary>		
        public int ID { get; set; }

        /// <summary>
        /// 上一级用户ID
        /// </summary>		
        public int ParentID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>		
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>		
        public string PassWord { get; set; }

        /// <summary>
        /// 交易密码
        /// </summary>		
        public string TranPassWord { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>		
        public string RealName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>		
        public string AnotherName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>		
        public int Sex { get; set; }

        /// <summary>
        /// 座机
        /// </summary>		
        public string Phone { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>		
        public string Mobile { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>		
        public string Email { get; set; }

        /// <summary>
        /// QQ
        /// </summary>		
        public string QQ { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>		
        public DateTime RegTime { get; set; }

        /// <summary>
        /// 最后一次登录时间
        /// </summary>		
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 最后一次登录IP
        /// </summary>		
        public string LastIP { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>		
        public int Times { get; set; }


        /// <summary>
        /// 是否禁用
        /// </summary>		
        public bool IsLock { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>		
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>		
        public string RoleId { get; set; }

        /// <summary>
        /// 关联ID（如果是担保公司需要关联member表ID）
        /// </summary>		
        public int RelationID { get; set; }

    }
}
