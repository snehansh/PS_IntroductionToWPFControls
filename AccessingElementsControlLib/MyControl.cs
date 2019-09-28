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

namespace AccessingElementsControlLib
{
    [TemplatePart(Name = TextBlockPart, Type = typeof(TextBlock))]
    public class MyControl : Control
    {
        private const string TextBlockPart = "PART_TextBlock";

        TextBlock textBlock;
        protected TextBlock TextBlock
        {
            get { return textBlock; }
            set
            {
                if (textBlock != null)
                {
                    textBlock.TextInput -= new TextCompositionEventHandler(TextBlock_TextInput);
                }

                textBlock = value;

                if (textBlock != null)
                {
                    textBlock.Text = "Set from code";
                    textBlock.TextInput += new TextCompositionEventHandler(TextBlock_TextInput);
                }
            }
        }

        static MyControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyControl), new FrameworkPropertyMetadata(typeof(MyControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //if (textBlock != null)
            //{
            //    textBlock.TextInput -= new TextCompositionEventHandler(TextBlock_TextInput);
            //}

            //var textBlock = GetTemplateChild(TextBlockPart) as TextBlock;

            TextBlock = GetTemplateChild(TextBlockPart) as TextBlock;
            //if (textBlock != null)
            //{
            //    textBlock.Text = "Set from code";
            //    textBlock.TextInput += new TextCompositionEventHandler(TextBlock_TextInput);
            //}
        }

        private void TextBlock_TextInput(object sender, TextCompositionEventArgs e)
        {
        }
    }
}
