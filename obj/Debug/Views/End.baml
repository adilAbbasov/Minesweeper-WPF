   M S B A M L   `   `   `  ����I  EMineSweeperWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:MineSweeperWPF.ViewsMineSweeperWPF.Views  5   	       MineSweeperWPF.Views.End   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:MineSweeperWPF.Views     5   	     � Title$	  End��5   	   $	��300��6   $	��400��6"    � WindowStartupLocation$ CenterScreen=�6.    � WindowStyle$
 None=�6S    � 
ResizeMode$ NoResize=�6f   .��5	      � .+�5
   
   � ���#4AC0FD � RowDefinitions 5      �� 5      $
��0.6* �6    60   �� 5      $
��0.4* �6    60   .+�5      � $��0��6    � ColumnDefinitions 5      �� 5      $��2* �6'   64   �� 5      $��* �6'   63   �� 5      $��* �6'   63   �� 5      $��2* �6'   64   �� 5      $��* �6'   63   .+�5      �� -   5        $��timer��5      $��Right=�6%   $��Center=�6A   $��	0,0,10,40q�6\   $��\Images\result_timer.png��6o   $��70��6�   $��70��6�   �� 5      -   5        $��timer_result��5   p   $��1��6   $
��Left=�6(   $��Center=�6C   $��0,0,0,30q�6^   $��20(�6�   
�������$
��Bold"�6�   $	��---��6�   �� 5      -   5        $��
restartBtn��5   )   $��2��6   $��Center=�6;   $��Bottom=�6X    &  "MaterialDesignFloatingActionButton#��[  6s   ���#FAC600$��50��6�   $��50��6�   $��0,0,0,5q�6�   ���#FAC600.��5      �� -   5        $��restart_btn��5      $��\Images\restart.png��6/   $��30��6L   $��30��6X   5      �� 5       -   5        $��trophy��5    r   $��3��6   $��Right=�6(   $��Center=�6D   $��	0,0,10,40q�6_   $��\Images\trophy.png��6�   $��70��6�   $��70��6�   �� 5!      -   5        $��trophy_result��5!   p   $��4��6   $
��Left=�6(   $��Center=�6C   $��0,0,0,30q�6^   $��20(�6�   $
��Bold"�6�   
�������$	��---��6�   5"      �� 5$      -   5        $��
background��5$   !   $��1��6   $��\Images\background.jpg��63   $
��Fill=�6S   5%      5&      5'      