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
            this.DataContext = this;

            var DiagnosisCollection = new ObservableCollection<Diagnosis>();

            for (int i = 0; i < 10; i++)
            {
                var diag = new Diagnosis(StaticTestData.Labels[i],StaticTestData.Diagnoses[i]);
                DiagnosisCollection.Add(diag);
            }

            foreach (var d in DiagnosisCollection)
            {
                var btn = new Button()
                {
                    Content = d.Name,
                    Tag = d.Text
                };
                btn.Click += new RoutedEventHandler(Button_Click);
                ButtonGrid.Children.Add(btn);
            }

            var Diagnoses = new ObservableCollection<TestObject>();

            Diagnoses.Add(new TestObject
            {
                Name = "Poel",
                Diagnosis = "hyperchondria",
                Code = "M6877485"
            });

            Diagnoses.Add(new TestObject
            {
                Name = "Henrietta",
                Diagnosis = "midichlorians",
                Code = "01904"
            });

            XmlSerializer Serializer = new XmlSerializer(Diagnoses.GetType());

            using (TextWriter writer = new StreamWriter(@".\WpfTest.xml"))
            {
                Serializer.Serialize(writer, Diagnoses);
            }










        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBoxController.TextAdd((sender as Button).Tag.ToString(), this.EditBox);
        }

        private void Arlo_Click(object sender, RoutedEventArgs e)
        {
            TextBoxController.TextAdd(StaticTestData.Diagnoses[2], this.EditBox);
        }

        private void EditBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.EditBox.Focus();
        }
    }
}
