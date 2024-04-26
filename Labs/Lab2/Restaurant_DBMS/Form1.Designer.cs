namespace Restaurant_DBMS
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
            this.childTableView = new System.Windows.Forms.DataGridView();
            this.parentTableView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.updateButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.childTableView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.parentTableView)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // childTableView
            // 
            this.childTableView.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.childTableView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.childTableView.Location = new System.Drawing.Point(717, 15);
            this.childTableView.Margin = new System.Windows.Forms.Padding(2);
            this.childTableView.Name = "childTableView";
            this.childTableView.RowHeadersWidth = 51;
            this.childTableView.RowTemplate.Height = 24;
            this.childTableView.Size = new System.Drawing.Size(642, 401);
            this.childTableView.TabIndex = 3;
            this.childTableView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.childTableView_DataError);
            // 
            // parentTableView
            // 
            this.parentTableView.AllowUserToAddRows = false;
            this.parentTableView.AllowUserToDeleteRows = false;
            this.parentTableView.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.parentTableView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.parentTableView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parentTableView.Location = new System.Drawing.Point(2, 15);
            this.parentTableView.Margin = new System.Windows.Forms.Padding(2);
            this.parentTableView.Name = "parentTableView";
            this.parentTableView.ReadOnly = true;
            this.parentTableView.RowHeadersWidth = 51;
            this.parentTableView.RowTemplate.Height = 24;
            this.parentTableView.Size = new System.Drawing.Size(711, 401);
            this.parentTableView.TabIndex = 2;
            this.parentTableView.SelectionChanged += new System.EventHandler(this.parentTableView_SelectionChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.DarkOrange;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.56306F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.43694F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.parentTableView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.childTableView, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.updateButton, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 405F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1362, 519);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(2, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(711, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Parent Table";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(717, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(643, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Child Table";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // updateButton
            // 
            this.updateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.updateButton.BackColor = System.Drawing.Color.GreenYellow;
            this.updateButton.Location = new System.Drawing.Point(625, 420);
            this.updateButton.Margin = new System.Windows.Forms.Padding(2);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(88, 49);
            this.updateButton.TabIndex = 4;
            this.updateButton.Text = "Save changes";
            this.updateButton.UseVisualStyleBackColor = false;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(1229, 522);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.childTableView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.parentTableView)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView childTableView;
        private System.Windows.Forms.DataGridView parentTableView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button updateButton;
    }
}
