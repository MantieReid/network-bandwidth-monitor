using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MahApps.Metro.Controls;

namespace Network_Meter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }


    /// <summary>
    ///  To store the list of network interfaces
    /// </summary>
    private NetworkInterface[] nicArr;

    /// <summary>
    /// Timer Update (every 1 sec)
    /// </summary>
    private const double timerUpdate = 1000;


    /// <summary>
    /// Main Timer Object 
    /// (we could use something more efficient such 
    /// as inter loop calls to HighPerformanceTimers)
    /// </summary>
    private Timer timer;

    DispatcherTimer dispatcherTimer; // a new dispatcher timer variable.


    public void StartTimers()
    {
      dispatcherTimer = new DispatcherTimer();
      dispatcherTimer.Tick += dispatcherTimer_Tick;
      dispatcherTimer.Interval = new TimeSpan(0, 0, 1); // update every second
      dispatcherTimer.Start();
    }



    private void InitializeNetworkInterface()
    {
      nicArr = NetworkInterface.GetAllNetworkInterfaces(); // the list is equal to names of all the network interfaces  on the computer.
      List<String> goodAdapters = new List<string>();  // create a new list  string called good adapters.

      foreach (NetworkInterface x in nicArr) // for each network interface in the list. 
      {
        if (x.SupportsMulticast && x.GetIPv4Statistics().UnicastPacketsReceived >= 1 && x.OperationalStatus.ToString() == "Up") //if the  network interface has more than one packet and is up. Filters out network interfaces that are down or not being used.
        {

          goodAdapters.Add(x.Name); // add the filtered network adapters to the list. 

        }

      }
      // if the number of good adapters is not equal to the number items in the  combo box and Good adapters count is not equal to zero. 
      if (goodAdapters.Count != ComboBox_Network_interface.Items.Count && goodAdapters.Count != 0)
      {
        ComboBox_Network_interface.Items.Clear(); // remove all items from the combo box
        foreach (string x2 in goodAdapters)
        {
          ComboBox_Network_interface.Items.Add(x2); // add the items from good adapters list to the combo box list as a item.


        }

        ComboBox_Network_interface.SelectedIndex = 0;


      }

      if (goodAdapters.Count == 0) ComboBox_Network_interface.Items.Clear();



    }

    private void UpdateNetworkInterface()
    {
      if (ComboBox_Network_interface.Items.Count >= 1) // if the number of items are less greater than or equal to one
      {
        // Grab NetworkInterface object that describes the current interface
        NetworkInterface nic = nicArr[ComboBox_Network_interface.SelectedIndex];


        IPInterfaceProperties properties = nic.GetIPProperties();
        Object test = nic.Speed;


        // Grab the stats for that interface
        IPv4InterfaceStatistics interfaceStats = nic.GetIPv4Statistics();

        //takes the bytes sent from the interface and put it in the text for the bytes sent amount text of the text block.
        long bytesSentSpeed = (long)(interfaceStats.BytesSent - double.Parse(BytesSentAmountLabel.Content)) / 1024;

        //takes the bytes received from the interface and put it in the text for the bytes received amount text of the text block.
        long bytesReceivedSpeed = (long)(interfaceStats.BytesReceived - double.Parse(BytesReceivedAmountLabel.Content)) / 125000;

        // Update the labels



        // takes the speed amount and puts it in the text of the speed amount text block. 
        Speed_Amount.Text = nic.Speed.ToString();





        // lblInterfaceType.Text = nic.NetworkInterfaceType.ToString();



        Bytes_Received_amount_Textblock.Text = interfaceStats.BytesReceived.ToString("N0");

        Bytes_Sent_amount.Text = interfaceStats.BytesSent.ToString("N0");

        Uploaded_Amount_TextBlock.Text = bytesSentSpeed.ToString() + " KB/s";



        Downloaded_amount_TextBlock.Text = bytesReceivedSpeed.ToString() + " KB/s";

        // get the IP address of the current selected network interface. 
        UnicastIPAddressInformationCollection ipInfo = nic.GetIPProperties().UnicastAddresses;

        foreach (UnicastIPAddressInformation item in ipInfo)
        {
          //if the IP address is in the system range of Ip addresses
          if (item.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)

          {
            IP_Address_Info_TextBlock.Text = item.Address.ToString(); // add the IP address to the text of IP address info text block.
            break;
          }
        }



      }

    }

  }
}
