   M S B A M L   `   `   `  ����D  @TCPServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:TCPServer	TCPServer  5   	       TCPServer.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:TCPServer     5   	     � Title$  Serveur TCP��5   	   $��290.457��6   $��516.4��6.    � 
ResizeMode$ NoResize=�6<    � WindowStartupLocation$ Manual=�6R   $��0q�6q    J� Opacity$
 0.85Q�6�   ���	#FF040404 � WindowStyle$
 None=�6�    � AllowsTransparency .  � ShowInTaskbar .   � Topmost . .��5	      � -   5        $��0,0,0,10q�5	       � ColumnDefinitions 5
   
   �� 5      $
��337* �6   �� 5      $
��187* �6   .+�5   
   �� -   5        	 ��Name$	 	TbReceive��5      $
��Left=�6'   $	��210��6B   $��	10,40,0,0q�6O   $
��Wrap=�6b   $	��Top=�6v   $	��497��6�   
����������	#FF37393A
 J� RenderTransformOrigin$
 
0.56,0.475&�6�   $��2��6�   �� 5   
   -   5         ��Name$ TbSend��5      $
��Left=�6"   $��20��6=   $��
10,260,0,0q�6I    �� TextWrapping$
 Wrap=�6]   $	��Top=�6q   $	��275��6�   ���	#FF37393A
�������6�   �� 5   
   -   5         ��Name$ button1��5      $
��Send��6"   $
��Left=�61   $��290,260,0,0q�6L   $	��Top=�6a   $��52��6y   $��2��6�   �� 5   
   -   5         ��Name$ CbClient��5      $
��Left=�6%   $��
55,260,0,0q�6@   $	��Top=�6T   $	��120��6l   $��1��6x   �� 5   
   -   5        $ 
TbUsername��5      $
��Left=�6&   $��20��6A   $��	10,10,0,0q�6M   $
 Wrap=�6`   $��Username��6t   $	��Top=�6�   $	��120��6�   ���	#FF37393A
�������$��13.333(�6�   	� 5   
   -   5         	�Name$ 
RBtnSmiley��5      $��Smiley��6*   $
��Left=�6;   $��	12,10,0,0q�6V   $	��Top=�6i    	� 	GroupName$	 IMU��6�   $��1��6�   
�������6�   	� 5   
   -   5        $ 	RBtnMouse��5      $��Mouse��6)   $
��Left=�69   $��	77,10,0,0q�6T   $	��Top=�6g   $	 IMU��6   $��1��6�   
�������6�   	� 5   
   -	   5        $ 
RBtnNormal��5      $��Normal��6*   $
��Left=�6;   $��
272,10,0,0q�6V   $	��Top=�6j    d� 	IsChecked$
 TrueY�6�   $	 IMU��6�   
�������6�   �� 5   
   -
   5        $ button��5      $�� ��6!   $��1��6,   $
��Left=�6<   $��	155,5,0,0q�6W   $	��Top=�6j   $��20��6�   $��20��6�   ���	#FF1D1A1A���	#FFFDFCFC$
 -2.294,1.346&�6�   $��1,5,1,1q�6�   6  5      5      