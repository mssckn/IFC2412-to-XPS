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

        // 每個量測點：CH1 Distance、CH2 Distance、Thickness，共 3 個輸出值
        private const int VALUES_PER_MEASUREMENT = 3;

        // 每次外部 Trigger 後，IFC2412 輸出 5 組量測值
        private const int TRIGGER_SAMPLE_COUNT = 5;

        // MAD (Median Absolute Deviation) 離群值判定門檻。
        // Modified Z-score 的常用門檻為 |Mz| > 3.5。
        private const double MAD_MODIFIED_Z_THRESHOLD = 3.5;

        // 5 筆中至少保留 3 筆，才接受這個 XY 點的量測結果。
        private const int MIN_VALID_SAMPLES = 3;

        // TransferData 可能分批拿到資料，所以先累積在這裡
        private readonly List<int> triggerRawBuffer = new List<int>();
        private readonly List<double> triggerScaledBuffer = new List<double>();


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

                // 設定外部 Trigger：XPS GPIO -> IFC2412 Multifunction Input (MFI)
                ConfigureExternalTrigger();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void ConfigureExternalTrigger()
        {
            // SP_TriggerSource:
            // 0 = NONE, 1 = SYNC, 2 = MFI, 3 = SOFTWARE, 4 = ENCODER1 ...
            // 這裡使用 MFI，因為 XPS GPIO 要接 IFC2412 的外部 Trigger / Multifunction input。
            if (sensor.SetIntExecSCmd("Set_TriggerSource", "SP_TriggerSource", 2) != ERR_CODE.ERR_NOERROR)
            {
                SensorError("Set_TriggerSource", ref sensor);
                return;
            }

            // Trigger mode: 0 = EDGE, 1 = PULSE(level)
            // 我們要 XPS 每送一個 edge，只觸發一次。
            if (sensor.SetIntExecSCmd("Set_TriggerMode", "SP_TriggerMode", 0) != ERR_CODE.ERR_NOERROR)
            {
                SensorError("Set_TriggerMode", ref sensor);
                return;
            }

            // Trigger level: 0 = HIGH / rising edge, 1 = LOW / falling edge
            // XPS open-collector 拉低時產生 falling edge，因此選 LOW。
            if (sensor.SetIntExecSCmd("Set_TriggerLevel", "SP_TriggerLevel", 1) != ERR_CODE.ERR_NOERROR)
            {
                SensorError("Set_TriggerLevel", ref sensor);
                return;
            }

            // 每收到一次 Trigger，輸出 5 組量測值。
            if (sensor.SetIntExecSCmd("Set_TriggerCount", "SP_TriggerCount", TRIGGER_SAMPLE_COUNT) != ERR_CODE.ERR_NOERROR)
            {
                SensorError("Set_TriggerCount", ref sensor);
                return;
            }

            // 切換成 Trigger 模式前可能殘留連線初期的連續量測資料，只在這裡清一次。
            sensor.SetIntExecSCmd("Clear_Buffers", "SP_AllDevices", 1);
            triggerRawBuffer.Clear();
            triggerScaledBuffer.Clear();

            ShowMsg("External trigger ready: Source=MFI, Mode=EDGE, Level=LOW, Count=" + TRIGGER_SAMPLE_COUNT);
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


        private static double Median(List<double> values)
        {
            if (values == null || values.Count == 0)
                throw new ArgumentException("Median requires at least one value.");

            List<double> sorted = new List<double>(values);
            sorted.Sort();

            int middle = sorted.Count / 2;
            if ((sorted.Count % 2) == 1)
                return sorted[middle];

            return (sorted[middle - 1] + sorted[middle]) / 2.0;
        }

        private List<int> GetMadValidIndices(List<double> thicknessValues, out double median, out double mad)
        {
            List<int> validIndices = new List<int>();
            List<double> finiteValues = new List<double>();

            for (int i = 0; i < thicknessValues.Count; i++)
            {
                double value = thicknessValues[i];
                if (!double.IsNaN(value) && !double.IsInfinity(value))
                    finiteValues.Add(value);
            }

            if (finiteValues.Count == 0)
            {
                median = double.NaN;
                mad = double.NaN;
                return validIndices;
            }

            median = Median(finiteValues);

            List<double> absoluteDeviations = new List<double>();
            for (int i = 0; i < finiteValues.Count; i++)
                absoluteDeviations.Add(Math.Abs(finiteValues[i] - median));

            mad = Median(absoluteDeviations);

            // MAD = 0 時代表至少一半資料完全重合或幾乎重合，
            // 此時 Modified Z-score 會除以 0。
            // 採保守策略：只保留與 median 幾乎相同的點。
            if (mad <= 1e-15)
            {
                double tolerance = Math.Max(1e-12, Math.Abs(median) * 1e-12);

                for (int i = 0; i < thicknessValues.Count; i++)
                {
                    double value = thicknessValues[i];
                    if (double.IsNaN(value) || double.IsInfinity(value))
                        continue;

                    if (Math.Abs(value - median) <= tolerance)
                        validIndices.Add(i);
                }

                return validIndices;
            }

            // Modified Z-score = 0.6745 * (x - median) / MAD
            for (int i = 0; i < thicknessValues.Count; i++)
            {
                double value = thicknessValues[i];
                if (double.IsNaN(value) || double.IsInfinity(value))
                    continue;

                double modifiedZ = 0.6745 * (value - median) / mad;
                if (Math.Abs(modifiedZ) <= MAD_MODIFIED_Z_THRESHOLD)
                    validIndices.Add(i);
            }

            return validIndices;
        }

        void TransferData()
        {
            int avail = 0;
            ERR_CODE err = sensor.DataAvail(ref avail);
            if (err != ERR_CODE.ERR_NOERROR && err != ERR_CODE.ERR_WARNING)
            {
                SensorError("DataAvail", ref sensor);
                return;
            }

            label7.Text = avail.ToString();

            if (avail <= 0)
                return;

            int[] rawData = new int[avail];
            double[] scaledData = new double[avail];

            int read = 0;
            if (sensor.TransferData(rawData, scaledData, avail, ref read) != ERR_CODE.ERR_NOERROR)
            {
                SensorError("TransferData", ref sensor);
                return;
            }

            // TransferData 可能一次只拿到部分資料。
            // 不要 Clear_Buffers；把實際讀到的值累積起來，直到湊滿 5 組。
            for (int i = 0; i < read; i++)
            {
                triggerRawBuffer.Add(rawData[i]);
                triggerScaledBuffer.Add(scaledData[i]);
            }

            int valuesPerTrigger = VALUES_PER_MEASUREMENT * TRIGGER_SAMPLE_COUNT; // 3 x 5 = 15

            while (triggerScaledBuffer.Count >= valuesPerTrigger)
            {
                List<double> ch1Values = new List<double>();
                List<double> ch2Values = new List<double>();
                List<double> thicknessValues = new List<double>();

                for (int sample = 0; sample < TRIGGER_SAMPLE_COUNT; sample++)
                {
                    int baseIndex = sample * VALUES_PER_MEASUREMENT;
                    ch1Values.Add(triggerScaledBuffer[baseIndex + 0]);
                    ch2Values.Add(triggerScaledBuffer[baseIndex + 1]);
                    thicknessValues.Add(triggerScaledBuffer[baseIndex + 2]);
                }

                double thicknessMedian;
                double thicknessMad;
                List<int> validIndices = GetMadValidIndices(
                    thicknessValues,
                    out thicknessMedian,
                    out thicknessMad
                );

                StringBuilder sampleLog = new StringBuilder();
                for (int i = 0; i < TRIGGER_SAMPLE_COUNT; i++)
                {
                    if (i > 0)
                        sampleLog.Append(", ");

                    sampleLog.Append(thicknessValues[i].ToString("0.000000"));
                    if (!validIndices.Contains(i))
                        sampleLog.Append("(OUT)");
                }

                if (validIndices.Count >= MIN_VALID_SAMPLES)
                {
                    double ch1Sum = 0.0;
                    double ch2Sum = 0.0;
                    double thicknessSum = 0.0;

                    for (int i = 0; i < validIndices.Count; i++)
                    {
                        int sampleIndex = validIndices[i];
                        ch1Sum += ch1Values[sampleIndex];
                        ch2Sum += ch2Values[sampleIndex];
                        thicknessSum += thicknessValues[sampleIndex];
                    }

                    double ch1Mean = ch1Sum / validIndices.Count;
                    double ch2Mean = ch2Sum / validIndices.Count;
                    double thicknessMean = thicknessSum / validIndices.Count;

                    // Raw 欄位顯示第一個有效 sample 的 raw 值；
                    // Scale 欄位顯示 MAD 篩選後的平均值。
                    int firstValidSample = validIndices[0];
                    int firstValidBaseIndex = firstValidSample * VALUES_PER_MEASUREMENT;

                    Lbl_index0_Raw.Text = triggerRawBuffer[firstValidBaseIndex + 0].ToString();
                    Lbl_index0_Scale.Text = ch1Mean.ToString("0.000000");

                    Lbl_index1_Raw.Text = triggerRawBuffer[firstValidBaseIndex + 1].ToString();
                    Lbl_index1_Scale.Text = ch2Mean.ToString("0.000000");

                    Lbl_index2_Raw.Text = triggerRawBuffer[firstValidBaseIndex + 2].ToString();
                    Lbl_index2_Scale.Text = thicknessMean.ToString("0.000000");

                    ShowMsg(
                        "Trigger complete | Thickness x5 = [" + sampleLog.ToString() + "]" +
                        " | Median = " + thicknessMedian.ToString("0.000000") +
                        " | MAD = " + thicknessMad.ToString("0.000000") +
                        " | Valid = " + validIndices.Count + "/" + TRIGGER_SAMPLE_COUNT +
                        " | Final mean = " + thicknessMean.ToString("0.000000")
                    );
                }
                else
                {
                    // 有效值少於 3 筆：這個量測點不接受，不硬算平均。
                    Lbl_index0_Raw.Text = "-";
                    Lbl_index0_Scale.Text = "INVALID";
                    Lbl_index1_Raw.Text = "-";
                    Lbl_index1_Scale.Text = "INVALID";
                    Lbl_index2_Raw.Text = "-";
                    Lbl_index2_Scale.Text = "INVALID";

                    ShowMsg(
                        "Trigger INVALID | Thickness x5 = [" + sampleLog.ToString() + "]" +
                        " | Median = " + thicknessMedian.ToString("0.000000") +
                        " | MAD = " + thicknessMad.ToString("0.000000") +
                        " | Valid = " + validIndices.Count + "/" + TRIGGER_SAMPLE_COUNT +
                        " (need >= " + MIN_VALID_SAMPLES + ")"
                    );
                }

                // 移除這一次 Trigger 已處理的 15 個值；若後面又有資料就留給下一次。
                triggerRawBuffer.RemoveRange(0, valuesPerTrigger);
                triggerScaledBuffer.RemoveRange(0, valuesPerTrigger);
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
