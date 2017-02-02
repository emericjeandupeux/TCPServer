using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows.Input;
using System.Windows.Forms;
using System.IO;


namespace TCPServer
{

    public class IMUValues
    {
        public string TimeStamp { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }

        public void Set(string t, double X, double Y, double Z)
        {
            TimeStamp = t;
            x = X;
            y = Y;
            z = Z;
        }
    }

    public class ComboboxClient
    {
        public string name { get; set; }
        public object Value { get; set; }
        public EndPoint id { get; set; }

        public override string ToString()
        {
            return name;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        NotifyIcon nIcon = new NotifyIcon();
        private IMUValues IMU;


        private Socket m_socket;
        private TcpListener m_tcplistener;
        private List<TcpClient> m_tcpclient = new List<TcpClient>();
        private Thread m_listenThread;
        private Thread m_clientThread;
        private IMUWindow ImuWindow;



        public MainWindow()
        {
            InitializeComponent();
            InitWindowPosition();
            InitTCPServer();

            IMU = new IMUValues();

            // FInd ressource folder and icon of the app
            var crtDir = Directory.GetCurrentDirectory();
            string resDir = null;

            crtDir = Directory.GetParent(crtDir).FullName;
            var subDir = Directory.GetDirectories(crtDir, "Ressources");
            if (subDir.Length == 1)
                resDir = subDir[0];
            else
            {
                crtDir = Directory.GetParent(crtDir).FullName;
                subDir = Directory.GetDirectories(crtDir, "Ressources");
                if (subDir.Length == 1)
                    resDir = subDir[0];
            }
            if (resDir != null)
            {
                this.nIcon.Icon = new System.Drawing.Icon(resDir + "/tcp.ico");
                //this.nIcon.ShowBalloonTip(5000, "Test", "Test baloon tip", ToolTipIcon.Info);
                nIcon.DoubleClick += NIcon_DoubleClick;
            }
            //m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void InitTCPServer()
        {
            m_tcplistener = new TcpListener(IPAddress.Any, 1234);
            m_listenThread = new Thread(new ThreadStart(ListenForClients));
            m_listenThread.Start();
        }

        private void InitWindowPosition()
        {
            var H = SystemParameters.FullPrimaryScreenHeight - this.Height + 25;
            var W = SystemParameters.FullPrimaryScreenWidth - this.Width;
            this.Top = H;
            this.Left = W;
        }

        private void NIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            nIcon.Visible = false; 
        }


        private void ListenForClients()
        {
            m_tcplistener.Start();

            while (true)
            {
                //blocks until a client has connected to the server
                var tcpClient = m_tcplistener.AcceptTcpClient();
                m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_tcpclient.Add(tcpClient);
                Action action = delegate ()
                {
                    TbReceive.Text += "Someone connected to you!\n";
                };

                Dispatcher.Invoke(action, System.Windows.Threading.DispatcherPriority.Normal);

                ///////////////////////////////////////////////////
                //create a thread to handle communication
                //with connected client
                m_clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                ComboboxClient itemClient = new ComboboxClient();
                itemClient.name = m_tcpclient.Count.ToString();
                itemClient.Value = m_tcpclient.Count + 1;
                itemClient.id = tcpClient.Client.RemoteEndPoint;

                Dispatcher.Invoke(new Action(() => CbClient.Items.Add(itemClient)));
                Dispatcher.Invoke(new Action(() => CbClient.SelectedIndex = m_tcpclient.Count - 1));
                m_clientThread.Start(m_tcpclient[m_tcpclient.Count - 1]);
            }
        }

        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();
            //var s = m_tcplistener.AcceptSocket();
            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;
                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    //a socket error has occured
                    break;
                }
                if (bytesRead == 0)
                    break;
                ASCIIEncoding encoder = new ASCIIEncoding();
                var bufferincmessage = encoder.GetString(message, 0, bytesRead);

                if (System.Text.RegularExpressions.Regex.IsMatch(bufferincmessage, "IMU:", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    if (ImuWindow == null)
                    {
                        Dispatcher.Invoke(new Action(() =>
                        {
                            ImuWindow = new IMUWindow();
                            ImuWindow.Show();
                        }));
                    }

                    var bufferincmessageresult = bufferincmessage.Split(' ');
                    var timestamp = bufferincmessageresult[1];
                    double x = 0;
                    var b1 = double.TryParse(bufferincmessageresult[2], out x);
                    double y = 0;
                    var b2 = double.TryParse(bufferincmessageresult[3], out y);
                    double z = 0;
                    var b3 = double.TryParse(bufferincmessageresult[4], out z);

                    IMU.Set(timestamp, x, y, z);
                    Dispatcher.Invoke(new Action(() => { MovingWindowCentered(x, y); }));
                    //byte[] buffer = encoder.GetBytes(inlogmessage);

                    /*clientStream.Write(buffer, 0, buffer.Length);
                    clientStream.Flush();*/
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(bufferincmessage, "-POPCORNTIME", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    if (!CheckProcessus("PopcornTimeDesktop"))
                        Process.Start("C:\\Program Files (x86)\\Popcorn Time\\PopcornTimeDesktop.exe","quantico");
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(bufferincmessage, "-DEEZER", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    Process.Start("C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe", "www.deezer.fr");
                }
                else
                {
                    var str = bufferincmessage.ToString();
                    str = tcpClient.Client.RemoteEndPoint.ToString() + ": " + str;
                    Dispatcher.Invoke(new Action(() => { TbReceive.Text += str; }));
                }
            }
        }

        private void MovingWindowCentered(double x, double y)
        {
            int ratio = 400;
            var H = SystemParameters.FullPrimaryScreenHeight / 2 + x * ratio - ImuWindow.ActualHeight / 2;
            var W = SystemParameters.FullPrimaryScreenWidth / 2 + y * ratio * 2 - ImuWindow.ActualWidth / 2;
            if ((bool)RBtnSmiley.IsChecked)
            {
                ImuWindow.Show();
                ImuWindow.Top = H;
                ImuWindow.Left = W;
            }
            else ImuWindow.Hide();
            if ((bool)RBtnMouse.IsChecked)
            {
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)W, (int)H);
            }


        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (TbSend.Text == "") return;
            var pseudo = TbUsername.Text + ": ";
            var str = pseudo + TbSend.Text + "\n";
            ASCIIEncoding encoder = new ASCIIEncoding();
            var msg = Encoding.UTF8.GetBytes(str);
            var bytes = new byte[256];
            var socket = m_tcpclient[CbClient.SelectedIndex].Client;
            int byteCount = 0;

            try
            {
                byteCount = socket.Send(msg, SocketFlags.None);
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.WouldBlock ||
                    ex.SocketErrorCode == SocketError.IOPending ||
                    ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                {
                    // socket buffer is probably full, wait and try again
                    Thread.Sleep(30);
                }
                else
                    throw ex;  // any serious error occurr
            }
            Dispatcher.Invoke(new Action(() => TbReceive.Text += str));
            TbSend.Text = "";
        }

        private void TbSend_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                button1_Click(sender, e);
        }

        private bool CheckProcessus(string processusName)
        {
            Process[] processCollection = Process.GetProcesses();
            foreach (Process p in processCollection)
            {
                if (p.ProcessName == processusName)
                    return true;
            }

            return false;
        }

        private void MouseEnter_Opacity(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.VisualOpacity = 1;
        }

        private void MouseLeave_Opacity(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.VisualOpacity = 0.85;
        }

        private void ClickMinimise(object sender, RoutedEventArgs e)
        {
            this.Hide();
            nIcon.Visible = true;
        }
    }
}
