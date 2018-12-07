using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfTest.Annotations;

namespace WpfTest
{
    public class Diagnosis : INotifyPropertyChanged, IComparable
    {
        //  Need a paramaterless constructor in order to serialize with XMLSerialiser:
        public Diagnosis()
        {
        }

        public Diagnosis(string name, string text)
        {
            //DiagnosisGroup initialized to name.  If created as variant then this will be overwritten with the pre-existing DiagnosisGroup.
            Name = DiagnosisGroup = name;
            Text = text;
        }

        public Diagnosis(string name, string text, string diagnosisGroup)
            : this(name, text)
        {
            DiagnosisGroup = diagnosisGroup;
        }

        public string Name { get; set; }
        public string Text { get; set; }
        public int UseCount { get; set; }
        public string DiagnosisGroup { get; set; }

        public int CompareTo(object obj)
        {
            return ToString().CompareTo(obj.ToString());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return "Diagnosis." + Name;
        }


        public void IncrementUseCount()
        {
            ++UseCount;
            OnPropertyChanged();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}