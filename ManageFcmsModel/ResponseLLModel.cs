using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class ResponseLLModel
    {
        public string ret_code { get; set; }
        public string ret_msg { get; set; }
        public string sign_type { get; set; }
        public string sign { get; set; }
        public string amt_unsettle { get; set; }
        public string amt_balance { get; set; }
    }
}
