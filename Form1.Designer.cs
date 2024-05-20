namespace txtwrite
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
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.rtbText = new System.Windows.Forms.RichTextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.listUndo = new System.Windows.Forms.ListBox();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnRedo = new System.Windows.Forms.Button();
            this.comboBoxFont = new System.Windows.Forms.ComboBox();
            this.comboBoxSize = new System.Windows.Forms.ComboBox();
            this.comboBoxStyle = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // rtbText
            // 
            this.rtbText.Location = new System.Drawing.Point(12, 58);
            this.rtbText.Name = "rtbText";
            this.rtbText.Size = new System.Drawing.Size(554, 380);
            this.rtbText.TabIndex = 0;
            this.rtbText.Text = "";
            this.rtbText.TextChanged += new System.EventHandler(this.rtbText_TextChanged);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(584, 58);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(99, 47);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "開啟檔案";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(689, 58);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(99, 47);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "存檔";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // listUndo
            // 
            this.listUndo.FormattingEnabled = true;
            this.listUndo.ItemHeight = 15;
            this.listUndo.Location = new System.Drawing.Point(584, 165);
            this.listUndo.Name = "listUndo";
            this.listUndo.Size = new System.Drawing.Size(204, 274);
            this.listUndo.TabIndex = 3;
            this.listUndo.SelectedIndexChanged += new System.EventHandler(this.listUndo_SelectedIndexChanged);
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(584, 111);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(99, 47);
            this.btnUndo.TabIndex = 4;
            this.btnUndo.Text = "上一步";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.Location = new System.Drawing.Point(689, 111);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(99, 47);
            this.btnRedo.TabIndex = 5;
            this.btnRedo.Text = "下一步";
            this.btnRedo.UseVisualStyleBackColor = true;
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // comboBoxFont
            // 
            this.comboBoxFont.Font = new System.Drawing.Font("新細明體", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBoxFont.FormattingEnabled = true;
            this.comboBoxFont.Location = new System.Drawing.Point(12, 12);
            this.comboBoxFont.Name = "comboBoxFont";
            this.comboBoxFont.Size = new System.Drawing.Size(180, 41);
            this.comboBoxFont.TabIndex = 6;
            this.comboBoxFont.SelectedIndexChanged += new System.EventHandler(this.listUndo_SelectedIndexChanged);
            // 
            // comboBoxSize
            // 
            this.comboBoxSize.Font = new System.Drawing.Font("新細明體", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBoxSize.FormattingEnabled = true;
            this.comboBoxSize.Location = new System.Drawing.Point(198, 12);
            this.comboBoxSize.Name = "comboBoxSize";
            this.comboBoxSize.Size = new System.Drawing.Size(64, 41);
            this.comboBoxSize.TabIndex = 7;
            this.comboBoxSize.SelectedIndexChanged += new System.EventHandler(this.listUndo_SelectedIndexChanged);
            // 
            // comboBoxStyle
            // 
            this.comboBoxStyle.Font = new System.Drawing.Font("新細明體", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBoxStyle.FormattingEnabled = true;
            this.comboBoxStyle.Location = new System.Drawing.Point(268, 12);
            this.comboBoxStyle.Name = "comboBoxStyle";
            this.comboBoxStyle.Size = new System.Drawing.Size(135, 41);
            this.comboBoxStyle.TabIndex = 8;
            this.comboBoxStyle.SelectedIndexChanged += new System.EventHandler(this.listUndo_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBoxStyle);
            this.Controls.Add(this.comboBoxSize);
            this.Controls.Add(this.comboBoxFont);
            this.Controls.Add(this.btnRedo);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.listUndo);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.rtbText);
            this.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbText;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ListBox listUndo;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnRedo;
        private System.Windows.Forms.ComboBox comboBoxFont;
        private System.Windows.Forms.ComboBox comboBoxSize;
        private System.Windows.Forms.ComboBox comboBoxStyle;
    }
}

