   M S B A M L   `   `   `  ����I  EMineSweeperWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:MineSweeperWPF.ViewsMineSweeperWPF.Views  5   	   $    MineSweeperWPF.Views.GameModes   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:MineSweeperWPF.Views     5   	     � Title$  	GameModes��5   	   $	��420��6   $	��450��6(    � 
ResizeMode$ NoResize=�64    � WindowStyle$
 None=�6J    � WindowStartupLocation$ CenterScreen=�6]   .��5	      � -   5        $��	main_grid��5	       � RowDefinitions 5
   
   �� 5      -   5         �Name$ GridRow1��5      $��* �6,   69   �� 5      -   5        $ GridRow2��5      $��6* �6,   6:   .+�5   
   � $��0��6   ���#4A752C � ColumnDefinitions 5      �� 5      $��3* �6#   60   �� 5      $
��1.5* �6#   62   �� 5      $
��1.5* �6#   62   �� 5      $
��1.5* �6#   62   �� 5      $
��1.5* �6#   62   �� 5      $��* �6#   6/   �� 5      $��* �6#   6/   �� 5      $��* �6#   6/   .+�5      �� -   5        $��Modes��5   )    �� SelectedIndex$ 0��6    "  MaterialDesignOutlinedComboBox#��[  5      $��Center=�5      $
��Left=�5       
�������$��14(�5"      $��SemiBold"�5#      $��10q�5$      $	��100��5%      $��10,0,0,0q�5&      {�5      ��  �� Path$ 	IsChecked�	 �� ElementName$%	 OutlinedComboBoxEnabledCheckBox����5      �� *
GameLevels+5'      �� 5)      -   5        $��red_flag��5)   $   $��1��6   $��Right=�64   $��\Images\red_flag.png��6P   $��40��6n   $��50��6z   �� 5+      -   5        $��
flag_count��5+   $   $��2��6   $��Center=�66   $
��Left=�6Q   
�������$��16(�6   $��5,0,0,0q�6�   ��6�   �� *	FlagCount+
 �� UpdateSourceTrigger$
 PropertyChanged=�6�   �� 5-      -   5        $��timer��5-   $   $��3��6   $��Right=�61   $��\Images\timer.png��6M   $��40��6h   $��50��6t   �� 5/      -   5        $��timer_seconds��5/   $   $��4��6   $��Center=�69   $
��Left=�6T   
�������$��16(�6�   $��5,0,0,0q�6�   ��6�   �� *TimerSeconds+$
 PropertyChanged=�6�   �� 51      -	   5        $��speaker_btn��51   %   $��5��6   $��Right=�68     
MenuButton#��[ 6T   $��20��6x   $��30��6�   .��52      �� $��\Images\sound.png��6   53      �� 55      -
   5        $��share��55   $   $��6��6   $��\Images\share.png��61   $��20��6L   $��30��6X   �� 57      -   5        $��	close_btn��57   %   $��7��6   $
��Left=�66   #��[ 6Q   $��20��6u   $��30��6�   .��58      �� $��\Images\close.png��6   59      5:      � 5<   
   -   5        $��Buttons��5<      $��1��6   ���#AAD7516B   5=      5>      