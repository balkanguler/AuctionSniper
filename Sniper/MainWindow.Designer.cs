namespace AuctionSniper
{
    partial class MainWindow
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
            this.gvSniper = new System.Windows.Forms.DataGridView();
            this.tbItemId = new System.Windows.Forms.TextBox();
            this.btnJoin = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvSniper)).BeginInit();
            this.SuspendLayout();
            // 
            // gvSniper
            // 
            this.gvSniper.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSniper.Location = new System.Drawing.Point(12, 88);
            this.gvSniper.Name = "gvSniper";
            this.gvSniper.Size = new System.Drawing.Size(414, 84);
            this.gvSniper.TabIndex = 1;
            // 
            // tbItemId
            // 
            this.tbItemId.Location = new System.Drawing.Point(29, 26);
            this.tbItemId.Name = "tbItemId";
            this.tbItemId.Size = new System.Drawing.Size(127, 20);
            this.tbItemId.TabIndex = 2;
            // 
            // btnJoin
            // 
            this.btnJoin.Location = new System.Drawing.Point(180, 23);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(75, 23);
            this.btnJoin.TabIndex = 3;
            this.btnJoin.Text = "Join Auction";
            this.btnJoin.UseVisualStyleBackColor = true;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 268);
            this.Controls.Add(this.btnJoin);
            this.Controls.Add(this.tbItemId);
            this.Controls.Add(this.gvSniper);
            this.Name = "MainWindow";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.gvSniper)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvSniper;
        private System.Windows.Forms.TextBox tbItemId;

        public System.Windows.Forms.TextBox TbItemId
        {
            get { return tbItemId; }
        }
        private System.Windows.Forms.Button btnJoin;

        public System.Windows.Forms.Button BtnJoin
        {
            get { return btnJoin; }
        }

    }
}

