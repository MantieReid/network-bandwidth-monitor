   M S B A M L   `   `   `  ���� H  DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf H DNetwork Meter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Network_MeterNetwork_Meter    Network_Meter.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     -localclr-namespace:Network_Meter     &oxyhttp://oxyplot.org/wpf        MetroWindowButtonStyle(   "MahApps.Metro.Controls.MetroWindow    WindowCloseButtonStyle#  [   � Title$ Network Meter��$	��477��$	��800��   WindowTitleBrush �	#FF0070FF���	#FF2E8AFF    NonActiveWindowTitleBrush �	#FF0070FF
�������   NonActiveBorderBrush �	#FF0070FF   NonActiveGlowBrush �	#FF0070FF   TitleForeground
 ����� J� OpacityMask
 �   �.���� $
��Left=�$
��Auto��$��0,0,0,0q�$	��Top=�$
��Auto�����	#FF2E8AFF.@��� $��Meter
��.��� ���	#FF2E8AFF � ColumnDefinitions �� $
��Auto ��� $
��Auto �	 � RowDefinitions	 �� $
��Auto ��� $��31 ��� $
��Auto �$��50���� $��50 ��� $��50 ��� $��50 ��� $��50 ��� $��50 ��� $
��Auto �.+��� -   
 ��Name$
 InterfaceLabel��$��	Interface��$��30��$��0,0,0,0q�$
��Auto��$��Bottom=�$
��Left=�
�������$��1���� -   $
 
SpeedLabel��$��Speed��$��30��$
��Auto��$��Center=�$
��Left=�$
��Left=�$��2��
�������$��	0,11,0,10q��� -   $
 ByteSentLabel��$��
Bytes Sent��$��30��$
��Auto��$��Center=�$
��Left=�$
��Left=�$��3��
�������$��	0,10,0,11q��� -   $
 BytesReceivedLabel��$��Bytes Received��$��30��$
��Auto��$��Center=�$
��Left=�$
��Left=�$��4��
�������$��0,0,0,0q��� -   $
 DownloadLabel��$��Download��$��30��$
��Auto��$��Center=�$
��Left=�$
��Left=�$��5��
�������$��	0,10,0,11q��� -   $
 UploadLabel��$��Upload��$��30��$
��Auto��$��Center=�$
��Left=�$
��Left=�$��6��
�������$��	0,11,0,10q��� -   $
 IP_AdressLabel��$��
IP Address��$��30��$
��Auto��$��Center=�$
��Left=�$
��Left=�$��7��
�������$��	0,10,0,11q��� -    ��Name$  ComboBox_Network_interface��$��1��$��1��$
��Left=�$��31��$	��0,0q�$��Center=�$	��115���� -	   $
 SpeedAmountLabel��$��0��$��1��$
��Left=�$��27��$
��0,12q�$��2��$��Center=�$
��Auto��
��������� -
   $
 BytesSentAmountLabel��$��0��$��1��$
��Left=�$��27��$
��0,12q�$��3��$��Center=�$
��Auto��
��������� -   $
 BytesReceivedAmountLabel��$��0��$��1��$
��Left=�$��27��$
��0,12q�$��4��$��Center=�$
��Auto��
��������� -   $
 DownloadAmountLabel��$��0��$��1��$
��Left=�$��27��$
��0,12q�$��5��$��Center=�$
��Auto��
��������� -   $
 UploadAmountLabel��$��0��$��1��$
��Left=�$��27��$
��0,12q�$��6��$��Center=�$
��Auto��
��������� -   $
 IP_Address_Of_Computer��$��0��$��1��$
��Left=�$��27��$
��0,12q�$��7��$��Center=�$
��Auto��
��������� -    ��Name$ Open_Window_1��$��8��$��Generate Report��#��� ��
�������$��2��$��0,0,76,0q��� $
��Data��.��� ���	#FF2E8AFF$
��Auto��$
��Auto��.+�&   System.Windows.Controls.DataGrid  -   $��NetworkDataGrid��   AutoGenerateColumns .   AzureDataGrid#��[ $��Center=�$��Center=�   
IsReadOnly . $
��Auto��$
��Auto��$	��250�����	#FF2E8AFF   HorizontalGridLinesBrush
 �   �   VerticalGridLinesBrush
 �   �   AlternatingRowBackground �	#FF2E8AFF   RowBackground �	#FF046BEE   GridLinesVisibility$	 All=�$��Center=�$��18(�
�������$��Center=� � 	Resources *  $System.Windows.Controls.DataGridCell �� 
TargetType �� Property �� Value �� 
TargetType��%V   '        �� "  .���  � �� �� "  .R�� #��z��.+��� $��Center=��� $��About��.��� ���	#FFE5E5E5	 �� $
��Auto ��� $
��Auto �.+��� $��Created by Mantie Reid II�����	#FF2E8AFF���	#CC0050EF
��������� $��1�����	#FF2E8AFF
�������.���� -   $=��7https://github.com/MantieReid/network-bandwidth-monitor?�.�Source Code