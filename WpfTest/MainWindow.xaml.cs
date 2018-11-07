using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace WpfTest
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string initialDiagnosisXML = @".\InitialDiagnoses.xml";
        public Diagnosis SelectedDiagnosis { get; set; }
        public CollectionViewSource DiagnosisViewSource { get; set; }
        public ObservableCollection<Diagnosis> DiagnosisCollection { get; set; }
        public Repository Repository { get; set; }
        public MainWindow()

        {
            InitializeComponent();
            DataContext = this;
            Repository = new Repository(DiagnosisCollection, initialDiagnosisXML);
            DiagnosisCollection = Repository.LoadCollection();

            DiagnosisViewSource = new CollectionViewSource();
            DiagnosisViewSource.Source = DiagnosisCollection;
            SelectedDiagnosis = DiagnosisCollection.First();

            GenerateButtons();
        }

 

        private void GenerateButtons()
        {
            ButtonGrid.Children.Clear();
            foreach (var d in DiagnosisCollection)
            {
                var btn = new Button
                {
                    Content = d.Name,
                    Tag = d
                };
                btn.Click += CommandBinding_AddDiagnosisCommand;
                ButtonGrid.Children.Add(btn);
            }
        }


        private void EditBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EditBox.Focus();
        }

        public void CommandBinding_AddDiagnosisCommand(object sender, RoutedEventArgs e)
        {
            var buttonTag = (sender as Button).Tag;
            var diagnosis = buttonTag as Diagnosis;
            TextBoxController.TextAdd(diagnosis.Text, EditBox);
            NameTextBox.Text = diagnosis.Name;
        }

        public void CommandBinding_SaveReportAsDiagnosisCommand(object sender, RoutedEventArgs e)
        {

            var name = TextBoxController.TextGet(NameTextBox);
            if (string.IsNullOrEmpty(name) || 
                DiagnosisCollection.Any(d => d.Name == TextBoxController.TextGet(NameTextBox)))
            {
                TextBoxController.ReplaceTextBox(NameTextBox, "Please enter new name");
                return;
            }
            var text = TextBoxController.TextGet(EditBox);
            if (string.IsNullOrEmpty(text))
            {
                TextBoxController.ReplaceTextBox(EditBox, "Please enter report text");
                return;
            }

            NameTextBox.Focus();
            NameTextBox.SelectAll();
            DiagnosisCollection.Add(new Diagnosis(name, text));
            Repository.SaveCollection(DiagnosisCollection);
            GenerateButtons();
        }


        private void cmbItem_PreviewMouseDown(object sender, EventArgs e)
        {
            //Sender is comboboxitem: Diagnosis
            var item = sender as ComboBoxItem;
            var diagnosis = (Diagnosis) item.Content;
            TextBoxController.TextAdd(diagnosis.Text, EditBox);
            NameTextBox.Text = diagnosis.Name;
        }


        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Do nothing
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            var name = SearchBox.Text;

            if (DiagnosisCollection.All(d => d.Name != name)) return;
            {
                var diagnosis = DiagnosisCollection.First(d => d.Name == name);
                if (diagnosis == null) return;
                TextBoxController.TextAdd(diagnosis.Text, EditBox);
                NameTextBox.Text = diagnosis.Name;
            }
        }
    }
}