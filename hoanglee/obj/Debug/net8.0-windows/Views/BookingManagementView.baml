   M S B A M L   `   `   `  ����C  ?hoanglee, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:hoanglee.Viewshoanglee.Views  5      *    $hoanglee.Views.BookingManagementView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:hoanglee.Views     5      .��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6   $��0,0,0,10q�6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��Auto �6#   .+�5      �� $��0��6   
������� �� CornerRadius$ 4��68    �� Padding$ 5q�6I   ._�5      �  5      �� 5      $
��Auto �6+   �� 5      $��* �6+   .+�5      �� $��0��6    $
��🔍��60   $��16(�6:   $��Center=�6H   $	��5,0q�6c   �� 5      $��1��6   $��0q�5       $��5q�62   $��Center=�5!      ��5   .   �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=�.��5!   9   ��5"      �� 5#       �� 
TargetType" ��6$    �� 	Resources 5$   "      	HintBrush j� TileMode �� Opacity	 j� Stretch
 j� 
AlignmentX !� Visual��%�   &	        !� 5%   &   $
 None=�6D   $	 0.5Q�6T   $
	 None=�6b   $

 Left=�6q    5&   *   �� 5'   .   $6��0Search bookings by ID, customer name or notes...��68   5)   '   5*   #    �� Triggers 5+   "   P� 5,   &    P� Property � �� P� Value 6>   Z� . �5-   *   ��  �� Property � �� �� Value# [  6G   5.   '   P� 5/   &    � ��$  ��6>   . �50   *   ��  � ��# [  6G   51   '   P� 52   &    �@J�IsKeyboardFocused$
 True��6K   . �53   *   ��  � ��
 �����6G   54   '   56      .��57   )   58      59      5:      �� 5<      $��1��6   $��Add New Booking��6%   $��10,0,0,0q�6?   $
��15,8q�6Q   ���#4CAF50
�������$��0q�5=   =   ��5>      �� *AddBookingCommand+5?      �� 5B   
   $��1��6   
�������$ 4��61   ._�5C      &   System.Windows.Controls.DataGrid     AutoGenerateColumns$ False��5D         
IsReadOnly$
 True��63   $��0q�5F         GridLinesVisibility$ 
Horizontal=�6+      RowHeaderWidth$ 0Q�5G      
������    AlternatingRowBackground �#F9F9F9��5C      �� *
Bookings+ �� SelectedItem 5E      �� *SelectedBooking+   Columns 5I      0  *System.Windows.Controls.DataGridTextColumn  5J      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 60 6^   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *BookingReservationId+6^     5K      $ Customer��6)   $	 150 6i    6;   �� *Customer.CustomerFullName+6i     5L      $ Booking Date��6)   $	 100 6o    6?   �� *BookingDate+ �� StringFormat$ d��6o     5M      $ Check-in��6)   $	 100 6k    6;   �� *CheckinDate+$ d��6k     5N      $ 	Check-out��6)   $	 100 6m    6<   �� *CheckoutDate+$ d��6m     5O      $ Total Price��6)   $	 100 6n    6>   �� *
TotalPrice+$ C2��6n   4  .System.Windows.Controls.DataGridTemplateColumn  5P      $ Status��6-   $	 100 6=      CellTemplate 5Q      �� 5R      .R�5S   "   �� $ 4��6)   $	 5,2q�6:   $
��Left=�6H   $��Center=�6c   ��5T   )   �� *BookingStatus+ ! BookingStatusToBrushConverter �� 	Converter# [ ._�5U   &   �� 
�������$��12(�5V   C   ��5U   0   �� *BookingStatus+   BookingStatusToTextConverter# [ 5V   C   5W   #   5X      5Z        5[      $ Actions��6-   $ * 6>    5\      �� 5]      .R�5^   "   ��  �� Orientation$ 
Horizontal=�6-   .+�5_   &   �� $
��Edit��6-   $	��5,0q�6<   $
��10,5q�6I   ��5`   -   �� * DataContext.EditBookingCommand+ �� RelativeSource ��   �� AncestorType  DataGrid��5a   -   �� �� 5b   &   $��Delete��6-   $	��5,0q�6>   $
��10,5q�6K   ��5c   -   �� *" DataContext.DeleteBookingCommand+ ��   DataGrid��5d   -   �� �� 5e   &   $��Details��6-   $	��5,0q�6?   $
��10,5q�6L   ��5f   -   �� * DataContext.ViewDetailsCommand+ ��   DataGrid��5g   -   �� 5h   #   5i      5k      5m      5n      9� 5q   
   $��2��6   $��Booking Details��6    $��0,10,0,0q�5r      
