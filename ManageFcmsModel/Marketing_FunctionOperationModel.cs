using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    //基本功能模块定义
    public class Marketing_FunctionOperationModel
    {
        public int Id { get; set; }
        public string OperationName { get; set; }
        public string OperationCode { get; set; }
        public string Remake { get; set; }

        public string OperType { get; set; }
    }
}
