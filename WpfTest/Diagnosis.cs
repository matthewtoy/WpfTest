using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfTest.Annotations;

namespace WpfTest
{
    public class Diagnosis : INotifyPropertyChanged, IComparable
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public int UseCount { get; set; }
        public string DiagnosisGroup { get; set; }


        //  Need a paramaterless constructor in order to serialize with XMLSerialiser:
        public Diagnosis()
        {
        }

        public Diagnosis(string name, string text)
        {
            //DiagnosisGroup initialized to name.  If created as variant then this will be overwritten with the pre-existing DiagnosisGroup.
            this.Name = this.DiagnosisGroup = name;
            this.Text = text;
        }

        public Diagnosis(string name, string text, string diagnosisGroup)
            :this(name, text)
        {
            this.DiagnosisGroup = diagnosisGroup;
        }

        public override string ToString() => "Diagnosis." + Name;

 
        public void IncrementUseCount()
        {
            ++UseCount;
            OnPropertyChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int CompareTo(object obj)
        {
            return this.ToString().CompareTo(obj.ToString());
        }
    }
}


