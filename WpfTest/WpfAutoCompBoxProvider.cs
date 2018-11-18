using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFAutoCompleteBox.Provider;

namespace WpfTest
{
    public class WpfAutoCompBoxProvider : IAutoCompleteDataProvider
    {

        public ObservableCollection<Diagnosis> Diagnoses { get; private set; }

        public WpfAutoCompBoxProvider(ObservableCollection<Diagnosis> diagnoses)
        {
            Diagnoses = diagnoses;
        }

  

        public IEnumerable<object> GetItems(string textPattern)
        {
            if (textPattern.Length < 1) return null; // Don't bother returning results until more than n character is entered.

            // This is an example of SQL-style LINQ:
            var diagnoses = from c in Diagnoses where c.Name != null && c.Name.ToUpper().Contains(textPattern.ToUpper()) orderby c.Name select c;

            return diagnoses;
        }
    }
}
