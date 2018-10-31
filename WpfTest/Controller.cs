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
        public static void TextAdd(string text, System.Windows.Controls.TextBox textbox)
        {
            //MessageBox.Show($"The context of GreenBox are: {this.GreenTextBox.Text}");
            textbox.SelectionStart = textbox.CaretIndex;
            textbox.SelectionLength = 0; //this was missing
            textbox.SelectedText = text;
            textbox.CaretIndex += text.Length;
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


