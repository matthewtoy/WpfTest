using System.ComponentModel;
using System.Windows.Data;

namespace WpfTest
{
    public static class FilterController
    {
        public static string FilterString = "";

        public static ICollectionView diagnosisView =
            CollectionViewSource.GetDefaultView(MainWindow.DiagnosisCollection);

        public static void SortByUse()
        {
            var DiagnosisView = CollectionViewSource.GetDefaultView(MainWindow.DiagnosisCollection);
            DiagnosisView.SortDescriptions.Clear();
            DiagnosisView.SortDescriptions.Add(new SortDescription("UseCount", ListSortDirection.Descending));
        }

        public static void SortByAlphabet()
        {
            // Custom sorter is faster than reflection-based inbuilt sort.  Need to cast to ListCollectionView as implements customsort and indexing. Probably best for most operations here.
            var diagnosisView =
                (ListCollectionView) CollectionViewSource.GetDefaultView(MainWindow.DiagnosisCollection);
            diagnosisView.SortDescriptions.Clear();
            diagnosisView.CustomSort = new AlphabeticSorter();
            // diagnosisView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }

        public static void SetDiagnosisGroupFilter(string filterString)
        {
            FilterString = filterString;
            diagnosisView.Filter = DiagnosisGroupFilter;
        }

        private static bool DiagnosisGroupFilter(object diagnosis)
        {
            if (!(diagnosis is Diagnosis d)) return false;
            return d.DiagnosisGroup.Contains(FilterString);
        }

        public static void SetSearchAsTypeFilter(string filterString)
        {
            FilterString = filterString;
            diagnosisView.Filter = SearchAsTypeFilter;
        }

        private static bool SearchAsTypeFilter(object diagnosis)
        {
            if (!(diagnosis is Diagnosis d)) return false;
            return d.Name.Contains(FilterString);
        }

        public static void ClearFilter()
        {
            diagnosisView.Filter = null;
        }
    }
}