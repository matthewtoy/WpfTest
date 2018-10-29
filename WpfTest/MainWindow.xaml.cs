using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Controller.TextAdd((sender as Button).Tag.ToString(), this.EditBox);
        }

        private void Arlo_Click(object sender, RoutedEventArgs e)
        {
            Controller.TextAdd(StaticTestData.Diagnoses[2], this.EditBox);
        }

        private void EditBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.EditBox.Focus();
        }

    }
}
