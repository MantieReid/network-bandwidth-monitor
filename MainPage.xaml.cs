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
        }

        /// <summary>
        ///  To store the list of network interfaces
        /// </summary>
        private NetworkInterface[] nicArr;



      private void InitializeNetworkInterface()
    {
      nicArr = NetworkInterface.GetAllNetworkInterfaces(); // the list is equal to names of all the network interfaces  on the computer.
      List<String> goodAdapters = new List<string>();  // create a new list  string called good adapters.

      foreach (NetworkInterface x in nicArr)
      {
        if (x.SupportsMulticast && x.GetIPv4Statistics().UnicastPacketsReceived >= 1 && x.OperationalStatus.ToString() == "Up")
        {

          goodAdapters.Add(x.Name);

        }

      }

    }


  }

}
