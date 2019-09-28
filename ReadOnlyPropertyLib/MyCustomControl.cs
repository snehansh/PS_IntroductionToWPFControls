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

namespace ReadOnlyPropertyLib
{
    public class MyCustomControl : Control
    {
        static MyCustomControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyCustomControl), new FrameworkPropertyMetadata(typeof(MyCustomControl)));
        }

        private static readonly DependencyPropertyKey HasBeenClickedPropertyKey = DependencyProperty.RegisterReadOnly("HasBeenClicked", typeof(bool), typeof(MyCustomControl), new PropertyMetadata(false));

        public static readonly DependencyProperty HasBeenClickedProperty = HasBeenClickedPropertyKey.DependencyProperty;

        public bool HasBeenClicked
        {
            get { return (bool)GetValue(HasBeenClickedProperty); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //demo purposes only, check for previous instances
            //and remove handler first
            var button = GetTemplateChild("PART_Button") as Button;
            if (button != null)
                button.Click += Button_Click;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetValue(HasBeenClickedPropertyKey, true);
        }
    }
}
