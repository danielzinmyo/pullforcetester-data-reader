using System.Drawing;
using System.Windows.Forms;

namespace pullforcetester
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private Button btnConnect;
        private Button btnSave;
        private TextBox txtData;
        private Label Status;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtData = new System.Windows.Forms.TextBox();
            this.Status = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(30, 30);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(112, 34);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(160, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 34);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(30, 80);
            this.txtData.Multiline = true;
            this.txtData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(740, 300);
            this.txtData.TabIndex = 2;
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Location = new System.Drawing.Point(30, 400);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(65, 25);
            this.Status.TabIndex = 3;
            this.Status.Text = "Status";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.Text = "Pull Force Reader";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}