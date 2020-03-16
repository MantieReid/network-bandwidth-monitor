using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
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



using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using CsvHelper;
using System.Globalization;

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
      this.StartTimers();
      this.DataContext = this;


     // users.Add(new User() { Id = 1, Name = "John Doe", Birthday = new DateTime(1971, 7, 23) });
      //users.Add(new User() { Id = 2, Name = "Jane Doe", Birthday = new DateTime(1974, 1, 17) });
      //users.Add(new User() { Id = 3, Name = "Sammy Doe", Birthday = new DateTime(1991, 9, 2) });

      //NetworkDataGrid.ItemsSource = users;


    }




    public class User255
    {
      public String DateTime { get; set; }

      public string Upload { get; set; }

      public string Download { get; set; }
    }

    List<User255> users22 = new List<User255>();



    public void UpdateList(String givenDateAndTime, string upload, string download)
    {
      users22.Add(new User255() {DateTime = givenDateAndTime, Upload = upload, Download = download });


      NetworkDataGrid.ItemsSource = users22;
      NetworkDataGrid.Items.Refresh();
      NetworkDataGrid.MinColumnWidth = 20;
      NetworkDataGrid.MinRowHeight = 20;
      NetworkDataGrid.HorizontalContentAlignment = HorizontalAlignment.Center;

      using (var writer = new StreamWriter("somefile.csv"))
      using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
      {
        csv.WriteRecords(users22); //Uses the list to write the data to the csv sheet. 
        writer.Flush(); //flush the writer
      }

      NetworkDataGrid.VerticalContentAlignment = VerticalAlignment.Center;
     NetworkDataGrid.HorizontalAlignment = HorizontalAlignment.Center;





  }



    //create a function here to check if the excel file exists, if it does, then generate the file. 




    // this function will generate the excel file
    //do not use this
    private void GenerateExcelFile()
    {
      var excelApp = new Excel.Application();

      //make the object visible
      excelApp.Visible = true;

      //create a new empty workbook and add it to the collection returned by proprety workbooks.
      excelApp.Workbooks.Add();

      // This example uses a single workSheet. The explicit type casting is
      // removed in a later procedure.
      Excel._Worksheet worksheet = (Excel.Worksheet)excelApp.ActiveSheet;


      worksheet.Cells[1, "A"] = "Date";
      worksheet.Cells[1, "B"] = "Upload";
      worksheet.Cells[1, "C"] = "Download";






      //saves the excel file

      worksheet.SaveAs("NetworkMeterReport.xlsx");

    }






    /// <summary>
    ///  To store the list of network interfaces
    /// </summary>
    private NetworkInterface[] nicArr;

    /// <summary>
    /// Timer Update (every 1 sec)
    /// </summary>
    private const double timerUpdate = 10000;


    /// <summary>
    /// Main Timer Object 
    /// (we could use something more efficient such 
    /// as inter loop calls to HighPerformanceTimers)
    /// </summary>
    private Timer timer;

    DispatcherTimer dispatcherTimer; // a new dispatcher timer variable.

   // private Timer timer;

   // DispatcherTimer dispatcherTimer; // a new dispatcher timer variable.


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



    Dictionary<String, Object> DataDictionary = new Dictionary<String, Object>();
    //Tuple<String, int, Object> DataTuple;

    // Datat

   




    private void GenerateExcelFileandUpdate(string upload, string Download)
    {
      //use the code that is provided by microsoft to do this.


      try
      {
        var excelApp = new Excel.Application();

        //make the object visible
       // excelApp.Visible = true;

        //create a new empty workbook and add it to the collection returned by proprety workbooks.
        excelApp.Workbooks.Add();

        // This example uses a single workSheet. The explicit type casting is
        // removed in a later procedure.
        Excel._Worksheet worksheet = (Excel.Worksheet)excelApp.ActiveSheet;


        worksheet.Cells[1, "A"] = "Date";
        worksheet.Cells[1, "B"] = "Upload";
        worksheet.Cells[1, "C"] = "Download";






        //saves the excel file

        worksheet.SaveAs(@"NetworkMeterReport.xlsx");
        excelApp.Quit();
      }

      catch
      {

        String path = @"NetworkMeterReport.xlsx";
        var excelApp2 = new Excel.Application();
        Workbook wb;
        Worksheet ws;

        wb = excelApp2.Workbooks.Open(path); // open the excel file that is already there.
        ws = wb.Worksheets[1];

        //LastRowis = ws.shee

           //gets the last row that is empty
           object nInLastRow = ws.Cells.Find("*", System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;

        //date time object
        DateTime now = DateTime.Now;


        // ws.Cells[nInLastRow, "A"]
        ws.Cells[nInLastRow, "A"].Value = now;
        ws.Cells[nInLastRow, "B"] = upload; // add the upload string to the cell 
        ws.Cells[nInLastRow, "C"] = Download; // add the download string to the cell





        // public string Readcell(int i, int j)
        //{

        //i++;
        //j++;
        //if(ws.Cells[i,j].value2 != null)
        //{

        //}



      }




    }


    







    //dont use this. IronXl is not developed well enough yet. 
   

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




        //TODO: Convert the download speed to Megabits. This can be done by converting the bytes to Kilobytes, then change it to megabits.

        //TODO: Convert the upload speed to Megabits.   This can be done by converting the bytes to Kilobytes, then change it to megabits.
        String BytesSentAmountCastContent2;
        BytesSentAmountCastContent2 = (String)BytesSentAmountLabel.Content;

       


        long bytesSentSpeed = (long)(interfaceStats.BytesSent - double.Parse(BytesSentAmountCastContent2))  / 1000; //converts the bytes to a KiloBytes(kB).

        long SentSpeedToMegaBytes = bytesSentSpeed / 1000; // converts the sentspeed from kilobytes(KB) to MegaBytes(MB)
        // String BytesSentSpeedToDouble = Convert.ToDouble(by)
        //long whatever = ByteSentSpeedToObject - (bytesSentSpeed / 1024);
        BytesSentAmountLabel.Content = interfaceStats.BytesSent.ToString("N0"); // sets the label text to be equal to the bytes sent speed.



        String BytesReceivedAmountCast;
        BytesReceivedAmountCast = (String)BytesReceivedAmountLabel.Content;

        //takes the bytes received from the interface and put it in the text for the bytes received amount text of the text block.
        long bytesReceivedSpeed = (long)(interfaceStats.BytesReceived - double.Parse(BytesReceivedAmountCast)) / 1024;
        //String ByteRecievedToString = bytesReceivedSpeed.ToString("N0") + " KB/s";  // converts it to a string with commas separating it.


        BytesReceivedAmountLabel.Content = interfaceStats.BytesReceived.ToString("N0");

        // Update the labels

        //TODO:Correct the Nic speed being converted into Kilobytes. It needs to be converted from BITS, not BYTES. 

        long SpeedAmountBytes = (long)(nic.Speed / 1024f);
        String SpeedAmountBytesToString = SpeedAmountBytes.ToString("N0") + " MB/s";

        SpeedAmountLabel.Content = SpeedAmountBytesToString;








        //Bytes_Received_amount_Textblock.Text = interfaceStats.BytesReceived.ToString("N0");

        // Bytes_Sent_amount.Text = interfaceStats.BytesSent.ToString("N0");

        UploadAmountLabel.Content = bytesSentSpeed.ToString() + " KB/s";



        DownloadAmountLabel.Content = bytesReceivedSpeed.ToString() + " KB/s";


        //gets the current date and time. 
        DateTime now = DateTime.Now;

        String BytseSentSpeedString = bytesSentSpeed.ToString();

        String DownloadString = bytesReceivedSpeed.ToString();

        String DateTimeString = now.ToString("hh:mm");

        UpdateList(DateTimeString, BytseSentSpeedString, DownloadString);

        //GenerateExcelFileandUpdate(BytseSentSpeedString, DownloadString);

        //add the current upload speed to the tuple.

        //var DataStuff = new List<Tuple<String, String, Object>>

        Tuple<String, String, Object> DataTuple = new Tuple<String, String, Object>(now.ToString(), "Upload", bytesSentSpeed.ToString());

        //DataTuple.




        // get the IP address of the current selected network interface. 
        UnicastIPAddressInformationCollection ipInfo = nic.GetIPProperties().UnicastAddresses;

        foreach (UnicastIPAddressInformation item in ipInfo)
        {
          //if the IP address is in the system range of Ip addresses
          if (item.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)

          {
            IP_Address_Of_Computer.Content = item.Address.ToString(); // add the IP address to the text of IP address info text block.
            break;
          }
        }



      }

    }



    void dispatcherTimer_Tick(Object sender, Object e)
    {
      InitializeNetworkInterface();
      UpdateNetworkInterface();

    }

    private void MenuItem_Click_Live_Chart_Open(object sender, RoutedEventArgs e)
    {

    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {

    }

    private void Open_Window_1_Click(object sender, RoutedEventArgs e)
    {
   
      NetworkDataGrid.VerticalContentAlignment = VerticalAlignment.Center;
      NetworkDataGrid.HorizontalAlignment = HorizontalAlignment.Center;


      Microsoft.Office.Interop.Excel.Application xlexcel;
      Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;

      object misValue = System.Reflection.Missing.Value;
      xlexcel = new Excel.Application();



      // Excel.Range all = xlexcel.get_Range


      var xlWorkBooks = xlexcel.Workbooks;

      xlexcel.Visible = true;
      Console.WriteLine(Directory.GetCurrentDirectory());

      //Console.Directory.GetCurrentDirectory



      string sourceDir = @"current";
      string backupDir = @"c:\archives\2008";

      //xlexcel.Quit();
      File.Copy(@"somefile.csv", @"SomeReport22.csv", true);

 

      var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"somereport22.csv");



      //ok it worked, Just need to get it to 
      xlWorkBooks.OpenText(path, misValue, misValue, Excel.XlTextParsingType.xlDelimited,
         Excel.XlTextQualifier.xlTextQualifierNone, misValue, misValue,
       misValue, misValue, misValue, misValue, misValue, misValue, misValue,
         misValue, misValue, misValue, misValue);

      // Set Sheet 1 as the sheet you want to work with
      xlWorkSheet = (Excel.Worksheet)xlWorkBooks[1].Worksheets.get_Item(1);


      //Excel.Range last = xlWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
      //Excel.Range range = xlWorkSheet.get_Range("A1", last);

     // int lastUsedRow = last.Row;
     // int lastUsedColumn = last.Column;


      xlWorkSheet.Shapes.AddChart(misValue, misValue, misValue, misValue, misValue).Select();



      var usedrange = xlWorkSheet.UsedRange;

      usedrange.RemoveDuplicates(1); // gets rid of the duplocat
      xlWorkSheet.Rows[2].Delete(); //gets rid of the row that is has upload and download speed that throws the entire chart off. 

      int nInLastRow = xlWorkSheet.Cells.Find("*", System.Reflection.Missing.Value,
System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;

      int nInLastCol = xlWorkSheet.Cells.Find("*", System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSearchOrder.xlByColumns, Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Column;
      System.Console.WriteLine(nInLastCol);
      System.Console.WriteLine(nInLastRow);

      String lastrownumber = nInLastRow.ToString();
      string DesriredRange = "$A${0}:$C${0}";

      string RangeCombinedWithRowNumber = string.Format(DesriredRange, lastrownumber);


      //~~> Make it a Line Chart
      xlexcel.ActiveChart.ApplyCustomType(Excel.XlChartType.xlLineMarkers);


      //~~> Set the data range
      xlexcel.ActiveChart.SetSourceData(xlWorkSheet.Range["$A$1:$C$1",RangeCombinedWithRowNumber]);
      
      //WorkBooks.

      
      //xlWorkSheet.SaveAs("somereportChart.xlsx");
      xlWorkBooks[1].SaveAs("SomeReportChart", Excel.XlFileFormat.xlWorkbookNormal);
    


      //uncomment this if required
      xlWorkBooks[1].Close(true, misValue, misValue);
      xlexcel.Quit();

      releaseObject(xlWorkSheet);
      releaseObject(xlWorkBooks);
      releaseObject(xlexcel);





    }



    private void releaseObject(object obj)
    {
      try
      {
        System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
        obj = null;
      }
      catch (Exception ex)
      {
        obj = null;
        MessageBox.Show("Unable to release the Object " + ex.ToString());
      }
      finally
      {
        GC.Collect();
      }
    }
    private void NetworkDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
  }

}
