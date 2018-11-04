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

        #region Diagnosis Collection
        // holds diagnosis canned text objects in collection that implements INotifyPropertyChanged or something:

        private ObservableCollection<Diagnosis> _DiagnosisCollection = new ObservableCollection<Diagnosis>();
        public ObservableCollection<Diagnosis> DiagnosisCollection { get { return _DiagnosisCollection; } }
        #endregion Diagnosis Collection


        public MainWindow()
        {
            InitializeComponent();
            // Window height binding to button needs this:
            this.DataContext = this;





            #region XML Serializer to Load Diagnoses
            LoadInitialDiagnoses();
            #endregion


            var MyCommands = new ObservableCollection<MyCommandWrapper>();

            MyCommands.Add(new MyCommandWrapper() { Command = Commands.AddDiagnosis, DisplayName = "Add Diagnosis" });

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

        #region XMLSaveLoad
        private void LoadInitialDiagnoses()
        {
            XmlSerializer Serializer = new XmlSerializer(DiagnosisCollection.GetType());

            using (TextReader reader = new StreamReader(InitialDiagnosisXML))
            {
                _DiagnosisCollection = (ObservableCollection<Diagnosis>)Serializer.Deserialize(reader);
            }
        }

        public void Serialize(ObservableCollection<Diagnosis> diagnosisCollection) {
            using (TextWriter writer = new StreamWriter(UserDiagnosisXML))
            {
                XmlSerializer Serializer = new XmlSerializer(DiagnosisCollection.GetType());
                Serializer.Serialize(writer, DiagnosisCollection);
            }
        }

        public void LoadNewDiagnoses(object sender, RoutedEventArgs e)
        {
            XmlSerializer Serializer = new XmlSerializer(DiagnosisCollection.GetType());

            using (TextReader reader = new StreamReader(UserDiagnosisXML))
            {
                _DiagnosisCollection = (ObservableCollection<Diagnosis>)Serializer.Deserialize(reader);
            }
            GenerateButtons();
        }

        public void ClearNewDiagnoses(object sender, RoutedEventArgs e)
        {
            DiagnosisCollection.Clear();
            LoadInitialDiagnoses();
            if (File.Exists(UserDiagnosisXML))
            {
                File.Delete(UserDiagnosisXML);
            }
            GenerateButtons();
        }
        #endregion XMLSaveLoad

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBoxController.TextAdd((sender as Button).Tag.ToString(), this.EditBox);
        }

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
            Serialize(DiagnosisCollection);
            GenerateButtons();


        }



    }
}
