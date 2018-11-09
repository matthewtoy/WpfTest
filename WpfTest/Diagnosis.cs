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


        //  Need a paramaterless constructor in order to serialize with XMLSerialiser:
        public Diagnosis()
        {
        }

        public Diagnosis(string name, string text)
        {
            this.Name = name;
            this.Text = text;
        }

        public override string ToString() => "Diagnosis." + Name;

 
        public void IncrementUseCount()
        {
            UseCount += 1;
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


