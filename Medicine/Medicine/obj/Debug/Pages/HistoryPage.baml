   M S B A M L   `   `   `  ����C  ?Medicine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Medicine.PagesMedicine.Pages  5   	        Medicine.Pages.HistoryPage   5      a ]DevExpress.Xpf.Grid.v20.1, Version=20.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af bDevExpress.Xpf.Grid.v20.1.Core, Version=20.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   a ]DevExpress.Xpf.Core.v20.1, Version=20.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  6W   ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  6�   c _DevExpress.Xpf.Charts.v20.1, Version=20.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aPmyGhttp://schemas.devexpress.com/winfx/2008/xaml/charts/rangecontrolclient  6�   j fDevExpress.Xpf.LayoutControl.v20.1, Version=20.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol  6-  e aDevExpress.Xpf.Controls.v20.1, Version=20.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aCdxwui7http://schemas.devexpress.com/winfx/2008/xaml/windowsui  6x  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W	 SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    	 5   	   R
 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 
   	 5   	   @d2http://schemas.microsoft.com/expression/blend/2008 
   	 5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 
   	 5   	   .localclr-namespace:Medicine.Pages 
   	 5   	   -   5        #��� ��5   2   .��5	      �   � RowDefinitions  5
   
   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $��* �6   6)   .+�5   
   �� $��0��6   $��2��6   $<��6История проведенных анализов��61   $
��Left=�6X   $��5,0,0,0q�6s   $	��Top=�6�   $��39��6�   $��24(�6�   �� 5   
   $��1��6   $��2��6   $����Здесь вы можете просмотреть подробную историю анализов, созданных по шаблонам в системе.��61   $��14(�6�   $��5,0,0,0q�6�   $��0q�6�   6�   +  %DevExpress.Xpf.LayoutControl.GroupBox  5   
   $��2��6   4  .DevExpress.Xpf.Core.HeaderedContentControlBase   Header$ Фильтрация��6%   $��5q�69   ,  &DevExpress.Xpf.Core.ContentControlBase   Content. 5      �  � ColumnDefinitions 5      �� 5      $
��auto �6'   66   �� 5      $
��auto �6'   66   �� 5      $��* �6'   63   �� 5      $
��auto �6'   66   �� 5      $��* �6'   63   �� 5      $
��auto �6'   66   .+�5       �� $��0��6    �� Orientation$ 
Horizontal=�6-   .+�5!      �� $��Период с��6   $��0,0,5,0q�6/   (  "System.Windows.Controls.DatePicker  5"      -   5          Name$ dpFrom��5"   !   �� 5#      $
��по��6   $��5,0,5,0q�6)     5$      -   5        $
 dpTo��5$   !   5%      �� 5'      $��1��6   $��Шаблон��6(   $��5,0,5,0q�69   )  #DevExpress.Xpf.Editors.ComboBoxEdit  5(      -   5          Name$ 
ceTemplate��5(   #   $��2��67   +  %DevExpress.Xpf.Editors.LookUpEditBase   SelectedIndex$ 0��6G      DisplayMember$
 Name��6Y   	   ValueMember$	 Id��6n   �� 5*      $��3��6   $��Пациент��6(   $��5,0,5,0q�6:     5+      -   5        $ 	cePatient��5+   #   $��4��66   $ 0��6F   $ FullName��6X   $	 Id��6q   �� 5-      $��5��6   $ 
Horizontal=�6-   .+�5.      &   DevExpress.Xpf.Core.SimpleButton  -   5        
  Name$
 sbReset��5.   B   $��Right=�6&   $��Сбросить��6S   $	��5,0q�6�      Glyph 6f   *  $DevExpress.Xpf.Core.DXImageExtension  *%#Images/Filter/ClearFilter_16x16.png+6�     5/      -   5        $
 sbApply��5/   B   $��Right=�6&   $��Применить��6S    6g     *,*Images/Spreadsheet/ReapplyFilter_16x16.png+6�   50      51      52      �� 54   
   $��3��6   $ 
Horizontal=�6"   $��5,0,5,5q�6;   .+�55        -   5        $
 sbAdd��55      $��Добавить��6-   $��0,0,5,0q�6@   $	��120��6Q    6]     *(&SvgImages/Icon Builder/Actions_Add.svg+6�     56      -	   5        $
 sbEdit��56      {�.  $��Изменить��6@   $��0,0,5,0q�6S   $	��120��6d    6p     *)'SvgImages/Icon Builder/Actions_Edit.svg+6�     57      -
   5        $
 sbDelete��57      {�.  $��Удалить��6B   $	��120��6T    6`     *+)SvgImages/Icon Builder/Actions_Delete.svg+6�     58      -   5        $
 sbPrint��58      {�.  $��
Отчет��6A   $	��120��6Q   $��5,0,0,0q�6]    6n     **(SvgImages/Business Objects/BO_Report.svg+6�   59      %	  DevExpress.Xpf.Grid.GridControl	  5;   
   -   5         	 Name$ 	gcHistory��5;      $��4��6-   )
  #DevExpress.Xpf.Grid.DataControlBase 
  SelectionMode$	 Row=�6:    	  View 5<      #  DevExpress.Xpf.Grid.TableView  5=      &   DevExpress.Xpf.Grid.DataViewBase   AllowEditing .     ShowSearchPanelMode$ Always=�65      	AutoWidth . &   DevExpress.Xpf.Grid.GridViewBase   ShowGroupPanel .    ShowAutoFilterRow . 6y    	  Columns. 5?      $  DevExpress.Xpf.Grid.GridColumn  $  DevExpress.Xpf.Grid.ColumnBase   	FieldName$ TemplateName��6   $  DevExpress.Xpf.Grid.BaseColumn   Header)  #DevExpress.Xpf.Core.ObjectConverter$ Шаблон 66     5@      $ Patient.FullName��6   $ Пациент 6:     5A      $ 
CreateDate��6   $ Дата анализа 64   5B      5C      5D      