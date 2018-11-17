using System.Collections;
using System.Collections.ObjectModel;
using FeserWard.Controls;

namespace WpfTest
{
    public class SearchAutoCompleteProvider : IIntelliboxResultsProvider
    {

        IEnumerable IIntelliboxResultsProvider.DoSearch(string searchTerm, int maxResults, object extraInfo)
        {
            var coll = new ObservableCollection<Diagnosis>();
            foreach (var d in MainWindow.DiagnosisCollection)
            {
                if (d.Name.ToLower().Contains(searchTerm.ToLower()))
                {
                    coll.Add(d);
                }

                if (coll.Count == 0)
                {
                    if (d.Text.ToLower().Contains(searchTerm.ToLower()))
                    {
                        coll.Add(d);
                    }
                }
            }

            return coll;
        }
    }
}