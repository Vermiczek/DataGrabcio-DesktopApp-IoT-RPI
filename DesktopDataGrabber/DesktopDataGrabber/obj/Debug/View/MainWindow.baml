   M S B A M L   `   `   `  ����M  IDesktopDataGrabber, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:DesktopDataGrabcio.ViewModelDesktopDataGrabcio.ViewModel  5   	   (    "DesktopDataGrabcio.View.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   R NOxyPlot.Wpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf  5   	   3vm*clr-namespace:DesktopDataGrabcio.ViewModel  5   	     � Title$"  DataGrabcio: Windows Edition��5	   	   $	��950��6.   $
��1400��6;    � DataContext 5
      0   *DesktopDataGrabcio.ViewModel.MainViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ MenuBtn��5      $��0��5      $��1��6   $��70��5      $
��Left=�6   $
��15,5q�63   .��6A   Menu5      � 5    
   -   5         �Name$
 Menu��5!      $��0��5       $��0��6   $��4��6,   
����� �$	��150��5"   %    5#      �� 5$      $
��Auto �6    �� 5%      $��50 �6    �� 5&      $
��Auto �6    �� 5'      $��50 �6    �� 5(      $
��Auto �6    �� 5)      $��50 �6    �� 5*      $
��Auto �6    .+�5,      �� -   5        $��0��5,      $	��100��6"   $��5q�6.   {�. .��6]   Charts6e   �� 5.      -   5        $��2��5.      $	��100��6"   $��5q�6.   {�. .��6]   LED6b   �� 50      -   5        $��4��50      $	��100��6"   $��5q�6.   {�. .��6\   	TableView6g   52      �� 54   
   $
��Auto=�6   $��1��6:   $��1��6G   .��55      �  56      �� 57      $	��400 �6$   �� 58      $	��400 �6$   �� 59      $	��400 �6$   �� 5:      $	��400 �6$   �� 5;      $	��400 �6$   .+�5=        OxyPlot.Wpf.PlotView  -   5          Name$ DataPlotViewTemp��5=      $��0��5>      $��1��6$   $��0,0,10,0q�5?      $y�	Collapsed=�5@         Model 5A      �� *	ChartTemp+  5B      -   5        $ DataPlotViewPressure��5B      $��1��5C      $��1��6$   $��0,0,10,0q�5D      $y�	Collapsed=�5E       5F      �� *
ChartPress+  5G      -   5        $ DataPlotViewHumid��5G      $��2��5H      $��1��6$   $��0,0,10,0q�5I      $y�	Collapsed=�5J       5K      �� *
ChartHumid+  5L      -	   5        $ DataPlotViewRPY��5L      $��3��5M      $��1��6$   $��0,0,10,0q�5N      $y�	Collapsed=�5O       5P      �� *
ChartRPY+  5Q      -
   5        $ DataPlotViewJOY��5Q      $��4��5R      $��1��6$   $��0,0,10,0q�5S      $y�	Collapsed=�5T       5U      �� *
ChartJOY+5V      5W      � 5Y   
   $��2��6   $��1��6    5Z      �� 5[      $
��Auto �6#   �� 5\      $
��Auto �6#   �� 5]      $
��Auto �6#   �� 5^      $
��Auto �6#   �� 5_      $
��Auto �6#   �� 5`      $
��Auto �6#   �� 5a      $
��Auto �6#   .+�5c      �� $��0��5d      $��
20,10,0,10q�5e      $��IP address: ��5f      $��Center=�6%   �� 5g      $��1��5h      $��	0,10,0,10q�5i      $	��100��5j      $
��Left=�5k      ��5l      �� *	IpAddress+�� 5m      $��2��5n      $��	0,10,0,10q�5o      $��	Samples: ��5p      $��Right=�5q      $��Center=�6-   �� 5r      $��3��5s      $��	0,10,0,10q�5t      $��50��5u      $
��Left=�5v      ��5w      �� *MaxSampleNumber+�� 5x      $��4��5y      $��	0,10,0,10q�5z      $��Sample time: ��5{      $��Right=�5|      $��Center=�6-   �� 5}      $��5��5~      $��	0,10,0,10q�5      $��50��5�      $
��Left=�5�      ��5�      �� *
SampleTime+�� 5�      $��6��5�      $��
0,10,20,10q�5�      $	�� ms��5�      $��Right=�5�      $��Center=�6-   5�      � 5�   
   $��3��6   $��1��6    5�      �� 5�      $��* �6#   �� 5�      $
