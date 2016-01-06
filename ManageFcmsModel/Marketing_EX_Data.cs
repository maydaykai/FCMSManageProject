using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class Marketing_EX_Data
    {
        //数据分页集合
        public int Id { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 组
        /// </summary>
        public string Group { get; set; }

        public string RoleName { get; set; }

        public string Name { get; set; }
        /// <summary>
        /// 注册人数
        /// </summary>
        public int Regcount { get; set; }

        public decimal Regmoney { get; set; }

        /// <summary>
        /// 续投人数
        /// </summary>
        public int BidNumContinued { get; set; }

        /// <summary>
        /// 续投金额
        /// </summary>
        public decimal SumBidAmount { get; set; }
        public decimal SuccessTransferMoney { get; set; }

        public decimal RealMoney { get; set; }
        public decimal Interest { get; set; }
        public decimal Curr_MouthMoney { get; set; }

    }
}
