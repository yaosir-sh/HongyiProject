
namespace AMTransferTool
{
    partial class TarnsferTool
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TarnsferTool));
            this.btnReader = new System.Windows.Forms.Button();
            this.txtLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnReader
            // 
            this.btnReader.Location = new System.Drawing.Point(81, 61);
            this.btnReader.Name = "btnReader";
            this.btnReader.Size = new System.Drawing.Size(137, 38);
            this.btnReader.TabIndex = 0;
            this.btnReader.Text = "读取数据";
            this.btnReader.UseVisualStyleBackColor = true;
            this.btnReader.Click += new System.EventHandler(this.btnReader_Click);
            // 
            // txtLabel
            // 
            this.txtLabel.Location = new System.Drawing.Point(79, 122);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(228, 23);
            this.txtLabel.TabIndex = 1;
            // 
            // TarnsferTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 342);
            this.Controls.Add(this.txtLabel);
            this.Controls.Add(this.btnReader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TarnsferTool";
            this.Text = "文件传输工具";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReader;
        private System.Windows.Forms.Label txtLabel;
    }
}

