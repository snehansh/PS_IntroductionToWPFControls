using System;
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

namespace CRCWpfCustomControlLibrary
{
    public static class ControlCommands
    {
        private static RoutedCommand _updateTextCommand = new RoutedCommand();
        public static RoutedCommand UpdateTextCommand
        {
            get
            {
                return _updateTextCommand;
            }
        }
    }

    public class MyCustomControl : Control
    {
        private Button _button;
        static MyCustomControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyCustomControl), new FrameworkPropertyMetadata(typeof(MyCustomControl)));
        }

        public MyCustomControl()
        {
            CommandBindings.Add(new CommandBinding(ControlCommands.UpdateTextCommand,
                ExecuteUpdate, CanExecuteUpdate));
        }

        private void CanExecuteUpdate(object sender, CanExecuteRoutedEventArgs e)
        {
            string param = (string)e.Parameter;
            if (param != null)
                e.CanExecute = true;
        }

        private void ExecuteUpdate(object sender, ExecutedRoutedEventArgs e)
        {
            if (_button != null)
                _button.Content = e.Parameter;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _button = (Button)GetTemplateChild("PART_BUTTON");
        }
    }
}
