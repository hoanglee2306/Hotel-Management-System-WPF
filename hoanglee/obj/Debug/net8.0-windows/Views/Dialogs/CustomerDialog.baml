   M S B A M L   `   `   `  ����C  ?hoanglee, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:hoanglee.Views.Dialogshoanglee.Views.Dialogs  5   	   +    %hoanglee.Views.Dialogs.CustomerDialog   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:hoanglee.Views.Dialogs     5   	   $	��500��5	   	   $	��500��6     � WindowStartupLocation$  CenterOwner=�5
   	    � 
ResizeMode$ NoResize=�5   	    � Title 5   	   �� *DialogTitle+ � 	Resources 5       �� 
TargetType �� Property �� Value(  "System.Windows.Controls.DatePicker��%k  '��       '�� u     'G� �     '  W    '�� �    �� 5   
   " ��6   .�5      ��  � ��$ 0,5,0,2q�6'   �� 5       � ��$ Medium"�6+   5      �� 5   
   " ��6   .�5      ��  � ��$ 8q�6(   �� 5       � ��$ 0,2,0,10q�6'   5      �� 5   
   " G�6   .�5      ��  � ��$ 8q�6(   �� 5       � ��$ 0,2,0,10q�6'   5      �� 5   
   "  6   .�5      ��  � ��$ 8q�6(   �� 5       � ��$ 0,2,0,10q�6'   5      �� 5   
   " ��6   .�5      ��  � ��$ 8q�6(   �� 5        � ��$ 0,2,0,10q�6'   5!      5"      .��5$      �� 
������� �� Padding$ 20q�6    ._�5%   
   �  � RowDefinitions 5&      �� 5'      $
��Auto �6    �� 5(      $��* �6    �� 5)      $
��Auto �6    .+�5-      �� $��0��6   $��20(�5.      $
��Bold"�6&   $��0,0,0,15q�68   ��5-   %   �� *DialogTitle+5.   8   �� 51      $��1��6   $
��Auto=�6(   .��52      �� .+�53      �� $��Full Name *��6    �� 54      ��6   �� *Customer.CustomerFullName+	 �� UpdateSourceTrigger$	 PropertyChanged=��� 55      
���  ��$��0,-8,0,5q�6L   ��6    �� *	NameError+y�56       �� *	NameError+   StringToVisibilityConverter
 �� 	Converter#
 [  �� 58      $��Email Address *��6    �� 59      ��6   �� *Customer.EmailAddress+$	 PropertyChanged=��� 5:      
���  ��$��0,-8,0,5q�6M   ��6    �� *
EmailError+y�5;       �� *
EmailError+#
 [  �� 5=      $��
Password *��6    y�62   �� *IsNewCustomer+   BooleanToVisibilityConverter#
 [ G� 5>      -   5         G�Name$ PasswordBox��5>   "   $��0,2,0,10q�67   $��8q�6I   y�5@   "   �� *IsNewCustomer+#
 [ �� 5A      
���  ��$��0,-8,0,5q�6P   ��6    �� *PasswordError+y�5B       �� *PasswordError+#
 [  �� 5D      $��Phone Number��6    �� 5E      ��6   �� *Customer.Telephone+$	 PropertyChanged=��� 5F      
���  ��$��0,-8,0,5q�6M   ��6    �� *
PhoneError+y�5G       �� *
PhoneError+#
 [  �� 5I      $��Birthday��6      5J         SelectedDate 6!   �� *BirthdayDate+ �� Mode$ TwoWay=��� 5L      $��Status��6    �� 5M       �� SelectedIndex 6   �� *StatusIndex+.@�5N      �� $��Active��6'   �� 5O      $��Inactive��6'   5P      �� 5R      $��	User Type��6    �� 5S       6   �� *UserTypeIndex+.@�5T      �� $��Customer��6'   �� 5U      $��Admin��6'   5V      5W      5X      �� 5[      $��2��6    �� Orientation$ 
Horizontal=�6&   $��Right=�6?   $��0,15,0,0q�6[   .+�5\      �� $
��Save��6   $	��100��6(   $��35��64   $��0,0,10,0q�6@    �� 	IsDefault$
 True��5]   9   ���#4CAF50
�������$��0q�5^   A   ��5]      �� *SaveCommand+5^   A   �� 5_      $��Cancel��6   $	��100��6*   $��35��66    �� IsCancel$
 True��5`   ;   ��6   �� *CancelCommand+6;   5a      5b      5c      5d      