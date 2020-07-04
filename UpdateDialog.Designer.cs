namespace Nicengi.Update
{
    partial class UpdateDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateDialog));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelSoftwareName = new System.Windows.Forms.Label();
            this.labelMessage = new System.Windows.Forms.Label();
            this.labelCurrentVersionTitle = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelDescriptionTitle = new System.Windows.Forms.Label();
            this.labelCurrentVersion = new System.Windows.Forms.Label();
            this.buttonRetry = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
            this.tableLayoutPanel.Controls.Add(this.buttonOK, 3, 6);
            this.tableLayoutPanel.Controls.Add(this.buttonCancel, 4, 6);
            this.tableLayoutPanel.Controls.Add(this.labelSoftwareName, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelMessage, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.labelCurrentVersionTitle, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.labelDescription, 0, 5);
            this.tableLayoutPanel.Controls.Add(this.labelDescriptionTitle, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.labelCurrentVersion, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.buttonRetry, 2, 6);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelSoftwareName
            // 
            resources.ApplyResources(this.labelSoftwareName, "labelSoftwareName");
            this.tableLayoutPanel.SetColumnSpan(this.labelSoftwareName, 5);
            this.labelSoftwareName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelSoftwareName.Name = "labelSoftwareName";
            // 
            // labelMessage
            // 
            resources.ApplyResources(this.labelMessage, "labelMessage");
            this.tableLayoutPanel.SetColumnSpan(this.labelMessage, 5);
            this.labelMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelMessage.Name = "labelMessage";
            // 
            // labelCurrentVersionTitle
            // 
            resources.ApplyResources(this.labelCurrentVersionTitle, "labelCurrentVersionTitle");
            this.labelCurrentVersionTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelCurrentVersionTitle.Name = "labelCurrentVersionTitle";
            // 
            // labelDescription
            // 
            resources.ApplyResources(this.labelDescription, "labelDescription");
            this.tableLayoutPanel.SetColumnSpan(this.labelDescription, 5);
            this.labelDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelDescription.Name = "labelDescription";
            // 
            // labelDescriptionTitle
            // 
            resources.ApplyResources(this.labelDescriptionTitle, "labelDescriptionTitle");
            this.tableLayoutPanel.SetColumnSpan(this.labelDescriptionTitle, 2);
            this.labelDescriptionTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelDescriptionTitle.Name = "labelDescriptionTitle";
            // 
            // labelCurrentVersion
            // 
            resources.ApplyResources(this.labelCurrentVersion, "labelCurrentVersion");
            this.labelCurrentVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelCurrentVersion.Name = "labelCurrentVersion";
            // 
            // buttonRetry
            // 
            resources.ApplyResources(this.buttonRetry, "buttonRetry");
            this.buttonRetry.Name = "buttonRetry";
            this.buttonRetry.UseVisualStyleBackColor = true;
            this.buttonRetry.Click += new System.EventHandler(this.ButtonRetry_Click);
            // 
            // UpdateDialog
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.tableLayoutPanel);
            this.MaximizeBox = false;
            this.Name = "UpdateDialog";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.UpdateDialog_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelSoftwareName;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Label labelCurrentVersionTitle;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelDescriptionTitle;
        private System.Windows.Forms.Label labelCurrentVersion;
        private System.Windows.Forms.Button buttonRetry;
    }
}