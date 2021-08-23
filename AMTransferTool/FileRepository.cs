using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMTransferTool
{
    public class FileRepository
    {
        //private static readonly ILog log = LogManager.GetLogger(typeof(FileRepository));
        private static readonly LogHelp _log = new LogHelp();

        public int InsertFileInfo(ISOReleaseIsovFileInfoModel model)
        {
            int count = 0;
            try
            {
                string strSql = @"INSERT INTO iso_release_isov_fileinfo VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}')";
                count = MysqlHelper.ExecuteSql(string.Format(strSql, 
                    model.Id, model.CreateTime,model.ModifiedTime, model.CreateId, model.UpdateId,
                    model.FileId,model.FileName,model.EnglishName,model.FileVerId, model.FileVersion,
                    model.FileCode, model.ArchiveFolderId, model.ArchiveFolderName, model.EffectiveDate,model.ExpirationDate,
                    model.FileStatus,model.FileSubStatus, model.Process, model.MainFolderId,model.TopfolderId, 
                    model.ArchiveFolderPathId,
                    model.RevisionIdentityId, 
                    model.ReviewPeriod,
                    model.DataSourceId,
                    model.DataSourceType, 
                    model.ResponsibleDeptIdentityId,
                    model.ResponsibleDeptId,
                    model.ResponsibleDeptName,
                    model.UserId, model.UserLoginName, model.UserName,model.DeptId, model.DeptName,
                    model.ApplyDate, model.Incident, model.InvalidDate, model.ArchiveFolderPathName, model.RevisionUserId,
                    model.RevisionUserName, model.Overdued, model.Isendtrain));
                  
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return count;

        }
        public int InsertNewFile(ISOReleaseIsovFileInfoModel model)
        {
            int count = 0;
            try
            {
                string strSql = @"INSERT INTO iso_release_isov_newfile VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}')";
                count = MysqlHelper.ExecuteSql(string.Format(strSql,
                    model.Id, model.CreateTime, model.ModifiedTime, model.CreateId, model.UpdateId,
                    model.FileCode, model.FileName, model.EnglishName, model.File, model.NewestVersion,
                    model.ResponsibleDept, model.IsTrain, model.UserName, model.DeptId, model.Urgent,
                    model.Secrecy, model.ExpectEffectiveDate, model.EffectiveDate, model.ReviewPeriod,
                    model.ExpirationDate, model.ArchiveFolderFullPath, model.ArchiveFolder, model.Remark,
                    model.DccManager, model.Reviewer, model.Approver, model.Trainer, model.UserId,
                    model.DeptName, model.Process, model.FileCodeId, model.MainFolderId, model.FilePreReviewId,
                    model.Isendtrain, model.UserLoginName, model.Incident, model.FlowStatus, model.FileCodeStrategyId,
                    model.FileId, model.IsAgreed, model.ReviewDate, model.ApprovalDate, model.ApprovalOpinions, model.AttachFile));

            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return count;
        }

        //更新文件信息
        public int updateallinfo(string BZBMMC, string hrmddc, string hrmbzbmmkfzr, string filecode, string CompilingDept, string oldfilecode, string CompilingDeptCode)
        {

            string strSql1 = "";
            string strSql2 = "";
            string strSql3 = "";
            string strSql4 = "";
            int i1 = -1;
            int i2 = -1;
            int i3 = -1;
            int i4 = -1;
            int i = 0;
            try
            {
                strSql1 = @" UPDATE eform_iso_edoc2_release_isov_htfileinfo SET BZBMMC = '{0}', hrmddc = '{1}', hrmbzbmmkfzr = '{2}', filecode = '{3}', CompilingDept = '{4}' WHERE filecode = '{5}'";
                strSql2 = @" UPDATE eform_iso_edoc2_release_isov_htfilerevision SET  hrmddc = '{0}', hrmbzbmmkfzr = '{1}', filecode = '{2}', CompilingDept = '{3}' WHERE filecode = '{4}'";
                strSql3 = @" UPDATE eform_iso_edoc2_release_isov_htnewfile SET hrmddc = '{0}', hrmbzbmmkfzr = '{1}', filecode = '{2}', CompilingDept = '{3}' WHERE filecode = '{4}'";
                strSql4 = @" UPDATE eform_iso_edoc2_release_isov_htrecordfileinfo SET RecordFileCode = '{0}' WHERE RecordFileCode = '{1}'";
                i1 = MysqlHelper.ExecuteSql(string.Format(strSql1, BZBMMC, hrmddc, hrmbzbmmkfzr, filecode, CompilingDeptCode, oldfilecode));
                i2 = MysqlHelper.ExecuteSql(string.Format(strSql2, hrmddc, hrmbzbmmkfzr, filecode, CompilingDept, oldfilecode));
                i3 = MysqlHelper.ExecuteSql(string.Format(strSql3, hrmddc, hrmbzbmmkfzr, filecode, CompilingDept, oldfilecode));
                i4 = MysqlHelper.ExecuteSql(string.Format(strSql4, filecode, oldfilecode));
                i = i1 + i2 + i3 + i4;
            }

            catch (Exception ex)
            {
                //log.Error(ex.Message);
                _log.Error(ex.Message);
            }
            return i;

        }

        //插入文件信息-线缆
        public int insertnewfile(string Id, string createTime, string modifiedTime, string createId, string updateId, string FileCode, string FileName, string EnglishName, string FileS, string NewestVersion, string ResponsibleDept, string IsTrain, string UserName, string DeptId, string ExpectEffectiveDate, string EffectiveDate, string ReviewPeriod, string ExpirationDate, string ArchiveFolderFullPath, string ArchiveFolder, string Remark, string Approver, string UserId, string DeptName, string Process, string FileCodeId, string MainFolderId, string FilePreReviewId, string Isendtrain, string UserLoginName, string Incident, string FlowStatus, string FileCodeStrategyId, string FileId, string IsAgreed, string ReviewDate, string ApprovalDate, string CompanyCode, string ApplicationScope, string FileLevel, string CompilingDept, string userCompany, string FileType, string DocType, string AdaptStandard, string IsDraft, string CompanyID, string wjlb, string hrmddc, string countSign, string Security, string OldFileCode, string BZBMFZR, string fileVersionID, string fileversion)
        {
            DataSet ds = null;
            DataTable dt = null;
            string strSql = "";
            int i = -1;
            try
            {
                //strSql= @"insert into eform_iso_edoc2_release_isov_htfileinfo('Id','createTime','modifiedTime','createId','updateId','FileId','FileName','EnglishName','FileVerId','FileVersion','FileCode','ArchiveFolderId','ArchiveFolderName','EffectiveDate','ExpirationDate','FileStatus','FileSubStatus','Process','MainFolderId','TopfolderId','ArchiveFolderPathId','DataSourceId','DataSourceType','UserId','UserLoginName','UserName','DeptId','DeptName','ApplyDate','Incident','InvalidDate','ArchiveFolderPathName','RevisionUserId','RevisionUserName','Overdued','Isendtrain','RevisionIdentityId','CompanyCode','ApplicationScope','FileLevel','CompilingDept','FileType','DocType','AdaptStandard','IsTrain','IsDraft','Remark','FlowStatus','userCompany','ZLStatus','wjlb','FileCodeStrategyId','waterFlow','hrmddc','countSign','Approver','Security')values('" + Id + "', getdate(), getdate(),'525da08294cb430f9a0af61f8062fbd5','525da08294cb430f9a0af61f8062fbd5','" + EformFileName + @"','3','','','','" + FileNumber + @"','','','','','0','1','" + ClassFlowCode + @"','admin','" + FourExclusive + @"')";
                strSql = @"INSERT INTO eform_iso_edoc2_release_isov_htnewfile VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}','{51}','{52}','{53}','{54}')";
                //i = MysqlDBHelp.ExecuteSql(string.Format(strSql, Id, createTime, modifiedTime, createId, updateId, FileCode, FileName, EnglishName, FileS, NewestVersion, ResponsibleDept, IsTrain, UserName, DeptId, ExpectEffectiveDate, EffectiveDate,ReviewPeriod, ExpirationDate, ArchiveFolderFullPath, ArchiveFolder, Remark, Approver, UserId, DeptName, Process, FileCodeId, MainFolderId, FilePreReviewId, Isendtrain, UserLoginName, Incident, FlowStatus, FileCodeStrategyId, FileId, IsAgreed, ReviewDate, ApprovalDate, CompanyCode, ApplicationScope, FileLevel, CompilingDept, userCompany, FileType, DocType, AdaptStandard, IsDraft, CompanyID, wjlb, hrmddc, countSign, Security, OldFileCode,BZBMFZR,fileVersionID,fileversion));
                i = MysqlHelper.ExecuteSql(string.Format(strSql, Id, createTime, modifiedTime, createId, updateId, UserName, userCompany, CompanyID, DeptId, ArchiveFolder, NewestVersion, fileversion, FileS, CompanyCode, FileType, ApplicationScope, wjlb, Security, countSign, Approver, FileLevel, CompilingDept, DocType, ExpectEffectiveDate, EffectiveDate, ExpirationDate, OldFileCode, AdaptStandard, IsTrain, Remark, hrmddc, UserId, DeptName, Process, FileCodeId, MainFolderId, FilePreReviewId, Isendtrain, FileName, FileCode, BZBMFZR, UserLoginName, Incident, FlowStatus, FileCodeStrategyId, FileId, IsAgreed, ReviewDate, ApprovalDate, IsDraft, fileVersionID, ArchiveFolderFullPath, EnglishName, ResponsibleDept, ReviewPeriod));

            }

            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return i;

        }

        //插入文件信息-线缆
        public int insertfileinfo(string Id, string createTime, string modifiedTime, string createId, string updateId, string FileId, string FileName, string EnglishName, string FileVerId, string FileVersion, string FileCode, string ArchiveFolderId, string ArchiveFolderName, string EffectiveDate, string ExpirationDate, string FileStatus, string FileSubStatus, string Process, string MainFolderId, string TopfolderId, string ArchiveFolderPathId, string DataSourceId, string DataSourceType, string UserId, string UserLoginName, string UserName, string DeptId, string DeptName, string ApplyDate, string Incident, string InvalidDate, string ArchiveFolderPathName, string RevisionUserId, string RevisionUserName, string Overdued, string Isendtrain, string RevisionIdentityId, string CompanyCode, string ApplicationScope, string FileLevel, string CompilingDept, string FileType, string DocType, string AdaptStandard, string IsTrain, string IsDraft, string Remark, string FlowStatus, string userCompany, string ZLStatus, string wjlb, string FileCodeStrategyId, string waterFlow, string hrmddc, string countSign, string Approver, string Security, string OldFileCode, string BZBMFZR, string CompanyID, string BZBMMC)
        {
            DataSet ds = null;
            DataTable dt = null;
            string strSql = "";
            int i = -1;
            try
            {

                //strSql= @"insert into eform_iso_edoc2_release_isov_htfileinfo('Id','createTime','modifiedTime','createId','updateId','FileId','FileName','EnglishName','FileVerId','FileVersion','FileCode','ArchiveFolderId','ArchiveFolderName','EffectiveDate','ExpirationDate','FileStatus','FileSubStatus','Process','MainFolderId','TopfolderId','ArchiveFolderPathId','DataSourceId','DataSourceType','UserId','UserLoginName','UserName','DeptId','DeptName','ApplyDate','Incident','InvalidDate','ArchiveFolderPathName','RevisionUserId','RevisionUserName','Overdued','Isendtrain','RevisionIdentityId','CompanyCode','ApplicationScope','FileLevel','CompilingDept','FileType','DocType','AdaptStandard','IsTrain','IsDraft','Remark','FlowStatus','userCompany','ZLStatus','wjlb','FileCodeStrategyId','waterFlow','hrmddc','countSign','Approver','Security')values('" + Id + "', getdate(), getdate(),'525da08294cb430f9a0af61f8062fbd5','525da08294cb430f9a0af61f8062fbd5','" + EformFileName + @"','3','','','','" + FileNumber + @"','','','','','0','1','" + ClassFlowCode + @"','admin','" + FourExclusive + @"')";
                strSql = @"INSERT INTO eform_iso_edoc2_release_isov_htfileinfo VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}','{51}','{52}','{53}','{54}','{55}','{56}','{57}','{58}','{59}','{60}','{61}','{62}')";
                //i = MysqlDBHelp.ExecuteSql(string.Format(strSql, Id,createTime,modifiedTime,createId,updateId,FileId,FileName,EnglishName,FileVerId,FileVersion,FileCode,ArchiveFolderId,ArchiveFolderName,EffectiveDate,ExpirationDate,FileStatus,FileSubStatus,Process,MainFolderId,TopfolderId,ArchiveFolderPathId,DataSourceId,DataSourceType,UserId,UserLoginName,UserName,DeptId,DeptName,ApplyDate,Incident,InvalidDate,ArchiveFolderPathName,RevisionUserId,RevisionUserName,Overdued,Isendtrain,RevisionIdentityId,CompanyCode,ApplicationScope,FileLevel,CompilingDept,    FileType,DocType,AdaptStandard,IsTrain,IsDraft,Remark,FlowStatus,userCompany,ZLStatus,wjlb,FileCodeStrategyId,waterFlow,hrmddc,countSign,Approver,Security,OldFileCode, BZBMFZR,CompanyID, BZBMMC));

                i = MysqlHelper.ExecuteSql(string.Format(strSql, Id, createTime, modifiedTime, createId, updateId, FileId, FileName, EnglishName, FileVerId, FileVersion, OldFileCode, FileCode, ArchiveFolderId, ArchiveFolderName, EffectiveDate, ExpirationDate, FileStatus, FileSubStatus, Process, MainFolderId, TopfolderId, ArchiveFolderPathId, CompanyCode, ApplicationScope, FileType, AdaptStandard, IsDraft, FlowStatus, userCompany, ZLStatus, waterFlow, hrmddc, Security, "", "", DataSourceId, DataSourceType, UserId, UserLoginName, UserName, DeptId, DeptName, ApplyDate, Incident, InvalidDate, ArchiveFolderPathName, RevisionUserId, RevisionUserName, Overdued, Isendtrain, RevisionIdentityId, FileLevel, CompilingDept, DocType, IsTrain, Remark, wjlb, FileCodeStrategyId, countSign, Approver, BZBMFZR, CompanyID, BZBMMC));

            }

            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return i;

        }


        public DataTable SelectFileInfo(string fileName)
        {
            DataSet ds = null;
            DataTable dt = null;
            string strSql = "";
            try
            {
                //                strSql = @"select * from
                //(select a.file_id,a.folder_id,a.file_name,file_isDeleted,LEFT(a.file_name,CHAR_LENGTH(a.file_name) - LOCATE('.',REVERSE(a.file_name))) AS file_nameNew,
                //b.folder_name,b.folder_path,a.file_lastVerId from edoc2v5.dms_file a LEFT JOIN dms_folder b on b.folder_id=a.folder_id) info where info.file_nameNew ='{0}'
                //and info.folder_path like '%149077%' and info.file_isDeleted='0'";
                strSql = @"select * from
(select a.file_id,a.folder_id,a.file_name,file_isDeleted,LEFT(a.file_name,CHAR_LENGTH(a.file_name) - LOCATE('.',REVERSE(a.file_name))) AS file_nameNew,
b.folder_name,b.folder_path,a.file_lastVerId from edoc2v5.dms_file a LEFT JOIN dms_folder b on b.folder_id=a.folder_id) info 
where info.file_nameNew ='{0}' and info.file_isDeleted='0'";
                ds = MysqlHelper.Query(string.Format(strSql, fileName));

                if (ds != null)
                {
                    dt = ds.Tables[0];

                }
            }

            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return dt;

        }

       

        public DataTable selectrecordfileinfo(string fileName)
        {
            DataSet ds = null;
            DataTable dt = null;
            string strSql = "";

            try
            {
                //strSql = @"select file_id,folder_id from edoc2v5.dms_file where file_name like '%{0}%' and file_path like '%337%'"; 682
                //strSql = @"select a.file_id,a.folder_id,a.file_name,b.folder_name,b.folder_path,a.file_lastVerId from edoc2v5.dms_file a LEFT JOIN dms_folder b on b.folder_id=a.folder_id where a.file_name like '%{0}%' and b.folder_path like '%746%'";

                strSql = @"select * from
(select a.file_id,a.folder_id,a.file_name,file_isDeleted,LEFT(a.file_name,CHAR_LENGTH(a.file_name) - LOCATE('.',REVERSE(a.file_name))) AS file_nameNew,
b.folder_name,b.folder_path,a.file_lastVerId from edoc2v5.dms_file a LEFT JOIN dms_folder b on b.folder_id=a.folder_id) info where info.file_nameNew ='{0}' and info.folder_path like '%146826%' and info.file_isDeleted='0'";
                ds = MysqlHelper.Query(string.Format(strSql, fileName));

                if (ds != null)
                {
                    dt = ds.Tables[0];

                }
            }

            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return dt;

        }



        public DataTable selectDepartmentInfo(string dept_identityID)
        {
            DataSet ds = null;
            DataTable dt = null;
            string strSql = "";
            try
            {
                //strSql = @"select file_id,folder_id from edoc2v5.dms_file where file_name like '%{0}%' and file_path like '%337%'"; 682
                strSql = @"select   dept_id,dept_name  from org_department  where dept_identityID='{0}'";
                ds = MysqlHelper.Query(string.Format(strSql, dept_identityID));

                if (ds != null)
                {
                    dt = ds.Tables[0];

                }
            }

            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return dt;

        }



        public DataTable selectUserInfo(string DeptCode)
        {
            DataSet ds = null;
            DataTable dt = null;
            string strSql = "";
            try
            {
                //strSql = @"select file_id,folder_id from edoc2v5.dms_file where file_name like '%{0}%' and file_path like '%337%'"; 682
                strSql = @"select * from eform_iso_CompilingDept where subcompany='59c1e597-add1-437c-9cfd-990d33e933bf' and DeptCode='{0}'";
                ds = MysqlHelper.Query(string.Format(strSql, DeptCode));

                if (ds != null)
                {
                    dt = ds.Tables[0];

                }
            }

            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return dt;

        }

        public DataTable selectFileInfo(string filecode)
        {
            DataSet ds = null;
            DataTable dt = null;
            string strSql = "";
            try
            {
                //strSql = @"select file_id,folder_id from edoc2v5.dms_file where file_name like '%{0}%' and file_path like '%337%'"; 682
                strSql = @"  SELECT * FROM eform_iso_edoc2_release_isov_htfileinfo where filecode = '{0}' and FileStatus = '20'";
                ds = MysqlHelper.Query(string.Format(strSql, filecode));

                if (ds != null)
                {
                    dt = ds.Tables[0];

                }
            }

            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return dt;

        }

        public DataTable selectrecordFileInfo(string filecode)
        {
            DataSet ds = null;
            DataTable dt = null;
            string strSql = "";
            try
            {
                //strSql = @"select file_id,folder_id from edoc2v5.dms_file where file_name like '%{0}%' and file_path like '%337%'"; 682
                strSql = @"  SELECT * FROM eform_iso_edoc2_release_isov_htrecordfileinfo where RecordFileCode = '{0}'";
                ds = MysqlHelper.Query(string.Format(strSql, filecode));

                if (ds != null)
                {
                    dt = ds.Tables[0];

                }
            }

            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return dt;

        }





        public DataTable selectUserId(string userid)
        {
            DataSet ds = null;
            DataTable dt = null;
            string strSql = "";
            try
            {
                //strSql = @"select file_id,folder_id from edoc2v5.dms_file where file_name like '%{0}%' and file_path like '%337%'"; 682
                strSql = @"select user_account from org_user where user_id ='{0}'";
                string ssss = string.Format(strSql, userid);
                ds = MysqlHelper.Query(ssss);

                if (ds != null)
                {
                    dt = ds.Tables[0];

                }
            }

            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return dt;

        }




        //插入文件信息-线缆
        public int insertDistribution(string Id, string createTime, string modifiedTime, string createId, string updateId, string distribution, string permission, string papernumber, string distributionType, string ReferenceId, string parentFormrecordId)
        {
            string strSql = "";
            int i = -1;
            try
            {
                //strSql= @"insert into eform_iso_edoc2_release_isov_htfileinfo('Id','createTime','modifiedTime','createId','updateId','FileId','FileName','EnglishName','FileVerId','FileVersion','FileCode','ArchiveFolderId','ArchiveFolderName','EffectiveDate','ExpirationDate','FileStatus','FileSubStatus','Process','MainFolderId','TopfolderId','ArchiveFolderPathId','DataSourceId','DataSourceType','UserId','UserLoginName','UserName','DeptId','DeptName','ApplyDate','Incident','InvalidDate','ArchiveFolderPathName','RevisionUserId','RevisionUserName','Overdued','Isendtrain','RevisionIdentityId','CompanyCode','ApplicationScope','FileLevel','CompilingDept','FileType','DocType','AdaptStandard','IsTrain','IsDraft','Remark','FlowStatus','userCompany','ZLStatus','wjlb','FileCodeStrategyId','waterFlow','hrmddc','countSign','Approver','Security')values('" + Id + "', getdate(), getdate(),'525da08294cb430f9a0af61f8062fbd5','525da08294cb430f9a0af61f8062fbd5','" + EformFileName + @"','3','','','','" + FileNumber + @"','','','','','0','1','" + ClassFlowCode + @"','admin','" + FourExclusive + @"')";
                strSql = @"INSERT INTO edoc2_release_isov_distribution VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')";
                i = MysqlHelper.ExecuteSql(string.Format(strSql, Id, createTime, modifiedTime, createId, updateId, distribution, permission, papernumber, distributionType, ReferenceId, parentFormrecordId));
            }

            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return i;

        }



        //插入文件信息-线缆
        public int insertDistributionUser(string Id, string createTime, string modifiedTime, string createId, string updateId, string referenceId, string memberId, string membername, string MemberType, string DistributionStatus, string RecycleStatus, string DistributionType, string AuthStatus, string IsDraft, string DataSourceId, string RefId, string DistributionPeople, string DistributionDate, string RecyclePeople, string RecycleDate, string RecycleReason, string PaperNumber, string PrintedNumber, string DistributionNumber, string RecycleNumber)
        {
            string strSql = "";
            int i = -1;
            try
            {
                strSql = @"INSERT INTO edoc2_release_isov_distributionunit VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}')";
                i = MysqlHelper.ExecuteSql(string.Format(strSql, Id, createTime, modifiedTime, createId, updateId, PaperNumber, DistributionNumber, referenceId, memberId, membername, MemberType, DistributionStatus, RecycleStatus, DistributionType, AuthStatus, IsDraft, DataSourceId, RefId, DistributionPeople, DistributionDate, RecyclePeople, RecycleDate, RecycleReason, PrintedNumber, RecycleNumber));
            }

            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return i;

        }
    }
}
