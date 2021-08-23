using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMTransferTool
{
    /// <summary>
    /// 安谋文件数据实体
    /// </summary>
    public class AmFileModel
    {
        /// <summary>
        /// 文档ID
        /// </summary>
        public string Docid { get; set; }

        /// <summary>
        /// 文件夹
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        /// 文档名称--实体文件
        /// </summary>
        public string DocName { get; set; }

        /// <summary>
        /// 所属人
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// 保密等级
        /// </summary>
        public string ConfidentialLevel { get; set; }

        /// <summary>
        /// 文档编号
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// 文档类型
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// 审批流程记录--实体文件
        /// </summary>
        public string ApproveLog { get; set; }

        /// <summary>
        /// 可以访问的用户编号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 可以访问的角色编号
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 可以访问的部门编号
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// ISO标准
        /// </summary>
        public string ISOType { get; set; }

        /// <summary>
        /// 现存路径
        /// </summary>
        public string ExistingpPath { get; set; }

        /// <summary>
        /// 目标路径
        /// </summary>
        public string Targetpath { get; set; }

        public string FileName { get; set; }
    }
}
