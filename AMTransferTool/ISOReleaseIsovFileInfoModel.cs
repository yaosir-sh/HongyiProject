using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMTransferTool
{
    /// <summary>
    /// 导入的文件信息实体
    /// </summary>
    public class ISOReleaseIsovFileInfoModel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateId { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateId { get; set; }

        /// <summary>
        /// 文件id
        /// </summary>
        public decimal? FileId { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 版本id
        /// </summary>
        public int FileVerId { get; set; }

        /// <summary>
        /// 文件版本
        /// </summary>
        public string FileVersion { get; set; }

        /// <summary>
        /// 文件编码
        /// </summary>
        public string FileCode { get; set; }

        /// <summary>
        /// 归档目录ID
        /// </summary>
        public decimal ArchiveFolderId { get; set; }

        /// <summary>
        /// 归档目录名称
        /// </summary>
        public string ArchiveFolderName { get; set; }

        /// <summary>
        /// 有效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }
        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime ExpirationDate { get; set; }
        
        /// <summary>
        /// 文件状态
        /// </summary>
        public string FileStatus { get; set; }

        /// <summary>
        /// 文件子状态00=正常，01=预审中，10=待修订（预审），11=待修订（复审），20=修订中（预审），21=修订中（复审），30=复审中，40=作废中，41=流程作废，42=复审作废，43=替换作废(新增)，44=替换作废(修订)
        /// </summary>
        public string FileSubStatus { get; set; }

        /// <summary>
        /// 流程ID
        /// </summary>
        public string Process { get; set; }

        /// <summary>
        /// 主文件夹ID
        /// </summary>
        public decimal? MainFolderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TopfolderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ArchiveFolderPathId { get; set; }

        /// <summary>
        ///修订人IdentityId
        /// </summary>
        public decimal? RevisionIdentityId { get; set; }

        /// <summary>
        /// 复审周期
        /// </summary>
        public string ReviewPeriod { get; set; }

        /// <summary>
        /// 源id
        /// </summary>
        public string DataSourceId { get; set; }

        /// <summary>
        /// 源类型
        /// </summary>
        public decimal? DataSourceType { get; set; }

        /// <summary>
        /// 责任部门IdentityId
        /// </summary>
        public decimal? ResponsibleDeptIdentityId { get; set; }

        /// <summary>
        /// 责任部门ID
        /// </summary>
        public string ResponsibleDeptId { get; set; }

        /// <summary>
        /// 责任部门名称
        /// </summary>
        public string ResponsibleDeptName { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户登录名
        /// </summary>
        public string UserLoginName { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 申请部门ID
        /// </summary>
        public string DeptId { get; set; }

        /// <summary>
        /// 申请部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyDate { get; set; }

        /// <summary>
        /// 实例号
        /// </summary>
        public string Incident { get; set; }

        /// <summary>
        /// 作废日期
        /// </summary>
        public DateTime InvalidDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ArchiveFolderPathName { get; set; }

        /// <summary>
        /// 修订人Id
        /// </summary>
        public string RevisionUserId { get; set; }

        /// <summary>
        /// 修订人Name
        /// </summary>
        public string RevisionUserName { get; set; }
        /// <summary>
        /// 是否过期
        /// </summary>
        public string Overdued { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Isendtrain { get; set; }


        #region newfile部分字段
        public string File { get; set; }
        public string NewestVersion { get; set; }
        public string ResponsibleDept { get; set; }
        public string IsTrain { get; set; }
        public string Urgent { get; set; }
        public string Secrecy { get; set; }
        public DateTime ExpectEffectiveDate { get; set; }
        public string ArchiveFolderFullPath { get; set; }

        public string ArchiveFolder { get; set; }

        public string Remark { get; set; }
        public string DccManager { get; set; }
        public string Reviewer { get; set; }
        public string Approver { get; set; }
        public string Trainer { get; set; }
        public string FileCodeId { get; set; }
        public string FlowStatus { get; set; }
        public string FilePreReviewId { get; set; }
        public string FileCodeStrategyId { get; set; }
        public string ReviewDate { get; set; }
        public string IsAgreed { get; set; }
        public string ApprovalDate { get; set; }
        public string ApprovalOpinions { get; set; }
        public string AttachFile { get; set; }


        #endregion
    }
}