�������$��1q�68   ���#E0E0E0! 9� 
IsExpanded! 5q   9   �� *SelectedBooking+  NotNullToBooleanConverter# [ .��5s        $ False��5t      $
 True��63   $��0q�5u      $ 
Horizontal=�6+   $ 0Q�5v      
������  �#F9F9F9$	��200��5x      ��5s      �� *BookingDetails+ 5y        5z      $ Room Number��6)   $	 120 6b    6>   �� *Room.RoomNumber+6b     5{      $ 	Room Type��6)   $	 150 6k    6<   �� *Room.RoomType.RoomTypeName+6k     5|      $ 
Start Date��6)   $	 100 6k    6=   �� *	StartDate+$ d��6k     5}      $ End Date��6)   $	 100 6g    6;   �� *	EndDate+$ d��6g     5~      $ 
Room Price��6)   $	 100 6l    6=   �� *	RoomPrice+$ C2��6l     5      $ Actual Price��6)   $	 100 6p    6?   �� *ActualPrice+$ C2��6p     5�      $
 Days��6-   $ * 6;    5�      �� 5�      .R�5�   "   �� ��5�   &   `� 5�   *     DateRangeConverter" `� 	Converter#" [ 67   .1�5�   .   �� # �� Path$# 	StartDate�66   �� 5�   .   $# EndDate�66   5�   +   5�   #   5�      5�      5�      5�      �� 5�   
   $��3��6   ���	#80000000y�69   �� *IsDialogOpen+   BooleanToVisibilityConverter# [ ._�5�      �� 
�������$ 8��6(   $	��800��69   $	��700��6H   $ 20q�6X   ._�5�      �   5�      �� 5�      $
��Auto �6(   �� 5�      $��* �6(   �� 5�      $
��Auto �6(   .+�5�      �� $��0��6    $��18(�5�       $
��Bold"�6.   $��0,0,0,15q�6@   ��5�   -   �� *$"DialogBooking.BookingReservationId+  BookingIdToTitleConverter# [ 5�   @   �� 5�      $��1��6#   $
��Auto=�60   .��5�      �  5�      �� 5�   "   $��* �63   �� 5�   "   $��* �63   .+�5�      �� $��0��6)   $��0,0,10,0q�69   .+�5�   "   �� $��Customer��6,   $	��0,5q�6<   �� 5�   "   $ �� SelectedValuePath$$ 
CustomerId��5�   +   % �� DisplayMemberPath$% CustomerFullName��5�   +   $��5q�5�   +   $��0,0,0,10q�67   ��5�   +   �� *	Customers+& �� SelectedValue& 5�   +   �� *DialogBooking.CustomerId+5�   7   �� 5�   "   $��Check-in Date��6,   $	��0,5q�6A   (  "System.Windows.Controls.DatePicker  5�   "   $��5q�5�   ,   $��0,0,0,10q�68   '   SelectedDate' 5�   -   �� *DialogBooking.CheckinDate+  DateOnlyToDateTimeConverter# [ 5�   8   �� 5�   "   $��Check-out Date��6,   $	��0,5q�6B     5�   "   $��5q�5�   ,   $��0,0,0,10q�68   ' 5�   -   �� *DialogBooking.CheckoutDate+# [ 5�   8   �� 5�   "   $��Status��6,   $	��0,5q�6:   �� 5�   "   $��5q�6a   $��0,0,0,10q�6m   ( �� SelectedIndex( 6+   �� *DialogBooking.BookingStatus+.@�5�   &   �� $��	Cancelled��63   �� 5�   &   $��Active��63   �� 5�   &   $��	Completed��63   5�   #   �� 5�   "   $��Notes��6,   $	��0,5q�69   �� 5�   "   $��5q�6O   $��0,0,0,10q�6[   ) �� TextWrapping$
) Wrap=�5�   *   * �� AcceptsReturn$
* True��6>   $��60��6S   ��5�   *   �� *DialogBooking.Notes+5�   S   �� 5�   "   $��Total Price��6,   $	��0,5q�6?   �� 5�   "   + �� 
IsReadOnly$
+ True��5�   *   ���#F0F0F0$��5q�6Q   $��0,0,0,10q�6]   ��5�   *   �� *DialogBooking.TotalPrice+$ C2��5�   ]   5�      � 5�      $��1��6#   $��10,0,0,0q�63     5�   "   �� 5�   &   $
��Auto �64   �� 5�   &   $��* �64   �� 5�   &   $
��Auto �64   .+�5�   "   �� $��0��6-   .+�5�   &   �� $��Available Rooms��60   $��SemiBold"�6G   $	��0,5q�6]   �� 5�   &   $	��150��6U   $��0,5,0,10q�6b   ��6.   �� *AvailableRooms+��5�   *   �� 5�   .   .R�5�   2   �  5�   6   �� 5�   :   $��* �6K   �� 5�   :   $
��Auto �6K   .+�5�   6   �� $��0��6A   .+�5�   :   �� $��SemiBold"�6`   ��6D   �� *
RoomNumber+6`   �� 5�   :   �� 5�   >   �6B   �� *RoomType.RoomTypeName+ �� 5�   >   $	� - ��6B    �� 5�   >   �6B   �� *RoomPricePerDate+$ C2�� �� 5�   >   $
�/day��6B   5�   ;   5�   7   �� 5�   6   $��1��6=   $	��Add��6M   $��10,0,0,0q�6[   $
��10,2q�6m   ��5�   =   �� *%#DataContext.AddRoomToBookingCommand+ ��   ListBox��5�   =   �� 5�   3   5�   /   5�   '   5�   #   �� 5�   "   $��1��6-   .+�5�   &   �� $��Selected Rooms��60   $��SemiBold"�6F   $	��0,5q�6\   �� 5�   &   $	��200��6U   $��0,5,0,10q�6b   ��6.   �� *BookingDetails+��5�   *   �� 5�   .   .R�5�   2   �  5�   6   �� 5�   :   $��* �6K   �� 5�   :   $
��Auto �6K   .+�5�   6   �� $��0��6A   .+�5�   :   �� $��SemiBold"�6e   ��6D   �� *Room.RoomNumber+6e   �� 5�   :   �� 5�   >   �6B   �� *	StartDate+$ d�� �� 5�   >   $
� to ��6B    �� 5�   >   �6B   �� *	EndDate+$ d��5�   ;   �� 5   :   $��SemiBold"�6r   ��6D   �� *ActualPrice+$ C2��6r   5  7   �� 5  6   $��1��6=   $��Remove��6M   $��10,0,0,0q�6^   $
��10,2q�6p   ��5  =   �� **(DataContext.RemoveRoomFromBookingCommand+ ��   ListBox��5  =   �� 5  3   5  /   5  '   5	  #   5
     5     5     �� 5     $��2��6!   $ 
Horizontal=�6.   $��Right=�6G   $��0,15,0,0q�6c   .+�5     �� $��Cancel��6!   $
��15,8q�6Z   $	��5,0q�6i   ��62   �� *CancelDialogCommand+6i   �� 5     $
��Save��6!   $
��15,8q�6W   $	��5,0q�6f   ���#4CAF50
���������60   �� *SaveBookingCommand+5  6   5     5     5     5     5     5     