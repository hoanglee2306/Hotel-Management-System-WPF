   M S B A M L   `   `   `  ����C  ?hoanglee, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:hoanglee.Viewshoanglee.Views  5          hoanglee.Views.ReportView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:hoanglee.Views     5      .��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   
������� �� CornerRadius$ 4��61    �� Padding$ 15q�6B   $��0,0,0,10q�6O   ._�5      �  � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   .+�5      �� $��0��6   $��Report Type:��6,   $��Center=�6@   $��0,0,10,0q�6[   �� 5      $��1��6    �� SelectedValuePath$ Content��6P   $	��150��6l   $��0,0,15,0q�6x    �� SelectedValue 6+   �� *
ReportType+.@�5      �� $��Bookings��6#   �� 5      $��	Occupancy��6#   �� 5       $��Revenue��6#   5!      �� 5#      $��2��6   $��From:��6,   $��Center=�69   $��0,0,5,0q�6T   (  "System.Windows.Controls.DatePicker  5$      $��3��6   $	��120��6�   $��0,0,15,0q�6�      SelectedDate 6-   �� *	StartDate+   DateOnlyToDateTimeConverter �� 	Converter# [  6�   �� 5&      $��4��6   $	��To:��6,   $��Center=�67   $��0,0,5,0q�6R     5'      $��5��6   $	��120��6�   $��0,0,15,0q�6�    6-   �� *	EndDate+# [  6�   �� 5)      $��6��6   $��Generate Report��6)   ���#2196F3
�������$
��15,5q�5*   A   ��5)   C   �� *GenerateReportCommand+5*   A   5+      5,      �� 5/   
   $��1��6   
�������$ 4��61   $ 15q�6B   ._�50      � .+�52      �� ���	#80FFFFFFy�60   �� *	IsLoading+   BooleanToVisibilityConverter# [ ._�53      �� $��Center=�6!   $��Center=�6<   .+�54      �� $��Loading report...��6$   $��16(�6=   $��Center=�6K   � 55       � IsIndeterminate$
 True��6&   $	��200��6=   $��10��6I   $��0,10,0,0q�6U   56      57      �� 5:      .@�5<      �� $��Booking Statistics��6   	 �� 
IsSelected	 6:   �� *
ReportType+  StringEqualsConverter# [ 
 �� ConverterParameter$
 Bookings��.��5=      �   5>      �� 5?   "   $
��Auto �60   �� 5@   "   $��* �60   .+�5C      �� $��0��6(   $��Booking Statistics Report��65   $��18(�6V   $��SemiBold"�6d   $��0,0,0,10q�6z   &   System.Windows.Controls.DataGrid  5E      $��1��6'      AutoGenerateColumns$ False��5F   '      
IsReadOnly$
 True��6C   $��0q�5G   '      GridLinesVisibility$ 
Horizontal=�6;      RowHeaderWidth$ 0Q�5H   '   
������    AlternatingRowBackground �#F9F9F9��5E   4   �� *BookingStatistics+   Columns 5J   "   0  *System.Windows.Controls.DataGridTextColumn  5K   &   ,  &System.Windows.Controls.DataGridColumn   Header$ ID��69      Width5  /System.Windows.Controls.DataGridLengthConverter$ 60 6n   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6E   �� *BookingReservationId+6n     5L   &   $ Customer��69   $	 150 6y    6K   �� *Customer.CustomerFullName+6y     5M   &   $ Booking Date��69   $	 100 6    6O   �� *BookingDate+ �� StringFormat$ d��6     5N   &   $ Check-in��69   $	 100 6{    6K   �� *CheckinDate+$ d��6{     5O   &   $ 	Check-out��69   $	 100 6}    6L   �� *CheckoutDate+$ d��6}     5P   &   $ Total Price��69   $	 100 6~    6N   �� *
TotalPrice+$ C2��6~   4  .System.Windows.Controls.DataGridTemplateColumn  5Q   &   $ Status��6=   $ * 6M      CellTemplate 5R   *   �� 5S   .   .R�5T   2   �� $ 4��69   $	 5,2q�6J   $
��Left=�6X   $��Center=�6s   ��5U   9   �� *BookingStatus+ ! BookingStatusToBrushConverter# [ ._�5V   6   �� 
�������$��12(�5W   S   ��5V   @   �� *BookingStatus+   BookingStatusToTextConverter# [ 5W   S   5X   3   5Y   /   5[   '   5]      5^      5_      �� 5b      $��Room Occupancy��6   	 66   �� *
ReportType+# [ $
 	Occupancy��.��5c      �   5d      �� 5e   "   $
��Auto �60   �� 5f   "   $��* �60   .+�5i      �� $��0��6(   $��Room Occupancy Report��65   $��18(�6R   $��SemiBold"�6`   $��0,0,0,10q�6v     5k      $��1��6'   $ False��5l   '   $
 True��6C   $��0q�5m   '   $ 
