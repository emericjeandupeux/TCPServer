   M S B A M L   `   `   `  ���� D  @TCPServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:TCPServer	TCPServer      TCPServer.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:TCPServer       � Title$  Serveur TCP��$��290.457��$��516.4�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ Manual=�$��0q� J� Opacity$
 0.85Q����	#FF040404 � WindowStyle$
 None=� � AllowsTransparency .  � ShowInTaskbar .   � Topmost . .��� -   $��0,0,0,10q� � ColumnDefinitions �� $
��337* ��� $
��187* �.+��� -   	 ��Name$	 	TbReceive��$
��Left=�$	��210��$��	10,40,0,0q�$
��Wrap=�$	��Top=�$	��497��
����������	#FF37393A
 J� RenderTransformOrigin$
 
0.56,0.475&�$��2���� -    ��Name$ TbSend��$
��Left=�$��20��$��
10,260,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��275�����	#FF37393A
��������� -    ��Name$ button1��$
��Send��$
��Left=�$��290,260,0,0q�$	��Top=�$��52��$��2���� -    ��Name$ CbClient��$
��Left=�$��
55,260,0,0q�$	��Top=�$	��120��$��1���� -   $ 
TbUsername��$
��Left=�$��20��$��	10,10,0,0q�$
 Wrap=�$��Username��$	��Top=�$	��120�����	#FF37393A
�������$��13.333(�	� -    	�Name$ RBtnPoo��$	��Poo��$
��Left=�$��	12,10,0,0q�$	��Top=� 	� 	GroupName$	 IMU��$��1��
�������	� -   $ 	RBtnMouse��$��Mouse��$
��Left=�$��	64,10,0,0q�$	��Top=�$	 IMU��$��1��
�������	� -	   $ 
RBtnNormal��$��Normal��$
��Left=�$��
272,10,0,0q�$	��Top=� d� 	IsChecked$
 TrueY�$	 IMU��
��������� -
   $ button��$�� ��$��1��$
��Left=�$��	155,5,0,0q�$	��Top=�$��20��$��20�����	#FF1D1A1A���	#FFFDFCFC$
 -2.294,1.346&�$��1,5,1,1q