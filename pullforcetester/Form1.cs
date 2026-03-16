using System;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace pullforcetester
{
    public partial class Form1 : Form
    {
        SerialPort serialPort;
        string connectionString;
        string pullMachineIP;

        public Form1()
        {
            InitializeComponent();

            // Load appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();

            connectionString = config.GetConnectionString("MESConnection");
            pullMachineIP = config["AppSettings:PullMachineIP"];

            LoadCuttingKanbanList();
            LoadUnitOptions();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Status.Text = "Not connected.";
            txtCuttingMachine.Text = "ECI-PF-007"; // default machine name
        }

        private void LoadCuttingKanbanList()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT cuttingKanban FROM [MES].[dbo].[CuttingKanbanVRTerminal]", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboCuttingKanban.Items.Add(reader["cuttingKanban"].ToString());
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Status.Text = "Error loading cuttingKanban: " + ex.Message;
            }
        }

        private void LoadUnitOptions()
        {
            comboUnit.Items.Add("Kg");
            comboUnit.Items.Add("lb");
            comboUnit.Items.Add("N");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort = new SerialPort("COM7", 9600);
                serialPort.DataReceived += SerialPort_DataReceived;
                serialPort.Open();
                Status.Text = "Connected. Listening...";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = serialPort.ReadExisting();
                if (!string.IsNullOrEmpty(data))
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        string timeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        int rowIndex = gridData.Rows.Add();

                        // Fill row with DB-aligned columns
                        gridData.Rows[rowIndex].Cells["cuttingKanban"].Value = comboCuttingKanban.SelectedItem?.ToString() ?? "";
                        gridData.Rows[rowIndex].Cells["cuttingKanbanID"].Value = "";
                        gridData.Rows[rowIndex].Cells["apu"].Value = "APU1";
                        gridData.Rows[rowIndex].Cells["fac"].Value = "TH";
                        gridData.Rows[rowIndex].Cells["cuttingMachine"].Value = txtCuttingMachine.Text;
                        gridData.Rows[rowIndex].Cells["terminalPart"].Value = txtTerminalPart.Text;
                        gridData.Rows[rowIndex].Cells["terminalName"].Value = txtTerminalName.Text;
                        gridData.Rows[rowIndex].Cells["createDate"].Value = timeNow;
                        gridData.Rows[rowIndex].Cells["pullMachineIP"].Value = pullMachineIP;

                        // Extract numeric value including decimal point
                        string numericOnly = new string(data.Where(c => char.IsDigit(c) || c == '.').ToArray());
                        gridData.Rows[rowIndex].Cells["resultPull"].Value = numericOnly;

                        // Unit chosen by operator
                        gridData.Rows[rowIndex].Cells["resultUnit"].Value = comboUnit.SelectedItem?.ToString() ?? "";

                        SaveToDatabase(numericOnly, comboUnit.SelectedItem?.ToString() ?? "");
                    }));
                }
            }
            catch (Exception ex)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    Status.Text = "Error: " + ex.Message;
                }));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (gridData.Rows.Count > 0)
            {
                var lastRow = gridData.Rows[gridData.Rows.Count - 1];
                string pullValue = lastRow.Cells["resultPull"].Value?.ToString();
                string unit = lastRow.Cells["resultUnit"].Value?.ToString();

                if (!string.IsNullOrEmpty(pullValue))
                {
                    SaveToDatabase(pullValue, unit ?? "");
                    Status.Text = "Manual save triggered.";
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                comboCuttingKanban.Items.Clear();   // clear old list
                LoadCuttingKanbanList();            // reload from DB
                Status.Text = "CuttingKanban list refreshed.";
            }
            catch (Exception ex)
            {
                Status.Text = "Error refreshing list: " + ex.Message;
            }
        }

        //private void SaveToDatabase(string pullValue, string unit)
        //{
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            string query = @"INSERT INTO [dbo].[CuttingPullRealtimeData]
        //                            (cuttingKanban, cuttingKanbanID, apu, fac, cuttingMachine, terminalPart, terminalName, resultPull, resultUnit, createDate, pullMachineIP)
        //                            VALUES (@kanban, @kanbanID, @apu, @fac, @machine, @part, @name, @pull, @unit, GETDATE(), @ip)";

        //            SqlCommand cmd = new SqlCommand(query, conn);

        //            cmd.Parameters.AddWithValue("@kanban", comboCuttingKanban.SelectedItem?.ToString() ?? "");
        //            cmd.Parameters.AddWithValue("@kanbanID", "");
        //            cmd.Parameters.AddWithValue("@apu", "APU1");
        //            cmd.Parameters.AddWithValue("@fac", "TH");

        //            cmd.Parameters.AddWithValue("@machine", txtCuttingMachine.Text);
        //            cmd.Parameters.AddWithValue("@part", txtTerminalPart.Text);
        //            cmd.Parameters.AddWithValue("@name", txtTerminalName.Text);

        //            cmd.Parameters.AddWithValue("@pull", pullValue);
        //            cmd.Parameters.AddWithValue("@unit", unit);
        //            cmd.Parameters.AddWithValue("@ip", pullMachineIP);

        //            conn.Open();
        //            int rows = cmd.ExecuteNonQuery();
        //            conn.Close();

        //            if (rows > 0)
        //                Status.Text = $"Data saved: {pullValue} {unit}";
        //            else
        //                Status.Text = "Insert failed: no rows affected.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Status.Text = "DB Error: " + ex.Message;
        //    }
        //}

        private void SaveToDatabase(string pullValue, string unit)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                MERGE [dbo].[CuttingPullRealtimeData] AS target
                USING (SELECT @kanban AS cuttingKanban, @part AS terminalPart) AS source
                ON (target.cuttingKanban = source.cuttingKanban AND target.terminalPart = source.terminalPart)
                WHEN MATCHED THEN
                    UPDATE SET resultPull = @pull,
                               resultUnit = @unit,
                               cuttingMachine = @machine,
                               terminalName = @name,
                               apu = @apu,
                               fac = @fac,
                               createDate = GETDATE(),
                               pullMachineIP = @ip
                WHEN NOT MATCHED THEN
                    INSERT (cuttingKanban, cuttingKanbanID, apu, fac, cuttingMachine, terminalPart, terminalName, resultPull, resultUnit, createDate, pullMachineIP)
                    VALUES (@kanban, @kanbanID, @apu, @fac, @machine, @part, @name, @pull, @unit, GETDATE(), @ip);";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@kanban", comboCuttingKanban.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@kanbanID", "");
                    cmd.Parameters.AddWithValue("@apu", "APU1");
                    cmd.Parameters.AddWithValue("@fac", "TH");
                    cmd.Parameters.AddWithValue("@machine", txtCuttingMachine.Text);
                    cmd.Parameters.AddWithValue("@part", txtTerminalPart.Text);
                    cmd.Parameters.AddWithValue("@name", txtTerminalName.Text);
                    cmd.Parameters.AddWithValue("@pull", Convert.ToDecimal(pullValue));
                    cmd.Parameters.AddWithValue("@unit", unit);
                    cmd.Parameters.AddWithValue("@ip", pullMachineIP);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    Status.Text = $"Latest data saved/updated: {pullValue} {unit}";
                }
            }
            catch (Exception ex)
            {
                Status.Text = "DB Error: " + ex.Message;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }
            base.OnFormClosing(e);
        }
    }
}