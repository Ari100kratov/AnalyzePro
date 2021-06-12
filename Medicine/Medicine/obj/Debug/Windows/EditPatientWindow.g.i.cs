﻿#pragma checksum "..\..\..\Windows\EditPatientWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7EFFE90F0A123464260BCA9EE06BF937AEE80EF13CA6923E487B54B59DCFF302"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DevExpress.Core;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.ConditionalFormatting;
using DevExpress.Xpf.Core.DataSources;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Core.ServerMode;
using DevExpress.Xpf.DXBinding;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.DataPager;
using DevExpress.Xpf.Editors.DateNavigator;
using DevExpress.Xpf.Editors.ExpressionEditor;
using DevExpress.Xpf.Editors.Filtering;
using DevExpress.Xpf.Editors.Flyout;
using DevExpress.Xpf.Editors.Popups;
using DevExpress.Xpf.Editors.Popups.Calendar;
using DevExpress.Xpf.Editors.RangeControl;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Editors.Settings.Extension;
using DevExpress.Xpf.Editors.Validation;
using DevExpress.Xpf.LayoutControl;
using Medicine.Windows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Medicine.Windows {
    
    
    /// <summary>
    /// EditPatientWindow
    /// </summary>
    public partial class EditPatientWindow : DevExpress.Xpf.Core.ThemedWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\Windows\EditPatientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.ImageEdit iePhoto;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Windows\EditPatientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.TextEdit teLastName;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Windows\EditPatientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.TextEdit teFirstName;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Windows\EditPatientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.TextEdit teMiddleName;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Windows\EditPatientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit ceMail;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Windows\EditPatientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit ceFemale;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Windows\EditPatientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.DateEdit dtBirth;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Windows\EditPatientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.TextEdit tePhone;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Windows\EditPatientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.TextEdit teOtherPhone;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\Windows\EditPatientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.TextEdit teRegAddress;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\Windows\EditPatientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.TextEdit teResAddress;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\Windows\EditPatientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Core.SimpleButton sbCancel;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\Windows\EditPatientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Core.SimpleButton sbSave;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Medicine;component/windows/editpatientwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\EditPatientWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\..\Windows\EditPatientWindow.xaml"
            ((Medicine.Windows.EditPatientWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.ThemedWindow_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.iePhoto = ((DevExpress.Xpf.Editors.ImageEdit)(target));
            return;
            case 3:
            this.teLastName = ((DevExpress.Xpf.Editors.TextEdit)(target));
            
            #line 30 "..\..\..\Windows\EditPatientWindow.xaml"
            this.teLastName.Validate += new DevExpress.Xpf.Editors.Validation.ValidateEventHandler(this.teLastName_Validate);
            
            #line default
            #line hidden
            return;
            case 4:
            this.teFirstName = ((DevExpress.Xpf.Editors.TextEdit)(target));
            
            #line 33 "..\..\..\Windows\EditPatientWindow.xaml"
            this.teFirstName.Validate += new DevExpress.Xpf.Editors.Validation.ValidateEventHandler(this.teFirstName_Validate);
            
            #line default
            #line hidden
            return;
            case 5:
            this.teMiddleName = ((DevExpress.Xpf.Editors.TextEdit)(target));
            
            #line 36 "..\..\..\Windows\EditPatientWindow.xaml"
            this.teMiddleName.Validate += new DevExpress.Xpf.Editors.Validation.ValidateEventHandler(this.teMiddleName_Validate);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ceMail = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            
            #line 41 "..\..\..\Windows\EditPatientWindow.xaml"
            this.ceMail.EditValueChanged += new DevExpress.Xpf.Editors.EditValueChangedEventHandler(this.ceMail_EditValueChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ceFemale = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            
            #line 44 "..\..\..\Windows\EditPatientWindow.xaml"
            this.ceFemale.EditValueChanged += new DevExpress.Xpf.Editors.EditValueChangedEventHandler(this.ceFemale_EditValueChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.dtBirth = ((DevExpress.Xpf.Editors.DateEdit)(target));
            
            #line 49 "..\..\..\Windows\EditPatientWindow.xaml"
            this.dtBirth.Validate += new DevExpress.Xpf.Editors.Validation.ValidateEventHandler(this.dtBirth_Validate);
            
            #line default
            #line hidden
            return;
            case 9:
            this.tePhone = ((DevExpress.Xpf.Editors.TextEdit)(target));
            return;
            case 10:
            this.teOtherPhone = ((DevExpress.Xpf.Editors.TextEdit)(target));
            return;
            case 11:
            this.teRegAddress = ((DevExpress.Xpf.Editors.TextEdit)(target));
            return;
            case 12:
            this.teResAddress = ((DevExpress.Xpf.Editors.TextEdit)(target));
            return;
            case 13:
            this.sbCancel = ((DevExpress.Xpf.Core.SimpleButton)(target));
            
            #line 80 "..\..\..\Windows\EditPatientWindow.xaml"
            this.sbCancel.Click += new System.Windows.RoutedEventHandler(this.sbCancel_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.sbSave = ((DevExpress.Xpf.Core.SimpleButton)(target));
            
            #line 81 "..\..\..\Windows\EditPatientWindow.xaml"
            this.sbSave.Click += new System.Windows.RoutedEventHandler(this.sbSave_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

