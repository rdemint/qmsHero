﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QmsDoc.Controls
{
    /// <summary>
    /// Interaction logic for ToggleCheckBox.xaml
    /// </summary>
    public partial class ToggleCheckBox : UserControl
    {
        public ToggleCheckBox()
        {
            InitializeComponent();
        }

        public static DependencyProperty ControlValueProperty =
           DependencyProperty.Register(
       "ControlValue",
       typeof(string),
       typeof(ToggleCheckBox),
       new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true }
       );

        public string ControlValue
        {
            get
            {
                if (this.MyCheckBox.IsChecked.HasValue ? this.MyCheckBox.IsChecked.Value : false)
                {
                    return (string)GetValue(ControlValueProperty);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue(ControlValueProperty, value);
            }
        }



        public static readonly DependencyProperty DisplayNameProperty =
            DependencyProperty.Register(
                "DisplayName",
                typeof(string),
                typeof(ToggleCheckBox),
                new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true }
           );

        public string DisplayName
        {
            get
            {
                return (string)GetValue(DisplayNameProperty);
            }
            set
            {
                SetValue(DisplayNameProperty, value);
            }
        }
    }
}
