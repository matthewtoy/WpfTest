using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Xml.Serialization;
using WpfTest;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public readonly string InitialDiagnosisXML = @".\InitialDiagnoses.xml";
        public readonly string UserDiagnosisXML = @".\WpfTest.xml";
        private ObservableCollection<Diagnosis> _DiagnosisCollection = new ObservableCollection<Diagnosis>();
        public ObservableCollection<Diagnosis> DiagnosisCollection { get { return _DiagnosisCollection; } }


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _DiagnosisCollection = Repository.LoadCollection(InitialDiagnosisXML);
            GenerateButtons();
        }

        private void GenerateButtons()
        {
            ButtonGrid.Children.Clear();
            foreach (var d in DiagnosisCollection)
            {
                var btn = new Button()
                {
                    Content = d.Name,
                    Tag = d,
                };
                btn.Click += new RoutedEventHandler(CommandBinding_AddDiagnosisCommand);
                ButtonGrid.Children.Add(btn);
            }
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    TextBoxController.TextAdd((sender as Button).Tag.ToString(), this.EditBox);
        //}

        private void EditBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.EditBox.Focus();
        }

        public void CommandBinding_AddDiagnosisCommand(object sender, RoutedEventArgs e)
        {
            var buttonTag = (sender as Button).Tag;
            var diagnosis = (buttonTag as Diagnosis);
            TextBoxController.TextAdd(diagnosis.Text, this.EditBox);
            this.NameTextBox.Text = diagnosis.Name;
        }

        public void CommandBinding_SaveReportAsDiagnosisCommand(object sender, RoutedEventArgs e)
        {
            var name = TextBoxController.TextGet(this.NameTextBox);
            if (String.IsNullOrEmpty(name))
            {
                this.NameTextBox.Focus();
                this.NameTextBox.SelectAll();
                this.NameTextBox.SelectedText = "Please enter name";
                return;
            } else if (DiagnosisCollection.Any(d => d.Name == name))
            {
                this.NameTextBox.Focus();
                this.NameTextBox.SelectAll();
                this.NameTextBox.SelectedText = "Please enter new name";
                return;
            }


            var text = TextBoxController.TextGet(this.EditBox);
            if (String.IsNullOrEmpty(text))
            {
                this.EditBox.Focus();
                this.EditBox.SelectAll();
                this.EditBox.SelectedText = "Please enter report text";
                return;
            }
                

            this.NameTextBox.Focus();
            this.NameTextBox.SelectAll();
            
            var diagnosis = new Diagnosis()
            {
                Name = name,
                Text = text
            };
            DiagnosisCollection.Add(diagnosis);
            Repository.SaveCollection(DiagnosisCollection, InitialDiagnosisXML);
            GenerateButtons();


        }

        private void cmbItem_PreviewMouseDown(object sender, EventArgs e)
        {
            //Sender is comboboxitem: Diagnosis
            var item = sender as ComboBoxItem;
            Diagnosis diagnosis = (Diagnosis)item.Content;

            TextBoxController.TextAdd(diagnosis.Text, this.EditBox);
            this.NameTextBox.Text = diagnosis.Name;


        }



        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
  // Do nothing
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var name = SearchBox.Text;


                if (DiagnosisCollection.Any(d => d.Name == name))
                {
                    var diagnosis = DiagnosisCollection.First(d => d.Name == name);
                    if (diagnosis != null)
                    {
                        TextBoxController.TextAdd(diagnosis.Text, this.EditBox);
                        this.NameTextBox.Text = diagnosis.Name;
                    }
                }
            }
        }
    }
}
