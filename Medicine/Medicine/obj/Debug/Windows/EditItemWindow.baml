   M S B A M L   `   `   `  ����C  ?Medicine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Medicine.WindowsMedicine.Windows  5   	   %    Medicine.Windows.EditItemWindow   5      j fDevExpress.Xpf.LayoutControl.v20.1, Version=20.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol  a ]DevExpress.Xpf.Core.v20.1, Version=20.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  6\   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Medicine.Windows     5   	   ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5   	   a ]DevExpress.Xpf.Grid.v20.1, Version=20.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af bDevExpress.Xpf.Grid.v20.1.Core, Version=20.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   6K   -   5        $	��450��5	   	   $	��750��6     � WindowStartupLocation$  CenterScreen=�6?   .��5
      �  � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   0  *DevExpress.Xpf.LayoutControl.LayoutControl  $��0��6   $��2��6*   .+�5      .  (DevExpress.Xpf.LayoutControl.LayoutGroup     Orientation$ Vertical=�6   .+�5      -  'DevExpress.Xpf.LayoutControl.LayoutItem     Label$ Наименование��6"      Content. 5      %  DevExpress.Xpf.Editors.TextEdit  -   5          Name$ teName��5   >   5        5      $ Тип параметра��6"   . 5      )  #DevExpress.Xpf.Editors.ComboBoxEdit  -   5          Name$ ceType��5   '   +  %DevExpress.Xpf.Editors.LookUpEditBase   DisplayMember$
 Name��67   	   ValueMember$	 Id��6L   6]   5        5      -   5        
  Name$
 lgCheckList��5   #   .+�5!        -   5        $
 lgSelectMode��5!   '   .+�5"        $% Заполните список��6*   . 5#        -   5        $ ceGroup��5#   /   $	 Id��6@   $
 Name��6Q   6f   5$        5%      $��30��6*   . 5&      &   DevExpress.Xpf.Core.SimpleButton  -   5          Name$ 
sbAddGroup��5&   .   $��22��6B      Glyph 6g   *  $DevExpress.Xpf.Core.DXImageExtension  *" Images/Actions/AddFile_16x16.png+5'        5(      $��30��6*   . 5)        -   5        $ sbEditGroup��5)   @   {�.  $��22��6U    6a     *Images/Edit/Edit_16x16.png+6�   5*        5+      $��30��6*   . 5,        -	   5        $ sbDeleteGroup��5,   @   {�.  $��22��6W    6c     *%#Images/Actions/DeleteList_16x16.png+6�   5-      5.        50      -
   5        $
 
lgEditMode��50   '   $y�	Collapsed=�6;   .+�51        $ Группа��6*   . 52        -   5        $ teGroup��52   +   6<   53        54      $��30��6*   . 55        -   5        $ sbGroupSave��55   .   $��22��6C    6i     *Images/Save/Save_16x16.png+56        57      $��30��6*   . 58        -   5        $ sbGroupCancel��58   .   $��22��6E    6m     *Images/XAF/Action_Cancel.png+59      5:      5;        5=      -   5          Name$ liMeasureUnit��5=   8   $ Ед. измерения��6"   . 5>        -   5        $ teMeasureUnit��5>   E   6\   5?      5B      5C      �� 5E   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��5q�6;   .+�5F      �� $-��'Задать для всех групп��6   )	  #DevExpress.Xpf.Editors.ToggleSwitch	  5G      -   5         	 Name$ tsForAll��5G      $
��Left=�62   6M   5H      )
  #DevExpress.Xpf.Grid.TreeListControl
  5J   
   -   5         
 Name$ 
tlcTargets��5J      $��2��63   $��0��6@   $	��5,0q�6P   )  #DevExpress.Xpf.Grid.DataControlBase   SelectionMode$
 Cell=�6�    
  View 5K      &   DevExpress.Xpf.Grid.TreeListView  5L      -   5          Name$ 
tlvTargets��5L   �      CheckBoxFieldName$ 	IsChecked��6$   !   IsCheckBoxEnabledFieldName$ 	IsEnabled��6B      ShowCheckboxes .    AutoExpandAllNodes .    NewItemRowPosition$
 None=�6�      ParentFieldName$ ParentId��6�      KeyFieldName$ Id��6�      	AutoWidth . &   DevExpress.Xpf.Grid.DataViewBase   AllowEditing .     FormatConditions 5M      )  #DevExpress.Xpf.Grid.FormatCondition  5N      -  'DevExpress.Xpf.Grid.FormatConditionBase   
Expression$ [IsChecked] = 'True'��6.      	FieldName$
 Name��6P       
ApplyToRow  . 1  +DevExpress.Xpf.Grid.ExpressionConditionBase!   Format.! 5O      6  0DevExpress.Xpf.Core.ConditionalFormatting.Format  "   
Background" �	#FFC4D5FF#   
Foreground
# �   �$   TextDecorations$ 6R   Z� 5P      5S      % 
  Columns.% 5U      (  "DevExpress.Xpf.Grid.TreeListColumn  $  DevExpress.Xpf.Grid.ColumnBase&   	FieldName$
& Name��6!   $  DevExpress.Xpf.Grid.BaseColumn'   Header)  #DevExpress.Xpf.Core.ObjectConverter$!' Целевая группа 62     5V      $& Description��6!   $' Примечание 69     5W      $& 	IsChecked��6!   (   Visible( .  67   5X      � 5Z   
   $��1��6   $��1��6   $��2��6,    5[      �� 5\      $
��auto �6    60   �� 5]      6"   �� 5^      6"   �� 5_      $
��auto �6    60   .+�5b      �� -   5        ) ��Name$) lblNotSelected��5b      
�������$2��,Выберите целевую группу��6D   $��4��6f   $��Center=�6w   $��Center=�6�   $��24(�6�   6�   �� 5c      -   5        $) 	lblHeader��5c      $8��2Задайте граничные значения��6'   $��18(�6L   $��Center=�6Z   6y   �� 5e      -   5        * ��Name$* spNumberNormal��5e   >   
������$��1��61   .+�5f      �� $$��В пределах нормы��6   $��16(�63   $��Center=�6A   6`     5g      .+�5h        $ Мин.��6&   . 5i      %  DevExpress.Xpf.Editors.SpinEdit  -   5        +  Name$+ seMinNormal��5i   '   5j        5k      $ 	Макс.��6&   . 5l        -   5        $+ seMaxNormal��5l   '   5m        5n      -   5        $ liNormalMeasureUnit��5n   <   $ ед. измерения��6&   5o      5p      5q      �� 5s      -   5        $* spNumberWarning��5s   ?   
�������$��2��62   .+�5t      �� $0��*В пределах допустимого��6   $��16(�69   $��Center=�6G   6f     5u      .+�5v        $ Мин.��6&   . 5w        -   5        $+ seMinWarning��5w   '   5x        5y      $ 	Макс.��6&   . 5z        -   5        $+ seMaxWarning��5z   '   5{        5|      -   5        $ liWarningMeasureUnit��5|   <   $ ед. измерения��6&   5}      5~      5      �� 5�      -   5        $* spListNormal��5�   >   
������$��1��61   .+�5�      �� $$��В пределах нормы��6   $��16(�63   $��Center=�6A   6`     5�      .+�5�        $( "Значение из списка��6&   . 5�        -   5        $ ceNormal��5�   +   $
 Name��6=   $	 Id��6R   5�      5�      5�      �� 5�      -   5        $* spListWarning��5�   H   
�������$��2��6;   .+�5�      �� $0��*В пределах допустимого��6   $��16(�69   $��Center=�6G   6f     5�      .+�5�        $( "Значение из списка��6&   . 5�        -    5        $ 	ceWarning��5�   +   $
 Name��6>   $	 Id��6S   5�      5�      5�        5�      -!   5        $ sbSaveChanges��5�      $��5q�65   $��30��6@   $��3��6L   $+��%Сохранить изменения��6Y    6w     * Images/Actions/Apply_16x16.png+6�   5�      �� 5�   
   $ 
Horizontal=�6   $��3��6.   $��2��6;   $��Center=�6O   .+�5�        -"   5        $ sbCancel��5�      $��Отменить��60   $��5q�6C   $��30��6N   $	��130��6Z    6f     *!Images/Actions/Cancel_16x16.png+6�     5�      -#   5        $ sbSave��5�      $��Сохранить��6.   $��30��6B   $	��130��6N   $	��0,5q�6Z    6g     *Images/Save/Save_16x16.png+6�   5�      5�      5�      