Horizontal=�6;   $ 0Q�5n   '   
������  �#F9F9F9��5k   4   �� *RoomOccupancyReports+ 5p   "     5q   &   $ Room Number��69   $	 120 6m    6N   �� *
RoomNumber+6m     5r   &   $ 	Room Type��69   $	 150 6i    6L   �� *
RoomType+6i     5s   &   $ Total Days Booked��69   $	 120 6y    6T   �� *TotalBookingDays+6y     5t   &   $ Occupancy %��6=   $	 150 6R    5u   *   �� 5v   .   .R�5w   2   �  5x   6   �� 5y   :   $
��Auto �6K   �� 5z   :   $��* �6K   .+�5|   6   �� $��0��6@   $��Center=�5}   @   $��0,0,5,0q�6[   ��5|   P   �� *OccupancyPercentage+$
 {0}%��5}   [   � 5~   6   $��1��6B    � Maximum$	 100Q�5   B   $��10��6P    � Value 5~   R   �� *OccupancyPercentage+��5�   B   �� *OccupancyPercentage+  OccupancyToColorConverter# [ 5�   3   5�   /   5�   '     5�   &   $ Total Revenue��69   $ * 6�    6P   �� *TotalRevenue+$ C2��6�   5�      5�      5�      �� 5�      $��Revenue��6   	 6/   �� *
ReportType+# [ $
 Revenue��.��5�      �   5�      �� 5�   "   $
��Auto �60   �� 5�   "   $
��Auto �60   �� 5�   "   $��* �60   .+�5�      �� $��0��6(   $��Revenue Report��65   $��18(�6K   $��SemiBold"�6Y   $��0,0,0,10q�6o   �� 5�      $��1��6%   ���#F5F5F5$ 4��6G   $ 15q�6X   $��0,0,0,15q�6e   ._�5�   "   �  5�   &   �� 5�   *   $��* �6;   �� 5�   *   $��* �6;   �� 5�   *   $��* �6;   .+�5�   &   �� $��0��61   $��Center=�6A   .+�5�   *   �� $��Total Revenue��64   $��SemiBold"�6I   $��Center=�6_   �� 5�   *   $��24(�5�   4   $
��Bold"�6B   $��Center=�6T   ��5�   4   �� *RevenueReport.TotalRevenue+$ C2��5�   T   5�   '   �� 5�   &   $��1��61   $��Center=�6A   .+�5�   *   �� $��Total Bookings��64   $��SemiBold"�6J   $��Center=�6`   �� 5�   *   $��24(�5�   4   $
��Bold"�6B   $��Center=�6T   ��5�   4   �� *RevenueReport.TotalBookings+5�   T   5�   '   �� 5�   &   $��2��61   $��Center=�6A   .+�5�   *   �� $��Average Booking Value��64   $��SemiBold"�6Q   $��Center=�6g   �� 5�   *   $��24(�5�   4   $
��Bold"�6B   $��Center=�6T   ��5�   4   �� *#!RevenueReport.AverageBookingValue+$ C2��5�   T   5�   '   5�   #   5�        5�      $��2��6'   $ False��5�   '   $
 True��6C   $��0q�5�   '   $ 
Horizontal=�6;   $ 0Q�5�   '   
������  �#F9F9F9��5�   4   �� *RevenueReport.DailyRevenues+ 5�   "     5�   &   $
 Date��69   $	 120 6p    6G   �� *Date+$ d��6p     5�   &   $ Revenue��69   $	 150 6w    6J   �� *	Revenue+$ C2��6w     5�   &   $ Bookings Count��69   $	 120 6s    6Q   �� *BookingsCount+6s     5�   &   $ Revenue Distribution��6=   $ * 6[    5�   *   �� 5�   .   .R�5�   2   � $��10��5�   >   ���#2196F3 5�   >   �� *	Revenue+ 5�   >   �� *)'DataContext.RevenueReport.DailyRevenues+ �� RelativeSource ��  �� AncestorType DataGrid  MaxRevenueConverter# [ 5�   J   5�   /   5�   '   5�      5�      5�      5�      5�      5�      5�      5�      