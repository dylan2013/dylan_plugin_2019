namespace AddMessageTestModule
{
    partial class Form1
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
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.textBoxX2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cbNew = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cbMessage = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cbWarning = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cbPost = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cbError = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.AutoSize = true;
            this.buttonX1.BackColor = System.Drawing.Color.Transparent;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(236, 261);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 25);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 0;
            this.buttonX1.Text = "加入訊息";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.AutoSize = true;
            this.buttonX2.BackColor = System.Drawing.Color.Transparent;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(317, 261);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 25);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 1;
            this.buttonX2.Text = "離開";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(29, 27);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "訊息標提";
            // 
            // textBoxX1
            // 
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX1.Location = new System.Drawing.Point(110, 25);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.Size = new System.Drawing.Size(282, 25);
            this.textBoxX1.TabIndex = 3;
            // 
            // textBoxX2
            // 
            // 
            // 
            // 
            this.textBoxX2.Border.Class = "TextBoxBorder";
            this.textBoxX2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX2.Location = new System.Drawing.Point(110, 67);
            this.textBoxX2.Name = "textBoxX2";
            this.textBoxX2.Size = new System.Drawing.Size(282, 25);
            this.textBoxX2.TabIndex = 5;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(29, 69);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 4;
            this.labelX2.Text = "訊息內文";
            // 
            // cbNew
            // 
            this.cbNew.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cbNew.BackgroundStyle.Class = "";
            this.cbNew.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbNew.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbNew.Checked = true;
            this.cbNew.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNew.CheckValue = "Y";
            this.cbNew.Location = new System.Drawing.Point(29, 119);
            this.cbNew.Name = "cbNew";
            this.cbNew.Size = new System.Drawing.Size(100, 23);
            this.cbNew.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbNew.TabIndex = 6;
            this.cbNew.Text = "最新消息";
            // 
            // cbMessage
            // 
            this.cbMessage.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cbMessage.BackgroundStyle.Class = "";
            this.cbMessage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbMessage.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbMessage.Location = new System.Drawing.Point(152, 119);
            this.cbMessage.Name = "cbMessage";
            this.cbMessage.Size = new System.Drawing.Size(100, 23);
            this.cbMessage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbMessage.TabIndex = 7;
            this.cbMessage.Text = "提示訊息";
            // 
            // cbWarning
            // 
            this.cbWarning.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cbWarning.BackgroundStyle.Class = "";
            this.cbWarning.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbWarning.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbWarning.Location = new System.Drawing.Point(276, 119);
            this.cbWarning.Name = "cbWarning";
            this.cbWarning.Size = new System.Drawing.Size(100, 23);
            this.cbWarning.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbWarning.TabIndex = 8;
            this.cbWarning.Text = "警示訊息";
            // 
            // cbPost
            // 
            this.cbPost.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cbPost.BackgroundStyle.Class = "";
            this.cbPost.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbPost.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbPost.Location = new System.Drawing.Point(29, 162);
            this.cbPost.Name = "cbPost";
            this.cbPost.Size = new System.Drawing.Size(100, 23);
            this.cbPost.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbPost.TabIndex = 9;
            this.cbPost.Text = "公告訊息";
            // 
            // cbError
            // 
            this.cbError.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cbError.BackgroundStyle.Class = "";
            this.cbError.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbError.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbError.Location = new System.Drawing.Point(152, 162);
            this.cbError.Name = "cbError";
            this.cbError.Size = new System.Drawing.Size(100, 23);
            this.cbError.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbError.TabIndex = 10;
            this.cbError.Text = "錯誤訊息";
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.AutoSize = true;
            this.buttonX3.BackColor = System.Drawing.Color.Transparent;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX3.Location = new System.Drawing.Point(139, 261);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(91, 25);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 11;
            this.buttonX3.Text = "新增訊息畫面";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 298);
            this.Controls.Add(this.buttonX3);
            this.Controls.Add(this.cbError);
            this.Controls.Add(this.cbPost);
            this.Controls.Add(this.cbWarning);
            this.Controls.Add(this.cbMessage);
            this.Controls.Add(this.cbNew);
            this.Controls.Add(this.textBoxX2);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.textBoxX1);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "來個好訊息";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX2;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbNew;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbMessage;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbWarning;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbPost;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbError;
        private DevComponents.DotNetBar.ButtonX buttonX3;
    }
}