   M S B A M L   `   `   `  ����C  ?hoanglee, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:hoanglee.Viewshoanglee.Views  5      +    %hoanglee.Views.CustomerManagementView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:hoanglee.Views     5      .��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
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

 Left=�6q    5&   *   �� 5'   .   $1��+Search customers by name, email or phone...��68   5)   '   5*   #    �� Triggers 5+   "   P� 5,   &    P� Property � �� P� Value 6>   Z� . �5-   *   ��  �� Property � �� �� Value# [  6G   5.   '   P� 5/   &    � ��$  ��6>   . �50   *   ��  � ��# [  6G   51   '   P� 52   &    �@J�IsKeyboardFocused$
 True��6K   . �53   *   ��  � ��
 �����6G   54   '   56      .��57   )   58      59      5:      �� 5<      $��1��6   $��Add New Customer��6%   $��10,0,0,0q�6@   $
��15,8q�6R   ���#4CAF50
�������$��0q�5=   =   ��5>      �� *
AddCommand+5?      �� 5B   
   $��1��6   
�������$ 4��61   $ 15,10q�6B   $��0,0,0,10q�6R   ._�5C      ��  �� Orientation$ 
Horizontal=�6   .+�5D      �� $��Total Customers: ��6   $��SemiBold"�65   �� 5E      $��5,0,15,0q�6=   ��6   �� *Customers.Count+6=   �� 5G      $��Active: ��6   $��SemiBold"�6,   �� 5H      $��5,0,15,0q�6B   ��6   �� *ActiveCustomersCount+6B   �� 5J      $��
Inactive: ��6   $��SemiBold"�6.   �� 5K      $	��5,0q�6D   ��6   �� *InactiveCustomersCount+6D   5L      5M      �� 5P   
   $��2��6   
�������$ 4��61   ._�5Q      �   5R      �� 5S      $��* �6$   �� 5T      $
��Auto �6$   .+�5W      &   System.Windows.Controls.DataGrid  $��0��6      AutoGenerateColumns$ False��5X         
IsReadOnly$
 True��67   $��0q�5Z         GridLinesVisibility$ 
Horizontal=�6/      RowHeaderWidth$ 0Q�5[      
������    AlternatingRowBackground �#F9F9F9��5W   (   �� *FilteredCustomers+ �� SelectedItem 5Y      �� *SelectedCustomer+   Columns 5]      0  *System.Windows.Controls.DataGridTextColumn  5^      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6X   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *
CustomerId+6X     5_      $
 Name��6-   $	 200 6`    6;   �� *CustomerFullName+6`     5`      $ Email��6-   $	 200 6]    6<   �� *EmailAddress+6]     5a      $ Phone��6-   $	 120 6Z    6<   �� *	Telephone+6Z     5b      $ Birthday��6-   $	 120 6t    6?   �� *CustomerBirthday+ �� StringFormat$ d��6t   4  .System.Windows.Controls.DataGridTemplateColumn  5c      $ Status��61   $	 100 6A      CellTemplate 5d      �� 5e   "   .R�5f   &   �� $ 4��6-   $	 5,2q�6>   $
��Left=�6L   $��Center=�6g   ��5g   -   �� *CustomerStatus+  StatusToBrushConverter �� 	Converter# [ ._�5h   *   �� 
�������$��12(�5i   G   ��5h   4   �� *CustomerStatus+  StatusToTextConverter# [ 5i   G   5j   '   5k   #   5m        5n      $ Actions��61   $ * 6B    5o      �� 5p   "   .R�5q   &   �� $ 
Horizontal=�61   .+�5r   *   �� $
��Edit��61   $	��5,0q�6@   $
��10,5q�6M   ��5s   1   �� *DataContext.EditCommand+ �� RelativeSource ��   �� AncestorType  DataGrid��5t   1   �� �� 5u   *   $��Delete��61   $	��5,0q�6B   $
��10,5q�6O   ��5v   1   �� *DataContext.DeleteCommand+ ��   DataGrid��5w   1   �� 5x   '   5y   #   5{      5}      �� 5�      $��1��6   $ 
Horizontal=�6*   $��Right=�6C   $��10q�6_   .+�5�      �� $��Previous��6   $��80��6X   $	��5,0q�6c   ��60   �� *PreviousPageCommand+6c   �� 5�      $��Center=�6=   $
��10,0q�6X   ��6    �� *CurrentPage+6X   �� 5�      $��of��6    $��Center=�6*   $	��5,0q�6E   �� 5�      $��Center=�6<   $
��10,0q�6W   ��6    �� *
TotalPages+6W   �� 5�      $
��Next��6   $��80��6P   $	��5,0q�6[   ��6,   �� *NextPageCommand+6[   5�      5�      5�      5�      5�      