��Auto �6#   �� 5�      $
��Auto �6#   �� 5�      $��* �6#   .+�5�      �� $��0��5�      $��30��5�      $	��150��5�      $��
0,10,30,10q�5�      $��Right=�5�   1   ��6   �� *StartButton+.��6M   START5�      �� 5�      $��1��5�      $��30��5�      $	��150��5�      $��
0,10,15,10q�5�      ��5�      �� *
StopButton+.��60   STOP5�      �� 5�      $��2��5�      $��30��5�      $	��150��5�      $��
15,10,0,10q�5�      $
��Left=�5�   8   ��6   �� *UpdateConfigButton+.��6S   UPDATE CONFIG5�      �� 5�      $��3��5�      $��30��5�      $	��150��5�      $��
30,10,0,10q�5�      $
��Left=�5�   9   ��6   �� *DefaultConfigButton+.��6T   DEFAULT CONFIG5�      5�      �� 5�   
   -   5        $��	LedMatrix��5�   F   $y�	Collapsed=�6   $��1��6)   $��1��69   $��Stretch=�6W    �� SelectionMode$ Extended=�6|   .@�5�      � $��0, 20, 0, 0q�6    5�      �� 5�      $��27 �6$   �� 5�      $��26 �6$   �� 5�      $
��Auto �6$    5�      �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   .+�5�      �� $��0��6   $��0��6,   $��COLOUR:��69   $��32(�6H   $
��Bold"�6V   $��30,10,100,0q�6h   $��2��6}   �� 5�      $��0��6   $��2��6,   $	��R: ��69   $��24(�6D   $
��Bold"�6R   $��30,10,100,0q�6d   �� 5�      $��2��6   $��24(�6:   $��70,10,100,0q�6H   $��60��6]   ��6'   �� *R+6]   �� 5�      $��2��6   $��24��6)   
���  ��$��30��6@   $��-250,10,-358,0q�6K   �� 5�      $��2��6   $	��G: ��6)   $��24(�64   $
��Bold"�6B   $��254,10,-119,0q�6T   $��6��6k   �� 5�      $��2��6   $��24(�6:   $��30,10,-130,0q�6H   $��5��6^   $��60��6n   ��6'   �� *G+6n   �� 5�      $��2��6   $��24��6*   
��� � �$��30��6C   $��-60,10,-358,0q�6N   $��3��6e   �� 5�      $��5��6   $��2��6,   $	��B: ��69   $��24(�6D   $
��Bold"�6R   $��215,10,-358,-10q�6d   �� 5�      $��2��6   $��24(�6:   $��258,10,-358,0q�6H   $��5��6_   $��60��6o   ��6'   �� *B+6o   �� 5�      -   5        $��Gradient��5�   )   $��2��6   $��24��6:   $��Right=�6F   $��30��6b   
����  �	 J� RenderTransformOrigin$	 -1.656,1.028&�6y   $��390,10,-358,0q�6�   $��4��6�   5�      � 5�      $��0, 30, 0, 0q�6    5�      �� 5�      $��27 �6$   �� 5�      $��26 �6$   �� 5�      $
��Auto �6$    5�      �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   .+�5�      �� $��0��6   $��0��6,   $��LED ID:��69   $��32(�6H   $
��Bold"�6V   $��30,10,100,0q�6h   $��2��6}   �� 5�      $��0��6   $��2��6,   $	��X: ��69   $��24(�6D   $
��Bold"�6R   $��30,10,100,0q�6d   �� 5�      $��2��6   $��24(�6:   $��70,10,100,0q�6H   ��6'   �� *X+6H   �� 5�      $��2��6   $	��Y: ��6)   $��24(�64   $
��Bold"�6B   $��254,10,-119,0q�6T   $��6��6k   �� 5�      $��2��6   $��24(�6:   $��30,10,-130,0q�6H   $��5��6^   ��6'   �� *Y+6^   �� 5�      $
��SEND��65   $
��Left=�6D   $��258,10,-363,0q�6_   $	��112��6v   $��1��6�   $��2��6�   ��6   �� *	SendLed+6�   5�      � 5�      $��0, 30, 0, 0q�6    5�      �� 5�      $��27 �6$   �� 5�      $��26 �6$   �� 5�      $
��Auto �6$    5�      �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   .+�5�      �� $��CLEAR��66   $
��Left=�6F   $��30,10,100,0q�6a   $	��112��6v   $��1��6�   $��2��6�   ��6   �� *
ClearLed+6�   5�      5�      �� 5�   
   -   5        $��
Data_table��5�   X   $y�	Collapsed=�6   $��1��6*   $��1��6:   $��3��6G   $��Stretch=�6j   $ Extended=�6�   .@�5�      �  5�      �� 5�      $��* �6$   �� 5�      $
��Auto �6$   .+�5�      �� $��0��6   ��6(   �� *	DataTable+
 �� View
 5�      �� 5�      .P�5�      �� $
��Name��6-    �� Width$	 100��6;    �� DisplayMemberBinding 6G   �� *Name+�� 5�      $
��Data��6-   $	 100��6;    6G   �� *Value+�� 5�      $
��Unit��6-   $	 100��6;    6G   �� *Unit+5�      5�      5     5     5     5     