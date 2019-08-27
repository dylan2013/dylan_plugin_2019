namespace iCampusManagerPlugin
{
    partial class AppVerForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.colAppName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemake = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPackageName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBundleID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAndroidVer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIOSVer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDSNS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSchoolName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToResizeRows = false;
            this.dataGridViewX1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewX1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAppName,
            this.colRemake,
            this.colPackageName,
            this.colBundleID,
            this.colAndroidVer,
            this.colIOSVer,
            this.colDSNS,
            this.colSchoolName});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.RowTemplate.Height = 24;
            this.dataGridViewX1.Size = new System.Drawing.Size(1017, 518);
            this.dataGridViewX1.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.AutoSize = true;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(869, 544);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(953, 544);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // colAppName
            // 
            this.colAppName.HeaderText = "APP名稱";
            this.colAppName.Name = "colAppName";
            this.colAppName.Width = 150;
            // 
            // colRemake
            // 
            this.colRemake.HeaderText = "備註";
            this.colRemake.Name = "colRemake";
            this.colRemake.Width = 200;
            // 
            // colPackageName
            // 
            this.colPackageName.HeaderText = "Package Name";
            this.colPackageName.Name = "colPackageName";
            this.colPackageName.Width = 200;
            // 
            // colBundleID
            // 
            this.colBundleID.HeaderText = "Bundle ID";
            this.colBundleID.Name = "colBundleID";
            this.colBundleID.Width = 200;
            // 
            // colAndroidVer
            // 
            this.colAndroidVer.HeaderText = "Android版本編號";
            this.colAndroidVer.Name = "colAndroidVer";
            this.colAndroidVer.Width = 150;
            // 
            // colIOSVer
            // 
            this.colIOSVer.HeaderText = "IOS版本編號";
            this.colIOSVer.Name = "colIOSVer";
            this.colIOSVer.Width = 150;
            // 
            // colDSNS
            // 
            this.colDSNS.HeaderText = "DSNS";
            this.colDSNS.Name = "colDSNS";
            this.colDSNS.Width = 150;
            // 
            // colSchoolName
            // 
            this.colSchoolName.HeaderText = "學校名稱";
            this.colSchoolName.Name = "colSchoolName";
            this.colSchoolName.Width = 150;
            // 
            // AppVerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 580);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridViewX1);
            this.DoubleBuffered = true;
            this.MaximizeBox = true;
            this.Name = "AppVerForm";
            this.Text = "版本控管";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAppName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemake;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPackageName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBundleID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAndroidVer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIOSVer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDSNS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSchoolName;
    }
}