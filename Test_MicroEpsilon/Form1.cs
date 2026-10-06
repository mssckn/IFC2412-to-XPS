using MicroEpsilon; // MEDAQLib
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Test_MicroEpsilon
{
    
    public partial class Form1 : Form
    {
        public static MEDAQLib sensor = null;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {
                ShowMsg(ex.ToString());
            }
        }
        void SensorError(string location, ref MEDAQLib sensor)
        {
            //output error message

            string errText = "";
            ERR_CODE err = sensor.GetError(ref errText);
            txt_Msg.Text += location + " :  " + errText + "\r\n";
        }
        void ShowMsg(string str)
        {
            txt_Msg.Text += str + "\r\n";
        }
        void open_Sensor()
        {
            try
            {
                string sensor_ip = "";
                sensor_ip = txtDeviceName.Text;
                if (sensor.OpenSensorTCPIP(sensor_ip) != ERR_CODE.ERR_NOERROR)//20161122
                {
                    SensorError("OpenSensorETH", ref sensor);
                    ShowMsg(txt_SensorType.Text + " Sensor error");
                    return;
                }

                ShowMsg(txt_SensorType.Text + " Sensor connected");




                //SetMeasureMode 0:Distance, 1:Thickness
                //if (sensor.SetIntExecSCmd("Set_MeasureMode", "SP_MeasureMode", 0) != ERR_CODE.ERR_NOERROR)
                //    SensorError("SetIntExecSCmd (Set_MeasureMode)", ref sensor);


                //設定資料輸出介面為Ehternet
                if (true)
                {
                    sensor.SetParameterInt("SP_OutputRS422", 0);
                    sensor.SetParameterInt("SP_OutputAnalog1", 0);
                    sensor.SetParameterInt("SP_OutputAnalog2", 0);
                    sensor.SetParameterInt("SP_OutputErrorOut", 0);
                    sensor.SetParameterInt("SP_OutputEthernet", 1);
                    sensor.SetParameterInt("SP_OutputIndustrialEthernet", 1);
                    
                    if (sensor.ExecSCmd("Set_DataOutInterface") != ERR_CODE.ERR_NOERROR)
                        SensorError("ExecSCmd (Update_DataOutInterface)", ref sensor);
                }

                //設定Ehternet 輸出選項
                if (true)
                {
                    
                    sensor.SetParameterInt("SP_OutputDistance1_Ch1_ETH", 1);//CH1 - Dis1
                    sensor.SetParameterInt("SP_OutputDistance1_Ch2_ETH", 1);//CH2 - Dis1
                    sensor.SetParameterString("SP_OutputCalculation_ETH", "thick");//CJ1-Thickness


                    sensor.SetParameterInt("SP_OutputVideoRaw_Ch1_ETH", 0);
                    sensor.SetParameterInt("SP_OutputAdditionalIntensity_Ch1_ETH", 0);
                    sensor.SetParameterInt("SP_OutputAdditionalShutterTime_Ch1_ETH", 0);
                    

                    if (sensor.ExecSCmd("Update_Output_ETH") != ERR_CODE.ERR_NOERROR)
                        SensorError("ExecSCmd (Update_Output_ETH)", ref sensor);
                }

                //設定Trigger step size
                //double protocao = 4;
                //if (sensor.SetIntExecSCmd("Set_TriggerSource", "SP_TriggerSource", protocao) != ERR_CODE.ERR_NOERROR)
                //{
                //    SensorError("Set_TriggerSource", ref sensor);
                //}
                //protocao = 40;

                //if (sensor.SetDoubleExecSCmd("Update_TriggerEncoderStepsize", "SP_TriggerEncoderStepsize", protocao) != ERR_CODE.ERR_NOERROR)
                //{
                //    SensorError("Set_TriggerCount", ref sensor);
                //}


                //ShowMsg("Set_TriggerCount:" + protocao.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void GetSensorParameters()
        {
            //ShowMsg("Get all parameters ...");

            if (sensor.SetIntExecSCmd("Get_AllParameters", "SP_Additional", 1) != ERR_CODE.ERR_NOERROR)
                SensorError("SetIntExecSCmd (Get_AllParameters)", ref sensor);

            string SensorName = "";
            if (sensor.GetParameterString("SA_Sensor", ref SensorName) != ERR_CODE.ERR_NOERROR)
                SensorError("GetParameterString (SA_Sensor)", ref sensor);

            string serial = "";
            if (sensor.GetParameterString("SA_SerialNumber", ref serial) != ERR_CODE.ERR_NOERROR)
                SensorError("GetParameterString (SA_SerialNumber)", ref sensor);


            string SA_HardwareRevision = "";
            if (sensor.GetParameterString("SA_HardwareRevision", ref SA_HardwareRevision) != ERR_CODE.ERR_NOERROR)
                SensorError("GetSA_HardwareRevision (HardwareRevision)", ref sensor);


            textBox1.Text = "SA_Sensor : " + SensorName + "\r\n" + 
                            "SA_SerialNumber : " + serial + "\r\n" +
                            "SA_HardwareRevision : " + SA_HardwareRevision + "mm\r\n";

            
        }



        void GetVideo()
        {
            Console.WriteLine("Get video signal ...");

            sensor.SetParameterInt("SP_ReadMode", 0);
            int WaitVideoTimeout = 0;
            sensor.GetParameterInt("SP_WaitVideoTimeout", ref WaitVideoTimeout);
            sensor.SetParameterInt("SP_WaitVideoTimeout", 100);

            if (sensor.ExecSCmd("Get_VideoStreamSignal") != ERR_CODE.ERR_NOERROR)
                SensorError("ExecSCmd (Get_VideoStreamSignal)", ref sensor);


            byte[] bArray = null;
            if (sensor.GetParameterBinary("SA_VideoDark_Ch1", ref bArray) != ERR_CODE.ERR_NOERROR)
                SensorError("GetParameterBinary (SA_VideoDark_Ch1)", ref sensor);

            short[] sArray = new short[bArray.Length / 2];
            Buffer.BlockCopy(bArray, 0, sArray, 0, bArray.Length);

            //顯示512訊號內容
            //ShowMsg("sArray Length:" + sArray.Length.ToString());

            //CeratNewChart_Video(sArray);
        }


        void TransferData()
        {

            //SetMeasureMode 0:Distance, 1:Thickness (Page.390)


            //sensor.ExecSCmd("Software_Trigger");
            //GetVideo();

            int avail = 0;
            ERR_CODE err = sensor.DataAvail(ref avail);
            if (err != ERR_CODE.ERR_NOERROR && err != ERR_CODE.ERR_WARNING)
                SensorError("DataAvail", ref sensor);

            label7.Text = avail.ToString();
            if (avail > 0)
            {
                

                int[] rawData = new int[avail];
                double[] scaledData = new double[avail];

                //Using TransferData methon to get data.
                int read = 0;
                if (sensor.TransferData(rawData, scaledData, avail, ref read) != ERR_CODE.ERR_NOERROR)
                    SensorError("TransferData", ref sensor);

                Lbl_index0_Raw.Text = rawData[0].ToString();
                Lbl_index0_Scale.Text = scaledData[0].ToString("0.000000");

                Lbl_index1_Raw.Text = rawData[1].ToString();
                Lbl_index1_Scale.Text = scaledData[1].ToString("0.000000");

                Lbl_index2_Raw.Text = rawData[2].ToString();
                Lbl_index2_Scale.Text = scaledData[2].ToString("0.000000");

                //Clear Buffer
                sensor.SetIntExecSCmd("Clear_Buffers", "SP_AllDevices", 1);
            }
            
        }


        private void btn_Connect_Click(object sender, EventArgs e)
        {
           
            if (sensor == null)
            {
                sensor = new MEDAQLib(txt_SensorType.Text);

                //與感測器連線
                open_Sensor();

                //取得感測器參數
                GetSensorParameters();

                
                btn_Connect.Text = "斷線";
            }
            else
            {
                if (btn_Connect.Text == "斷線")
                {
                    ShowMsg("sensor is disconnected");
                    sensor.CloseSensor();
                    sensor = null;
                    btn_Connect.Text = "連線";
                    timer1.Stop();
                    startTimer = false;
                    button2.Text = "開啟自動讀取";
                }
            }
            
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (sensor != null)
            //{
            //    sensor.CloseSensor();
            //    sensor = null;
            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                TransferData();

                sensor.SetParameterInt("SP_AllDevices", 1);
            }
            catch (Exception ex)
            {
                ShowMsg("Timer is Stop");
                timer1.Stop();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TransferData();
        }

        bool startTimer = false;
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (startTimer == false)
            {
                ShowMsg("Transfer data ...");

                startTimer = true;
                button2.Text = "關閉自動讀取";
            }
            else
            {
                ShowMsg("Stop Transfer data ...");

                startTimer = false;
                button2.Text = "開啟自動讀取";
            }

            timer1.Enabled = startTimer;
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            GetVideo();
        }
        private bool _once = true;
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (_once)
            {
                Rectangle ee = new Rectangle(10, 10, 30, 30);
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    e.Graphics.DrawRectangle(pen, ee);
                }
                _once = false;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {

            

        }
        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn_CreateMaster_Click(object sender, EventArgs e)
        {
            sensor.SetParameterInt("SP_MasterSignal", -1);
            sensor.SetParameterString("SP_MasterSignalName", "thick");
            sensor.SetParameterDouble("SP_MasterValue", 1.0);

            if (sensor.ExecSCmd("Set_MasterSignal") != ERR_CODE.ERR_NOERROR)
                SensorError("ExecSCmd (Set_MasterSignal)", ref sensor);


        }

        private void btn_Master_Click(object sender, EventArgs e)
        {
            //啟動Master
            sensor.SetParameterInt("SP_MasterSignal", -1);
            sensor.SetParameterString("SP_MasterSignalName", "thick");
            sensor.SetParameterInt("SP_MasterAction", 1);
            if (sensor.ExecSCmd("Exec_Master") != ERR_CODE.ERR_NOERROR)
                SensorError("ExecSCmd (Exec_Master)", ref sensor);
        }

        private void Btn_ResetMaster_Click(object sender, EventArgs e)
        {
            //Reset Master
            sensor.SetParameterInt("SP_MasterSignal", -1);
            sensor.SetParameterString("SP_MasterSignalName", "thick");
            sensor.SetParameterInt("SP_MasterAction", 0);
            if (sensor.ExecSCmd("Exec_Master") != ERR_CODE.ERR_NOERROR)
                SensorError("ExecSCmd (Exec_Master)", ref sensor);
        }
    }
}
