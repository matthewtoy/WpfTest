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

            XmlSerializer Serializer = new XmlSerializer(DiagnosisCollection.GetType());

            using (TextReader reader = new StreamReader(@".\InitialDiagnoses.xml"))
            {
                DiagnosisCollection = (ObservableCollection<Diagnosis>)Serializer.Deserialize(reader);
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

            using (TextWriter writer = new StreamWriter(@".\WpfTest.xml"))
            {
                Serializer.Serialize(writer, DiagnosisCollection);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBoxController.TextAdd((sender as Button).Tag.ToString(), this.EditBox);
        }

        private void Arlo_Click(object sender, RoutedEventArgs e)
        {
            //not implemented
        }

        private void EditBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.EditBox.Focus();
        }
    }
}
