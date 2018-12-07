using System.Collections;

namespace WpfTest
{
    public class AlphabeticSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            var diagX = x as Diagnosis;
            var diagY = y as Diagnosis;
            return diagX.Name.CompareTo(diagY.Name);
        }
    }
}