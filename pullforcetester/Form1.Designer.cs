using System.Drawing;
using System.Windows.Forms;

namespace pullforcetester
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private Button btnConnect;
        private Button btnSave;
        private Button btnRefresh;
        private DataGridView gridData;
        private Label Status;

        private Label lblCuttingMachine;
        private TextBox txtCuttingMachine;
        private Label lblTerminalPart;
        private TextBox txtTerminalPart;
        private Label lblTerminalName;
        private TextBox txtTerminalName;

        private Label lblUnit;
        private ComboBox comboUnit;

        private Label lblCuttingKanban;
        private ComboBox comboCuttingKanban;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.gridData = new System.Windows.Forms.DataGridView();
            this.Status = new System.Windows.Forms.Label();

            this.lblCuttingMachine = new System.Windows.Forms.Label();
            this.txtCuttingMachine = new System.Windows.Forms.TextBox();
            this.lblTerminalPart = new System.Windows.Forms.Label();
            this.txtTerminalPart = new System.Windows.Forms.TextBox();
            this.lblTerminalName = new System.Windows.Forms.Label();
            this.txtTerminalName = new System.Windows.Forms.TextBox();

            this.lblUnit = new System.Windows.Forms.Label();
            this.comboUnit = new System.Windows.Forms.ComboBox();

            this.lblCuttingKanban = new System.Windows.Forms.Label();
            this.comboCuttingKanban = new System.Windows.Forms.ComboBox();

            ((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(30, 20);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(120, 34);
            this.btnConnect.Text = "Connect";
            //this.btnConnect.BackColor = Color.FromArgb(59, 130, 246); 
            //this.btnConnect.ForeColor = Color.White;                  
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(160, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 34);
            this.btnSave.Text = "Save";
            //this.btnSave.BackColor = Color.FromArgb(59, 130, 246);
            //this.btnSave.ForeColor = Color.White;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(290, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 34);
            this.btnRefresh.Text = "Refresh";
            //this.btnRefresh.BackColor = Color.FromArgb(59, 130, 246);
            //this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblCuttingMachine
            // 
            this.lblCuttingMachine.Location = new System.Drawing.Point(30, 70);
            this.lblCuttingMachine.Size = new System.Drawing.Size(140, 25);
            this.lblCuttingMachine.Text = "Cutting Machine:";
            // 
            // txtCuttingMachine
            // 
            this.txtCuttingMachine.Location = new System.Drawing.Point(180, 70);
            this.txtCuttingMachine.Size = new System.Drawing.Size(200, 31);
            // 
            // lblTerminalPart
            // 
            this.lblTerminalPart.Location = new System.Drawing.Point(400, 70);
            this.lblTerminalPart.Size = new System.Drawing.Size(140, 25); // wider so text shows fully
            this.lblTerminalPart.Text = "Terminal Part:";
            // 
            // txtTerminalPart
            // 
            this.txtTerminalPart.Location = new System.Drawing.Point(520, 70);
            this.txtTerminalPart.Size = new System.Drawing.Size(200, 31);
            // 
            // lblTerminalName
            // 
            this.lblTerminalName.Location = new System.Drawing.Point(30, 120);
            this.lblTerminalName.Size = new System.Drawing.Size(140, 25); // wider so text shows fully
            this.lblTerminalName.Text = "Terminal Name:";
            // 
            // txtTerminalName
            // 
            this.txtTerminalName.Location = new System.Drawing.Point(180, 120);
            this.txtTerminalName.Size = new System.Drawing.Size(200, 31);
            // 
            // lblUnit
            // 
            this.lblUnit.Location = new System.Drawing.Point(400, 120);
            this.lblUnit.Size = new System.Drawing.Size(80, 25);
            this.lblUnit.Text = "Unit:";
            // 
            // comboUnit
            // 
            this.comboUnit.Location = new System.Drawing.Point(520, 120);
            this.comboUnit.Size = new System.Drawing.Size(120, 31);
            this.comboUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            // 
            // lblCuttingKanban
            // 
            this.lblCuttingKanban.Location = new System.Drawing.Point(30, 170);
            this.lblCuttingKanban.Size = new System.Drawing.Size(140, 25);
            this.lblCuttingKanban.Text = "Cutting Kanban:";
            // 
            // comboCuttingKanban
            // 
            this.comboCuttingKanban.Location = new System.Drawing.Point(180, 170);
            this.comboCuttingKanban.Size = new System.Drawing.Size(200, 31);
            this.comboCuttingKanban.DropDownStyle = ComboBoxStyle.DropDownList;
            // 
            // gridData
            // 
            this.gridData.Location = new System.Drawing.Point(30, 220);
            this.gridData.Size = new System.Drawing.Size(740, 200);
            this.gridData.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.gridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridData.Name = "gridData";

            // Add columns with SAME names as DB
            this.gridData.Columns.Add("cuttingKanban", "cuttingKanban");
            this.gridData.Columns.Add("cuttingKanbanID", "cuttingKanbanID");
            this.gridData.Columns.Add("apu", "apu");
            this.gridData.Columns.Add("fac", "fac");
            this.gridData.Columns.Add("cuttingMachine", "cuttingMachine");


            this.gridData.Columns.Add("terminalPart", "terminalPart");
            this.gridData.Columns.Add("terminalName", "terminalName");
            this.gridData.Columns.Add("resultPull", "resultPull");
            this.gridData.Columns.Add("resultUnit", "resultUnit");
            this.gridData.Columns.Add("createDate", "createDate");
            this.gridData.Columns.Add("pullMachineIP", "pullMachineIP");
            // 
            // Status
            // 
            this.Status.Location = new System.Drawing.Point(30, 440);
            this.Status.Size = new System.Drawing.Size(400, 25);
            this.Status.Text = "Status";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.gridData);
            this.Controls.Add(this.comboCuttingKanban);
            this.Controls.Add(this.lblCuttingKanban);
            this.Controls.Add(this.comboUnit);
            this.Controls.Add(this.lblUnit);
            this.Controls.Add(this.txtTerminalName);
            this.Controls.Add(this.lblTerminalName);
            this.Controls.Add(this.txtTerminalPart);
            this.Controls.Add(this.lblTerminalPart);
            this.Controls.Add(this.txtCuttingMachine);
            this.Controls.Add(this.lblCuttingMachine);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnConnect);
            this.Text = "Pull Force Reader";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
            this.ResumeLayout(false);
        }
    }
}