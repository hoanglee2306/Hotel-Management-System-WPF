   M S B A M L   `   `   `  ����C  ?hoanglee, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:hoanglee.Viewshoanglee.Views  5      '    !hoanglee.Views.RoomManagementView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:hoanglee.Views     5      .��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   � $��0��6   $��0,0,0,10q�6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��Auto �6#   .+�5      �� $��0��6   
������� �� CornerRadius$ 4��68    �� Padding$ 5q�6I   ._�5      �  5      �� 5      $
��Auto �6+   �� 5      $��* �6+   .+�5      �� $��0��6    $
��🔍��60   $��16(�6:   $��Center=�6H   $	��5,0q�6c   �� 5      $��1��6   $��0q�5      $��5q�62   $��Center=�5       ��5   .   �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=�.��5    9   ��5!      �� 5"       �� 
TargetType" ��6$    �� 	Resources 5#   "      	HintBrush j� TileMode �� Opacity	 j� Stretch
 j� 
AlignmentX !� Visual��%�   &	        !� 5$   &   $
 None=�6D   $	 0.5Q�6T   $
	 None=�6b   $

 Left=�6q    5%   *   �� 5&   .   $4��.Search rooms by number, description or type...��68   5(   '   5)   #    �� Triggers 5*   "   P� 5+   &    P� Property � �� P� Value 6>   Z� . �5,   *   ��  �� Property � �� �� Value# [  6G   5-   '   P� 5.   &    � ��$  ��6>   . �5/   *   ��  � ��# [  6G   50   '   P� 51   &    �@J�IsKeyboardFocused$
 True��6K   . �52   *   ��  � ��
 �����6G   53   '   55      .��56   )   57      58      59      �� 5;      $��1��6   $��Add New Room��6%   $��10,0,0,0q�6<   $
��15,8q�6N   ���#4CAF50
�������$��0q�5<   =   ��5=      �� *AddRoomCommand+5>      �� 5A   
   $��1��6   
�������$ 4��61   ._�5B      &   System.Windows.Controls.DataGrid     AutoGenerateColumns$ False��5C         
IsReadOnly$
 True��63   $��0q�5E         GridLinesVisibility$ 
Horizontal=�6+      RowHeaderWidth$ 0Q�5F      
������    AlternatingRowBackground �#F9F9F9��5B      �� *Rooms+ �� SelectedItem 5D      �� *SelectedRoom+   Columns 5H      0  *System.Windows.Controls.DataGridTextColumn  5I      ,  &System.Windows.Controls.DataGridColumn   Header$ Room Number��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$	 120 6]   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   �� *
RoomNumber+6]     5J      $
 Type��6)   $	 150 6a    67   �� *RoomType.RoomTypeName+6a     5K      $ Description��6)   $	 200 6b    6>   �� *RoomDescription+6b     5L      $ Capacity��6)   $ 80 6_    6;   �� *RoomMaxCapacity+6_     5M      $ 	Price/Day��6)   $	 100 6r    6<   �� *RoomPricePerDate+ �� StringFormat$ C2��6r   4  .System.Windows.Controls.DataGridTemplateColumn  5N      $ Status��6-   $	 100 6=      CellTemplate 5O      �� 5P      .R�5Q   "   �� $ 4��6)   $	 5,2q�6:   $
��Left=�6H   $��Center=�6c   ��5R   )   �� *
RoomStatus+  RoomStatusToBrushConverter �� 	Converter# [ ._�5S   &   �� 
�������$��12(�5T   C   ��5S   0   �� *
RoomStatus+  RoomStatusToTextConverter# [ 5T   C   5U   #   5V      5X        5Y      $ Actions��6-   $ * 6>    5Z      �� 5[      .R�5\   "   ��  �� Orientation$ 
Horizontal=�6-   .+�5]   &   �� $
��Edit��6-   $	��5,0q�6<   $
��10,5q�6I   ��5^   -   �� *DataContext.EditRoomCommand+ �� RelativeSource ��   �� AncestorType  DataGrid��5_   -   �� �� 5`   &   $��Delete��6-   $	��5,0q�6>   $
��10,5q�6K   ��5a   -   �� *DataContext.DeleteRoomCommand+ ��   DataGrid��5b   -   �� 5c   #   5d      5f      5h      5i      �� 5l   
   $��2��6   ���	#80000000y�69   �� *IsDialogOpen+   BooleanToVisibilityConverter# [ ._�5m      �� 
�������$ 8��6(   $	��500��69   $	��600��6H   $ 20q�6X   ._�5n      �   5o      �� 5p      $
��Auto �6(   �� 5q      $��* �6(   �� 5r      $
��Auto �6(   .+�5v      �� $��0��6    $��18(�5w       $
��Bold"�6.   $��0,0,0,15q�6@   ��5v   -   �� *DialogRoom.RoomId+  RoomIdToTitleConverter# [ 5w   @   �� 5z      $��1��6#   $
��Auto=�60   .��5{      �� .+�5|      �� $��Room Number��6(   $	��0,5q�6;   �� 5}      $��5q�6r   $��0,0,0,10q�6~   ��6&   �� *DialogRoom.RoomNumber+$ PropertyChanged=�6~   �� 5      $��	Room Type��6(   $	��0,5q�69   �� 5�      ! �� SelectedValuePath$! 
RoomTypeId��5�   '   " �� DisplayMemberPath$" RoomTypeName��5�   '   $��5q�5�   '   $��0,0,0,10q�63   ��5�   '   �� *	RoomTypes+# �� SelectedValue# 5�   '   �� *DialogRoom.RoomTypeId+5�   3   �� 5�      $��Description��6(   $	��0,5q�6;   �� 5�      $��5q�6R   $��0,0,0,10q�6^   $ �� TextWrapping$
$ Wrap=�5�   &   % �� AcceptsReturn$
% True��6:   $��60��6O   ��5�   &   �� *DialogRoom.RoomDescription+5�   O   �� 5�      $��Maximum Capacity��6(   $	��0,5q�6@   �� 5�      $��5q�6w   $��0,0,0,10q�6�   ��6&   �� *DialogRoom.RoomMaxCapacity+$ PropertyChanged=�6�   �� 5�      $��Price Per Day��6(   $	��0,5q�6=   �� 5�      $��5q�6x   $��0,0,0,10q�6�   ��6&   �� *DialogRoom.RoomPricePerDate+$ PropertyChanged=�6�   �� 5�      $��Status��6(   $	��0,5q�66   �� 5�      $��5q�6W   $��0,0,0,10q�6c   & �� SelectedIndex& 6'   �� *DialogRoom.RoomStatus+.@�5�   "   �� $��Inactive��6/   �� 5�   "   $��Active��6/   5�      5�      5�      �� 5�      $��2��6!   $ 
Horizontal=�6.   $��Right=�6G   $��0,15,0,0q�6c   .+�5�      �� $��Cancel��6!   $
��15,8q�6Z   $	��5,0q�6i   ��62   �� *CancelDialogCommand+6i   �� 5�      $
��Save��6!   $
��15,8q�6T   $	��5,0q�6c   ���#4CAF50
���������60   �� *SaveRoomCommand+5�   6   5�      5�      5�      5�      5�      5�      