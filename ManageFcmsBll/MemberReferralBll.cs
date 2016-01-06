using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsModel;
using ManageFcmsDal;

namespace ManageFcmsBll
{
   public class MemberReferralBll
    {
       public static bool Insert(MemberRecommendedModel model)
       {
            return MemberReferralDal.insert(model);
       }
    }
}

