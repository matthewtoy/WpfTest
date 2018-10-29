namespace WpfTest
{
    class Diagnosis
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public Diagnosis(string name, string text)
        {
            this.Name = name;
            this.Text = text;
        }

        public override string ToString()
        {
            return "Diagnosis." + Name;
        }
    }

}


