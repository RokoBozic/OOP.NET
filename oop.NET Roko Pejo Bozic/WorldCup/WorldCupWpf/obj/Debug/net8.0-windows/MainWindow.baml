   M S B A M L   `   `   `  ����F  BWorldCupWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:WorldCupWpfAppWorldCupWpfApp  5   	       WorldCupWpfApp.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:WorldCupWpfApp     5   	   -   5          � Title$  World Cup App��5   	   $	��450��6   $	��800��6,   .��5      � -   5         �Name$ MainGrid��5      .+�5   
   � $��20q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      ��  �� Orientation$ Vertical=�6   $��0��60   $��0,0,0,10q�6=   .+�5      �� -   5         ��Name$ SelectTeamLabel��5      $��Select Your Team:��65   $
��Bold"�6N   �� 5      -   5         ��Name$ TeamComboBox��5      61   5      �� 5      $ Vertical=�6   $��1��60   $��0,0,0,10q�6=   .+�5      �� -   5        $ SelectOpponentLabel��5      $��Select Opponent:��69   $
��Bold"�6Q   �� 5      -   5        $ OpponentComboBox��5      65   5      �� 5      -   5        $ 
ResultText��5   %   $��2��6   $��16(�69   $
��Bold"�6G   $��	0,10,0,10q�6Y   �� 5!      $ 
Horizontal=�6   $��3��62   $��Center=�6?   $	��Top=�6\   $��0,20,0,0q�6t   .+�5"      �� -   5         ��Name$ HomeInfoButton��5"      $��Your Team Info��61   $��10q�6g    �� 	IsDefault$ False��6s   $|�False��6�   �� 5#      -	   5        $ AwayInfoButton��5#      $��Opponent Info��61   $��10q�6f   $ False��6r   $|�False��6�   5$      �� 5%      -
   5        $ ShowLineupButton��5%      $��View Starting XI��6/   $��Center=�6J   $��0,83,0,0q�6g   $��3��6y   $	��Top=�6�   $��31��6�   $	��179��6�   4  .System.Windows.Automation.AutomationProperties   Name$ 
ShowLineup��6�   $ False��6�   $|�False��6  5&      � 5(   
   -   5        $ LoadingOverlay��5(      ���	#80000000$y�	Collapsed=�6>   	 K� ZIndex$		 100��6U   .+�5)      �� $��Center=�6   $��Center=�66   .+�5*      � 
 � IsIndeterminate$

 True��6   $	��120��65   $��20��6A   �� 5+      -   5        $ LoadingText��5+      $��
Loading...��61   
�������$��Center=�6V   $��0,10,0,0q�6s   5,      5-      �� 5/   
   -   5        $ BackToSettingsButton��5/      $��Settings��6/   $��80��6B   $��28��6M   $��Right=�6Y   $	��Top=�6u   $��0,5,5,0q�6�   $ False��6�   $|�False��6�   50      51      