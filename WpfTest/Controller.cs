using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTest
{
    static class Controller
    {
        public static void TextAdd(string text, System.Windows.Controls.TextBox textbox)
        {
            //MessageBox.Show($"The context of GreenBox are: {this.GreenTextBox.Text}");
            textbox.SelectionStart = textbox.CaretIndex;

            textbox.SelectionLength = 0; //this was missing
            textbox.SelectedText = text;
            textbox.CaretIndex += text.Length;
        }
    }
}
