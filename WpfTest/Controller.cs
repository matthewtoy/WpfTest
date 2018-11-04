using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfTest
{
    static class TextBoxController
    {
        public static void TextAdd(string text, System.Windows.Controls.TextBox textBox)
        {
            //MessageBox.Show($"The context of GreenBox are: {this.GreenTextBox.Text}");
            textBox.SelectionStart = textBox.CaretIndex;
            textBox.SelectionLength = 0; //this was missing
            textBox.SelectedText = text;
            textBox.CaretIndex += text.Length;
        }

        public static string TextGet(System.Windows.Controls.TextBox textBox)
        {
            textBox.SelectAll();
            return textBox.SelectedText;
        }
    }

    public class DataModel  
    {
        public string Content { get; set; }

        public ICommand Command { get; set; }

        public DataModel(string content, ICommand command)
        {
            Content = content;
            Command = command;
        }
        private readonly ObservableCollection<DataModel> _MyData = new ObservableCollection<DataModel>();
        public ObservableCollection<DataModel> MyData { get { return _MyData; } }
    }
}


