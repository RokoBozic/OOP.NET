   M S B A M L   `   `   `  ����  WorldCupWpf.    (WorldCupWpfApp.Views.PlayerDetailsWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Player Details��5   	   $	��450��6    $	��600��6-    � WindowStartupLocation$ CenterScreen=�5   	    J� Opacity$ 0Q�5   	   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $��* �6    � ColumnDefinitions 5   
   �� 5      �� 5      $	��255 �6   .+�5   
   �� -   5         ��Name$ 
PlayerName��5      $��0��6(   $��0��65   $��24(�6E   $
��Bold"�6S   $��0,0,0,20q�6e   �� 5   
   $��1��6   $��0��6"   $��0,0,20,0q�62   .+�5      �� -   5        $ ShirtNumber��5      $��16(�6-   $��0,0,0,10q�6;   �� 5      -   5        $ Position��5      $��16(�6*   $��0,0,0,10q�68   �� 5      -   5        $ 	IsCaptain��5      $��16(�6+   $��0,0,0,10q�69   �� 5      -   5        $ Goals��5      $��16(�6'   $��0,0,0,10q�65   �� 5      -   5        $ YellowCards��5      $��16(�6-   $��0,0,0,10q�6;   �� 5      -   5        $��Close��5      $
��Left=�6%   $��0,20,0,0q�6T   5      �� 5   
   $��1��6   $��1��6   
�������$��1q�6A    �� Padding$ 5q�6U   ._�5       �� -	   5         ��Name$ PlayerImage��5       $��UniformToFill=�6)   $��Stretch=�6A   $��Stretch=�6]   5!      5"      5#      