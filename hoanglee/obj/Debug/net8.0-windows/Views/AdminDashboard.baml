   M S B A M L   `   `   `  ����C  ?hoanglee, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:hoanglee.Viewshoanglee.Views  5   	   9!clr-namespace:hoanglee.ViewModelshoanglee.ViewModels  5   	   #    hoanglee.Views.AdminDashboard   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:hoanglee.Views     5   	   8
viewmodels!clr-namespace:hoanglee.ViewModels     5   	     � Title$0  *FU Mini Hotel Management - Admin Dashboard��5	   	   $	��700��5
   	   $
��1200��6    � WindowStartupLocation$ CenterScreen=�5   	    � 	Resources 5         	NavButton �� 
TargetType �� Property �� Value �� Triggers P� Property P� Value��%�  &	        �� 5   
   " ��6"   .�5      ��  � ��$ 50��6'   �� 5       � ��$ 5q�6'   �� 5       � ��$ 10q�6(   �� 5       � ��
 ���� 6+   �� 5       � ��$ 0q�60   �� 5       � ��$
 Left=�6;   �� 5       � ��$ 14(�6)    5      P� 5       �@J�IsMouseOver$
 True��61   . �5      ��  � �� �#E0E0E063   5      5      5      .��5      � 	 � ColumnDefinitions	 5   
   �� 5       $	��220 �6   �� 5!      $��* �6   .+�5%   
   �� ���#2E3A47$��0��6&   ._�5&      ]� .+�5'      �� $	��Top=�6   $��	0,20,0,30q�62   .+�5(      �� $��FU Mini Hotel��6    
�������$��22(�6H   $
��Bold"�6V   $��Center=�6h   �� 5)      $��Admin Dashboard��6    ���#AAAAAA$��14(�6L   $��Center=�6Z   5*      �� 5,      $��Bottom=�6   $��10q�65   .+�5-      �� $��Logout��6   ���#FF5252
�������$��40��5/      $��0q�6)   ��5-   .   �� *LogoutCommand+5/   )   50      �� 52      .+�53      �� $��Customer Management��6   #��[  6;   
�������$��	Customers��54   A   ��6   �� *NavigateCommand+6A   �� 56      $��Room Management��6   #��[  67   
�������$��Rooms��57   A   ��6   �� *NavigateCommand+6A   �� 59      $��Booking Management��6   #��[  6:   
�������$��Bookings��5:   A   ��6   �� *NavigateCommand+6A   �� 5<      $��Reports��6   #��[  6/   
�������$��Reports��5=   A   ��6   �� *NavigateCommand+6A   5>      5?      5@      �� 5C   
   $��1��6   ���#F5F5F5._�5D      � 
 � RowDefinitions
 5E      �� 5F      $��60 �6$   �� 5G      $��* �6$   .+�5K      �� $��0��6   
�������$��0,0,0,1q�69   ���#E0E0E0._�5L      � 	 5M      �� 5N      $��* �6/   �� 5O      $
��Auto �6/   .+�5R      �� $��0��6$   $��18(�5S   $   $��SemiBold"�62   $��Center=�5T   $   $��20,0,0,0q�6?   ��5R   4   �� *CurrentViewTitle+5T   ?   �� 5V      $��1��6%    �� Orientation$ 
Horizontal=�65   $��0,0,20,0q�6N   .+�5W      �� $��Medium"�5X   (   $��Center=�6<   $��0,0,10,0q�6W   ��5W   (   �� *CurrentUser.CustomerFullName+5X   W   �� 5Y      $��40��6%   $��40��60    �� CornerRadius$ 20��6<   ���#E0E0E0$
��10,0q�6c   ._�5Z   "   �� $��Center=�5[   ,   $��Center=�6I   $
��Bold"�5\   ,   ��5Z   ,   �� *CurrentUserInitials+5\   ,   5]      5^      5_      5`      �� 5c      $��1��6!   $��20q�6N   ��6.   �� *CurrentView+6N   5d      5e      5f      5g      