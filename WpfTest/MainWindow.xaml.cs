using System;
using System.Collections.Generic;
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
        }


        private void Arlo_Click(object sender, RoutedEventArgs e)
        {
            Controller.TextAdd(StaticTestData.Diagnoses[2], this.EditBox);
        }

        private void EditBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.EditBox.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Controller.TextAdd(StaticTestData.Diagnoses[0], this.EditBox);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Controller.TextAdd(StaticTestData.Diagnoses[3], this.EditBox);
        }
    }
}
