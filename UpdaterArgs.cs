namespace Nicengi.Update
{
    public sealed class UpdaterArgs
    {
        #region Properties
        /// <summary>
        /// 获取或设置更新服务器地址。
        /// </summary>
        public string UpdateUrl { get; set; }

        /// <summary>
        /// 获取或设置软件的名称。
        /// </summary>
        public string SoftwareName { get; set; }

        /// <summary>
        /// 获取或设置软件的 GUID。
        /// </summary>
        public string SoftwareGuid { get; set; }

        /// <summary>
        /// 获取或设置软件的当前版本。
        /// </summary>
        public string CurrentVersion { get; set; }

        /// <summary>
        /// 获取或设置更新包运行时要传递的命令行参数。
        /// </summary>
        public string Arguments { get; set; }

        /// <summary>
        /// 获取或设置软件的进程 ID，用以发送消息。
        /// </summary>
        public int ProcessId { get; set; }

        /// <summary>
        /// 获取或设置软件的窗口类名，用以发送消息。
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否仅在有可用更新时显示对话框。
        /// </summary>
        public bool SilentMode { get; set; }
        #endregion

        #region Methods
        public string GetUpdateInfoUrl()
        {
            return $@"{UpdateUrl}{(UpdateUrl[UpdateUrl.Length - 1] != '/' ? "/" : "")}{SoftwareGuid}/UpdateInfo.xml";
        }
        #endregion
    }
}
