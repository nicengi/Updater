using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Serialization;

namespace Nicengi.Update
{
    public sealed class Updater : IDisposable
    {
        #region Fields
        internal readonly WebClient WebClient;
        private UpdateInfo updateInfo;
        private UpdateDialog updateDialog;
        #endregion

        #region Constructor
        public Updater(UpdaterArgs args)
        {
            Args = args;
            WebClient = new WebClient();
            WebClient.OpenReadCompleted += WebClient_OpenReadCompleted;
            WebClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
        }
        #endregion

        #region Properties
        public UpdaterArgs Args { get; }

        /// <summary>
        /// 获取软件的更新信息。
        /// </summary>
        public UpdateInfo UpdateInfo
        {
            get
            {
                return updateInfo;
            }

            private set
            {
                updateInfo = value;
            }
        }

        /// <summary>
        /// 获取一个值，该值指示软件是否需要更新。
        /// </summary>
        public bool IsNeedToBeUpdated
        {
            get
            {
                if (UpdateInfo != null)
                    return UpdateInfo.NeedToBeUpdated(Args.CurrentVersion);
                return false;
            }
        }

        /// <summary>
        /// 获取更新包缓存文件名。
        /// </summary>
        public string UpdatePackageName
        {
            get
            {
                if (UpdateInfo == null)
                    return null;
                return Path.Combine(Path.GetTempPath(), Args.SoftwareGuid, Path.GetFileName(UpdateInfo.Location));
            }
        }

        /// <summary>
        /// 获取更新对话框。
        /// </summary>
        public UpdateDialog UpdateDialog
        {
            get
            {
                if (updateDialog == null)
                    updateDialog = new UpdateDialog(this);
                return updateDialog;
            }
        }
        #endregion

        #region Methods
        public void Start()
        {
            if (!UpdateDialog.Visible)
            {
                if (Args.SilentMode)
                {
                    try
                    {
                        CheckForUpdate();
                        if (IsNeedToBeUpdated)
                            UpdateDialog.ShowDialog(/*new WindowWrapper(NativeMethods.FindWindow(Args.ClassName, null))*/);
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    UpdateDialog.ShowDialog(/*new WindowWrapper(NativeMethods.FindWindow(Args.ClassName, null))*/);
                }
            }
        }

        /// <summary>
        /// 获取软件的更新信息。
        /// </summary>
        public void CheckForUpdate()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UpdateInfo));
            Stream stream = WebClient.OpenRead(Args.GetUpdateInfoUrl());
            UpdateInfo = (UpdateInfo)serializer.Deserialize(stream);
        }

        /// <summary>
        /// <inheritdoc cref="CheckForUpdate()"/>
        /// </summary>
        public void CheckForUpdateAsync()
        {
            WebClient.OpenReadAsync(new Uri(Args.GetUpdateInfoUrl()), nameof(CheckForUpdateAsync));
        }

        /// <summary>
        /// 下载更新包。
        /// </summary>
        public void DownloadUpdate()
        {
            if (UpdateInfo == null)
                throw new ArgumentNullException(nameof(UpdateInfo));
            if (IsNeedToBeUpdated)
                WebClient.DownloadFile(new Uri(UpdateInfo.Location), UpdatePackageName);
        }

        /// <summary>
        /// <inheritdoc cref="DownloadUpdate()"/>
        /// </summary>
        public void DownloadUpdateAsync()
        {
            if (UpdateInfo == null)
                throw new ArgumentNullException(nameof(UpdateInfo));
            if (IsNeedToBeUpdated)
            {
                string dir = Path.GetDirectoryName(UpdatePackageName);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                WebClient.DownloadFileAsync(new Uri(UpdateInfo.Location), UpdatePackageName, nameof(DownloadUpdateAsync));
            }
        }

        /// <summary>
        /// 安装更新。
        /// </summary>
        public void InstallUpdate()
        {
            if (!IsNeedToBeUpdated) return;
            if (Args.ProcessId > 0)
            {
                Process process = Process.GetProcessById(Args.ProcessId);
                if (process != null)
                {
                    EventWaitHandle eventWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset, Args.SoftwareGuid);
                    NativeMethods.PostThreadMessage(process.Threads[0].Id, NativeMethods.WM_APP_UPDATE, Marshal.StringToHGlobalAuto(Args.SoftwareGuid), Marshal.GetFunctionPointerForDelegate(new Action(() => eventWaitHandle.Set())));
                    eventWaitHandle.WaitOne(60000);
                    eventWaitHandle.Close();
                }
            }
            else if (!string.IsNullOrEmpty(Args.ClassName))
            {
                IntPtr hWnd = NativeMethods.FindWindow(Args.ClassName, null);
                while (hWnd != IntPtr.Zero)
                {
                    NativeMethods.SendMessage(hWnd, NativeMethods.WM_CLOSE, (IntPtr)NativeMethods.WM_APP_UPDATE, IntPtr.Zero);
                    hWnd = NativeMethods.FindWindow(Args.ClassName, null);
                }
            }
            Process.Start(UpdatePackageName, string.Format(Args.Arguments, Args.SoftwareName, Args.SoftwareGuid, Args.CurrentVersion, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));
        }

        private void WebClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.UserState.ToString() != nameof(CheckForUpdateAsync) || e.Error != null) return;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(UpdateInfo));
                UpdateInfo = (UpdateInfo)serializer.Deserialize(e.Result);
            }
            catch (Exception ex)
            {
                UpdateDialog.ShowException(ex, () => UpdateDialog.CheckForUpdate());
            }
        }

        private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.UserState.ToString() != nameof(Updater.DownloadUpdateAsync) || e.Error != null) return;
            try
            {
                InstallUpdate();
            }
            catch (Exception ex)
            {
                UpdateDialog.ShowException(ex, () => InstallUpdate());
            }
        }

        public void Dispose()
        {
            WebClient.Dispose();
            if (updateDialog != null)
                updateDialog.Dispose();
        }
        #endregion
    }
}
