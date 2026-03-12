using System;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;

namespace pullforcetester
{
    public partial class Form1 : Form
    {
        SerialPort serialPort;
        string filePath = "pullforce_data.csv";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Status.Text = "Not connected.";
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
                // Read whatever is available without blocking
                string data = serialPort.ReadExisting();

                if (!string.IsNullOrEmpty(data))
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        string timeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        txtData.AppendText(timeNow + "  " + data + Environment.NewLine);

                        using (StreamWriter writer = new StreamWriter(filePath, true))
                        {
                            writer.WriteLine($"{timeNow},{data}");
                        }
                    }));
                }
            }
            catch (OperationCanceledException)
            {
                // Ignore if port was closed while reading
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
            MessageBox.Show("Data is already being saved automatically to CSV.");
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