   M S B A M L   `   `   `  ����D  @EAD_ASS_5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:EAD_ASS_5	EAD_ASS_5  5   	   9!clr-namespace:EAD_ASS_5.ConvertorEAD_ASS_5.Convertor  5   	   "    EAD_ASS_5.OrderDetailsWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:EAD_ASS_5     5   	   7	convertor!clr-namespace:EAD_ASS_5.Convertor     5   	   -   5          � Title$  Order Details Window��5	   	   $	��700��6&    � 
ResizeMode$ NoResize=�62    � SizeToContent$ Height=�6H    � 	Resources 5
      #  EAD_ASS_5.Convertor.BoolToInt   	BoolToInt��%   &	          5   
   62   5      .��5      �� $
��Auto=�6   .��5   
   � -   5        $
��grid��5      $��20q�6    � RowDefinitions 5   
   �� 5      $	��120 �6   6+   �� 5      $��2000* �6   6-   �� 5      $��70 �6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   .+�5   
   �  � Orientation$ 
Horizontal=�6   $��50��6-   $��0��69   $��0��6F   .+�5      �� $%��Images/com.aadhk.retail.pos.png��5      $��20 0 0 0q�5      �� 5      $��POINT OF SALE��5      $��Center=�5      ���	#FFE8443B$
��Bold"�5!      $��25(�5"      $��HelveticaNeue)�6#   $��20 0 0 0q�5#      5&      �� 5'   
   $��1��6   $��0 20 0 0q�6"   $��2��64   .+�5(      �� $��16(�6   Order Number: �� 65   �69   ��  �� Path$ order_id�6Z   6`   �� 5*      -   5        $
��list��5+   ?   ��6   �� $ 
orderItems� �� View 5,      �� 5-      .P�5.      �� $��Item Id��6)   	 �� Width$	 50��6c   
 �� DisplayMemberBinding
 6:   �� $ id�6c   �� 5/      $��	Item Name��6)   $		 150��6g   
 6<   �� $
 name�6g   �� 50      $��Price��6)   $	 50��6d   
 68   �� $ price�6d   �� 51      $��Quantity��6)   $	 60��6j   
 6;   �� $ quantity�6j   �� 52      $��Total��6)   $	 60��6i   
 68   �� $ 
totalPrice�6i   �� 53      $	��Tax��6)   $	 30��6�   
 66   �� $	 tax� �� 	Converter# [  6�   �� 54      $��Comments��6)   $		 210��6j   
 6;   �� $ comments�6j   55      57      58      �� 59   
   $��0��6   $��1��6"   $��0 10 0 0q�62   .+�5:      �� $��11(�6   @>*3% Tax will be applied on items having price greater than 1006f   �� 5;      $��12(�6   $	��0 2q�6&   �� 64   .�69   .�6E   Sub Total: �� $	��200��6O   
������ 6[   6w   �� 6}   �6�   �� $ subTotal�6�   6�   �� 5<      $��12(�6   $	��0 2q�6&   �� 64   .�69   .�6D   
Discount: �� $	��203��6N   
������ 6Z   6v   �� 6|   �6�   �� $ discountPercentage� �� StringFormat$
 {0}%��6�   6�   �� 5=      $��12(�6   $	��0 2q�6&   �� 64   .�69   .�6?   Tax: �� $	��234��6I   
������ 6U   6q   �� 6w   �6{   �� $	 Tax�6�   6�   �� 5>      $	��0 2q�6   
���   �6:   �� 5?      $��12(�6   $	��0 2q�6&   �� 64   .�69   .�6A   	Total: �� $	��225��6K   
������ 6W   6s   �� 6y   �6}   �� $ 
grandTotal�6�   6�   �� 5@      
���   �6-   �� 5A      $��Center=�6   $��12(�65   $��0 10 0 0q�6C   Thanks for Shopping6j   �� 5B      $��Center=�6   $��12(�65   Developed By: Aizaz Ahmad6^   5C      �� 5D   
   $��2��6   $��2��6#   $��0 10 0 0q�67   .+�5E      �� $��Center=�6   $��16(�65   Thanks for Shopping6X   �� 5F      $��Center=�6   $��16(�65   $��0 5 0 0q�6C   Developed By: Aizaz Ahmad6o   5G      5H      5I      5J      