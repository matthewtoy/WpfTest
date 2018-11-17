using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mindscape.WpfElements;

namespace WpfTest
{
    public class MsAutoCompleteTextBoxProvider : IAutoCompleteSuggestionProvider
    {

        public IEnumerable<string> GetSuggestions(string input, int maxCount)
        {
            var coll = new ObservableCollection<string>();
            foreach (var d in MainWindow.DiagnosisCollection)
            {
                if (d.Name.ToLower().Contains(input.ToLower()))
                {
                    coll.Add(d.Name);
                }

                if (coll.Count == 0)
                {
                    if (d.Text.ToLower().Contains(input.ToLower()))
                    {
                        coll.Add(d.Name);
                    }
                }
            }

            var stringlist = new List<string>();
                                  
                stringlist.Add("Adema");
                stringlist.Add("Adele");
                stringlist.Add("Adam");
                stringlist.Add("Apple");
                stringlist.Add("Banana");
                stringlist.Add("Orange");






            return stringlist;
        }

    }
}

