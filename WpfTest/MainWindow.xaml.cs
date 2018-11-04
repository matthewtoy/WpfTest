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
        public MainWindow()
        {
            InitializeComponent();
            // Window height binding to button needs this:
            this.DataContext = this;

            #region Diagnosis Collection
            // holds diagnosis canned text objects in collection that implements INotifyPropertyChanged or something:
            var DiagnosisCollection = new ObservableCollection<Diagnosis>();
            #endregion Diagnosis Collection


            #region XML Serializer to Load Diagnoses
            XmlSerializer Serializer = new XmlSerializer(DiagnosisCollection.GetType());

            using (TextReader reader = new StreamReader(@".\InitialDiagnoses.xml"))
            {
                DiagnosisCollection = (ObservableCollection<Diagnosis>)Serializer.Deserialize(reader);
            }
            #endregion

            #region Generate Buttons
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
            #endregion

            #region XML Serializer save diagnoses
            using (TextWriter writer = new StreamWriter(@".\WpfTest.xml"))
            {
                Serializer.Serialize(writer, DiagnosisCollection);
            } 
            #endregion
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBoxController.TextAdd((sender as Button).Tag.ToString(), this.EditBox);
        }


        private void EditBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.EditBox.Focus();
        }

        public void CommandBinding_DoSomething(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("hello");
        }

        public void CommandBinding_AddDiagnosisCommand(object sender, RoutedEventArgs e)
        {
            var buttonTag = (sender as Button).Tag;
            var diagnosis = (buttonTag as Diagnosis);
            TextBoxController.TextAdd(diagnosis.Text, this.EditBox);

        }
    }

}
