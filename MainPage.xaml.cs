using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Timers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace network_bandwidth_monitor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.StartTimers();
           // this.dispatcherTimer_Tick();

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
      if (goodAdapters.Count != Interface_ComboBox.Items.Count && goodAdapters.Count != 0)
      {
        Interface_ComboBox.Items.Clear(); // remove all items from the combo box
        foreach(string x2 in goodAdapters)
        {
          Interface_ComboBox.Items.Add(x2); // add the items from good adapters list to the combo box list as a item.


        }

        Interface_ComboBox.SelectedIndex = 0;


      }

      if (goodAdapters.Count == 0) Interface_ComboBox.Items.Clear();



    }
    //This is the combo box.


    /// <summary>
    /// Initialize the Timer
    /// </summary>



      // private void InitializeTimer()
    //{
      //timer = new Timer();
      //timer.Interval = (int)timerUpdate;
      //timer.Tick += new EventHandler(timer_Tick);
      //timer.Start();

    



    /// <summary>
    /// Update GUI components for the network interfaces
    /// </summary>
    private void UpdateNetworkInterface()
    {
      if (Interface_ComboBox.Items.Count >= 1) // if the number of items are less greater than or equal to one
      {
        // Grab NetworkInterface object that describes the current interface
        NetworkInterface nic = nicArr[Interface_ComboBox.SelectedIndex];


        IPInterfaceProperties properties = nic.GetIPProperties();
        Object test  = nic.Speed;


        // Grab the stats for that interface
        IPv4InterfaceStatistics interfaceStats = nic.GetIPv4Statistics();

        //takes the bytes sent from the interface and put it in the text for the bytes sent amount text of the text block.
        long bytesSentSpeed = (long)(interfaceStats.BytesSent - double.Parse(Bytes_Sent_amount.Text)) / 1024;

        //takes the bytes received from the interface and put it in the text for the bytes received amount text of the text block.
       long bytesReceivedSpeed = (long)(interfaceStats.BytesReceived - double.Parse(Bytes_Received_amount_Textblock.Text)) / 125000;

        // Update the labels



        // takes the speed amount and puts it in the text of the speed amount text block. 
        Speed_Amount.Text = nic.Speed.ToString();





       // lblInterfaceType.Text = nic.NetworkInterfaceType.ToString();



        Bytes_Received_amount_Textblock.Text = interfaceStats.BytesReceived.ToString("N0");

        Bytes_Sent_amount.Text = interfaceStats.BytesSent.ToString("N0");

        Uploaded_Amount_TextBlock.Text = bytesSentSpeed.ToString() + " KB/s";

       Bytes_Received_amount_Textblock.Text = bytesReceivedSpeed.ToString() + " KB/s";

        // get the IP address of the current selected network interface. 
        UnicastIPAddressInformationCollection ipInfo = nic.GetIPProperties().UnicastAddresses;

        foreach (UnicastIPAddressInformation item in ipInfo)
        {
          //if the IP address is in the system range of Ip addresses
          if( item.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)  

          {
            IP_Address_Info_TextBlock.Text = item.Address.ToString(); // add the IP address to the text of IP address info text block.
            break;
          }
        }



      }

    }


    void dispatcherTimer_Tick (Object sender, Object e)
    {
      InitializeNetworkInterface();
      UpdateNetworkInterface();

    }


    private void Interface_TextBlock1_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
  }

}
