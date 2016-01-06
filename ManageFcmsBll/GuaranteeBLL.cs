using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsDal;
using ManageFcmsModel;

namespace ManageFcmsBll
{
    //Guarantee
    public partial class GuaranteeBll
    {
        readonly GuaranteeDal _dal = new GuaranteeDal();
        public List<GuaranteeModel> GetGuaranteeModelList()
        {
            return _dal.GetGuaranteeListModel();
        }

        public DataSet GetGuaranteeDataSet()
        {
            return _dal.GetGuaranteeDataSet();
        }

        /// <summary>
        /// 担保公司剩余应还本金
        /// </summary>
        /// <returns></returns>
        public DataSet GuaranteeRePrincipalTotal()
        {
            return _dal.GuaranteeRePrincipalTotal();
        }

    }
}
