   M S B A M L   `   `   `  ����C  ?hoanglee, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:hoanglee.Viewshoanglee.Views  5      (    "hoanglee.Views.CustomerProfileView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:hoanglee.Views     5      .��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   
������� �� CornerRadius$ 8��61    �� Padding$ 20q�6B   $��0,0,0,20q�6O   ._�5      �  � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   .+�5      �� $��0��6   $��80��6)   $��80��64   $ 40��6@   ���#E1F5FE$��0,0,20,0q�6g   ._�5      �� $
��👤��6    $��40(�6*   $��Center=�68   $��Center=�6U   5      �� 5      $��1��6   .+�5       �� $��24(�6K   $��SemiBold"�6Y   ��6    �� *Customer.CustomerFullName+6Y   �� 5!      $��16(�6G   ���#757575$��0,5,0,0q�6j   ��6    �� *Customer.EmailAddress+6j   �� 5"      $��14(�6D   ���#757575$��0,5,0,0q�6g   ��6    �� *Customer.Telephone+6g   5#      �� 5&      $��2��6   $��Edit Profile��6)   $
��15,8q�5(      ���#2196F3
�������$��0q�6P   ��5&   @   �� *EditCommand+y�5'      �� *
IsEditMode+ '  #InverseBooleanToVisibilityConverter �� 	Converter# [  5(   P   5)      5*      �� 5-   
   $��1��6   
�������$ 8��61   $ 20q�6B   ._�5.      �   5/      �� 50      $��* �6$   �� 51      $
��Auto �6$   .+�55      �� $��0��6   $
��Auto=�6,   .��56      �� .+�57      �� $��Personal Information��6$   $��18(�6@   $��SemiBold"�6N   $��0,0,0,15q�6d   �� 5:      $��	Full Name��6$   $��Medium"�65   $	��0,5q�6I   �� 5;      $
��10,8q�5>   "   $��0,0,0,15q�61   ��5;   "   �� *Customer.CustomerFullName+ �� UpdateSourceTrigger$ PropertyChanged=� �� 
IsReadOnly 5<   "   �� *
IsEditMode+  InverseBooleanConverter# [ ��5=   "   �� *
IsEditMode+ ! EditModeToBackgroundConverter# [ 5>   1   �� 5A      $��Email Address��6$   $��Medium"�69   $	��0,5q�6M   �� 5B      $
 True��6I   ���#F5F5F5$
��10,8q�5C   7   $��0,0,0,15q�6F   ��5B   "   �� *Customer.EmailAddress+5C   F   �� 5F      $��Phone Number��6$   $��Medium"�68   $	��0,5q�6L   �� 5G      $
��10,8q�5J   "   $��0,0,0,15q�61   ��5G   "   �� *Customer.Telephone+$ PropertyChanged=� 5H   "   �� *
IsEditMode+# [ ��5I   "   �� *
IsEditMode+# [ 5J   1   �� 5M      $��Birthday��6$   $��Medium"�64   $	��0,5q�6H   (  "System.Windows.Controls.DatePicker  5N      $
��10,8q�5P   %   $��0,0,0,15q�64      SelectedDate 5N   %   �� *Customer.CustomerBirthday+  DateOnlyToDateTimeConverter# [ {�5O   %   �� *
IsEditMode+5P   4   9� 5S      $��Change Password��6#   $��0,15,0,0q�6<   .��5T      � $��0,10,0,0q�6#     5U   "   �� 5V   &   $
��Auto �64   �� 5W   &   $
��Auto �64   �� 5X   &   $
��Auto �64   �� 5Y   &   $
��Auto �64   .+�5]   "   �� $��0��6-   .+�5^   &   �� $��Current Password��60   $��Medium"�6H   $	��0,5q�6\   G� 5_   &   -   5         G�Name$ CurrentPasswordBox��5_   2   $
��10,8q�6N   $��0,0,0,10q�6]   5`   2   5a   #   �� 5d   "   $��1��6-   .+�5e   &   �� $��New Password��60   $��Medium"�6D   $	��0,5q�6X   G� 5f   &   -   5        $ NewPasswordBox��5f   2   $
��10,8q�6J   $��0,0,0,10q�6Y   5g   2   5h   #   �� 5k   "   $��2��6-   .+�5l   &   �� $��Confirm Password��60   $��Medium"�6H   $	��0,5q�6\   G� 5m   &   -   5        $ ConfirmPasswordBox��5m   2   $
��10,8q�6N   $��0,0,0,10q�6]   5n   2   5o   #   �� 5r   "   $��3��6)   $��Change Password��66   $
��Left=�5t   )   $
��15,8q�6D   ���#FF9800
�������$��0q�5u   Q   ��5s   )   �� *ChangePasswordCommand+5u   Q   5v      5w      5x      5y      �� 5|      $��1��6   	 �� Orientation$	 
Horizontal=�6*   $��Right=�6C   $��0,15,0,0q�6_   y�5}      �� *
IsEditMode+   BooleanToVisibilityConverter# [ .+�5~      �� $��Cancel��6   $
��15,8q�5      $��0,0,10,0q�6,   ��5~   .   �� *CancelCommand+5   ,   �� 5�      $��Save Changes��6   $
��15,8q�5�      ���#4CAF50
�������$��0q�6T   ��5�   4   �� *SaveCommand+5�   T   5�      5�      5�      5�      5�      