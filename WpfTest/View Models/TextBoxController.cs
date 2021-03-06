﻿using System.Windows.Controls;

namespace WpfTest
{
    internal static class TextBoxController
    {
        public static void TextAdd(string text, TextBox textBox)
        {
            //MessageBox.Show($"The context of GreenBox are: {this.GreenTextBox.Text}");
            textBox.SelectionStart = textBox.CaretIndex;
            textBox.SelectionLength = 0; //this was missing
            textBox.SelectedText = text;
            textBox.CaretIndex += text.Length;
        }

        public static string TextGet(TextBox textBox)
        {
            textBox.SelectAll();
            return textBox.SelectedText;
        }

        public static void ReplaceTextBox(string text, TextBox textBox)
        {
            textBox.Focus();
            textBox.SelectAll();
            textBox.SelectedText = text;
        }

        public static void AppendText(string text, TextBox textBox)
        {
            textBox.Focus();
            textBox.CaretIndex = textBox.Text.Length;
            textBox.AppendText(" ");
            textBox.SelectedText = text;
        }
    }
}