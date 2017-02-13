namespace SubComponentExporter
{
    partial class SubCompExport
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubCompExport));
            this.rmaDataGridView = new System.Windows.Forms.DataGridView();
            this.isFound = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubCompValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubComponent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubComponentPartNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Serial_Ver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Serial_SubVer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.statusLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.serialNumMTB = new System.Windows.Forms.MaskedTextBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.populateBtn = new System.Windows.Forms.Button();
            this.openChk = new System.Windows.Forms.CheckBox();
            this.excelBtn = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.highlightChk = new System.Windows.Forms.CheckBox();
            this.filterChk = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.rmaDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rmaDataGridView
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.rmaDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.rmaDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rmaDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isFound,
            this.SubCompValue,
            this.SerialNumber,
            this.PartNumber,
            this.SubComponent,
            this.SubComponentPartNumber,
            this.Serial_Ver,
            this.Serial_SubVer});
            this.rmaDataGridView.Location = new System.Drawing.Point(322, 18);
            this.rmaDataGridView.Name = "rmaDataGridView";
            this.rmaDataGridView.Size = new System.Drawing.Size(548, 529);
            this.rmaDataGridView.TabIndex = 5;
            this.rmaDataGridView.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseUp);
            this.rmaDataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.rmaDataGridView_CellPainting);
            // 
            // isFound
            // 
            this.isFound.HeaderText = "Found";
            this.isFound.Name = "isFound";
            this.isFound.ReadOnly = true;
            this.isFound.Visible = false;
            this.isFound.Width = 10;
            // 
            // SubCompValue
            // 
            this.SubCompValue.HeaderText = "Comp#";
            this.SubCompValue.Name = "SubCompValue";
            this.SubCompValue.ReadOnly = true;
            this.SubCompValue.Width = 50;
            // 
            // SerialNumber
            // 
            this.SerialNumber.HeaderText = "Serial Number";
            this.SerialNumber.Name = "SerialNumber";
            // 
            // PartNumber
            // 
            this.PartNumber.HeaderText = "Part Number";
            this.PartNumber.Name = "PartNumber";
            // 
            // SubComponent
            // 
            this.SubComponent.HeaderText = "Sub Serial Number";
            this.SubComponent.Name = "SubComponent";
            this.SubComponent.Width = 120;
            // 
            // SubComponentPartNumber
            // 
            this.SubComponentPartNumber.HeaderText = "Sub Part Number";
            this.SubComponentPartNumber.Name = "SubComponentPartNumber";
            this.SubComponentPartNumber.Width = 120;
            // 
            // Serial_Ver
            // 
            this.Serial_Ver.HeaderText = "Serial Version";
            this.Serial_Ver.Name = "Serial_Ver";
            this.Serial_Ver.ReadOnly = true;
            this.Serial_Ver.Visible = false;
            // 
            // Serial_SubVer
            // 
            this.Serial_SubVer.HeaderText = "Serial SubVer";
            this.Serial_SubVer.Name = "Serial_SubVer";
            this.Serial_SubVer.ReadOnly = true;
            this.Serial_SubVer.Visible = false;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.statusLbl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.progressBar);
            this.groupBox1.Controls.Add(this.serialNumMTB);
            this.groupBox1.Controls.Add(this.addBtn);
            this.groupBox1.Controls.Add(this.populateBtn);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 134);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RMA Information";
            // 
            // statusLbl
            // 
            this.statusLbl.AutoSize = true;
            this.statusLbl.Location = new System.Drawing.Point(9, 109);
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(175, 13);
            this.statusLbl.TabIndex = 8;
            this.statusLbl.Text = "Please Add Serial Numbers to List...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Serial Number:";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(9, 79);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(275, 23);
            this.progressBar.TabIndex = 7;
            // 
            // serialNumMTB
            // 
            this.serialNumMTB.Location = new System.Drawing.Point(89, 24);
            this.serialNumMTB.Mask = "#####-####";
            this.serialNumMTB.Name = "serialNumMTB";
            this.serialNumMTB.Size = new System.Drawing.Size(103, 20);
            this.serialNumMTB.TabIndex = 2;
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(209, 20);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 23);
            this.addBtn.TabIndex = 3;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // populateBtn
            // 
            this.populateBtn.Location = new System.Drawing.Point(209, 50);
            this.populateBtn.Name = "populateBtn";
            this.populateBtn.Size = new System.Drawing.Size(75, 23);
            this.populateBtn.TabIndex = 4;
            this.populateBtn.Text = "Process";
            this.populateBtn.UseVisualStyleBackColor = true;
            this.populateBtn.Click += new System.EventHandler(this.exportBtn_Click);
            // 
            // openChk
            // 
            this.openChk.AutoSize = true;
            this.openChk.Location = new System.Drawing.Point(11, 26);
            this.openChk.Name = "openChk";
            this.openChk.Size = new System.Drawing.Size(105, 17);
            this.openChk.TabIndex = 9;
            this.openChk.Text = "Open After Save";
            this.openChk.UseVisualStyleBackColor = true;
            // 
            // excelBtn
            // 
            this.excelBtn.Location = new System.Drawing.Point(209, 66);
            this.excelBtn.Name = "excelBtn";
            this.excelBtn.Size = new System.Drawing.Size(75, 23);
            this.excelBtn.TabIndex = 6;
            this.excelBtn.Text = "Save Excel";
            this.excelBtn.UseVisualStyleBackColor = true;
            this.excelBtn.Click += new System.EventHandler(this.excelBtn_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.clearAllItemsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 48);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteToolStripMenuItem.Text = "Delete Selected Row";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // clearAllItemsToolStripMenuItem
            // 
            this.clearAllItemsToolStripMenuItem.Name = "clearAllItemsToolStripMenuItem";
            this.clearAllItemsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.clearAllItemsToolStripMenuItem.Text = "Clear All Items";
            this.clearAllItemsToolStripMenuItem.Click += new System.EventHandler(this.clearAllItemsToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.highlightChk);
            this.groupBox2.Controls.Add(this.filterChk);
            this.groupBox2.Controls.Add(this.openChk);
            this.groupBox2.Controls.Add(this.excelBtn);
            this.groupBox2.Location = new System.Drawing.Point(13, 152);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(303, 100);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Excel Export";
            // 
            // highlightChk
            // 
            this.highlightChk.AutoSize = true;
            this.highlightChk.Checked = true;
            this.highlightChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.highlightChk.Location = new System.Drawing.Point(11, 74);
            this.highlightChk.Name = "highlightChk";
            this.highlightChk.Size = new System.Drawing.Size(130, 17);
            this.highlightChk.TabIndex = 11;
            this.highlightChk.Text = "Highlight Header Row";
            this.highlightChk.UseVisualStyleBackColor = true;
            // 
            // filterChk
            // 
            this.filterChk.AutoSize = true;
            this.filterChk.Checked = true;
            this.filterChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.filterChk.Location = new System.Drawing.Point(11, 50);
            this.filterChk.Name = "filterChk";
            this.filterChk.Size = new System.Drawing.Size(140, 17);
            this.filterChk.TabIndex = 10;
            this.filterChk.Text = "Filter NOT SERIALIZED";
            this.filterChk.UseVisualStyleBackColor = true;
            // 
            // SubCompExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 559);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rmaDataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SubCompExport";
            this.Text = "Sub Component Exporter";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rmaDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView rmaDataGridView;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label statusLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.MaskedTextBox serialNumMTB;
        private System.Windows.Forms.Button excelBtn;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button populateBtn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.CheckBox openChk;
        private System.Windows.Forms.ToolStripMenuItem clearAllItemsToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox filterChk;
        private System.Windows.Forms.CheckBox highlightChk;
        private System.Windows.Forms.DataGridViewTextBoxColumn isFound;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubCompValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubComponent;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubComponentPartNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Serial_Ver;
        private System.Windows.Forms.DataGridViewTextBoxColumn Serial_SubVer;
    }
}

