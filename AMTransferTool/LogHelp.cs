using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AMTransferTool
{
    public class LogHelp
    {
        private static readonly ILog logInfo = LogManager.GetLogger("Log");
        private static readonly ILog logErr = LogManager.GetLogger("Err");
        /// <summary>
        /// 记录正常的消息
        /// </summary>
        /// <param name="msg">消息内容</param>
        public  void Info(string msg)
        {
            logInfo.Info(msg);
        }
        /// <summary>
        /// 记录异常信息
        /// </summary>
        /// <param name="msg">异常信息内容</param>
        public  void Error(string msg)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();
            logErr.Error("类名:" + methodBase.ReflectedType.Name + " 方法名:" + methodBase.Name + " 信息:" + msg);
        }

    }
}
