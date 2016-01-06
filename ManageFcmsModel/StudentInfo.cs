using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class StudentInfo
    {
        /// <summary>
        ///ID
        /// </summary>		
        public int ID { get; set; }

        /// <summary>
        /// 会员Id
        /// </summary>
        public int MemberID { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        public string Mobile { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 籍贯省份
        /// </summary>
        public int NativePlaceProvince { get; set; }

        /// <summary>
        /// 籍贯城市
        /// </summary>
        public int NativePlaceCity { get; set; }

        /// <summary>
        /// 现居住地所在省份
        /// </summary>
        public int PlaceResidenceProvince { get; set; }

        /// <summary>
        /// 现居住地所在城市
        /// </summary>
        public int PlaceResidenceCity { get; set; }

        /// <summary>
        /// 现居住地详细地址
        /// </summary>
        public string PlaceResidenceDetailed { get; set; }

        /// <summary>
        /// 户口所在地省份
        /// </summary>
        public int AccountLocationProvince { get; set; }

        /// <summary>
        /// 户口所在地城市
        /// </summary>
        public int AccountLocationCity { get; set; }

        /// <summary>
        /// 户口所在详细地址
        /// </summary>
        public string AccountLocationDetailed { get; set; }

        /// <summary>
        /// 院校所在省份
        /// </summary>
        public int CollegeProvince { get; set; }

        /// <summary>
        /// 院校所在城市
        /// </summary>
        public int CollegeCity { get; set; }

        /// <summary>
        /// 院校名称
        /// </summary>
        public string UniversityName { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public string Professional { get; set; }

        /// <summary>
        /// 入学年份
        /// </summary>
        public DateTime EnrollmentYear { get; set; }

        /// <summary>
        /// 毕业时间
        /// </summary>
        // public DateTime GraduationDate { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public string Education { get; set; }

        /// <summary>
        /// 学号
        /// </summary>
        public string StudentID { get; set; }

        /// <summary>
        /// 月生活费
        /// </summary>
        public string MonthlyLivingExpenses { get; set; }

        /// <summary>
        /// 家庭人数
        /// </summary>
        //public int FamilySize { get; set; }

        /// <summary>
        /// 亲属联系方式
        /// </summary>
        public string RelativeContactMethod { get; set; }

        /// <summary>
        /// 友人联系方式
        /// </summary>
        public string FriendContactMethod { get; set; }

        /// <summary>
        /// 身份证正面
        /// </summary>
        public string PositiveIdentityCard { get; set; }

        /// <summary>
        /// 身份证反面
        /// </summary>
        public string NegativeIdentityCard { get; set; }

        /// <summary>
        /// 学生证正面
        /// </summary>
        public string StudentIDCard { get; set; }

        /// <summary>
        /// 学籍信息截图
        /// </summary>
        public string StudentInformationScreenshot { get; set; }

        /// <summary>
        /// 银行流水截图
        /// </summary>
        public string Bankwater { get; set; }

        /// <summary>
        /// 记录生成时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 支付宝信息
        /// </summary>
        public string StudentInformationalipay { get; set; }
        public string HeadsetIdentityCard { get; set; }
        public string StudentInformationlastYearAlipay { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }




    }

    public class RelationshipModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 关系
        /// </summary>
        public string Relationship { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        public string Employer { get; set; }
        public string Address { get; set; }


    }
}
