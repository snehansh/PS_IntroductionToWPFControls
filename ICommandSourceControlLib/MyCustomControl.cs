using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ICommandSourceControlLib
{
    // Implementing ICommandSource because the custom control 'TextBlock/Border' does not implement commands impliciltly like in case of other controls like 'Button'
    public class MyCustomControl : Control, ICommandSource
    {
        private Border _border;
        static MyCustomControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyCustomControl), new FrameworkPropertyMetadata(typeof(MyCustomControl)));
        }

        #region Command
        // Keeps a copy of the CanExecuteChangedHandler so it doesn't get garbage collected.
        private EventHandler canExecuteChangedHandler;

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(MyCustomControl),
                new PropertyMetadata((ICommand)null, new PropertyChangedCallback(OnCommandChanged)));

        [TypeConverter(typeof(CommandConverter))]
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MyCustomControl control = (MyCustomControl)d;
            if (control != null)
                control.OnCommandChanged((ICommand)e.OldValue, (ICommand)e.NewValue);
        }

        protected virtual void OnCommandChanged(ICommand oldValue, ICommand newValue)
        {
            if (oldValue != null)
                UnhookCommand(oldValue, newValue);

            HookupCommand(oldValue, newValue);

            CanExecuteChanged(null, null);
        }

        private void UnhookCommand(ICommand oldCommand, ICommand newCommand)
        {
            EventHandler handler = CanExecuteChanged;
            oldCommand.CanExecuteChanged -= CanExecuteChanged;
        }

        private void HookupCommand(ICommand oldCommand, ICommand newCommand)
        {
            EventHandler handler = new EventHandler(CanExecuteChanged);
            canExecuteChangedHandler = handler;
            if (newCommand != null)
                newCommand.CanExecuteChanged += canExecuteChangedHandler;
        }

        private void CanExecuteChanged(object sender, EventArgs e)
        {
            if (Command != null)
            {
                RoutedCommand rc = Command as RoutedCommand;

                if (rc != null)
                    IsEnabled = rc.CanExecute(CommandParameter, CommandTarget) ? true : false;
                else
                    IsEnabled = Command.CanExecute(CommandParameter) ? true : false;
            }
        }

        #endregion

        #region Command Parameter
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(MyCustomControl), new PropertyMetadata(null));
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        #endregion

        #region Command Target
        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(MyCustomControl),
                new PropertyMetadata(null));
        public IInputElement CommandTarget
        {
            get { return (IInputElement)GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }
        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_border != null)
                _border.MouseLeftButtonUp -= Border_MouseLeftButtonUp;
            _border = (Border)GetTemplateChild("PART_Border");
            if (_border != null)
                _border.MouseLeftButtonUp += Border_MouseLeftButtonUp;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RaiseCommand();
        }

        private void RaiseCommand()
        {
            if (Command != null)
            {
                RoutedCommand rc = Command as RoutedCommand;
                if (rc != null)
                    rc.Execute(CommandParameter, CommandTarget);
                else
                    Command.Execute(CommandParameter);
            }
        }
    }
}
