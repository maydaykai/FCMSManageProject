using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsModel
{
    public class Ex_PersonModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Sex { get; set; }

        public string Mobile { get; set; }

        public int Age { get; set; }

        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 用户自定义属性XML
        /// </summary>
        public string ExpandToXML { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDel { get; set; }
    }
}
