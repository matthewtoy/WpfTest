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

        }

        public Diagnosis SelectedDiagnosis { get; set; }
        public CollectionViewSource DiagnosisViewSource { get; set; }
        public static ObservableCollection<Diagnosis> DiagnosisCollection { get; set; }
        public Repository Repository { get; set; }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            FilterController.ClearFilter();
        }


        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Repository.SaveCollection(DiagnosisCollection);
        }


        private void EditBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EditBox.Focus();
        }

        private void PreviewBox_OnTextChangedBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //do nothing.
        }

//todo: Refactor common code in report printing

        public void PrintDiagnosis(Diagnosis diagnosis)
        {
            TextBoxController.ReplaceTextBox(diagnosis.Text, PreviewBox);
            TextBoxController.TextAdd(diagnosis.Text, EditBox);
            NameTextBox.Text = diagnosis.Name;
            diagnosis.IncrementUseCount();
            var diagnosisView = CollectionViewSource.GetDefaultView(DiagnosisCollection);
            diagnosisView.MoveCurrentTo(diagnosis);
            diagnosisView.Refresh();
            FilterController.ClearFilter();
        }

        public void CommandBinding_PrintDiagnosisCommand(object sender, RoutedEventArgs e)
        {
            //Hallelujah! Original source!
            var source = e.OriginalSource as Button;
            if (!(source.Tag is Diagnosis diagnosis)) return;
            PrintDiagnosis(diagnosis);
        }

        private void cmbItem_PreviewMouseDown(object sender, EventArgs e)
        {
            //Sender is combobox-item: Diagnosis
            var item = sender as ComboBoxItem;
            if (item == null) return;
            var diagnosis = (Diagnosis)item.Content;
            PrintDiagnosis(diagnosis);
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            var name = SearchBox.Text;

            if (e.Key != Key.Enter) return;

            if (DiagnosisCollection.All(d => d.Name != name)) return;
            {
                var diagnosis = DiagnosisCollection.First(d => d.Name == name);
                if (diagnosis == null) return;
                PrintDiagnosis(diagnosis);
            }
        }

        private void SearchBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!(CollectionViewSource.GetDefaultView(DiagnosisCollection).CurrentItem is Diagnosis diagnosis)) return;
            var diagnosisGroup = diagnosis.DiagnosisGroup;

            if (e.Key == Key.Right)
            {
                FilterController.SetDiagnosisGroupFilter(diagnosisGroup);
                e.Handled = true;
            }

            if (e.Key == Key.Left)
            {
                FilterController.ClearFilter();
                e.Handled = true;
            }

        }

        private void SearchBox_TextInput(object sender, TextCompositionEventArgs e)
        {
//           NameTextBox.Text = e.Text;
//            NameTextBox.Text = SearchBox.Text;

        }

        private void SearchBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = sender as ComboBox;
            if (item == null || e == null) return;
            if (e.AddedItems == null || e.AddedItems.Count < 1) return;
            var diagnosis = e.AddedItems[0] as Diagnosis;
            if (diagnosis == null) return;
            TextBoxController.ReplaceTextBox(diagnosis.Text, PreviewBox);
            SearchBox.Focus();
        }


        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = SearchBox.Text;

            FilterController.SetSearchAsTypeFilter(text);
            e.Handled = false;
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
        }


        private void SortAZButton_Click(object sender, RoutedEventArgs e)
        {
            FilterController.SortByAlphabet();
        }


        private void SortByUseButton_Click(object sender, RoutedEventArgs e)
        {
            FilterController.SortByUse();
        }


        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            FilterController.SetDiagnosisGroupFilter(NameTextBox.Text);
        }

        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {
            FilterController.ClearFilter();
        }

        private void CommandBinding_SaveReportAsDiagnosisCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_SaveAsVariantCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_SaveAsVariant_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!(CollectionViewSource.GetDefaultView(DiagnosisCollection).CurrentItem is Diagnosis diagnosis)) return;
            var diagnosisGroup = diagnosis.DiagnosisGroup;
            var name = TextBoxController.TextGet(NameTextBox);
            if (string.IsNullOrEmpty(name) ||
                DiagnosisCollection.Any(d => d.Name == TextBoxController.TextGet(NameTextBox)))
            {
                TextBoxController.AppendText("Please enter variant name", NameTextBox);
                return;
            }

            var text = PreviewBox.Text;
            if (string.IsNullOrEmpty(text))
            {
                PreviewBox.Text = "Please enter report text";
                return;
            }

            NameTextBox.Focus();
            NameTextBox.SelectAll();
            DiagnosisCollection.Add(new Diagnosis(name, text, diagnosisGroup));
            Repository.SaveCollection(DiagnosisCollection);
        }

        private void CommandBinding_SaveOverExistingCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_SaveOverExisting_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (DiagnosisViewSource.View.CurrentItem is Diagnosis diagnosis)
            {
                diagnosis.Text = PreviewBox.Text;
                NameTextBox.Text = "Saved: " + diagnosis.Name;
            }

            NameTextBox.Focus();
            NameTextBox.SelectAll();
            Repository.SaveCollection(DiagnosisCollection);
        }

        private void CommandBinding_DeleteDiagnosisCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_DeleteDiagnosis_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!(CollectionViewSource.GetDefaultView(DiagnosisCollection).CurrentItem is Diagnosis diagnosis)) return;
            DiagnosisCollection.Remove(diagnosis);
            NameTextBox.Text = "Deleted: " + diagnosis.Name;
        }
    }
}