using EDoc2.IAppService;
using EDoc2.IAppService.Model;
using EDoc2.Sdk;
using EDoc2.SDK.Transfer;
using EDoc2.SDK.Transfer.Modules;
using log4net;
using NPOI.SS.UserModel;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMTransferTool
{
    public partial class TarnsferTool : Form
    {
        public TarnsferTool()
        {
            InitializeComponent();
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(TarnsferTool));
        private static readonly LogHelp logs = new LogHelp();
        FileRepository repository = new FileRepository();
        ITransferAppService transfer = ServiceContainer.GetService<ITransferAppService>();
        int successCount = 0;
        int excelCount = 0;
        private void btnReader_Click(object sender, EventArgs e)
        {
            try
            {
                string file = System.Configuration.ConfigurationManager.AppSettings["filelocation"];
                //log.Info(file); //写日志
                //logs.Error(file);
                DataTable dt = LoadExcelData(file);
                txtLabel.Text = "一共迁移"+excelCount+"条数据，成功了"+ successCount + "条";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// 加载excel数据
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private DataTable LoadExcelData(string file)
        {
            try
            {
                if (!File.Exists(file))
                {
                    MessageBox.Show("迁移工具的excel文件位置未找到!");
                }
                DataTable dt = new DataTable();
                #region 基础配置
                SdkBaseInfo.BaseUrl = System.Configuration.ConfigurationManager.AppSettings["ApiServiceUrl"];
                IOrgAppService orgAppService = ServiceContainer.GetService<IOrgAppService>();
                IFileAppService fileAppService = ServiceContainer.GetService<IFileAppService>();
                IDocAppService docAppService = ServiceContainer.GetService<IDocAppService>();
                IFolderAppService folderAppService = ServiceContainer.GetService<IFolderAppService>();
                UserLoginIntegrationByUserLoginNameDto userLoginDto = new UserLoginIntegrationByUserLoginNameDto();
                IFilePermissionAppService filePermissionAppService = ServiceContainer.GetService<IFilePermissionAppService>();
                #endregion

                #region 获取用户登录token
                userLoginDto.LoginName = "admin";
                userLoginDto.IPAddress = "127.0.0.1";
                //userLoginDto.IntegrationKey = "46aa92ec-66af-4818-b7c1-8495a9bd7f17";
                userLoginDto.IntegrationKey = "46aa92ec-66af-4818-b7c1-8495a9bd7f17";
                var loginResult = orgAppService.UserLoginIntegrationByUserLoginName(userLoginDto);
                if (loginResult == null || string.IsNullOrWhiteSpace(loginResult.Data))
                {

                    return dt;
                }
                var token = loginResult.Data;//获取admin的token或者更改为管理员的token
                #endregion

                FileStream fsRead = new FileStream(file.ToString(), FileMode.Open);
                //创建工作薄
                IWorkbook workBook = WorkbookFactory.Create(fsRead);
                //获取Sheet
                ISheet sheet = workBook.GetSheetAt(0);
                int ExcelRowsCount = sheet.LastRowNum;
                excelCount = ExcelRowsCount-1;
                int[] sss = sheet.ColumnBreaks;
                IsTrue(ExcelRowsCount == 1, "未读到Excel数据!");
                IRow currentRow;

                /*
            * 地址传递可访问的ecm服务的地址
            * version 就传递当前使用的版本,非必要，但如果使用的话，请还是传递正确的版本，为未来的优化工作做铺垫
            */
                var config = new TransferConfig("http://192.168.251.185/", Edoc2Version.V5_17)
                {
                    /*
                     * 是否重用tcp连接,可减少握手带来的延迟，但同时由于复用了连接，没有了dns刷新，此处自由取舍
                     * 建议场景:一次多个文件需要上传的场景，可以开启
                     */
                    CacheHttpClient = true,
                    MaxErrorRetry = 2, // 重试次数
                    OpenSpeedLog = true,// 是否开启上传速率日志,打印至控制台
                    Timeout = TimeSpan.FromMilliseconds(60 * 1000),// 超时时间(毫秒ms)

                };
                EDoc2TransferClient client = new EDoc2TransferClient(config);
                for (int i = 1; i < ExcelRowsCount; i++)
                {
                    //当前行数据
                    currentRow = sheet.GetRow(i);
                    AmFileModel amFileModel = new AmFileModel();
                    amFileModel.Docid = currentRow.Cells[0].ToString();//文档id
                    amFileModel.Folder = currentRow.Cells[1].ToString();//文件夹
                    amFileModel.DocName = currentRow.Cells[2].ToString();//文档名称--实体文件
                    amFileModel.Owner = currentRow.Cells[3].ToString();//所属人
                    amFileModel.ConfidentialLevel = currentRow.Cells[4].ToString();//保密等级
                    amFileModel.DocumentNumber = currentRow.Cells[5].ToString();//文档编号
                    amFileModel.DocumentType = currentRow.Cells[6].ToString();//文档类型
                    amFileModel.ApproveLog = currentRow.Cells[7].ToString();//审批流程记录--实体文件
                    amFileModel.UserId = currentRow.Cells[8].ToString();//可以访问的用户编号
                    amFileModel.RoleId = currentRow.Cells[9].ToString();//可以访问的角色编号
                    amFileModel.DepartmentId = currentRow.Cells[10].ToString();//可以访问的部门编号
                    amFileModel.ISOType = currentRow.Cells[11].ToString();//ISO标准
                    amFileModel.ExistingpPath = currentRow.Cells[12].ToString();//现有文件路径
                    amFileModel.Targetpath = currentRow.Cells[13].ToString();//上传文件路径
                    amFileModel.FileName = currentRow.Cells[14].ToString();//文件名称
                    UploadFileCreateAsync(client, token, amFileModel.ExistingpPath, amFileModel.FileName);

                    string name = Path.GetFileNameWithoutExtension(amFileModel.FileName);
                    DataTable fileInfos = null;
                    fileInfos = repository.SelectFileInfo(name);//根据实例号获取文件信息  加入parentfolderid条件  文件夹id
                    //根据上传返回文件信息新增
                    if (fileInfos != null && fileInfos.Rows.Count == 1)
                    {
                        InsertFile(fileInfos, token, fileAppService, amFileModel);
                    }
                    else
                    {
                        log.Error("未在数据库中查到该文件：" + amFileModel.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        /// <summary>
        /// 新增迁移文件
        /// </summary>
        /// <param name="fileInfos"></param>
        /// <param name="token"></param>
        /// <param name="fileAppService"></param>
        private void InsertFile(DataTable fileInfos, string token, IFileAppService fileAppService, AmFileModel amFileModel)
        {
            string folder_id = fileInfos.Rows[0]["folder_id"].ToString();//文件夹ID
            string file_id = fileInfos.Rows[0]["file_id"].ToString();//文件ID
            string file_name = fileInfos.Rows[0]["file_name"].ToString();//文件名称
            string folder_name = fileInfos.Rows[0]["folder_name"].ToString();//文件夹名称
            string file_lastVerId = fileInfos.Rows[0]["file_lastVerId"].ToString();//文件夹名称
            string fileS = "[{\"fileId\":\"" + file_id + "\",\"fileName\":\"" + file_name + "\"}]";//newfile 需要
            string archiveFolder = "[{\"folderId\":\"" + folder_id + "\",\"folderName\":\"" + folder_name + "\"}]";//newfile 需要
            string folder_path = fileInfos.Rows[0]["folder_path"].ToString();//文件夹名称
            FileDto fileinfo = new FileDto();
            fileinfo.Token = token;
            fileinfo.FileId = Convert.ToInt32(file_id);
            var result = fileAppService.PublishFileVersion(fileinfo);//发布文件版本;
            int fileLastVerId = result.Data.FileLastVerId;//最新文件版本号  需更新至文件内


            #region iso_release_isov_fileinfo表数据结构
            ISOReleaseIsovFileInfoModel isovFileInfoModel = new ISOReleaseIsovFileInfoModel();
            isovFileInfoModel.Id = Guid.NewGuid().ToString();
            isovFileInfoModel.CreateTime = DateTime.Now;
            isovFileInfoModel.ModifiedTime = DateTime.Now;
            isovFileInfoModel.CreateId = "90eb0c6350ce4284828e633304decc01";
            isovFileInfoModel.UpdateId = "90eb0c6350ce4284828e633304decc01";
            isovFileInfoModel.FileName = file_name;
            isovFileInfoModel.FileCode = fileinfo.FileId.ToString();//新编号
            isovFileInfoModel.Incident = "测试实例号";
            isovFileInfoModel.UserName = "admin";
            isovFileInfoModel.DeptName = amFileModel.DepartmentId;
            isovFileInfoModel.EffectiveDate = DateTime.Now;
            isovFileInfoModel.ExpirationDate = DateTime.Now.AddYears(3);
            isovFileInfoModel.TopfolderId = "";
            isovFileInfoModel.ArchiveFolderPathName = "";
            isovFileInfoModel.FileId = Convert.ToDecimal(file_id);
            isovFileInfoModel.EnglishName = "测试英文";
            isovFileInfoModel.FileVerId = 0;
            isovFileInfoModel.FileVersion = "1.0";
            isovFileInfoModel.ArchiveFolderId = Convert.ToDecimal(folder_id);
            isovFileInfoModel.ArchiveFolderName = "归档目录名称";
            isovFileInfoModel.FileStatus = "20";
            isovFileInfoModel.FileSubStatus = "00";
            isovFileInfoModel.Process = "流程ID";
            isovFileInfoModel.MainFolderId = 0;
            isovFileInfoModel.ArchiveFolderPathId = "";
            isovFileInfoModel.RevisionIdentityId = 0;
            isovFileInfoModel.ReviewPeriod = "复审周期";
            isovFileInfoModel.DataSourceId = "源id";
            isovFileInfoModel.DataSourceType = 1;
            isovFileInfoModel.ResponsibleDeptIdentityId = 0;
            isovFileInfoModel.ResponsibleDeptId = "责任部门ID";
            isovFileInfoModel.ResponsibleDeptName = "责任部门名称";
            isovFileInfoModel.UserId = "90eb0c6350ce4284828e633304decc01";
            isovFileInfoModel.UserLoginName = "admin";
            isovFileInfoModel.DeptId = "申请部门ID";
            isovFileInfoModel.ApplyDate = DateTime.Now;
            isovFileInfoModel.InvalidDate = DateTime.Now.AddYears(-1);//作废日期
            isovFileInfoModel.RevisionUserId = "修订人Id";
            isovFileInfoModel.RevisionUserName = "修订人Name";
            isovFileInfoModel.Overdued = "是否过期";
            isovFileInfoModel.Isendtrain = "-1";

            #endregion



            
            
            //string Security = "0";
            //string OldFileCode = amFileModel.DocumentNumber;
            //string IsAgreed = "Y";
            //string ReviewDate = ExpirationDate;
            //string ApprovalDate = EffectiveDate;
            //string CompanyID = "44a922b4-3b61-490c-b160-417fd066c505";
            //string FileCodeId = "";
            //string FilePreReviewId = "";
            //string ArchiveFolderFullPath = "";
            //string ResponsibleDept = "";
            //string NewestVersion = "0.1";
            //string ExpectEffectiveDate = EffectiveDate;
            //string FileCodeStrategyId = "2825a4cf-4666-4cdd-8c4b-7b39f18e82fa";//线缆 正式机 4375939b-f52f-42c0-ac25-9daa3fef1e66  测试机 4da1211d-bac9-42a9-859e-df23fd6a134d
            //string waterFlow = waterflowcode;
            //string Security = "0";
            //string OldFileCode = oldFileNumber;
            //string IsAgreed = "Y";
            //string ReviewDate = ExpirationDate;
            //string ApprovalDate = EffectiveDate;
            //string CompanyID = "44a922b4-3b61-490c-b160-417fd066c505";
            //string NewestVersion = "0.1";
            //string ExpectEffectiveDate = EffectiveDate;

            //string hrmbzbmmkfzr = "安谋测试001";
            //string fileinfoid = Guid.NewGuid().ToString();
            ////2.0修改ECM文件信息主要是修改生效时间（后续设置水印需要用到）
            //UpdateFileDto updatefileinfo = new UpdateFileDto();
            //updatefileinfo.Token = token;
            //updatefileinfo.FileId = Convert.ToInt32(FileId);
            //updatefileinfo.NewName = "";
            //updatefileinfo.FileCode = isovFileInfoModel.FileCode;
            //updatefileinfo.EffectiveTime = DateTime.Now.ToString();
            //updatefileinfo.ExpirationTime = "";
            //updatefileinfo.Remark = "迁移数据";
            //var changeResult = fileAppService.ChangeFileById(updatefileinfo);
            //string distributionId = Guid.NewGuid().ToString();
            //string unit1id = Guid.NewGuid().ToString();

            //int sb1 = repository.insertnewfile(Id, createTime, modifiedTime, createId, updateId, FileCode, FileName, EnglishName, fileS, NewestVersion, ResponsibleDept, IsTrain, UserName, DeptId, ExpectEffectiveDate, EffectiveDate, ReviewPeriod, ExpirationDate, ArchiveFolderFullPath, archiveFolder, Remark, Approver, UserId, DeptName, Process, FileCodeId, MainFolderId, FilePreReviewId, Isendtrain, UserLoginName, Incident, FlowStatus, FileCodeStrategyId, FileId, IsAgreed, ReviewDate, ApprovalDate, companyCode, ApplicationScope, FileLevel, CompilingDept, userCompany, FileType, DocType, AdaptStandard, IsDraft, CompanyID, wjlb, hrmddc, countSign, Security, OldFileCode, hrmbzbmmkfzr, FileVerId, FileVersion);

            int fileCount = repository.InsertNewFile(isovFileInfoModel);
            //int sb = repository.InsertFileInfo(fileinfoid, createTime, modifiedTime, createId, updateId, FileId, FileName, EnglishName, FileVerId, FileVersion, FileCode, ArchiveFolderId, ArchiveFolderName, EffectiveDate, ExpirationDate, FileStatus, FileSubStatus, Process, MainFolderId, TopfolderId, ArchiveFolderPathId, DataSourceId, DataSourceType, UserId, UserLoginName, UserName, DeptId, DeptName, ApplyDate, Incident, InvalidDate, ArchiveFolderPathName, RevisionUserId, RevisionUserName, Overdued, Isendtrain, RevisionIdentityId, NewCompanyCode, ApplicationScope, FileLevel, CompilingDept, FileType, DocType, AdaptStandard, IsTrain, IsDraft, Remark, FlowStatus, userCompany, ZLStatus, wjlb, FileCodeStrategyId, waterFlow, hrmddc, countSign, Approver, Security, OldFileCode, hrmbzbmmkfzr, CompanyID, bianzhibumen);//根据实例号获取文件信息  
            int count = repository.InsertFileInfo(isovFileInfoModel);//根据实例号获取文件信息  
            if (count <= 0)
            {
                log.Error("该条数据迁移失败："+file_id);
            }
            successCount += count;

        }

        public void IsTrue(bool flag, string msg)
        {
            if (flag)
            {
                throw new Exception(msg);
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token"></param>
        /// <param name="existingpPath"></param>
        /// <param name="fName"></param>
        /// <returns></returns>
        public async Task<CreateFileResponse> UploadFileCreateAsync(EDoc2TransferClient client, string token, string existingpPath, string fName)
        {
            // 用户的登录令牌,如需运行请自行，找一个，或者使用 Samples.1_auth.GetToken
            //var filePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "测试文件.docx");
            var filePath = Path.Combine(existingpPath, fName);
            // 文件至少是一个真实存在的文件
            if (!File.Exists(filePath))
            {
                log.Error("文件不存在，请指定一个存在的文件：" + filePath);
                return null;
            }

            var fileStream = File.OpenRead(filePath);
            var fileName = Path.GetFileName(filePath);
            var request = new CreateFileRequest()
            {
                Token = token,
                FileName = fileName,
                FileRemark = "安谋迁移",
                FileSize = fileStream.Length,// 真实的文件大小
                FolderId = 1,
                /*
                 * 上传遇到同名文件处理策略
                 * UpdateStrategy.Skip 跳过上传,不再继续上传
                 * UpdateStrategy.UpdateVersion 自动将上传新文件改成更新同名文件的版本
                 */
                UpdateStrategy = UpdateStrategy.UpdateVersion,
                ChunkSize = 5 * 1024 * 1024//单位 byte
            };

            var resp = await client.CreateFile(request, filePath);
            if (!resp.IsSuccess)
            {
                log.Error("错误码" + resp.ErrorCode.ToString() + "错误消息" + resp.Reason);//错误码

            }
            return resp;
        }

    }
}
