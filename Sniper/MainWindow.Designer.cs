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
            this.tbStopPrice = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvSniper)).BeginInit();
            this.SuspendLayout();
            // 
            // gvSniper
            // 
            this.gvSniper.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSniper.Location = new System.Drawing.Point(12, 52);
            this.gvSniper.Name = "gvSniper";
            this.gvSniper.Size = new System.Drawing.Size(588, 178);
            this.gvSniper.TabIndex = 1;
            // 
            // tbItemId
            // 
            this.tbItemId.Location = new System.Drawing.Point(59, 18);
            this.tbItemId.Name = "tbItemId";
            this.tbItemId.Size = new System.Drawing.Size(127, 20);
            this.tbItemId.TabIndex = 2;
            // 
            // btnJoin
            // 
            this.btnJoin.Location = new System.Drawing.Point(418, 16);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(75, 23);
            this.btnJoin.TabIndex = 3;
            this.btnJoin.Text = "Join Auction";
            this.btnJoin.UseVisualStyleBackColor = true;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // tbStopPrice
            // 
            this.tbStopPrice.Location = new System.Drawing.Point(269, 18);
            this.tbStopPrice.Name = "tbStopPrice";
            this.tbStopPrice.Size = new System.Drawing.Size(127, 20);
            this.tbStopPrice.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Item:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Stop Price:";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 242);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbStopPrice);
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
        private System.Windows.Forms.TextBox tbStopPrice;

        public System.Windows.Forms.TextBox TbStopPrice
        {
            get { return tbStopPrice; }
        }
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        public System.Windows.Forms.Button BtnJoin
        {
            get { return btnJoin; }
        }

    }
}

