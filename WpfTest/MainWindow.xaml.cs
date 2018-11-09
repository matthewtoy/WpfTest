using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

            Closed += MainWindow_Closed;

            GenerateButtons();
        }


        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Repository.SaveCollection(DiagnosisCollection);
        }

        private void GenerateButtons()
        {
            ButtonGrid.Children.Clear();
            var sortedList = DiagnosisCollection.ToList();
            sortedList.Sort();

            foreach (var d in sortedList)
            {
                var btn = new Button
                {
                    Content = d.Name,
                    Tag = d
                };
                btn.Click += CommandBinding_PrintDiagnosisCommand;
                ButtonGrid.Children.Add(btn);
            }
        }


        private void EditBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EditBox.Focus();
        }

        private void PreviewBox_OnTextChangedBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //do nothing.
        }

        public void CommandBinding_PrintDiagnosisCommand(object sender, RoutedEventArgs e)
        {
            var buttonTag = (sender as Button).Tag;
            var diagnosis = buttonTag as Diagnosis;
            TextBoxController.ReplaceTextBox(diagnosis.Text, PreviewBox);
            TextBoxController.TextAdd(diagnosis.Text, EditBox);
            diagnosis.IncrementUseCount();
            NameTextBox.Text = diagnosis.Name;
            var DiagnosisView = CollectionViewSource.GetDefaultView(DiagnosisCollection);
            DiagnosisView.Refresh();
        }

        public void CommandBinding_SaveReportAsDiagnosisCommand(object sender, RoutedEventArgs e)
        {
            var name = TextBoxController.TextGet(NameTextBox);
            if (string.IsNullOrEmpty(name) ||
                DiagnosisCollection.Any(d => d.Name == TextBoxController.TextGet(NameTextBox)))
            {
                TextBoxController.ReplaceTextBox("Please enter new name", NameTextBox);
                return;
            }

            var text = TextBoxController.TextGet(EditBox);
            if (string.IsNullOrEmpty(text))
            {
                TextBoxController.ReplaceTextBox("Please enter report text", EditBox);
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
            TextBoxController.ReplaceTextBox(diagnosis.Text, PreviewBox);
            TextBoxController.TextAdd(diagnosis.Text, EditBox);
            NameTextBox.Text = diagnosis.Name;
            diagnosis.IncrementUseCount();
            var DiagnosisView = CollectionViewSource.GetDefaultView(DiagnosisCollection);
            DiagnosisView.Refresh();
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
                TextBoxController.ReplaceTextBox(diagnosis.Text, PreviewBox);
                TextBoxController.TextAdd(diagnosis.Text, EditBox);
                NameTextBox.Text = diagnosis.Name;
                diagnosis.IncrementUseCount();
                var DiagnosisView = CollectionViewSource.GetDefaultView(DiagnosisCollection);
                DiagnosisView.Refresh();
            }
        }

        private void SortAZButton_Click(object sender, RoutedEventArgs e)
        {
            var DiagnosisView = CollectionViewSource.GetDefaultView(DiagnosisCollection);
            DiagnosisView.SortDescriptions.Clear();
            DiagnosisView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }

        private void SortByUseButton_Click(object sender, RoutedEventArgs e)
        {
            var DiagnosisView = CollectionViewSource.GetDefaultView(DiagnosisCollection);
            DiagnosisView.SortDescriptions.Clear();
            DiagnosisView.SortDescriptions.Add(new SortDescription("UseCount", ListSortDirection.Descending));
        }


        private void SearchBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = sender as ComboBoxItem;
            if (item == null) return;
            var diagnosis = e.AddedItems[0] as Diagnosis;
            TextBoxController.ReplaceTextBox(diagnosis.Text, PreviewBox);
            SearchBox.Focus();
        }

        private void CommandBinding_SaveReportAsDiagnosisCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}