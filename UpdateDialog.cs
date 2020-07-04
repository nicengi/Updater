using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;

namespace Nicengi.Update
{
    public sealed partial class UpdateDialog : Form
    {
        #region Fields
        private Action retryAction;
        #endregion

        #region Constructor
        private UpdateDialog()
        {
            InitializeComponent();
        }

        public UpdateDialog(Updater updater) : this()
        {
            Updater = updater ?? throw new ArgumentNullException(nameof(updater));
            Updater.WebClient.OpenReadCompleted += WebClient_OpenReadCompleted;
            Updater.WebClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
            Updater.WebClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
        }
        #endregion

        #region Properties
        public Updater Updater { get; }

        private Action RetryAction
        {
            get
            {
                return retryAction;
            }

            set
            {
                retryAction = value;

                if (retryAction != null)
                {
                    buttonRetry.Visible = true;
                    buttonRetry.Focus();
                }
            }
        }
        #endregion

        #region Methods
        public void ShowException(Exception e, Action retryAction = null)
        {
            labelMessage.Text = string.Format(Properties.Resources.Msg_Exception, e.Message);
            RetryAction = retryAction;
        }

        public void CheckForUpdate()
        {
            BeginInvoke(new Action(() =>
            {
                labelMessage.Text = string.Format(Properties.Resources.Msg_CheckingForUpdates, Updater.Args.SoftwareName);
            }));
            Updater.CheckForUpdateAsync();
        }

        private void UpdateDialog_Load(object sender, EventArgs e)
        {
            labelSoftwareName.Text = Updater.Args.SoftwareName;
            labelCurrentVersion.Text = Updater.Args.CurrentVersion;
            CheckForUpdate();
        }

        private void WebClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.UserState.ToString() != nameof(Updater.CheckForUpdateAsync)) return;
            if (e.Error != null)
            {
                ShowException(e.Error, () => CheckForUpdate());
                return;
            }
            if (Updater.IsNeedToBeUpdated)
            {
                labelMessage.Text = string.Format(Properties.Resources.Msg_UpdateAvailable, Updater.Args.SoftwareName);
                buttonOK.Enabled = true;
                buttonOK.Focus();
            }
            else
            {
                labelMessage.Text = string.Format(Properties.Resources.Msg_LatestVersion, Updater.Args.SoftwareName);
                buttonOK.Enabled = false;
            }
            labelDescription.Visible =
            labelDescriptionTitle.Visible = !string.IsNullOrWhiteSpace(Updater.UpdateInfo.Description);
            labelDescription.Text = Updater.UpdateInfo.Description;
        }

        private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.UserState.ToString() != nameof(Updater.DownloadUpdateAsync)) return;
            if (e.Error != null)
            {
                ShowException(e.Error);
                return;
            }
            Close();
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (e.UserState.ToString() == nameof(Updater.DownloadUpdateAsync))
                labelMessage.Text = string.Format(Properties.Resources.Msg_Updating, Updater.Args.SoftwareName, e.ProgressPercentage);
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            if (Updater.IsNeedToBeUpdated)
            {
                buttonOK.Enabled = false;
                Updater.DownloadUpdateAsync();
                labelMessage.Text = string.Format(Properties.Resources.Msg_Updating, Updater.Args.SoftwareName, 0);
            }
        }

        private void ButtonRetry_Click(object sender, EventArgs e)
        {
            RetryAction?.Invoke();
            RetryAction = null;
            buttonRetry.Visible = false;
        }
        #endregion
    }
}
