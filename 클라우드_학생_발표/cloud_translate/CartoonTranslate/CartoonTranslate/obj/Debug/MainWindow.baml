   M S B A M L   `   `   `  ����K  GCartoonTranslate, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:CartoonTranslateCartoonTranslate  5   	   !    CartoonTranslate.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:CartoonTranslate     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   .+�5      �� $��0��6   $��
Input URL:��6%   �� 5      -   5         ��Name$ tb_Url��5      $��1��6&   $	��600��63   $U��Ohttps://szumilabo.com/wp-content/uploads/2017/01/setuzokusi1-e1485025156833.png��5      �� 5      -   5         ��Name$ btn_Show��5      $
��Show��6'   $	��100��6M   �� 5      -   5        $ btn_OCR��5      $	��OCR��6&   $	��100��6J   �� 5      -   5        $ btn_Translate��5      $��	Translate��6,   $	��100��6\   5      �� 5   
   $��1��6   $ 
Horizontal=�6"   .+�5      �� $��Engine:��6   	� 5      -   5         	�Name$ rb_V1��5       	� 	GroupName$ 	gn_Engine��6)   $	��OCR��6?   $
��20,0q�6M    d� 	IsChecked$
 TrueY�6[   6l   	� 5      -   5        $ rb_V2��5      $ 	gn_Engine��6)   $��Recognize Text��6?   6X   �� 5      $��	Language:��6   $
��20,0q�6)   	� 5      -   5        $ 
rb_English��5      $ gn_Language��6.   $��English��6F   	� 5      -   5        $ rb_Japanese��5      $ gn_Language��6/   $��Japanese��6G   $
 TrueY�6Z   $
��20,0q�6k   5      � 5    
   $��3��6    � ColumnDefinitions 5!      �� 5"      $��* �6#   �� 5#      $��40 �6#   �� 5$      $��* �6#   .+�5&      �� $��0��6   $��Source Image��6(   $��Center=�6<   $��Center=�6W   �� 5'      $��2��6   $��Target Image��6(   $��Center=�6<   $��Center=�6W   �� 5(      -	   5        	 ��Name$	 	imgSource��5(      $��0��6'   $
��None=�67   $
��Left=�6F   $	��Top=�6a   �� 5)      -
   5        $	 	imgTarget��5)      $��2��6'   $
��None=�67   $
��Left=�6F   $	��Top=�6a   �� 5*      -   5        
 ��Name$
 canvas_1��5*      $��0��6'   �� 5+      -   5        $
 canvas_2��5+      $��2��6'   5,      5-      5.      