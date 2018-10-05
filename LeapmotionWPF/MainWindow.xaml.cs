using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Leap;
using System.ComponentModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MathWorks.MATLAB.NET.Arrays;
using normalVector;
using System.IO;

//using Windows.UI.Xaml;
namespace LeapmotionWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        StreamWriter sw = new StreamWriter("myText.txt", true);

        private long mmm = 0;
        private int counter = 0;
        private int filterTmp = 0;
        private int[] filter_tmp = new int[3];
        private string serviceInfo = null;
        private string deviceInfo = null;
        private Canvas drawingCanvas;
        private int numOfFinger = -1;
        private int numOfHand = 0;
        private Int64 frameIdGet = 0;
        private float frameRate = 0;
        private string handState = "open";
        private float grabAngle = 0;
        private float elbowX = 0;
        private float elbowY = 0;
        private float elbowZ = 0;
        private float wristX = 0;
        private float wristY = 0;
        private float wristZ = 0;
        private float wristNormX = 0;
        private float wristNormY = 0;
        private float wristNormZ = 0;
        public event PropertyChangedEventHandler PropertyChanged;
        public Int64 FrameIdGet
        {
            get { return this.frameIdGet; }
            set
            {
                if (this.frameIdGet != value)
                {
                    this.frameIdGet = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new
                 PropertyChangedEventArgs("FrameIdGet"));
                    }
                }
            }
        }
        public int NumOfHand
        {
            get { return this.numOfHand; }
            set
            {
                if (this.numOfHand != value)
                {
                    this.numOfHand = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new
                 PropertyChangedEventArgs("NumOfHand"));
                    }
                }
            }
        }
        private float x = 0;
        public int NumOfFinger
        {
            get { return this.numOfFinger; }
            set
            {
                if (numOfFinger != value)
                {
                    numOfFinger = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("NumOfFinger"));
                    }
                }
            }
        }
        public float X
        {
            get { return this.x; }
            set
            {
                if (x != value)
                {
                    x = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("X"));
                    }
                }
            }
        }
        public float ElbowX
        {
            get { return this.elbowX; }
            set
            {
                if (elbowX != value)
                {
                    elbowX = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ElbowX"));
                    }
                }
            }
        }
        public float ElbowY
        {
            get { return this.elbowY; }
            set
            {
                if (elbowY != value)
                {
                    elbowY = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ElbowY"));
                    }
                }
            }
        }
        public float ElbowZ
        {
            get { return this.elbowZ; }
            set
            {
                if (elbowZ != value)
                {
                    elbowZ = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ElbowZ"));
                    }
                }
            }
        }
        public float WristX
        {
            get { return this.wristX; }
            set
            {
                if (wristX != value)
                {
                    wristX = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("WristX"));
                    }
                }
            }
        }
        public float WristY
        {
            get { return this.wristY; }
            set
            {
                if (wristY != value)
                {
                    wristY = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("WristY"));
                    }
                }
            }
        }
        public float WristZ
        {
            get { return this.wristZ; }
            set
            {
                if (wristZ != value)
                {
                    wristZ = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("WristZ"));
                    }
                }
            }
        }

        public float WristNormX
        {
            get { return this.wristNormX; }
            set
            {
                if (wristNormX != value)
                {
                    wristNormX = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("WristNormX"));
                    }
                }
            }
        }
        public float WristNormY
        {
            get { return this.wristNormY; }
            set
            {
                if (wristNormY != value)
                {
                    wristNormY = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("WristNormY"));
                    }
                }
            }
        }
        public float WristNormZ
        {
            get { return this.wristNormZ; }
            set
            {
                if (wristNormZ != value)
                {
                    wristNormZ = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("WristNormZ"));
                    }
                }
            }
        }
        public string ServiceInfo
        {
            get { return this.serviceInfo; }
            set
            {
                if (this.serviceInfo != value)
                {
                    this.serviceInfo = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new
                 PropertyChangedEventArgs("ServiceInfo"));
                    }
                }
            }
        }
        public string DeviceInfo
        {
            get { return this.deviceInfo; }
            set
            {
                if (this.deviceInfo != value)
                {
                    this.deviceInfo = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new
                 PropertyChangedEventArgs("DeviceInfo"));
                    }
                }
            }
        }
        public float GrabAngle
        {
            get { return this.grabAngle; }
            set
            {
                if (grabAngle != value)
                {
                    grabAngle = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("GrabAngle"));
                    }
                }
            }
        }
        public float FrameRate
        {
            get { return this.frameRate; }
            set
            {
                if (frameRate != value)
                {
                    frameRate = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("FrameRate"));
                    }
                }
            }
        }
        public string HandState
        {
            get { return this.handState; }
            set
            {
                if (this.handState != value)
                {
                    this.handState = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new
                 PropertyChangedEventArgs("HandState"));
                    }
                }
            }
        }


        private static byte[] result = new byte[1024];
        Controller leapMotion = null;
        Socket clientSocket;
        Boolean enableSend = false;
        Boolean isListen = false;
        string enableSend_str;
        
        //private int[] p;//normvector related to palmNorm and wrist-elbow vector

        MWArray[] argsout;
        MWArray[] argsin;
        normalVector.MatLab_Tool_suyu normTool;
        Leap.Vector p;
        Leap.Vector EW_vec;
        int curr_J1 = 0;
        int curr_J2 = 0;
        int curr_J3 = 0;
        int curr_J4 = 0;
        int curr_J5 = 0;
        int curr_J6 = 0;

        int prev_J1 = 0;
        int prev_J2 = 0;
        int prev_J3 = 0;
        int prev_J4 = 0;
        int prev_J5 = 0;
        int prev_J6 = 0;

        int a = 340;
        int amplify_factor = 1;
        double radian2 = 0;
        int angle2 =0 ;

        //---------------------TEST 2----------------------
        //L1,L2,L3,L4,H,TEST2-A,M,N
        int l1=200;
        int l2 = 100;
        double l3 = 0;
        double l4 = 0;
        float test2_a = 1.5f;
        int h = 200;
        int M = -200;
        int N = 0;
        Boolean angleSend=false;
        int select = 0;
        public MainWindow()
        {



            this.DataContext = this;
            //setupDawingCanvas();
            InitializeComponent();
            leapMotion = new Controller();

            leapMotion.Connect += OnServiceConnect;
            leapMotion.Disconnect += OffServiceConnect;
            leapMotion.Device += OnDeviceConnect;
            leapMotion.DeviceLost += OffDeviceConnect;
            //leapMotion.FrameReady += GetFrame;

            //sw = new StreamWriter("myText.txt", true);

            normTool = new normalVector.MatLab_Tool_suyu();
          //  MWArray[] argsin = new MWNumericArray[] { 6, 9, 4, 3, 4, 2 };
            //MWArray[] argsout = new MWNumericArray[3];
            //normTool.normalVector(3, ref argsout, argsin);
            //double[] result = new double[] { -9,-9,-9};
            //float[] result_float = new float[3];
            //MWNumericArray y;
            //MWNumericArray y=argsout[0] as MWNumericArray;
            //WristX = (float)Math.Floor((double)Z123);
            Thread leapTread = new Thread(getDataFromLeap);
            leapTread.Start();
            //getDataFromLeap();
        
              //  result[0] = (double)y;
                //result_float[0] = (float)(result[0]);
                //WristX = result_float[0];
          
        }

        private void getDataFromLeap()
        {
            while (true)
            {
                Thread.Sleep(500);
                Leap.Frame frame = leapMotion.Frame();
                //Leap.Frame frame = leapMotion.Frame();
                // FrameRate = frame.CurrentFramesPerSecond;
                //NumOfHand = frame.Id;
                List<Hand> hands = frame.Hands;
                if (hands.Count > 0)
                {
                    NumOfHand = hands.Count;
                    foreach (Hand hand in hands)
                    {
                        if (hand.IsRight)
                        {
                            GrabAngle = hand.GrabAngle;
                            if (grabAngle > 3.14)
                            {
                                HandState = "Fist";
                            }

                            else if (grabAngle >= 0 && grabAngle < 1)
                            {
                                HandState = "open";
                            }
                            else
                            {
                                HandState = "unknow";
                            }



                            Arm arm = hand.Arm;
                            List<Finger> fingers = hand.Fingers;
                            Finger finger = fingers[0];

                                
                            Leap.Vector wrist = arm.WristPosition;
                            Leap.Vector elbow = arm.ElbowPosition;
                            Leap.Vector palmNormal = hand.PalmNormal;

                            angleSend = true;
                            //counter++;

                            //float xxxx = wristFilter((int)Math.Floor(wrist.x), counter);
                            //if (xxxx != -1000)
                            WristX = wrist.x;
                              //  WristX = xxxx;
                            WristY = wrist.y;
                            WristZ = wrist.z;

                            ElbowX = elbow.x;
                            ElbowY = elbow.y;
                            ElbowZ = elbow.z;

                            

                            //---------------TEST1-----------------
                            /*
                            curr_J1 = angerJone(wrist);
                            curr_J2 = angerJtwo(wrist);
                            curr_J3 = angerJthree();
                            EW_vec = wrist_elbow(wrist, elbow);
                            //p = normVector(palmNormal, EW_vec);
                            p = finger.Direction;
                            curr_J4 = angerJfour(p);
                            curr_J5 = angerJfive(p);
                            */
                            //--------------------------------------------


                            //------------------TEST2--------------------
                            l3 = L3(wrist);
                            l4 = L4(wrist);
                            select = 0;
                            if (l4 > 2 * a)
                                continue;
                            curr_J1 = angleJ1(wrist);
                            curr_J2 = angleJ2_test3(wrist);
                            curr_J3 = angleJ3_test3(curr_J2,wrist);
                            EW_vec = wrist_elbow(wrist, elbow);
                            p = normVector(palmNormal, EW_vec);
                            //p = finger.Direction;
                            curr_J4 = angleJ4(p);
                            curr_J5 = -90;
                            curr_J6 = angleJ6(wrist);
                            //---------------------------------------------

                            //ElbowX = curr_J1;
                            //ElbowY = curr_J2;
                            //ElbowZ = curr_J3;

                            if (curr_J1 >= 160 || curr_J1 <= -160)
                                //angleSend = false;
                                continue;
                            if (curr_J2 > 85 || curr_J2 < -120)
                                //angleSend = false;
                                continue;
                            if (curr_J3 > 80 || curr_J3 < -50)
                                //angleSend = false;
                                continue;
                            if (angleSend && (curr_J4 >= 185 || curr_J4 <= -185 ))
                                //angleSend = false;
                                continue;
                            if (angleSend && (curr_J5 >= 110 || curr_J5 <= -110))
                                //angleSend = false;
                                continue;

                            //sw.WriteLine("hello");
                            /*
                            InteractionBox box = frame.InteractionBox;
                            Leap.Vector wristNorm = box.NormalizePoint(wrist);

                            WristNormX = palmNormal.x;
                            WristNormY = palmNormal.y;
                            WristNormZ = palmNormal.z;
                            */
                            if (!(Math.Pow((curr_J1 - prev_J1), 2) > 9
                                || Math.Pow((curr_J2 - prev_J2), 2) > 4
                                || Math.Pow((curr_J3 - prev_J3), 2) > 4
                                || Math.Pow((curr_J4 - prev_J4), 2) > 9
                                || Math.Pow((curr_J5 - prev_J5), 2) > 9
                                ))
                            {
                                continue;
                            }


                            //curr_J1 = angle1Filter(curr_J1);                      
                           // curr_J3 =angle3Filter(curr_J3, prev_J3);
                            //curr_J2 = angle3Filter(curr_J2, prev_J2);
                            //-------------------------------------------------------------------------------------------------------------------
                            //Test 2-------------------------------

                            /*
                            int test_x = (int)wrist.x;
                            int test_y = 368 - (int)wrist.z;
                            int test_z = (int)wrist.y-169;
                            curr_J4 = curr_J4-180;
                            curr_J5 = curr_J5 + 180;
                            //end
                            */
                            //------------------------------------------------------------------------

                            if (enableSend)
                            {
                                try
                                {
                                    // mmm++;
                                    //Thread.Sleep(1000);    //等待1秒钟
                                    int Jx = 0;
                                    int Jz = -1;
                                    int Jy = 270
                                        ;
                                    //string str_send = curr_J1 + "," + Jx + "," + Jx + "," + curr_J4 + "," + curr_J5 + "," + Jx;

                                    //-------------------------------------------
                                    //test2

                                    //string str_send1 = test_x + "," + test_y + "," + test_z + "," + curr_J4 + "," + curr_J5 + "," + Jx;

                                    //end
                                    //---------------------------------------
                                    string str_send2 =select +"," + curr_J1 + "," + curr_J2 + "," +curr_J3 + "," + Jz + "," + curr_J5 + "," + Jy;

                                    //--------------------------------------------------------
                                    //test3


                                    clientSocket.Send(Encoding.ASCII.GetBytes(str_send2));
                                    prev_J1 = curr_J1;
                                    prev_J2 = curr_J2;
                                    prev_J3 = curr_J3;
                                    prev_J4 = curr_J4;
                                    prev_J5 = curr_J5;
                                    //clientSocket.Send(Encoding.ASCII.GetBytes("HELLO"));
                                    //Console.WriteLine("向服务器发送消息：{0}" + sendMessage);
                                }
                                catch
                                {
                                    clientSocket.Shutdown(SocketShutdown.Both);
                                    clientSocket.Close();
                                    break;
                                }
                            }

                            
                        }
                    }
                }
            }
        }

        private int angle1Filter(int c_J1)
        {
            double cur = c_J1;
            
            if( Math.Abs(cur) <= 30)
            {
                cur =  cur / 2;
            }

            else if (Math.Abs(cur) > 30)
            {
                cur = 15*cur/Math.Abs(cur);
            }

            return (int)cur;
        }

        public double L3(Leap.Vector wristVector)
        {
            double x_m = wristVector.x - M;
            double l3_tmp = Math.Pow((l2 + test2_a * x_m), 2) + Math.Pow(test2_a * wristVector.z, 2);
            l3_tmp = Math.Sqrt(l3_tmp);
            return l3_tmp;
        }

        public double L4(Leap.Vector wristVector)
        {
            double h0 = test2_a * wristVector.y - h;
            double l4;
            double l4_tmp = Math.Pow(l3, 2) + Math.Pow(test2_a * h0, 2);
            l4_tmp = Math.Sqrt(l4_tmp);
            return l4_tmp;
        }

        private int angle3Filter(int curr_J, int prev_J)
        {
            double cur = curr_J;
            double prev = prev_J;
            if (Math.Pow(cur, 2) - Math.Pow(prev, 2) > 16)
                cur = 4*(cur - prev)/Math.Abs(cur - prev)+prev;
            return (int)cur;
        }

        private void btnConnect(object sender, RoutedEventArgs e)
        {
            IPAddress ip = IPAddress.Parse("172.18.11.152");
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(ip, 8885)); //配置服务器IP与端口  
                clientSocket.Send(Encoding.ASCII.GetBytes("L"));
                txtInformation.Text = "已连接服务器";
                Thread recieve = new Thread(DataFromServer);
                recieve.Start(clientSocket);
                isListen = true;

            }
            catch
            {
                //return;
                txtInformation.Text = "连接失败";
                isListen = false;
            }
        }

        private void btnDisconnect(object sender, RoutedEventArgs e)
        {
            // IPAddress ip = IPAddress.Parse("172.18.9.241");
            // clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                isListen = false;
                clientSocket.Close();
                txtInformation.Text = "断开服务器";
                //isListen = false;

            }
            catch
            {
                //return;
                txtInformation.Text = "断开服务器";
            }
        }

        private void DataFromServer(Object clientSocket)
        {
            Socket socketCp = (Socket)clientSocket;
            while (isListen)
            {
                try
                {
                    //通过clientSocket接收数据  
                    int receiveNumber = socketCp.Receive(result);
                    enableSend_str = Encoding.ASCII.GetString(result, 0, receiveNumber);
                    if (enableSend_str == "true")
                    {
                        enableSend = true;
                    }
                    else if (enableSend_str == "false")
                    {
                        enableSend = false;
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                    // socketCp.Shutdown(SocketShutdown.Both);
                    //socketCp.Close();
                    break;
                }
            }
        }

        public void OnServiceConnect(object sender, ConnectionEventArgs args)
        {
            ServiceInfo = "Service connected";
        }
        public void OffServiceConnect(object sender, ConnectionLostEventArgs args)
        {
            ServiceInfo = "Service lost";
        }
        public void OnDeviceConnect(object sender, DeviceEventArgs args)
        {
            DeviceInfo = "Device Connected";
        }
        public void OffDeviceConnect(object sender, DeviceEventArgs args)
        {
            DeviceInfo = "Device Disconnected";
        }

        public float wristFilter(int position, int count)
        {
            counter++;
            if (counter == 1)
            {
                filterTmp = 0;
                filter_tmp[0] = position;
            }
            else
            {
                int cal = position - filter_tmp[counter - 2];
                cal = cal * cal;
                if (cal < 2)
                    filter_tmp[counter - 1] = filter_tmp[counter - 2];
                else
                    filter_tmp[counter - 1] = position;
                if (counter == 3)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        filterTmp += filter_tmp[j];
                    }
                    filterTmp = filterTmp / 3;
                    counter = 0;
                    return filterTmp;
                }

            }
            return -1000;

        }

        public int angerJone(Leap.Vector wristVector)
        {
            double tan = (-1) * wristVector.x / wristVector.z;
            double radian = Math.Atan(tan);
            double Jone = (-1)*radian / Math.PI * 180;
            int J1 = (int)Math.Floor(Jone);
            return J1;
        }


        public int angerJtwo(Leap.Vector wristVector)
        {
            int J2;

            //calculate 01
            double sum1 = Math.Pow(wristVector.x, 2) + Math.Pow(wristVector.z,2);
            double tan = wristVector.y / Math.Sqrt(sum1);
            double radian1 = Math.Atan(tan);
            int angle1= (int)Math.Floor(radian1/Math.PI * 180);
            //calculate 02   
            //02 is x_angle
            double sum2 = Math.Pow(wristVector.x, 2) + Math.Pow( wristVector.y, 2)+Math.Pow(wristVector.z,2);
            double cos = (amplify_factor*Math.Sqrt(sum2) )/ (2*a);
            radian2 = Math.Acos(cos);

            //tmp is angle1+angle2
            double tmp = (radian1 + radian2) / Math.PI * 180;
            angle2 =(int) Math.Floor(radian2 / Math.PI * 180);
            J2 = (int)Math.Floor(tmp-90);
            return J2;
        }

        public int angerJthree()
        {
            int angle3 = (-1)*curr_J2 + 2 * angle2 - 90;
            int J3 = (-1) * curr_J2 - angle3;
            return J3;
        }
        public int angerJfour(Leap.Vector p)
        {
            double sum = Math.Pow(p.x, 2) + Math.Pow(p.y, 2)+Math.Pow(p.z,2);
            sum = Math.Sqrt(sum);
            double cos,radian ;
            if (p.y < 0)
            {
                cos = (-1) * p.y / sum;
                radian = Math.Acos(cos);
            }

            else
            {
                cos = p.y / sum;
                radian = Math.PI-Math.Acos(cos);
            }
            double Jfour= radian / Math.PI * 180;
            int J4 = (int)Math.Floor(Jfour);
            return J4;
        }

        public int angerJfive(Leap.Vector p)
        {
            double tan;
            double radian;
            double Jfive;
            int J5;
            if (p.z >= 0)
            {
                tan = (-1)*p.z / p.x;
                //ElbowX =(float) tan;
                radian = Math.Atan(tan)+Math.PI/2 ;
                Jfive = radian / Math.PI * 180;
                J5 =(int)Math.Floor(Jfive)-curr_J1;
                J5 = (-1) * J5;
                //ElbowX = J5;
                return J5;
            }
            else
            {
                tan = p.z / p.x;
                radian = Math.PI/2-Math.Atan(tan);
                Jfive = radian / Math.PI * 180;
                J5 = (int)Math.Floor(Jfive)-curr_J1;
                J5 = (-1) * J5;
                return J5;
            }
        }

        //--------------------TEST2 METHONDS----------------------------------------
        public int angleJ1(Leap.Vector wristVector)
        {
            float L4 = 400;
            double tan;
            double radian;
            double Jone;
            int J1;
            /*
            if (wristVector.z < 0) { 
                tan =( -1)*test2_a * wristVector.z  / (wristVector.x-M);
                radian = Math.Atan(tan);
            }
            else { 
                tan =test2_a * wristVector.z / (wristVector.x - M);
                radian =(-1) * Math.Atan(tan);
            }
        Jone = radian / Math.PI * 180;
            J1 =(int) Math.Floor(Jone);
            return J1;
            */
            /*
            tan = Math.Abs(wristVector.z) / (wristVector.x - M + L4);
            radian = Math.PI / 2 - Math.Atan(tan);
            Jone = radian / Math.PI * 180;
            J1 = (int)Math.Floor(Jone);
            if (wristVector.z < 0)
            {
                return J1;
            }
            else
            {
                J1 = (-1) * J1;
                return J1;
            }
            */
            if (wristVector.z > 100)
                J1 = -11;
            else if (wristVector.z < -60)
                J1 = 11;
            else
            {
                Jone = -11 - 22 * (wristVector.z - 100) / 160;
                J1 = (int)Jone;
            }
            return J1;
        }

        public int angleJ2(Leap.Vector wristVector)
        {
            //  l3
            //  test2_a    放大倍数
            
            double h0 = test2_a*wristVector.y - h;
            
            //ElbowY = (float)l3;
            //L4,斜边
           
            //angle 01
            double radian1;
            radian1 = Math.Atan(h0 / l3);
            ElbowX = (float)(radian1 / Math.PI * 180);
            //angle 02
            radian2 = Math.Acos(l4 / (2 * a));

            //angle J2
            double Jtwo =(-1)* (Math.PI/2-radian1-radian2) / Math.PI * 180;
            //ElbowY = (float)Jtwo;
            int J2 = (int)Math.Floor(Jtwo);
            //ElbowY = J2;
            return J2;
        }

        public int angleJ3()
        {
            double radian3;
            //ElbowY = (float)(radian2 / Math.PI * 180);

            radian3 = Math.PI / 2 - 2 * radian2;
            double Jthree = radian3 / Math.PI * 180;
            int J3 = (int)Math.Floor(Jthree);
            //ElbowZ = J3;
            return J3;
        }

        public int angleJ4(Leap.Vector P) 
        {
            double tan;
            double radian;
            double Jfour;
            int J4;
            if (p.z >= 0)
            {
                tan = Math.Abs(p.z / p.x);
                //ElbowX =(float) tan;
                radian = Math.Atan(tan);
                double angle_tmp = radian / Math.PI * 180;

                if (curr_J1 > 0)
                {                   
                    Jfour = curr_J1 + angle_tmp-90;
                    J4 = (int)Jfour;
                }
                else
                {
                    Jfour = curr_J1 - angle_tmp;
                    J4 = (int)Jfour;
                }
               
            }
            else
            {
                tan = Math.Abs(p.z / p.x);
                //ElbowX =(float) tan;
                radian = Math.Atan(tan);
                double angle_tmp = radian / Math.PI * 180;

                if (curr_J1 > 0)
                {
                    Jfour = curr_J1 + angle_tmp;
                    J4 = (int)Jfour;
                }
                else
                {
                    Jfour = angle_tmp+curr_J1;
                    J4 = (int)Jfour;
                }
            }

            return J4;
        }

        public int angleJ5(Leap.Vector P)
        {
            double sum = Math.Pow(p.x, 2) + Math.Pow(p.y, 2) + Math.Pow(p.z, 2);
            sum = Math.Sqrt(sum);
            double cos, radian,Jfive;
            int J5;

            cos = Math.Abs(p.y) / sum;
            radian = Math.Acos(cos);
            double angle_tmp = radian / Math.PI * 180;
            //ElbowZ =(float) angle_tmp;
            if (curr_J3 > 0)
            {
                
                Jfive = Math.Abs(curr_J2)-curr_J3- angle_tmp - 90;
                //ElbowY = (float)Jfive;
                J5 = (int)Jfive;
                //ElbowZ = (float)J5;
            }

            else
            {
                Jfive = Math.Abs(curr_J2)+Math.Abs(curr_J3)-90 - angle_tmp;
                J5 = (int)Jfive;
            }
            return J5;
        }

        public int angleJ6(Leap.Vector wristVector)
        {
            double tan,radian, Jsix;
            int J6;
            tan = Math.Abs(wristVector.z) / (wristVector.x - M);
            radian = Math.Atan(tan);

            if (curr_J1 > 0)
            {
                Jsix = curr_J1 - radian / Math.PI * 180;
            }
            else
            {
                Jsix = radian / Math.PI * 180+curr_J1;
            }
            J6=(int)Jsix;
            return J6;
        }
        //--------------------------------------------------------------------------

            //-----------------------------------------------------------------------
            //------------------------TEST3--------------------------------------------
            public int angleJ2_test3(Leap.Vector wristVector)
        {
            double dis = wristVector.x;
            double Jtwo = 0;
            int J2;
            if (wristVector.y < 320)
            {
                if (dis >= 70)
                {
                    Jtwo = -9;
                }
                else if (dis <= -70)
                {
                    Jtwo = 12;
                }
                else
                {
                    Jtwo = 12 - 21 * (dis + 70) / 140;
                }
                
            }
            else
            {
                /*
                if (dis >= 70)
                {
                    Jtwo = -9;
                }
                else if (dis <= -70)
                {
                    Jtwo = 18;
                }
                else
                {
                    Jtwo = 18 - 27 * (dis + 70) / 140;
                }
                J2 = (int)(Jtwo);
                */
                select = 9;
            }
            J2 = (int)(Jtwo);
            return J2;
        }

        public int angleJ3_test3(int J2, Leap.Vector wristVector)
        {
            int J3 = 0;
            if (wristVector.y < 320)
            {
                if (J2 >= -9 && J2 <= 12)
                {
                    J3 = 16 - 21 * (J2 + 9) / 21;
                    return J3;
                }
                else
                    return 24;
            }
            else
            {
                if (J2 >= -9 && J2 <= 18)
                {
                    J3 = 23 - 28 * (J2 + 9) / 27;
                    return J3;
                }
                else
                    return 24;
            }
        }
        public Leap.Vector normVector(Leap.Vector palmnorm, Leap.Vector  ew)
        {
            //ElbowX = palmnorm.x;
           // argsout = new MWArray[3];
            argsin = new MWArray[] {palmnorm.x,palmnorm.y,palmnorm.z,ew.x,ew.y,ew.z};
            normTool.normalVector(3, ref argsout, argsin);
            MWNumericArray m ;
            double tmp;
            float[] vector_p=new float[3];
            for(int i = 0; i < 3; i++)
            {
                m= argsout[i] as MWNumericArray;
                tmp = (double)m;
                vector_p[i] = (float)(tmp);
                //WristX = vector_p[i];
            }
            Leap.Vector p = new Leap.Vector(vector_p[0], vector_p[1], vector_p[2]);
            //ElbowX = p.x;
            return p;
        }
        
        public Leap.Vector wrist_elbow(Leap.Vector wrist,Leap.Vector elbow)
        {
            float x = wrist.x - elbow.x;
            float y = wrist.y - elbow.y;
            float z = wrist.z - elbow.z;
            Leap.Vector vector_EW = new Leap.Vector(x, y, z);
            return vector_EW;
        }
        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            this.leapMotion.StopConnection();
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            this.leapMotion.StartConnection();
        }

    }

    

}
