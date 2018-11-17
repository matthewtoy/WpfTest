using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeserWard.Controls;

namespace WpfTest
{
    public class SearchAutoCompleteProviderProperty : IIntelliboxResultsProvider

    {
        public IEnumerable DoSearch(string searchTerm, int maxResults, object extraInfo)
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
