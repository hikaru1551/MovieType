namespace MovieType
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.rtbEnterArticle = new System.Windows.Forms.RichTextBox();
            this.rdbCKIP = new System.Windows.Forms.RadioButton();
            this.rdbMovieType = new System.Windows.Forms.RadioButton();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.btStar = new System.Windows.Forms.Button();
            this.btCleared = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbEnterArticle
            // 
            this.rtbEnterArticle.Location = new System.Drawing.Point(26, 25);
            this.rtbEnterArticle.Name = "rtbEnterArticle";
            this.rtbEnterArticle.Size = new System.Drawing.Size(324, 154);
            this.rtbEnterArticle.TabIndex = 0;
            this.rtbEnterArticle.Text = "";
            // 
            // rdbCKIP
            // 
            this.rdbCKIP.AutoSize = true;
            this.rdbCKIP.Checked = true;
            this.rdbCKIP.Location = new System.Drawing.Point(392, 26);
            this.rdbCKIP.Name = "rdbCKIP";
            this.rdbCKIP.Size = new System.Drawing.Size(71, 16);
            this.rdbCKIP.TabIndex = 1;
            this.rdbCKIP.TabStop = true;
            this.rdbCKIP.Text = "中文斷詞";
            this.rdbCKIP.UseVisualStyleBackColor = true;
            this.rdbCKIP.CheckedChanged += new System.EventHandler(this.rdbCKIP_CheckedChanged);
            // 
            // rdbMovieType
            // 
            this.rdbMovieType.AutoSize = true;
            this.rdbMovieType.Location = new System.Drawing.Point(392, 60);
            this.rdbMovieType.Name = "rdbMovieType";
            this.rdbMovieType.Size = new System.Drawing.Size(95, 16);
            this.rdbMovieType.TabIndex = 2;
            this.rdbMovieType.TabStop = true;
            this.rdbMovieType.Text = "辨識電影類型";
            this.rdbMovieType.UseVisualStyleBackColor = true;
            this.rdbMovieType.CheckedChanged += new System.EventHandler(this.rdbMovieType_CheckedChanged);
            // 
            // rtbResult
            // 
            this.rtbResult.Location = new System.Drawing.Point(26, 204);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.ReadOnly = true;
            this.rtbResult.Size = new System.Drawing.Size(324, 96);
            this.rtbResult.TabIndex = 3;
            this.rtbResult.Text = "";
            // 
            // btStar
            // 
            this.btStar.Location = new System.Drawing.Point(275, 328);
            this.btStar.Name = "btStar";
            this.btStar.Size = new System.Drawing.Size(75, 23);
            this.btStar.TabIndex = 4;
            this.btStar.Text = "Star";
            this.btStar.UseVisualStyleBackColor = true;
            this.btStar.Click += new System.EventHandler(this.btStar_Click);
            // 
            // btCleared
            // 
            this.btCleared.Location = new System.Drawing.Point(372, 328);
            this.btCleared.Name = "btCleared";
            this.btCleared.Size = new System.Drawing.Size(75, 23);
            this.btCleared.TabIndex = 5;
            this.btCleared.Text = "Clear";
            this.btCleared.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btCleared.UseVisualStyleBackColor = true;
            this.btCleared.Click += new System.EventHandler(this.btCleared_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 381);
            this.Controls.Add(this.btCleared);
            this.Controls.Add(this.btStar);
            this.Controls.Add(this.rtbResult);
            this.Controls.Add(this.rdbMovieType);
            this.Controls.Add(this.rdbCKIP);
            this.Controls.Add(this.rtbEnterArticle);
            this.Name = "Form1";
            this.Text = "MT_demo(v1.0.0)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbEnterArticle;
        private System.Windows.Forms.RadioButton rdbCKIP;
        private System.Windows.Forms.RadioButton rdbMovieType;
        private System.Windows.Forms.RichTextBox rtbResult;
        private System.Windows.Forms.Button btStar;
        private System.Windows.Forms.Button btCleared;
    }
}

