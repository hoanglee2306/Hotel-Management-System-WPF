   M S B A M L   `   `   `  ����C  ?hoanglee, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:hoangleehoanglee  5   	   9!clr-namespace:hoanglee.ViewModelshoanglee.ViewModels  5   	       hoanglee.LoginWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:hoanglee     5   	   8
viewmodels!clr-namespace:hoanglee.ViewModels     5   	     � Title$&   FU Mini Hotel Management - Login��5	   	   $	��600��62   $	��800��6?    � WindowStartupLocation$ CenterScreen=�5
   	    � 
ResizeMode$ NoResize=�6.    � 	Resources 5       �� 
TargetType �� Property �� Value��%T  '��       '�� M     'G� �     '�� w    �� 5   
   " ��6   .�5      ��  � ��$ 0,5,0,5q�6'   5      �� 5   
   " ��6   .�5      ��  � ��$ 5q�6(   �� 5       � ��$ 0,5,0,10q�6'   �� 5       � ��$ 30��6'   5      �� 5   
   " G�6   .�5      ��  � ��$ 5q�6(   �� 5       � ��$ 0,5,0,10q�6'   �� 5       � ��$ 30��6'   5      �� 5   
   " ��6   .�5      ��  � ��$
 15,5q�6(   �� 5       � ��$ 5q�6'   �� 5       � ��$ 35��6'   �� 5       � ��$ 80��6)   5      5       .��5"      � .+�5#   
   �� 
������� �� CornerRadius$ 10��6$   $��20q�66    �� Padding$ 20q�6B   	 J� Effect	 5$      3  -System.Windows.Media.Effects.DropShadowEffect  5%      
   
BlurRadius$
 15Q�6#      	Direction$	 -90Q�63      ShadowDepth$ 2Q�6C      Opacity$	 0.2Q�6S   ._�5(      �  � RowDefinitions 5)      �� 5*      $
��Auto �6$   �� 5+      $��* �6$   �� 5,      $
��Auto �6$   .+�50      �� $��0��6   .+�51      �� $��FU Mini Hotel Management��6    $��24(�6@   $
��Bold"�6N   $��Center=�6`   $��0,0,0,10q�6}   �� 52      $��Login to your account��6    $��16(�6=   $��Center=�6K   $��0,0,0,20q�6h   �� 53      54      �� 57      $��1��6   $��30,20q�6*   .+�58      �� $��Email��6    $��Medium"�6-   �� 59      -   5         ��Name$ txtEmail��59      ��60   �� *Email+ �� UpdateSourceTrigger$ PropertyChanged=��� 5;      $��Password��6    $��Medium"�60   G� 5<      -   5         G�Name$ txtPassword��5<   "   67   �� 5>      
���  ��$
��Wrap=�6O   $
��0,10q�6c   ��6    �� *ErrorMessage+6c   5?      �� 5B      $��2��6    �� Orientation$ 
Horizontal=�6*   $��Center=�6C   .+�5C      �� $��Login��6    �� 	IsDefault$
 True��6N   ���#007BFF
�������$��0q�6�   ��6-   �� *LoginCommand+6�   �� 5D      $
��Exit��6    �� IsCancel$
 True��6L   ��6,   �� *ExitCommand+6L   5E      5F      5G      5H      5I      